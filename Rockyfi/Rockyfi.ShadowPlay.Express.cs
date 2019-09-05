using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    #region advance lex parser
    // simple lex
    // copy from lua 5.3 :
    // Numeral ::= [0-9]+ | [0-9]*.[0-9]+
    // LiteralString ::= '[^']*'
    // binop ::=  ‘+’ | ‘-’ | ‘*’ | ‘/’ | '==' | '!=' | '||' | '&&'
    // unop ::=  '!'
    // exp ::=  null | false | true | Numeral | '(’ exp ‘)’ | LiteralString | exp binop exp | unop exp
    //
    // advance lex parser
    public class Lex
    {
        public enum TokenType
        {
            Unknow, //
            Add,    // +
            Sub,    // -
            Mul,    // *
            Div,    // /
            Eq,     // ==
            Neq,    // !=
            Or,     // ||
            And,    // &&
            Inverse, // !
            Null,  // null
            False, // false
            True, // true
            NumberInteger,  // 2
            NumberFloat,   // .2
            LeftBrackets,  // )
            RightBrackets, // (
            String,   // 'hi ?'
            ObjectId, // item | time.hour
            EOF,
        }
        public object currentValue;
        public TokenType currentType;
        public StreamReader reader;

        void LookHead(char c)
        {
            if(reader.Peek() != c)
            {
                throw new Exception("parse error");
            }

            reader.Read();
        }

        string ReadStringLiteral()
        {
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                int c = reader.Read();
                if(c == '\'')
                    return sb.ToString();

                if (c == -1)
                    throw new Exception("parse error, read string but EOF!");

                sb.Append((char)c);
            }
        }

        string[] ReadId(char firstChar)
        {
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.Append(firstChar);
            bool lastIsDot = firstChar == '.';
            while (true)
            {
                int peek = reader.Peek();

                if (lastIsDot && ( peek == '.' || char.IsNumber((char)peek)))
                {
                    result.Add(sb.ToString() + (char)peek);
                    throw new Exception("parse id error, unknow " + string.Join(".", result.ToArray()));
                }

                if (char.IsLetterOrDigit((char)peek)) // current
                {
                    lastIsDot = false;
                    sb.Append((char)reader.Read());
                }
                else if (peek == '.') // next split
                {
                    lastIsDot = true;
                    reader.Read();
                    result.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                else
                {
                    result.Add(sb.ToString());
                    return result.ToArray();
                }
            }
        }

        object ReadNumber(char firstChar, out bool isInteger)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(firstChar);
            bool alreadHasDot = firstChar == '.';
            while (true)
            {
                int peek = reader.Peek();
                if (peek == -1)
                    break;

                if (peek == '.')
                {
                    if (alreadHasDot)
                        throw new Exception("parse error, unknow :" + sb.ToString() + (char)peek );

                    sb.Append((char)reader.Read());
                    alreadHasDot = true;
                } else if (char.IsLetterOrDigit((char)peek))
                    sb.Append((char)reader.Read());
                else
                    break;
            }


            isInteger = !alreadHasDot;
            if (isInteger) return int.Parse(sb.ToString());
            return float.Parse(sb.ToString());
        }

        public string GetValueString()
        {
            var s = currentValue as string[];
            if (s != null)
            {
                return "[object symbol]" + string.Join(".", s);
            }
            return currentValue != null ? currentValue.ToString() : null;
        }

        public void NextToken()
        {
            // skip whitespace
            while (true)
            {
                int c = reader.Peek();
                if (c != ' ')
                    break;
                reader.Read();
            }

            currentValue = null;
            currentType = TokenType.Unknow;
            while (true)
            {
                int c = reader.Read();
                if (c == -1)
                {
                    currentType = TokenType.EOF;
                    return;
                }

                switch (c)
                {
                    case '+': currentType = TokenType.Add; return;
                    case '-': currentType = TokenType.Sub; return;
                    case '*': currentType = TokenType.Mul; return;
                    case '/': currentType = TokenType.Div; return;
                    case '=': LookHead('='); currentType = TokenType.Eq; return;
                    case '!':
                        if (reader.Peek() != '=')
                        {
                            currentType = TokenType.Inverse;
                            return;
                        }
                        LookHead('='); currentType = TokenType.Neq; return;
                    case '|': LookHead('|'); currentType = TokenType.Or; return;
                    case '&': LookHead('&'); currentType = TokenType.And; return;
                    case '\'': // string
                        currentValue = ReadStringLiteral();
                        currentType = TokenType.String;
                        return;
                }

                if (char.IsNumber((char)c) || c == '.')
                {
                    currentValue = ReadNumber((char)c, out bool isInteger);
                    currentType = isInteger ? TokenType.NumberInteger : TokenType.NumberFloat;
                    return;
                }

                if (char.IsLetter((char)c))
                {
                    var str = ReadId((char)c);
                    if (str.Length == 1)
                    {
                        switch (str[0])
                        {
                            case "null": currentType = TokenType.Null; return;
                            case "false": currentType = TokenType.False; return;
                            case "true": currentType = TokenType.True; return;
                            default: break;
                        }
                    }
                    currentType = TokenType.ObjectId; currentValue = str; return;
                }
                throw new Exception("parse error unknow : " + c);
            }
        }
    }
    // TODO : write test ......
    #endregion

    public partial class ShadowPlay<T> where T: BridgeElement<T>
    {
        #region Abstract syntax tree
        // The Regex class itself is thread safe and immutable(read-only). That is,
        // Regex objects can be created on any thread and shared between threads;
        // matching methods can be called from any thread and never alter any global state.
        // However, result objects (Match and MatchCollection) returned by Regex should be used on a single thread..
        internal static Regex ifRegex = new Regex(@"^([^=]+)\s*((==|!=)\s*([^=]+))?$");
        //internal static Regex ifRegex = new Regex(@"^(((-?\d+)(\.\d+)?)|(([_a-zA-Z]\w*)(\.([_a-zA-Z]\w*))*))\s+(==|!=)\s+(((-?\d+)(\.\d+)?)|(([_a-zA-Z]\w*)(\.([_a-zA-Z]\w*))*))$");
        internal static Regex forRegex = new Regex(@"^([_a-zA-Z][_\w]*)\s+in\s+(([_a-zA-Z][_\w]*)(\.([_a-zA-Z][_\w]*))*)$");
        internal static Regex strRegex = new Regex(@"^'([^']*)'$");
        internal static Regex objRegex = new Regex(@"^(([_a-zA-Z][_\w]*)(\.([_a-zA-Z][_\w]*))*)$");

        public const string forInToken = "in";


        // xxx.yy.zz -> [xxx, yy, zz]
        static bool TryParseDotValue(string input, out string[] result)
        {
            if (objRegex.IsMatch(input))
            {
                result = input.Split('.');
                return true;
            }
            result = null;
            return false;
        }
        static bool TryParseStringValue(string input, out string result)
        {
            if (strRegex.IsMatch(input))
            {
                result = input.Substring(1, input.Length - 2);
                return true;
            }
            result = null;
            return false;
        }

        public static bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }




        /// <summary>
        /// 11  : int
        /// 11.11  : float
        /// 'hello ?': string
        /// true: boolean
        /// false: boolean
        /// xxx.bc.fd: object value
        /// </summary>
        internal class ObjectDataBindExpress
        {
            Expr.Tree ast;
            string express;
            public object value
            {
                get; private set;
            }


            /// <summary>
            /// faild parse return null, otherwise return DataBindObjectExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static ObjectDataBindExpress Parse(string express)
            {
                try
                {
                    var dboe = new ObjectDataBindExpress();
                    dboe.ast = new Expr.Parser().Parse(express);
                    dboe.express = express;
                    return dboe;
                }
                catch
                {
                }
                return null;
            }

            public bool TryEvaluate(ContextStack contextStack, out object result)
            {
                try
                {
                    var evaluater = new Expr.Evaluator();
                    evaluater.VariableHolder = contextStack;
                    result = evaluater.Eval(ast);
                }
                catch (Exception e)
                {
                    throw new Exception($"eval express `{express}` error: {e.Message}", e);
                }
                return true;
            }
        }


        ///// <summary>
        ///// el-once="xxxxxx"
        ///// </summary>
        //internal class OnceDataBindExpress
        //{
        //    string express;
        //    ObjectDataBindExpress objectExpress = null;

        //    /// <summary>
        //    /// fail return null
        //    /// </summary>
        //    /// <returns></returns>
        //    public static OnceDataBindExpress Parse(string express)
        //    {
        //        var onceExpress = new OnceDataBindExpress();
        //        onceExpress.express = express;
        //        onceExpress.objectExpress = ObjectDataBindExpress.Parse(express);
        //        return onceExpress.objectExpress != null ? onceExpress : null;
        //    }

        //    public void Evaluate(ContextStack contextStack)
        //    {
        //        objectExpress.TryEvaluate(contextStack, out var _);
        //    }
        //}

        /// <summary>
        /// xxxxx {{ item.desc }} xxxxxx yyyyyy
        /// </summary>
        internal class TextDataBindExpress
        {
            private TextDataBindExpress(List<TextToken> list)
            {
                this.tokensList = list;
            }
            readonly List<TextToken> tokensList;
            struct TextToken
            {
                internal readonly bool IsText;
                internal readonly ObjectDataBindExpress Express;
                internal readonly string Text;
                public TextToken(string text)
                {
                    IsText = true;
                    this.Express = null;
                    this.Text = text;
                }
                public TextToken(ObjectDataBindExpress express)
                {
                    IsText = false;
                    this.Express = express;
                    this.Text = null;
                }

            }

            struct RawTextToken
            {
                public readonly char value;
                public readonly int type; // 0 == char, -1 => {{,  1 => }}
                public RawTextToken(int t, char c) { value = c; type = t;  }

                public override string ToString()
                {
                    if (type == 0)
                        return value.ToString();
                    if (type == -1)
                        return "{{";
                    return "}}";
                }


                public static string ToString(List<RawTextToken> list, int start, int length)
                {
                    StringBuilder sb = new StringBuilder();
                    int endIndex = start + length;
                    for (int i = start; i < list.Count && i < endIndex; i++)
                        sb.Append(list[i]);
                    return sb.ToString();
                }
            }

            /// <summary>
            /// fail return null
            /// </summary>
            /// <returns></returns>
            public static TextDataBindExpress Parse(string input)
            {
                if (input == null || "".Equals(input))
                    return null;

                List<RawTextToken> rawList = new List<RawTextToken>();
                for (int i = 0; i < input.Length; i++)
                {
                    var curr = input[i];
                    if (curr == '{' && i + 1 < input.Length && input[i + 1] == '{')
                    {
                        rawList.Add(new RawTextToken(-1, ' '));
                        i++;
                    }
                    else if (curr == '}' && i + 1 < input.Length && input[i + 1] == '}')
                    {
                        rawList.Add(new RawTextToken(1, ' '));
                        i++;
                    }
                    else
                    {
                        rawList.Add(new RawTextToken(0, curr));
                    }
                }

                // RawTextToken to TextToken
                List<TextToken> tokenList = new List<TextToken>();
                for (int i = 0; i < rawList.Count;)
                {
                    if (rawList[i].type == -1)
                    {
                        int startIndex = i + 1;
                        i++;
                        while (i < rawList.Count && rawList[i].type != 1)
                        {
                            i++;
                        }

                        var str = RawTextToken.ToString(rawList, startIndex, i - startIndex);
                        if (i == rawList.Count) // EOF
                        {
                            // normal text
                            tokenList.Add(new TextToken(str));
                            break;
                        }

                        var express = ObjectDataBindExpress.Parse(str);
                        if (express != null)
                        {
                            tokenList.Add(new TextToken(express));
                        }

                        i++;
                    }
                    else
                    {
                        int startIndex = i;
                        while (i < rawList.Count && rawList[i].type != -1)
                        {
                            i++;
                        }
                        int length = i - startIndex;
                        if (length > 0)
                        {
                            var str = RawTextToken.ToString(rawList, startIndex, length);
                            tokenList.Add(new TextToken(str));
                        }
                    }
                }

                return tokenList.Count > 0 ? new TextDataBindExpress(tokenList) : null;
            }

            public string Evaluate(ContextStack contextStack)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var token in tokensList)
                {
                    if (token.IsText)
                        sb.Append(token.Text);
                    else if (token.Express.TryEvaluate(contextStack, out var result) && result != null)
                        sb.Append(result.ToString());
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// el-bind:width="item.width"
        /// </summary>
        internal class AttributeDataBindExpress
        {
            protected string express;

            /// <summary>
            /// el-bind:xxxx="yyy.width" ---> xxxx
            /// </summary>
            public string TargetName { get; protected set; }

            ObjectDataBindExpress objectExpress = null;

            /// <summary>
            /// el-bind:xxxx="yyy.width" ---> yyy.width
            /// </summary>
            public string Express => express;

            /// <summary>
            /// fail return null
            /// </summary>
            /// <returns></returns>
            public static AttributeDataBindExpress Parse(string express, string targetName)
            {
                if ("".Equals(targetName))
                    return null;

                var attributeExpress = new AttributeDataBindExpress();
                attributeExpress.TargetName = targetName;
                attributeExpress.express = express;
                attributeExpress.objectExpress = ObjectDataBindExpress.Parse(express);
                return attributeExpress.objectExpress != null ? attributeExpress : null;
            }

            public virtual bool TryEvaluate(ContextStack contextStack, out object result)
            {
                return objectExpress.TryEvaluate(contextStack, out result);
            }

            public override string ToString()
            {
                return base.ToString() + " tagName " + TargetName + "   expres : " + express;
            }
        }


        /// <summary>
        /// color="#ffff"
        /// </summary>
        internal class ConstStringAttributeDataBindExpress : AttributeDataBindExpress
        {
            /// <summary>
            /// fail return null
            /// </summary>
            /// <returns></returns>
            public static ConstStringAttributeDataBindExpress ParseConst(string express, string targetName)
            {
                if ("".Equals(targetName))
                    return null;

                var attributeExpress = new ConstStringAttributeDataBindExpress();
                attributeExpress.TargetName = targetName;
                attributeExpress.express = express;
                return attributeExpress;
            }

            public override bool TryEvaluate(ContextStack contextStack, out object result)
            {
                result = Express;
                return true;
            }

            public override string ToString()
            {
                return base.ToString() + " tagName " + TargetName + "   expres : " + express;
            }
        }

        /// <summary>
        /// item in aa.bb.c
        /// item in list
        /// </summary>
        internal class ForDataBindExpress
        {
            string express;

            /// <summary>
            /// el-for="item in list.box" ---> item
            /// </summary>
            public string IteratorName { get; private set; }
            ObjectDataBindExpress objectExpress = null;

            /// <summary>
            /// faild parse return null, otherwise return DataBindObjectExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static ForDataBindExpress Parse(string express)
            {
                if (express == null)
                    return null;

                express = express.Trim();
                int index = express.IndexOf(forInToken);
                if (index < 0)
                    return null;
                var forExp = new ForDataBindExpress();
                forExp.express = express;
                forExp.IteratorName = express.Substring(0, index).Trim();
                forExp.objectExpress = ObjectDataBindExpress.Parse(express.Substring(index + 2).Trim());
                return forExp.objectExpress != null ? forExp : null;
            }

            /// <summary>
            /// faild parse return null, otherwise return IEnumerable<object>
            /// </summary>
            /// <returns></returns>
            public System.Collections.IEnumerable Evaluate(ContextStack contextStack)
            {
                return objectExpress.TryEvaluate(contextStack, out var result) ? (result as System.Collections.IEnumerable) : null;
            }
        }

        /// <summary>
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// case 1:
        ///    bind:if="item"
        ///    string is null or empty ?
        ///    number is 0 ?
        /// case 2:
        /// </summary>
        internal class IfDataBindExpress
        {
            string express;
            ObjectDataBindExpress objectExpress = null;

            /// <summary>
            /// faild parse return null, otherwise return DataBindIfExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static IfDataBindExpress Parse(string express)
            {
                var ifExp = new IfDataBindExpress();
                ifExp.express = express;
                ifExp.objectExpress = ObjectDataBindExpress.Parse(express);
                return ifExp.objectExpress != null ? ifExp : null;
            }

            public bool TryEvaluate(ContextStack contextStack, out bool boolResult)
            {
                if(objectExpress.TryEvaluate(contextStack, out var result) && TryLossyBoolJudge(result, out boolResult))
                {
                    return true;
                }
                boolResult = false;
                return false;
            }

            public static bool TryLossyBoolJudge(object obj, out bool result)
            {
                if (obj == null)
                {
                    result = false;
                    return true;
                }

                if (obj is bool) // if bool
                {
                    result = (bool)obj;
                    return true;
                }

                // https://stackoverflow.com/questions/745172/better-way-to-cast-object-to-int
                if (IsNumber(obj))
                {
                    result = (0.0).Equals(obj);
                    return true;
                }

                if (obj is string)
                {
                    result = !"".Equals(obj);
                    return true;
                }

                result = false;
                return false;
            }

        }
        #endregion
    }
}
