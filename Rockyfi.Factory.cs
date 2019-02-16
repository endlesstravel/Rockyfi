using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    // MeasureFunc describes function for measuring
    internal delegate Size MeasureFunc(Node node, float width, MeasureMode widthMode, float height, MeasureMode heightMode);

    // PrintFunc defines function for printing
    internal delegate void PrintFunc(Node node);

    // BaselineFunc describes function for baseline
    public delegate float BaselineFunc(Node node, float width, float height);

    // Logger defines logging function
    public delegate int LoggerFunc(Config config, Node node, LogLevel level, string format, params object[] args);


    public delegate int DrawNodeFunc(float x, float y, float width, float height, Node node);

    partial class Factory
    {
        const string RootTagName = "div";
        const string ElementTagName = "div";

        Node root;
        Config config = Node.CreateDefaultConfig();

        Regex valueRegex = new Regex(@"-?(\d*\.)?(\d+)(px|%)");

        Value parseValueFromString(string text)
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

        Node SetupNode(XElement ele)
        {
            Node node = Node.CreateDefaultNode();
            foreach (var attr in ele.Attributes())
            {
                switch(attr.Name.ToString())
                {
                    case "width":
                        node.Helper_SetDimensions(parseValueFromString(attr.Value), Dimension.Width);
                        break;
                    case "height":
                        node.Helper_SetDimensions(parseValueFromString(attr.Value), Dimension.Height);
                        break;
                }
            }
            return node;
        }

        Node SetupTraverse(XElement element)
        {
            Node node = SetupNode(element);
            foreach (var e in element.Elements())
            {
                Node.InsertChild(node, SetupTraverse(e), node.Children.Count);
            }
            return node;

        }
        Node SetupTraverseRoot(XElement element)
        {
            if (element.Name.Equals(RootTagName))
            {
                return SetupTraverse(element);
            }

            throw new Exception("root element is not <root /> !");
        }

        public void Update(float availableWidth, float availableHeight,
            Direction parentDirection,
            MeasureMode widthMeasureMode, MeasureMode heightMeasureMode,
            float parentWidth, float parentHeight,
            bool performLayout)
        {
            root.Update(availableWidth, availableHeight, parentDirection,
                widthMeasureMode, heightMeasureMode, parentWidth, parentHeight,
                performLayout, 
                this.config);
        }

        public void LoadFromString(string xml, float rootWidth, float rootHeight)
        {
            string tmp = @"
<div>
    <div width=""100px"" height=""30%"">
    </div>
</div>
";
            root = SetupTraverseRoot(XElement.Parse(xml));
        }
    }
}
