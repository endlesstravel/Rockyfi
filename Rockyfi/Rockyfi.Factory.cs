using System;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    // MeasureFunc describes function for measuring
    public delegate Size MeasureFunc(Node node, float width, MeasureMode widthMode, float height, MeasureMode heightMode);

    // PrintFunc defines function for printing
    public delegate void PrintFunc(Node node);

    // BaselineFunc describes function for baseline
    public delegate float BaselineFunc(Node node, float width, float height);

    // Logger defines logging function
    public delegate int LoggerFunc(Config config, Node node, LogLevel level, string format, params object[] args);

    public delegate void DrawNodeFunc(float x, float y, float width, float height, Node node);


    public partial class Node
    {
        internal void Helper_SetDimensions(Value value, Dimension dimension)
        {
            if (dimension == Dimension.Width)
            {
                if (value.unit == Unit.Auto)
                    StyleSetWidthAuto();
                else if (value.unit == Unit.Percent)
                    StyleSetWidthPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetWidth(value.value);
            }
            else
            {
                if (value.unit == Unit.Auto)
                    StyleSetHeightAuto();
                else if (value.unit == Unit.Percent)
                    StyleSetHeightPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetHeight(value.value);
            }
        }

        internal void Helper_SetMinDimensions(Value value, Dimension dimension)
        {
            if (dimension == Dimension.Width)
            {
                if (value.unit == Unit.Percent)
                    StyleSetMinWidthPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetMinWidth(value.value);
                else StyleSetMinWidth(float.NaN);
            }
            else
            {
                if (value.unit == Unit.Percent)
                    StyleSetMinHeightPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetMinHeight(value.value);
                else StyleSetMinHeight(float.NaN);
            }
        }

        internal void Helper_SetMaxDimensions(Value value, Dimension dimension)
        {
            if (dimension == Dimension.Width)
            {
                if (value.unit == Unit.Percent)
                    StyleSetMaxWidthPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetMaxWidth(value.value);
                else StyleSetMaxWidth(float.NaN);
            }
            else
            {
                if (value.unit == Unit.Percent)
                    StyleSetMaxHeightPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetMaxHeight(value.value);
                else StyleSetMaxHeight(float.NaN);
            }
        }

        internal void Helper_SetMarginPaddingBorder(string tag, Edge edge, Value value)
        {
            if (tag == "margin")
            {
                if (value.unit == Unit.Auto)
                    StyleSetMarginAuto(edge);
                else if (value.unit == Unit.Percent)
                    StyleSetMarginPercent(edge, value.value);
                else if (value.unit == Unit.Point)
                    StyleSetMargin(edge, value.value);
                else // if (value.unit == Unit.Undefined)
                    StyleSetMargin(edge, float.NaN);
            }
            else if (tag == "padding")
            {
                if (value.unit == Unit.Percent)
                    StyleSetPaddingPercent(edge, value.value);
                else if (value.unit == Unit.Point)
                    StyleSetPadding(edge, value.value);
                else StyleSetPadding(edge, float.NaN);
            }
            else if (tag == "border")
            {
                if (value.unit == Unit.Point)
                    StyleSetBorder(edge, value.value);
                else StyleSetBorder(edge, float.NaN);
            }
        }
    }

    public partial class Factory
    {
        const string ForELAttributeName = "el-for";
        const string IfELAttributeName = "el-if";
        const string BindELAttributePrefix = "el-bind";
        const string RootTagName = "div";
        const string ElementTagName = "div";

        static Regex styleValueRegex = new Regex(@"-?(\d*\.)?(\d+)(px|%)");
        static Regex elBindAttributeRegex = new Regex(BindELAttributePrefix + @":(\w|-)+");

        XmlDocument xmlDocument;
        Node root;

        Value ParseValueFromString(string text)
        {
            if (text == "auto")
            {
                return new Value(0, Unit.Auto);
            }

            var res = Value.UndefinedValue;
            if (styleValueRegex.IsMatch(text))
            {
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
            }

            return res;
        }
        Value[] ParseFourValueFromString(string text)
        {
            // Edge.Left  = 0;
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

        void RenderNodeProcessStyleAttribute(Node node, string attrKey, string attrValue)
        {
            switch (attrKey)
            {
                case "position":
                    if (Rockyfi.StringToPositionType(attrValue, out PositionType position))
                    {
                        node.StyleSetPositionType(position);
                    }
                    break;
                case "align-content":
                    if (Rockyfi.StringToAlign(attrValue, out Align alignContent))
                    {
                        node.StyleSetAlignContent(alignContent);
                    }
                    break;
                case "align-items":
                    if (Rockyfi.StringToAlign(attrValue, out Align alignItem))
                    {
                        node.StyleSetAlignItems(alignItem);
                    }
                    break;
                case "align-self":
                    if (Rockyfi.StringToAlign(attrValue, out Align alignSelf))
                    {
                        node.StyleSetAlignSelf(alignSelf);
                    }
                    break;
                case "flex-direction":
                    if (Rockyfi.StringToFlexDirection(attrValue, out FlexDirection flexDirection))
                    {
                        node.StyleSetFlexDirection(flexDirection);
                    }
                    break;
                case "flex-wrap":
                    if (Rockyfi.StringToWrap(attrValue, out Wrap flexWrap))
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
                    if (Rockyfi.StringToJustify(attrValue, out Justify justifyContent))
                    {
                        node.StyleSetJustifyContent(justifyContent);
                    }
                    break;
                case "direction":
                    if (Rockyfi.StringToDirection(attrValue, out Direction direction))
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
                            else if (Rockyfi.StringToEdge(tail, out Edge edge))
                            {
                                node.Helper_SetMarginPaddingBorder(head, edge, ParseValueFromString(attrValue));
                            }
                        }
                    }
                    break;
            }
            node.Atrribute.Add(attrKey, attrValue);
        }

        void RenderNodeProcessStyle(Node node, XmlNode ele, ContextStack contextStack)
        {
            foreach (XmlAttribute attr in ele.Attributes)
            {
                string attrKey = attr.Name;
                string attrValue = attr.Value;

                if (contextStack.TryGet(attrKey, out var bindContext))
                    attrValue = (bindContext.Data != null) ? bindContext.Data.ToString(): "";

                RenderNodeProcessStyleAttribute(node, attrKey, attrValue);
            }
        }

        bool TryRenderNodeProcessForEL(XmlNode element, ContextStack contextStack, out ForDataBindExpress forExpress)
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                if (ForELAttributeName.Equals(attr.Name))
                {
                    return ForDataBindExpress.TryParse(attr.Value, out forExpress);
                }
            }
            forExpress = null;
            return false;
        }

        bool TryRenderNodeProcessIfEL(XmlNode element, ContextStack contextStack, out IfDataBindExpress ifExpress)
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                if (IfELAttributeName.Equals(attr.Name)) // process el-if
                {
                    return IfDataBindExpress.TryParse(attr.Value, out ifExpress);
                }
            }
            ifExpress = null;
            return false;
        }

        bool TryRenderNodeProcessBindEL(XmlNode element, ContextStack contextStack, out LinkedList<ObjectDataBindExpress> objectExpressesList)
        {
            objectExpressesList = new LinkedList<ObjectDataBindExpress>();
            foreach (XmlAttribute attr in element.Attributes)
            {
                // process el-bind:xxxx="xxx-yy"
                if (elBindAttributeRegex.IsMatch(attr.Name))
                {
                    if (ObjectDataBindExpress.TryParse(attr.Value, out var bindExpress))
                    {
                        objectExpressesList.AddLast(bindExpress);
                    }
                }
            }
            return objectExpressesList.Count != 0;
        }

        LinkedList<Node> RenderNode(XmlNode element, ContextStack contextStack, bool processForEL, Node parentNode)
        {
            LinkedList<Node> nodeList = new LinkedList<Node>();
            bool isIfElExist = TryRenderNodeProcessIfEL(element, contextStack, out var ifExpress);
            bool isBindElExist = TryRenderNodeProcessBindEL(element, contextStack, out var objectExpressList);
            bool isForElExist = false;
            ForDataBindExpress forExpress = null;
            if (processForEL && (isForElExist = TryRenderNodeProcessForEL(element, contextStack, out forExpress)))
            {// expand with for list
                if (forExpress.TryEvaluate(contextStack, out var forList))
                {
                    foreach (object forContext in forList)
                    {

                        contextStack.EnterScope();
                        contextStack.Set(forExpress.IteratorName, CreateDataContext(forContext));

                        bool skipElement = (isIfElExist
                            && ifExpress.TryEvaluate(contextStack, out var ifCondition)
                            && ifCondition == false);
                        if (!skipElement)
                        {
                            if (isBindElExist)
                            {
                                foreach (var objExpress in objectExpressList)
                                {
                                    if (objExpress.TryEvaluate(contextStack, out var objExpressResult))
                                        contextStack.Set(objExpress.TargetKeys, CreateDataContext(objExpressResult));
                                }
                            }
                            foreach (var forSiblingNode in RenderNode(element, contextStack, false, parentNode))
                            {
                                nodeList.AddLast(forSiblingNode);
                            }
                        }
                        contextStack.LeaveScope();
                    }
                }
            }
            else
            {
                bool skipElement = (isIfElExist
                    && ifExpress.TryEvaluate(contextStack, out var ifCondition)
                    && ifCondition == false);

                if (!skipElement)
                {
                    contextStack.EnterScope();
                    if (isBindElExist)
                    {
                        foreach (var objExpress in objectExpressList)
                        {
                            if (objExpress.TryEvaluate(contextStack, out var objExpressResult))
                                contextStack.Set(objExpress.TargetKeys, CreateDataContext(objExpressResult));
                        }
                    }
                    var node = RenderTree(element, contextStack);
                    nodeList.AddLast(node);
                    contextStack.LeaveScope();

                    foreach (var objExpress in objectExpressList)
                    {
                        BindObjectExpressWithNode(element, objExpress, node, parentNode);
                    }
                }
            }

            // bind express <=> node
            if (nodeList.Count == 0)
            {
                BindIfExpressWithNode(element, isIfElExist ? ifExpress : null, null, parentNode);
                BindForExpressWithNode(element, (processForEL && isForElExist) ? forExpress : null, null, parentNode);
            }
            else
            {
                foreach (var node in nodeList)
                {
                    BindIfExpressWithNode(element, isIfElExist ? ifExpress : null, node, parentNode);
                    BindForExpressWithNode(element, (processForEL && isForElExist) ? forExpress : null, node, parentNode);
                    foreach (var objExpress in objectExpressList)
                    {
                        BindObjectExpressWithNode(element, objExpress, node, parentNode);
                    }
                }
            }
            return nodeList;
        }

        Node RenderTree(XmlNode element, ContextStack contextStack)
        {
            var treeRootNode = Rockyfi.CreateDefaultNode();
            RenderNodeProcessStyle(treeRootNode, element, contextStack);
            foreach (XmlNode ele in element.ChildNodes)
            {
                foreach (var childNode in RenderNode(ele, contextStack, true, treeRootNode))
                {
                    treeRootNode.AddChild(childNode);
                }
            }
            return treeRootNode;
        }

        public Direction Direction = Direction.LTR;
        public float MaxWidth = float.NaN;
        public float MaxHeight = float.NaN;

        public void CalculateLayout()
        {
            root.CalculateLayout(MaxWidth, MaxHeight, Direction);
        }

        public void Draw(DrawNodeFunc drawFunc)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                drawFunc(node.LayoutGetLeft(), node.LayoutGetTop(),
                    node.LayoutGetWidth(), node.LayoutGetHeight(),
                    node
                    );

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public void LoadFromString(string xml)
        {
            LoadFromString(xml, null);
        }
        public void LoadFromString(string xml, Dictionary<string, object> contextDictionary)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var rootElement = xmlDocument.FirstChild;

            if (!RootTagName.Equals(rootElement.Name))
                throw new Exception("root element is not <div /> !");

            if (rootElement.Attributes.GetNamedItem(ForELAttributeName) != null)
                throw new Exception("root element should not contains 'el-for' attribute !");

            if (rootElement.Attributes.GetNamedItem(ForELAttributeName) != null)
                throw new Exception("root element should not contains 'el-if' attribute !");

            var dataBindContext = new Dictionary<string, DataBindContext>();
            if (contextDictionary != null)
            {
                foreach (var kv in contextDictionary)
                {
                    dataBindContext.Add(kv.Key, CreateDataContext(kv.Value));
                }
            }
            root = RenderTree(rootElement, new ContextStack(dataBindContext));


            var writer = new System.Text.StringBuilder();
            var printer = new NodePrinter(writer, true, true, true);
            printer.Print(root);
            var got = writer.ToString();
            Console.WriteLine(got);
        }
    }
}
