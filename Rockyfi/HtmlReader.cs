using System.Collections.Generic;
using System.IO;

namespace Rockyfi
{
    public class HtmlAttribute
    {
        /// <summary>
        /// el-bind:width='100px' => el-bind:width
        /// </summary>
        public readonly string Name;


        /// <summary>
        /// el-bind:width='100px' => 100px
        /// </summary>
        public readonly string Value;


        /// <summary>
        /// el-bind:width='100px' => el-bind
        /// </summary>
        public readonly string Prefix;

        /// <summary>
        /// el-bind:width='100px' => width
        /// </summary>
        public readonly string LocalName; 

        public HtmlAttribute(string name, string value)
        {
            this.Name = (name ?? "").Trim();
            this.Value = value ?? "";

            var group = Name.Split(':');
            if (group.Length >= 2)
            {
                Prefix = group[0];
                LocalName = string.Join(":", group, 1, group.Length - 1);
            }
            else
            {
                Prefix = "";
                LocalName = "";
            }

        }
    }

    public class HtmlNode
    {
        readonly string Tag;
        readonly Dictionary<string, string> attr;
        readonly List<HtmlNode> children;
        readonly string contentText;
        public bool HasContentText => contentText != null && contentText != "";

        public string TagName => Tag ?? "";
        public string ContentText => contentText ?? "";

        public Dictionary<string, string> AttrDict => attr ?? new Dictionary<string, string>();
        public LinkedList<HtmlAttribute> Attr
        {
            get
            {
                var list = new LinkedList<HtmlAttribute>();
                foreach (var kv in AttrDict)
                {
                    list.AddLast(new HtmlAttribute(kv.Key, kv.Value));
                }
                return list;
            }
        }

        public List<HtmlNode> ChildNodes => new List<HtmlNode>(children);

        public HtmlNode(string tag, Dictionary<string, string> attr, List<HtmlNode> children, string contentText)
        {
            Tag = tag;
            this.attr = attr;
            this.children = children;
            this.contentText = contentText;
        }

        public string ToString(int tab)
        {
            char[] whiteSpace = new char[tab * 4];
            for (int i = 0; i < whiteSpace.Length; i++) whiteSpace[i] = ' ';
            string ws = new string(whiteSpace);

            var sb = new System.Text.StringBuilder();
            sb.Append(ws);
            sb.Append($"<{Tag}");
            foreach (var kv in attr)
            {
                sb.Append($" {kv.Key}={kv.Value}");
            }
            sb.Append($">");

            if (HasContentText || children.Count > 0)
            {
                sb.AppendLine();
            }

            foreach (var child in children)
            {
                sb.Append(child.ToString(tab + 1));
            }

            if (HasContentText)
            {
                sb.Append(ws);
                sb.Append(ws);
                sb.AppendLine(contentText.ToString());
            }

            sb.Append(ws);
            sb.AppendLine($"</{Tag}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(0);
        }
    }

    public class HtmlReader
    {
        StreamReader sr;

        void SkipWhiteSpace()
        {
            while (sr.Peek() == ' ' || sr.Peek() == '\n' || sr.Peek() == '\r') sr.Read();
        }

        void MatchToken(int c)
        {
            SkipWhiteSpace();
            if (Expect(c) == false)
                throw new System.Exception($"want get a '{(char)c}', but get '{(char)sr.Peek()}'   ");

            sr.Read();
        }

        bool ExpectToken(int c)
        {
            SkipWhiteSpace();
            if (Expect(c))
            {
                sr.Read();
                return true;
            }

            return false;
        }
        
        // ab3242c:_dfd3.422sf
        string MatchNextTagToken()
        {
            SkipWhiteSpace();

            var sb = new System.Text.StringBuilder();
            bool isFirstChar = true;

            while (true)
            {
                var cc = sr.Peek();
                bool isAlpha = ('A' <= cc && cc <= 'Z') || ('a' <= cc && cc <= 'z');
                bool isNum = ('0' <= cc && cc <= '9');
                bool isSpecial = ('_' == cc || '-' == cc || cc == ':' || cc == '.');

                if (isFirstChar)
                {
                    if (isAlpha || cc == '_')
                    {
                        sr.Read();
                        sb.Append((char)cc);
                    }
                    else
                        throw new System.Exception($" illegal id '{(char)cc}' !");

                    isFirstChar = false;
                }
                else if (isAlpha || isNum || isSpecial)
                {
                    sr.Read();
                    sb.Append((char)cc);
                }
                else
                {
                    return sb.ToString();
                }
            }
        }

        string MatchNextPlanText()
        {
            var sb = new System.Text.StringBuilder();
            var escapeBuffer = new List<char>();

            while (true)
            {
                int c = sr.Peek();

                if (escapeBuffer.Count > 0)
                {
                    if (c == ';' || escapeBuffer.Count > 5)
                    {
                        if (LEQ(escapeBuffer, "&quot")) sb.Append('\"');
                        else if (LEQ(escapeBuffer, "&amp")) sb.Append('&');
                        else if (LEQ(escapeBuffer, "&lt")) sb.Append('<');
                        else if (LEQ(escapeBuffer, "&gt")) sb.Append('>');
                        else
                        {
                            sb.Append(escapeBuffer.ToString());
                            sb.Append(c);
                            escapeBuffer.Clear();
                        }
                    }
                    else
                    {
                        escapeBuffer.Add((char)c);
                    }
                }
                else if (c == -1 || c == '<' || c == '>')
                {
                    return sb.ToString();
                }
                else if (c == '&')
                {
                    escapeBuffer.Add('&');
                }
                else
                {
                    sb.Append((char)c);
                }

                sr.Read();
            }
        }

