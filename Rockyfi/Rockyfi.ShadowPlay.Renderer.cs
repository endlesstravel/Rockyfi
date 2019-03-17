using System;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace Rockyfi
{
    /// <summary>
    /// Provide a convenient way to render tree
    /// </summary>
    public partial class ShadowPlay<T> where T: BridgeElement<T>
    {
        public ShadowPlay() { }
        const string ForELAttributeName = "el-for";
        const string IfELAttributeName = "el-if";
        const string BindELAttributePrefix = "el-bind";

        XmlDocument xmlDocument;
        Node root;
        TemplateNode templateRoot;

        readonly Dictionary<string, Value> valueDictionaryCache = new Dictionary<string, Value>();

        Value ParseValueFromString(string text)
        {
            if (text == "auto")
            {
                return new Value(0, Unit.Auto);
            }

            if (valueDictionaryCache.TryGetValue(text, out var cachedValue))
            {
                return cachedValue;
            }

            var res = Value.UndefinedValue;
            string dig = text;
            Unit uu = Unit.Undefined;

            if (text.EndsWith("%"))
            {
                dig = text.Substring(0, text.Length - 1);
                uu = Unit.Percent;
            }
            else if (text.EndsWith("px"))
            {
                dig = text.Substring(0, text.Length - 2);
                uu = Unit.Point;
            }

            if (float.TryParse(dig, out res.value))
            {
                res.unit = uu;
            }
            else
            {
                res.unit = Unit.Undefined;
            }

            valueDictionaryCache.Add(text, res);
            return res;
        }

        /// <summary>
        /// margin="2px" | margin="12px 13px" | margin="12px 13px 1px" | margin="1px 2px 3px 4px"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Value[] ParseFourValueFromString(string text)
        {
            // Edge.Left = 0;
            // Edge.Top = 1;
            // Edge.Right = 2;
            // Edge.Bottom = 3;
            var vStr = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vStr.Length > 4)
            {
                return null;
            }
            Value[] res = new Value[vStr.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = ParseValueFromString(vStr[i]);
            }
            if (res.Length == 0)
            {
                return null;
            }
            if (res.Length == 1)
            {
                return new Value[] { res[0], res[0], res[0], res[0], };
            }
            else if (res.Length == 2)
            {
                return new Value[] { res[1], res[0], res[1], res[0], };
            }
            else if (res.Length == 3)
            {
                return new Value[] { res[1], res[0], res[1], res[2], };
            }
            if (res.Length > 4)
            {
                OnWarnning($"{text} too much params");
            }
            //else if (res.Length >= 4)
            return new Value[] { res[3], res[0], res[1], res[2], };
        }

        // margin-left --> ("margin", "left")
        // margin --> ("margin", "")
        static bool ParseBreakWork(string input, out string head, out string tail)
        {
            head = "";
            tail = "";
            var t = input.Split('-');
            if (t.Length == 2)
            {
                head = t[0];
                tail = t[1];
                return true;
            }
            if (t.Length == 1)
            {
                head = t[0];
                return true;
            }

            return false;
        }
        void OnWarnning(string msg)
        {

        }

        #region render virtual node to real node
        internal void ProcessNodeStyle(Node node, string attrKey, string attrValue)
        {
            switch (attrKey)
            {
                case "position":
                    if (Flex.StringToPositionType(attrValue, out PositionType position))
                    {
                        node.StyleSetPositionType(position);
                    }
                    break;
                case "align-content":
                    if (Flex.StringToAlign(attrValue, out Align alignContent))
                    {
                        node.StyleSetAlignContent(alignContent);
                    }
                    break;
                case "align-items":
                    if (Flex.StringToAlign(attrValue, out Align alignItem))
                    {
                        node.StyleSetAlignItems(alignItem);
                    }
                    break;
                case "align-self":
                    if (Flex.StringToAlign(attrValue, out Align alignSelf))
                    {
                        node.StyleSetAlignSelf(alignSelf);
                    }
                    break;
                case "flex-direction":
                    if (Flex.StringToFlexDirection(attrValue, out FlexDirection flexDirection))
                    {
                        node.StyleSetFlexDirection(flexDirection);
                    }
                    break;
                case "flex-wrap":
                    if (Flex.StringToWrap(attrValue, out Wrap flexWrap))
                    {
                        node.StyleSetFlexWrap(flexWrap);
                    }
                    break;
                case "flex-basis":
                    var flexBasisValue = ParseValueFromString(attrValue);
                    if (flexBasisValue.unit == Unit.Auto)
                    {
                        node.NodeStyleSetFlexBasisAuto();
                    }
                    else if (flexBasisValue.unit == Unit.Point)
                    {
                        node.StyleSetFlexBasis(flexBasisValue.value);
                    }
                    else if (flexBasisValue.unit == Unit.Percent)
                    {
                        node.StyleSetFlexBasisPercent(flexBasisValue.value);
                    }
                    break;
                case "flex-shrink":
                    if (float.TryParse(attrValue, out float flexShrink))
                    {
                        node.StyleSetFlexShrink(flexShrink);
                    }
                    break;
                case "flex-grow":
                    if (float.TryParse(attrValue, out float flexGrow))
                    {
                        node.StyleSetFlexGrow(flexGrow);
                    }
                    break;
                case "justify-content":
                    if (Flex.StringToJustify(attrValue, out Justify justifyContent))
                    {
                        node.StyleSetJustifyContent(justifyContent);
                    }
                    break;
                case "direction":
                    if (Flex.StringToDirection(attrValue, out Direction direction))
                    {
                        node.StyleSetDirection(direction);
                    }
                    break;
                case "width":
                    node.Helper_SetDimensions(ParseValueFromString(attrValue), Dimension.Width);
                    break;
                case "height":
                    node.Helper_SetDimensions(ParseValueFromString(attrValue), Dimension.Height);
                    break;
                case "min-width":
                    node.Helper_SetMinDimensions(ParseValueFromString(attrValue), Dimension.Width);
                    break;
                case "min-height":
                    node.Helper_SetMinDimensions(ParseValueFromString(attrValue), Dimension.Height);
                    break;
                case "max-width":
                    node.Helper_SetMaxDimensions(ParseValueFromString(attrValue), Dimension.Width);
                    break;
                case "max-height":
                    node.Helper_SetMaxDimensions(ParseValueFromString(attrValue), Dimension.Height);
                    break;
                default:
                    // parse [margin|padding|border]-[Edgexxxx]
                    if (ParseBreakWork(attrKey, out string head, out string tail))
                    {
                        if (head == "margin" || head == "padding" || head == "border")
                        {
                            if (tail == "")
                            {
                                var valueArray = ParseFourValueFromString(attrValue);
                                if (valueArray != null)
                                {
                                    for (int i = 0; i < valueArray.Length; i++)
                                    {
                                        node.Helper_SetMarginPaddingBorder(head, (Edge)i, valueArray[i]);
                                    }
                                }
                            }
                            else if (Flex.StringToEdge(tail, out Edge edge))
                            {
                                node.Helper_SetMarginPaddingBorder(head, edge, ParseValueFromString(attrValue));
                            }
                        }
                    }
                    break;
            }
        }
        Node RenderTemplateTree(TemplateNode tnode, ContextStack contextStack, object forContext)
        {
            Node node = Flex.CreateDefaultNode();
            var ra = CreateRuntimeNodeAttribute(node, tnode);

            // set el-for value
            ra.forExpressItemCurrentValue = forContext;

            // copy style
            Style.Copy(node.nodeStyle, tnode.nodeStyle);

            // process node el-bind
            var originalAttrs = ra.attributes;
            foreach (var attr in tnode.attributeDataBindExpressList)
            {
                if (attr.TryEvaluate(contextStack, out var attrValue))
                {
                    if (originalAttrs.TryGetValue(attr, out var oldValue) && oldValue == attrValue)
                    {
                        // equal and not
                        // Console.WriteLine("no need to process style");
                    }
                    else
                    {
                        ra.attributes[attr] = attrValue;
                        ProcessNodeStyle(node, attr.TargetName, attrValue != null ? attrValue.ToString() : "");
                    }
                }
                else
                {
                    ra.attributes.Remove(attr);
                }
            }

            // process innerText
            if (tnode.textDataBindExpress != null)
            {
                ra.textDataBindExpressCurrentValue = tnode.textDataBindExpress.Evaluate(contextStack);
            }

            // render children
            ra.ResetChildren((appendChild) =>
            {
                foreach (var vchild in tnode.Children)
                {
                    foreach (var child in RenderTemplateTreeExpand(vchild, contextStack))
                    {
                        node.AddChild(child);
                        appendChild(vchild, child);
                    }
                }
            });
            return node;
        }
        LinkedList<Node> RenderTemplateTreeExpand(TemplateNode tnode, ContextStack contextStack)
        {
            LinkedList<Node> nodeList = new LinkedList<Node>();
            bool useFor = tnode.forExpress != null;
            IEnumerable<object> forList = useFor ? tnode.forExpress.Evaluate(contextStack) : new List<object> { 0 };
            if (forList != null)
            {
                foreach (object forContext in forList)
                {
                    if (useFor)
                    {
                        contextStack.EnterScope();
                        contextStack.Set(tnode.forExpress.IteratorName, forContext);
                    }
                    bool skipElement = (tnode.ifExpress != null
                        && tnode.ifExpress.TryEvaluate(contextStack, out var result)
                        && result == false);
                    if (!skipElement)
                    {
                        var fc = useFor ? forContext : null;
                        var node = RenderTemplateTree(tnode, contextStack, fc);
                        var ra = GetNodeRuntimeAttribute(node);
                        // ra.IsThunk = useFor;
                        nodeList.AddLast(node);
                    }
                    if (useFor)
                    {
                        contextStack.LeaveScope();
                    }
                }
            }
            return nodeList;
        }
        Node RenderTemplateTreeRoot(TemplateNode tnode)
        {
            if (tnode.forExpress != null)
                throw new Exception("root element should not contains 'el-for' attribute !");
            if (tnode.ifExpress != null)
                throw new Exception("root element should not contains 'el-if' attribute !");
            return RenderTemplateTree(templateRoot, GenerateContextStack(), null);
        }
        #endregion

        #region render xml to virtual node
        void ProcessTemplateStyle(Style style, string attrKey, string attrValue)
        {
            switch (attrKey)
            {
                case "direction":
                    if (Flex.StringToDirection(attrValue, out Direction direction))
                    {
                        style.Direction = direction;
                    }
                    break;
                case "flex-direction":
                    if (Flex.StringToFlexDirection(attrValue, out FlexDirection flexDirection))
                    {
                        style.FlexDirection = flexDirection;
                    }
                    break;
                case "justify-content":
                    if (Flex.StringToJustify(attrValue, out Justify justifyContent))
                    {
                        style.JustifyContent = justifyContent;
                    }
                    break;
                case "align-content":
                    if (Flex.StringToAlign(attrValue, out Align alignContent))
                    {
                        style.AlignContent = alignContent;
                    }
                    break;
                case "align-items":
                    if (Flex.StringToAlign(attrValue, out Align alignItem))
                    {
                        style.AlignItems = alignItem;
                    }
                    break;
                case "align-self":
                    if (Flex.StringToAlign(attrValue, out Align alignSelf))
                    {
                        style.AlignSelf = alignSelf;
                    }
                    break;
                case "flex-wrap":
                    if (Flex.StringToWrap(attrValue, out Wrap flexWrap))
                    {
                        style.FlexWrap = flexWrap;
                    }
                    break;
                case "overflow":
                    if (Flex.StringToOverflow(attrValue, out Overflow overflow))
                    {
                        style.Overflow = overflow;
                    }
                    break;
                case "display":
                    if (Flex.StringToDisplay(attrValue, out Display display))
                    {
                        style.Display = display;
                    }
                    break;
                case "flex":
                    if (float.TryParse(attrValue, out float flex))
                    {
                        style.Flex = flex;
                    }
                    break;
                case "flex-grow":
                    if (float.TryParse(attrValue, out float flexGrow))
                    {
                        style.FlexGrow = flexGrow;
                    }
                    break;
                case "flex-shrink":
                    if (float.TryParse(attrValue, out float flexShrink))
                    {
                        style.FlexShrink = flexShrink;
                    }
                    break;
                case "flex-basis":
                    style.FlexBasis = ParseValueFromString(attrValue);
                    break;
                case "position":
                    if (Flex.StringToPositionType(attrValue, out PositionType position))
                    {
                        style.PositionType = position;
                    }
                    break;
                case "width":
                    style.Dimensions[(int)Dimension.Width] = ParseValueFromString(attrValue);
                    break;
                case "height":
                    style.Dimensions[(int)Dimension.Height] = ParseValueFromString(attrValue);
                    break;
                case "min-width":
                    style.MinDimensions[(int)Dimension.Width] = ParseValueFromString(attrValue);
                    break;
                case "min-height":
                    style.MinDimensions[(int)Dimension.Height] = ParseValueFromString(attrValue);
                    break;
                case "max-width":
                    style.MaxDimensions[(int)Dimension.Width] = ParseValueFromString(attrValue);
                    break;
                case "max-height":
                    style.MaxDimensions[(int)Dimension.Height] = ParseValueFromString(attrValue);
                    break;
                default:
                    // parse [margin|padding|border]-[Edgexxxx]
                    if (ParseBreakWork(attrKey, out string head, out string tail))
                    {
                        Value[] valuesToSet = null;
                        switch (head)
                        {
                            case "margin": valuesToSet = style.Margin; break;
                            case "padding": valuesToSet = style.Padding; break;
                            case "border": valuesToSet = style.Border; break;
                        }
                        if (valuesToSet == null)
                            break;

                        if (tail == "")
                        {
                            var valueArray = ParseFourValueFromString(attrValue);
                            if (valueArray != null)
                            {
                                for (int i = 0; i < valueArray.Length; i++)
                                {
                                    valuesToSet[i] = valueArray[i];
                                }
                            }
                        }
                        else if (Flex.StringToEdge(tail, out Edge edge))
                        {
                            valuesToSet[(int)edge] = ParseValueFromString(attrValue);
                        }
                    }
                    break;
            }
        }
        TemplateNode ConvertXmlToTemplate(XmlNode element)
        {
            TemplateNode renderTreeNode = new TemplateNode(element.Name);
            foreach (XmlAttribute attr in element.Attributes)
            {
                if (ForELAttributeName.Equals(attr.Name)) // process el-for
                {
                    renderTreeNode.forExpress = ForDataBindExpress.Parse(attr.Value);
                }
                else if (IfELAttributeName.Equals(attr.Name)) // process el-if
                {
                    renderTreeNode.ifExpress = IfDataBindExpress.Parse(attr.Value);
                }
                else if (BindELAttributePrefix.Equals(attr.Prefix)) // process el-bind:width="item.width"
                {
                    var attrExpress = AttributeDataBindExpress.Parse(attr.Value, attr.LocalName);
                    if (attrExpress != null)
                    {
                        renderTreeNode.attributeDataBindExpressList.AddLast(attrExpress);
                    }
                }
                else
                {
                    // try process style
                    ProcessTemplateStyle(renderTreeNode.nodeStyle, attr.Name, attr.Value);
                }

                renderTreeNode.attributes[attr.Name] = attr.Value;
            }

            // process children
            // render children
            foreach (XmlNode ele in element.ChildNodes)
            {
                if (XmlNodeType.Element == ele.NodeType)
                {
                    var trn = ConvertXmlToTemplate(ele);
                    renderTreeNode.Children.Add(trn);
                }
                else if (XmlNodeType.Text == ele.NodeType)
                {
                    renderTreeNode.textDataBindExpress = TextDataBindExpress.Parse(ele.Value);
                }
            }

            return renderTreeNode;
        }
        #endregion

        public Direction Direction = Direction.LTR;
        public float MaxWidth = float.NaN;
        public float MaxHeight = float.NaN;

        void ProcessStyleBind(Node node, ContextStack contextStack)
        {
            var ra = GetNodeRuntimeAttribute(node);
            var tnode = ra.template;

            // copy style
            Style.Copy(node.nodeStyle, tnode.nodeStyle);

            // process node el-bind
            var originalAttrs = ra.attributes;
            foreach (var attr in tnode.attributeDataBindExpressList)
            {
                if (attr.TryEvaluate(contextStack, out var attrValue))
                {
                    if (originalAttrs.TryGetValue(attr, out var oldValue) && oldValue == attrValue)
                    {
                        // equal and not
                        // Console.WriteLine("no need to process style");
                    }
                    else
                    {
                        ra.attributes[attr] = attrValue;
                        ProcessNodeStyle(node, attr.TargetName, attrValue != null ? attrValue.ToString() : "");
                    }
                }
                else
                {
                    ra.attributes.Remove(attr);
                }
            }

            // process innerText
            if (tnode.textDataBindExpress != null)
            {
                ra.textDataBindExpressCurrentValue = tnode.textDataBindExpress.Evaluate(contextStack);
            }

        }

        /// <summary>
        /// Update and re-renderer the tree, according the data.
        /// </summary>
        public void Update()
        {
            if (bridge != null)
            {
                var newRoot = RenderTemplateTreeRoot(templateRoot);
                var patch = VirtualDom.Patch.Diff(GetNodeRuntimeAttribute(root), GetNodeRuntimeAttribute(newRoot));
                patch.DoPatch(this, bridge);
            }
            else
            {
                root = RenderTemplateTreeRoot(templateRoot);
            }

            CalculateLayout();
        }

        /// <summary>
        /// re calculate layout
        /// </summary>
        public void CalculateLayout()
        {
            root.CalculateLayout(MaxWidth, MaxHeight, Direction);
        }

        public void Build(string xml, params string[] properties)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlReaderSettings settings = new XmlReaderSettings { NameTable = new NameTable() };
                XmlNamespaceManager xmlns = new XmlNamespaceManager(settings.NameTable);
                xmlns.AddNamespace(BindELAttributePrefix, "Rockyfi.ShadowPlay");
                XmlParserContext context = new XmlParserContext(null, xmlns, "", XmlSpace.Default);
                XmlReader reader = XmlReader.Create(stringReader, settings, context);
                xmlDocument = new XmlDocument();
                xmlDocument.Load(reader);

                var rootElement = xmlDocument.FirstChild;
                if (rootElement.Attributes.GetNamedItem(ForELAttributeName) != null)
                    throw new Exception("root element should not contains 'el-for' attribute !");

                if (rootElement.Attributes.GetNamedItem(ForELAttributeName) != null)
                    throw new Exception("root element should not contains 'el-if' attribute !");

                // set the properties
                SetProperties(properties);

                // convert to tree
                templateRoot = ConvertXmlToTemplate(rootElement);
            }
        }

        public override string ToString()
        {
            return NodePrinter.PrintToString(root);
        }

        public static ShadowPlay<T> BuildStage(string xml, params string[] properties)
        {
            var factory = new ShadowPlay<T>();
            factory.Build(xml, properties);
            return factory;
        }
    }
}
