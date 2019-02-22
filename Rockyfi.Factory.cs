using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    // MeasureFunc describes function for measuring
    public delegate Size MeasureFunc(Node node, float width, MeasureMode widthMode, float height, MeasureMode heightMode);

    // PrintFunc defines function for printing
    internal delegate void PrintFunc(Node node);

    // BaselineFunc describes function for baseline
    public delegate float BaselineFunc(Node node, float width, float height);

    // Logger defines logging function
    public delegate int LoggerFunc(Config config, Node node, LogLevel level, string format, params object[] args);

    public delegate void DrawNodeFunc(float x, float y, float width, float height, Node node);

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

                node.Atrribute.Add(attr.Name.ToString(), attr.Value);
            }
            return node;
        }

        Node SetupTraverse(XElement element)
        {
            Node node = SetupNode(element);
            foreach (var e in element.Elements())
            {
                var child = SetupTraverse(e);
                Node.InsertChild(node, child, node.Children.Count);
            }
            return node;

        }

        Node SetupTraverseRoot(XElement element)
        {
            if (element.Name.LocalName.Equals(RootTagName))
            {
                return SetupTraverse(element);
            }

            throw new Exception("root element is not <div /> !");
        }

        public void Update(Direction direction)
        {
            root.Update(float.NaN, float.NaN, direction);
        }

        public void Draw(DrawNodeFunc drawFunc)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                drawFunc(node.LayoutGetLeft(), node.LayoutGetRight(),
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
            root = SetupTraverseRoot(XElement.Parse(xml));
        }
    }
}