        bool LEQ(List<char> input, string str)
        {
            if (input.Count != str.Length)
                return false;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != str[i])
                    return false;
            }

            return true;
        }


        // a  = 200
        // b = bif
        string MatchSerialString()
        {
            var sb = new System.Text.StringBuilder();
            while (true)
            {
                int c = sr.Peek();
                if (c != -1 && ( 
                    ('0' <= c && c <= '9')
                    || ('a' <= c && c <= 'z')
                    || ('A' <= c && c <= 'Z')
                    || ('_' == c)
                    ))
                {
                    sr.Read();
                    sb.Append((char)c);
                }
                else
                {
                    return sb.ToString();
                }
            }
        }

        //   "i like it"
        //   'i like it'
        //   "i'm like it"
        //   'i\'m like it'
        string MatchQuotationString()
        {
            SkipWhiteSpace();
            if ((Expect('\"') || Expect('\'')) == false)
            {
                return MatchSerialString();
            }

            int escapeChar = sr.Read();
            var sb = new System.Text.StringBuilder();
            bool lastIsEscapeChar = false;

            while (sr.Peek() != escapeChar || lastIsEscapeChar == true)
            {
                int cc = sr.Read();
                if (lastIsEscapeChar)
                {
                    sb.Append((char)cc);
                    lastIsEscapeChar = false;
                }
                else if (cc == '\\')
                {
                    lastIsEscapeChar = true;
                }
                else
                {
                    sb.Append((char)cc);
                }
            }

            sr.Read();
            return sb.ToString();
        }

        void Match(int c)
        {
            if (sr.EndOfStream == true || sr.Peek() != c)
            {
                throw new System.Exception($"unexpect token '{(char)sr.Peek()}'  !");
            }

            sr.Read();
        }

        bool Expect(int c)
        {
            if (sr.EndOfStream == true || sr.Peek() != c)
                return false;

            return true;
        }

        bool MatchAsComment()
        {
            if (ExpectToken('!'))
            {
                Match('-');
                Match('-');


                while (sr.EndOfStream == false)
                {
                    if (Expect('-'))
                    {
                        sr.Read();
                        if (Expect('-'))
                        {
                            sr.Read();
                            Match('>');
                            break;
                        }
                    }
                    sr.Read();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        HtmlNode ProgressElement(bool needMatchLeftBracket)
        {
            SkipWhiteSpace();
            if (sr.EndOfStream)
                return null;

            Dictionary<string, string> attr = new Dictionary<string, string>();
            List<HtmlNode> children = new List<HtmlNode>();


            var contentText = new System.Text.StringBuilder();
            if (needMatchLeftBracket == true)
            {
                MatchToken('<');
            }


            if (MatchAsComment())
            {
                return null;
            }

            var tag = MatchNextTagToken();
            while (ExpectToken('>') == false)
            {
                if (ExpectToken('/'))
                {
                    Match('>');
                    return new HtmlNode(tag, attr, children, contentText.ToString());
                }

                var attrKey = MatchNextTagToken();
                if (ExpectToken('='))
                {
                    var attrValue = MatchQuotationString();

                    if (attr.ContainsKey(attrKey))
                    {
                        throw new System.Exception($"Duplicated attribute declarations '${attrKey}' !");
                    }

                    attr[attrKey] = attrValue;
                }
                else
                {
                    attr[attrKey] = "";
                }
            }

            while (true)
            {
                if (ExpectToken('<'))
                {
                    if (ExpectToken('/'))
                    {
                        break;
                    }
                    else
                    {
                        // child element
                        var node = ProgressElement(false);
                        if (node != null)
                            children.Add(node);
                    }
                }
                else
                {
                    contentText.Append(MatchNextPlanText());
                }
            }


            var endOfTag = MatchNextTagToken();
            if (endOfTag != tag)
            {
                throw new System.Exception($" unexpect end of tag ${endOfTag} !");
            }
            MatchToken('>');
            return new HtmlNode(tag, attr, children, contentText.ToString());
        }

        HtmlReader(StreamReader sr)
        {
            this.sr = sr;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static List<HtmlNode> Parse(string text) => 
            Parse(new StreamReader(GenerateStreamFromString(text ?? throw new System.ArgumentNullException(nameof(text)))));

        public static List<HtmlNode> Parse(StreamReader sr)
        {
            List<HtmlNode> list = new List<HtmlNode>();
            while (sr.EndOfStream == false)
            {
                var node = new HtmlReader(sr).ProgressElement(true);
                if (node != null)
                    list.Add(node);
            }
            return list;
        }
    }

}