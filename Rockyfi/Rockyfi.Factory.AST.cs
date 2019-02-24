using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    // simple lex
    // copy and adapt from lua 5.3 :
    // Numeral ::= [0-9]+ | [0-9]*.[0-9]+
    // LiteralString ::= '[^']*'
    // binop ::=  ‘+’ | ‘-’ | ‘*’ | ‘/’ | '==' | '!=' | '||' | '&&'
    // unop ::=  '!'
    // exp ::=  null | false | true | Numeral | '(’ exp ‘)’ | LiteralString | exp binop exp | unop exp 
    //

    class Lex
    {
        enum TokenType
        {
            Unknow, //
            Add,    // +
            Sub,    // -
            Mut,    // *
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
        object currentValue;
        TokenType currentType;
        StreamReader reader;

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
                if(c == -1)
                    throw new Exception("parse error, read string but EOF!");

                if (reader.Peek() != '\'')
                    sb.Append((char)c);
                else
                {
                    return sb.ToString();
                }
            }
        }

        string ReadId(char firstChar)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(firstChar);
            bool lastIsDot = firstChar == '.';
            while (true)
            {
                int peek = reader.Peek();
                if (char.IsLetterOrDigit((char)peek))
                {
                    lastIsDot = false;
                    sb.Append((char)reader.Read());
                }
                else if (peek == '.')
                {
                    if (lastIsDot)
                        throw new Exception("parse error, read mutiply dot xx..yy!");

                    lastIsDot = true;
                    reader.Read();
                }
                else
                {
                    return sb.ToString();
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
                        throw new Exception("parse error, read number but more then one '.'!");

                    sb.Append((char)reader.Read());
                    alreadHasDot = true;
                }

                if (char.IsNumber((char)peek))
                    sb.Append((char)reader.Read());
                else
                    break;
            }


            isInteger = !alreadHasDot;
            if (isInteger) return int.Parse(sb.ToString());
            return float.Parse(sb.ToString());
        }

        void NextToken()
        {
            // skip whitespace
            while (true)
            {
                int c = reader.Peek();
                if (c != ' ')
                    break;
                reader.Read();
            }

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
                    case '*': currentType = TokenType.Mut; return;
                    case '/': currentType = TokenType.Div; return;
                    case '=': LookHead('='); currentType = TokenType.Eq; return;
                    case '!':
                        if (reader.Peek() != '=')
                        {
                            reader.Read();
                            currentType = TokenType.Inverse;
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
                    switch (str)
                    {
                        case "null": currentType = TokenType.Null; return;
                        case "false": currentType = TokenType.False; return;
                        case "true": currentType = TokenType.True; return;
                        default: currentType = TokenType.ObjectId; return;
                    }
                }

                throw new Exception("parse error unknow : " + c);
            }
        }
    }


    public partial class Factory
    {
        #region Abstract syntax tree
        // The Regex class itself is thread safe and immutable(read-only). That is, 
        // Regex objects can be created on any thread and shared between threads; 
        // matching methods can be called from any thread and never alter any global state.
        // However, result objects (Match and MatchCollection) returned by Regex should be used on a single thread..
        internal static Regex strRegex = new Regex(@"^'([^']*)'$");
        internal static Regex objRegex = new Regex(@"^(([a-zA-Z]\w*)(\.([a-zA-Z]\w*))*)$");


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

        enum DataBindObjectType
        {
            Unknow,
            Integer,
            Float,
            String,
            ObjectSymbol,
        }

        delegate object ABSEvaluateContext(string[] strPath);

        public static bool LossyBoolJudge(object obj)
        {
            if (obj == null)
            {
                return false;
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
        class DataBindObjectExpress
        {
            public DataBindObjectType Type
            {
                get
                {
                    return this.type;
                }
            }

            string express;
            DataBindObjectType type = DataBindObjectType.Unknow;
            public object value
            {
                get; private set;
            }

            bool ParseExpress(string exp)
            {
                type = DataBindObjectType.Unknow;
                if (int.TryParse(express, out int intResult))
                {
                    value = intResult;
                    type = DataBindObjectType.Integer;
                }
                else if (float.TryParse(express, out float floatResult))
                {
                    value = intResult;
                    type = DataBindObjectType.Float;
                }
                else if (TryParseDotValue(express, out string[] objResult))
                {
                    value = intResult;
                    type = DataBindObjectType.ObjectSymbol;
                }
                else if (TryParseStringValue(express, out string strResult))
                {
                    value = intResult;
                    type = DataBindObjectType.String;
                }

                return type != DataBindObjectType.Unknow;
            }

            /// <summary>
            /// faild parse return null, otherwise return DataBindObjectExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static DataBindObjectExpress Parse(string express)
            {
                var dboe = new DataBindObjectExpress();
                dboe.express = express;
                return dboe.ParseExpress(express) ? dboe : null;
            }

            public bool UpdateExpress(string exp, Factory factory)
            {
                if (express == exp)
                    return true;
                express = exp;
                return ParseExpress(express);
            }


            public object Evaluate(ABSEvaluateContext context)
            {
                if (type == DataBindObjectType.Unknow)
                    return null;

                if (type == DataBindObjectType.ObjectSymbol)
                    return context((string[])value);

                return value;
            }
        }

        internal static Regex ifRegex = new Regex(@"^([^=]+)\s*(==\s*([^=]+))?$");
        /// <summary>
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// case 1: 
        ///    bind:if="item"
        ///    string is null or empty ?
        ///    number is 0 ?
        /// case 2:
        /// </summary>
        class DataBindIfExpress
        {
            string express;
            bool isMono; // a == b ? or a ?
            DataBindObjectExpress leftExpress;
            DataBindObjectExpress RightExpress;

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
                        isMono = true;
                        leftExpress = DataBindObjectExpress.Parse(match.Groups[1].Value.Trim());
                        if (leftExpress == null)
                            return false;

                        return true;
                    }
                    else if (match.Groups.Count == 4)
                    {
                        isMono = false;
                        leftExpress = DataBindObjectExpress.Parse(match.Groups[1].Value.Trim());
                        RightExpress = DataBindObjectExpress.Parse(match.Groups[3].Value.Trim());
                        if (leftExpress == null || RightExpress == null)
                            return false;

                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// faild parse return null, otherwise return DataBindIfExpress
            /// </summary>
            /// <param name="express"></param>
            /// <returns></returns>
            public static DataBindIfExpress Parse(string exp)
            {
                var ifExp = new DataBindIfExpress();
                ifExp.express = exp;
                return ifExp.ParseExpress(ifExp.express) ? ifExp : null;
            }

            bool Evaluate(ABSEvaluateContext context)
            {

            }
        }
        #endregion
    }
}
