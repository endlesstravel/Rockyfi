using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockyfi
{
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

    public partial class ShadowPlay
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
        /// xxxxx {{ item.desc }} xxxxxx yyyyyy
        /// </summary>
        internal class TextDataBindExpress
        {
            private TextDataBindExpress(LinkedList<TextToken> list)
            {
                this.tokensList = list;
                TargetKeys = new List<string>();

                foreach (var tk in list)
                {
                    if (!tk.IsText && tk.Express.TargetKey != null)
                        TargetKeys.Add(tk.Express.TargetKey);
                }
            }
            readonly LinkedList<TextToken> tokensList;
            internal readonly List<string> TargetKeys;
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

            struct BracesPos
            {
                public readonly int index;
                public readonly int lenght;
                public readonly string value;
                public BracesPos(int p, int l, string v) { this.index = p; this.lenght = l;  value = v; }
            }

            // TODO: improve performance without regex
            static Regex bindTextRegex = new Regex(@"(?<!\{)\{\{\s*(([_a-zA-Z][_\w]*)(\.([_a-zA-Z][_\w]*))*)\s*\}\}(?!\})");
            static LinkedList<BracesPos> MatchIndex(string input)
            {
                var list = new LinkedList<BracesPos>();
                var m = bindTextRegex.Match(input);
                while (m.Success)
                {
                    list.AddLast(new BracesPos(m.Index, m.Length, m.Groups[2].Value));
                    m = m.NextMatch();
                }
                return list;
            }

            /// <summary>
            /// fail return null
            /// </summary>
            /// <returns></returns>
            public static TextDataBindExpress Parse(string text)
            {
                LinkedList<TextToken> tokensList = new LinkedList<TextToken>();
                var splitList = MatchIndex(text);
                if (splitList.Count == 0)
                {
                    return null;
                }
                int index = 0;
                foreach (var pos in splitList)
                {
                    tokensList.AddLast(new TextToken(text.Substring(index, pos.index - index)));
                    var objExpress = ObjectDataBindExpress.Parse(pos.value);
                    if (objExpress != null)
                    {
                        tokensList.AddLast(new TextToken(objExpress));
                    }
                    else
                    {
                        tokensList.AddLast(new TextToken($"{{{pos.value}}}"));
                    }
                    index = pos.index + pos.lenght;
                }
                if (index < text.Length)
                    tokensList.AddLast(new TextToken(text.Substring(index, text.Length - index)));

                return new TextDataBindExpress(tokensList);
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
            string express;

            /// <summary>
            /// el-bind:xxxx="yyy.width" ---> xxxx
            /// </summary>
            public string TargetName { get; private set; }

            /// <summary>
            /// el-bind:xxxx="yyy.width" ---> yyyy
            /// </summary>
            public string TargetKey => objectExpress.TargetKey;
            ObjectDataBindExpress objectExpress = null;


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

            public bool TryEvaluate(ContextStack contextStack, out object result)
            {
                return objectExpress.TryEvaluate(contextStack, out result);
            }
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
            enum ObjectDataBindType
            {
                Unknow,
                Integer,
                Float,
                String,
                ObjectSymbol,
            }

            string express;
            ObjectDataBindType type = ObjectDataBindType.Unknow;
            public object value
            {
                get; private set;
            }

            public string TargetKey => IsEffectedByContext && value != null ? (value as string[])[0] : null;
            public bool IsEffectedByContext => type == ObjectDataBindType.ObjectSymbol;

            /// <summary>
            /// faild parse return null, otherwise return DataBindObjectExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static ObjectDataBindExpress Parse(string express)
            {
                express = express.Trim();

                var dboe = new ObjectDataBindExpress();
                dboe.express = express;
                dboe.type = ObjectDataBindType.Unknow;
                if (int.TryParse(express, out int intResult))
                {
                    dboe.value = intResult;
                    dboe.type = ObjectDataBindType.Integer;
                }
                else if (float.TryParse(express, out float floatResult))
                {
                    dboe.value = floatResult;
                    dboe.type = ObjectDataBindType.Float;
                }
                else if (TryParseDotValue(express, out string[] objResult))
                {
                    dboe.value = objResult;
                    dboe.type = ObjectDataBindType.ObjectSymbol;
                }
                else if (TryParseStringValue(express, out string strResult))
                {
                    dboe.value = strResult;
                    dboe.type = ObjectDataBindType.String;
                }

                return dboe.type != ObjectDataBindType.Unknow ? dboe : null;
            }


            public bool TryEvaluate(ContextStack contextStack, out object result)
            {
                result = null;
                if (type == ObjectDataBindType.Unknow)
                    return false;

                if (type == ObjectDataBindType.ObjectSymbol)
                {
                    return contextStack.TryGetFromPath((string[])value, out result);
                }

                result = value;
                return true;
            }
        }

        /// <summary>
        /// item in aa.bb.c
        /// item in list
        /// </summary>
        internal class ForDataBindExpress
        {
            string express;

            public string IteratorName { get; private set; }
            public string[] DataSourceName => dataSourceName;

            /// <summary>
            /// el-for="list.box" ---> list
            /// </summary>
            public string TargetKey => dataSourceName != null ? dataSourceName[0] : null;
            public bool IsEffectedByContext => true;

            string[] dataSourceName;

            /// <summary>
            /// faild parse return null, otherwise return DataBindObjectExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static ForDataBindExpress Parse(string express)
            {
                express = express.Trim();
                Match match = forRegex.Match(express);
                if (match.Success)
                {
                    var forExp = new ForDataBindExpress();
                    forExp.express = express;
                    forExp.IteratorName = match.Groups[1].Value.Trim();
                    TryParseDotValue(match.Groups[2].Value.Trim(), out forExp.dataSourceName);

                    if (TryParseDotValue(match.Groups[2].Value.Trim(), out forExp.dataSourceName))
                        return forExp;
                }
                return null;
            }

            /// <summary>
            /// faild parse return null, otherwise return IEnumerable<object>
            /// </summary>
            /// <returns></returns>
            public IEnumerable<object> Evaluate(ContextStack contextStack)
            {
                if (dataSourceName != null && contextStack.TryGetFromPath(dataSourceName, out var resultObject))
                {
                    return resultObject as IEnumerable<object>;
                }
                return null;
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
            bool isJustObjectExpress; // a == b ? or a ?
            bool isEqualOpt; //  a == b ? or a != b ?
            ObjectDataBindExpress leftExpress;
            ObjectDataBindExpress rightExpress;

            public string[] TargetKeys
            {
                get
                {
                    if (isJustObjectExpress)
                    {
                        return leftExpress.TargetKey != null ? new string[] { leftExpress.TargetKey } : null;
                    }
                    else if (leftExpress.TargetKey != null && rightExpress.TargetKey == null)
                    {
                        return leftExpress.TargetKey != null ? new string[] { leftExpress.TargetKey } : null;
                    }
                    else if (leftExpress.TargetKey == null && rightExpress.TargetKey != null)
                    {
                        return rightExpress.TargetKey != null ? new string[] { rightExpress.TargetKey } : null;
                    }
                    return new string[] { leftExpress.TargetKey, rightExpress.TargetKey };
                }
            }

            public bool IsEffectedByContext => isJustObjectExpress ? leftExpress.IsEffectedByContext
                : leftExpress.IsEffectedByContext || rightExpress.IsEffectedByContext;

            /// <summary>
            /// true: parse success, false parse failed
            /// </summary>
            /// <returns></returns>
            bool ParseExpress(string express)
            {
                Match match = ifRegex.Match(express);
                if (match.Success)
                {
                    if (match.Groups.Count == 2)
                    {
                        isJustObjectExpress = true;
                        leftExpress = ObjectDataBindExpress.Parse(match.Groups[1].Value);
                        return leftExpress != null;
                    }
                    else if (match.Groups.Count == 5)
                    {
                        isJustObjectExpress = false;
                        isEqualOpt = "==".Equals(match.Groups[3].Value.Trim());
                        leftExpress = ObjectDataBindExpress.Parse(match.Groups[1].Value);
                        rightExpress = ObjectDataBindExpress.Parse(match.Groups[4].Value);
                        return leftExpress != null && rightExpress != null;
                    }
                }

                return false;
            }

            /// <summary>
            /// faild parse return null, otherwise return DataBindIfExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static IfDataBindExpress Parse(string exp)
            {
                exp = exp.Trim();
                var ifExp = new IfDataBindExpress();
                ifExp.express = exp;
                return ifExp.ParseExpress(ifExp.express) ? ifExp : null;
            }

            public bool TryEvaluate(ContextStack contextStack, out bool result)
            {
                result = false;
                if (isJustObjectExpress)
                {
                    if (!leftExpress.TryEvaluate(contextStack, out var obj))
                        return false;

                    return TryLossyBoolJudge(obj, out result);
                }
                else if (leftExpress.TryEvaluate(contextStack, out object leftObj)
                    && rightExpress.TryEvaluate(contextStack, out object rightObj))
                {
                    if (IsNumber(leftObj) && IsNumber(rightObj)) // number equals .....
                    {
                        result = (double)leftObj == (double)rightObj;
                        result = isEqualOpt ? result : !result;
                        return true;
                    }
                    else if (leftObj is string && rightObj is string) // string equal .....
                    {
                        result = string.Equals(leftObj, rightObj);
                        result = isEqualOpt ? result : !result;
                        return true;
                    }

                    var leftTry = TryLossyBoolJudge(leftObj, out var leftResult);
                    var rightTry = TryLossyBoolJudge(rightObj, out var RightResult);
                    result = (leftTry ^ rightTry) ? false: leftResult == RightResult;
                    result = isEqualOpt ? result : !result;
                    return false;
                }
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
