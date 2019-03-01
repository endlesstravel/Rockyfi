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

        static Regex valueRegex = new Regex(@"-?(\d*\.)?(\d+)(px|%)");

        Node root;
        Config config = Rockyfi.CreateDefaultConfig();

        Value ParseValueFromString(string text)
        {
            if (text == "auto")
            {
                return new Value(0, Unit.Auto);
            }

            var res = Value.UndefinedValue;
            if (valueRegex.IsMatch(text))
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

        void RenderNodeStyleAttribute(Node node, XmlNode ele, XmlAttribute attr)
        {
            var attrName = attr.Name.ToString().ToLower();
            switch (attrName)
            {
                case "position":
                    if (Rockyfi.StringToPositionType(attr.Value, out PositionType position))
                    {
                        node.StyleSetPositionType(position);
                    }
                    break;
                case "align-content":
                    if (Rockyfi.StringToAlign(attr.Value, out Align alignContent))
                    {
                        node.StyleSetAlignContent(alignContent);
                    }
                    break;
                case "align-items":
                    if (Rockyfi.StringToAlign(attr.Value, out Align alignItem))
                    {
                        node.StyleSetAlignItems(alignItem);
                    }
                    break;
                case "align-self":
                    if (Rockyfi.StringToAlign(attr.Value, out Align alignSelf))
                    {
                        node.StyleSetAlignSelf(alignSelf);
                    }
                    break;
                case "flex-direction":
                    if (Rockyfi.StringToFlexDirection(attr.Value, out FlexDirection flexDirection))
                    {
                        node.StyleSetFlexDirection(flexDirection);
                    }
                    break;
                case "flex-wrap":
                    if (Rockyfi.StringToWrap(attr.Value, out Wrap flexWrap))
                    {
                        node.StyleSetFlexWrap(flexWrap);
                    }
                    break;
                case "flex-basis":
                    var flexBasisValue = ParseValueFromString(attr.Value);
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
                    if (float.TryParse(attr.Value, out float flexShrink))
                    {
                        node.StyleSetFlexShrink(flexShrink);
                    }
                    break;
                case "flex-grow":
                    if (float.TryParse(attr.Value, out float flexGrow))
                    {
                        node.StyleSetFlexGrow(flexGrow);
                    }
                    break;
                case "justify-content":
                    if (Rockyfi.StringToJustify(attr.Value, out Justify justifyContent))
                    {
                        node.StyleSetJustifyContent(justifyContent);
                    }
                    break;
                case "direction":
                    if (Rockyfi.StringToDirection(attr.Value, out Direction direction))
                    {
                        node.StyleSetDirection(direction);
                    }
                    break;
                case "width":
                    node.Helper_SetDimensions(ParseValueFromString(attr.Value), Dimension.Width);
                    break;
                case "height":
                    node.Helper_SetDimensions(ParseValueFromString(attr.Value), Dimension.Height);
                    break;
                case "min-width":
                    node.Helper_SetMinDimensions(ParseValueFromString(attr.Value), Dimension.Width);
                    break;
                case "min-height":
                    node.Helper_SetMinDimensions(ParseValueFromString(attr.Value), Dimension.Height);
                    break;
                case "max-width":
                    node.Helper_SetMaxDimensions(ParseValueFromString(attr.Value), Dimension.Width);
                    break;
                case "max-height":
                    node.Helper_SetMaxDimensions(ParseValueFromString(attr.Value), Dimension.Height);
                    break;
                default:
                    // parse [margin|padding|border]-[Edgexxxx]
                    if (ParseBreakWork(attrName, out string head, out string tail))
                    {
                        if (head == "margin" || head == "padding" || head == "border")
                        {
                            if (tail == "")
                            {
                                var valueArray = ParseFourValueFromString(attr.Value);
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
                                node.Helper_SetMarginPaddingBorder(head, edge, ParseValueFromString(attr.Value));
                            }
                        }
                    }
                    break;
            }
            node.Atrribute.Add(attr.Name.ToString(), attr.Value);
        }

        Node[] RenderNode(XmlNode ele, ContextStack stack)
        {
            Node node = Rockyfi.CreateDefaultNode();
            foreach (XmlAttribute attr in ele.Attributes)
            {
                RenderNodeStyleAttribute(node, ele, attr);
            }
            return node;
        }

        // 
        static Regex elBindAttributeRegex = new Regex(BindELAttributePrefix + @":(\w|-)+");

        void RenderNodeProcessELBind(XmlNode element, ContextStack contextStack)
        {
            // process el-for
            var forELExpress = element.Attributes.GetNamedItem(ForELAttributeName);
            if (forELExpress != null && DataBindForExpress.TryParse(forELExpress.Value, out var forExpress))
            {
                var evaluatedForValue = forExpress.Evaluate(contextStack);
                if (evaluatedForValue != null)
                {
                    var forContext = CreateDataContext(evaluatedForValue);
                    contextStack.Set(forExpress.IteratorName, forContext);
                    BindExpressWithNode();
                }
            }
            
            foreach (XmlAttribute attr in element.Attributes)
            {
                // process el-bind:xxxx="xxxx"
                if (elBindAttributeRegex.IsMatch(attr.Name))
                {
                    string bindELExpress = attr.Value;
                    if (DataBindObjectExpress.TryParse(bindELExpress, out var bindExpress))
                    {
                        var bindObjValue = bindExpress.Evaluate(contextStack);
                        if (bindObjValue != null)
                        {
                            string targetName = attr.Name.Split(':')[1];
                            contextStack.Set(targetName, CreateDataContext(bindObjValue));
                        }
                    }
                }

                // process el-if
                if (IfELAttributeName.Equals(attr.Name))
                {

                }
            }
        }

        LinkedList<Node> TraverseRenderNode(XmlNode element, ContextStack contextStack)
        {
            contextStack.EnterScope();
            LinkedList<Node> nodeList = new LinkedList<Node>();
            RenderNodeProcessELBind(element, contextStack);
            foreach (var node in nodeList)
            {
                foreach (XmlNode e in element.ChildNodes)
                {
                    var children = TraverseRenderNode(e, contextStack);
                    foreach (var child in children)
                    {
                        node.InsertChild(child, node.Children.Count);
                    }
                }
            }
            contextStack.LeaveScope();
            return nodeList;
        }

        Node TraverseRootRenderNode(XmlNode element)
        {
            if (element.Name.Equals(RootTagName))
            {
                return TraverseRenderNode(element).First.Value;
            }

            throw new Exception("root element is not <div /> !");
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
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            root = TraverseRootRenderNode(doc.FirstChild);
        }
    }
}
