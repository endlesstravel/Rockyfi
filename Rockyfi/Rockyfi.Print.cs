using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rockyfi
{
    public class NodePrinter
    {
        public NodePrinter(StringBuilder writer, bool PrintOptionsLayout,bool PrintOptionsStyle,bool PrintOptionsChildren)
        {
            this.writer = writer;
            this.PrintOptionsLayout = PrintOptionsLayout;
            this.PrintOptionsStyle = PrintOptionsStyle;
            this.PrintOptionsChildren = PrintOptionsChildren;
        }

        StringBuilder writer;
        bool PrintOptionsLayout;
        bool PrintOptionsStyle;
        bool PrintOptionsChildren;

        public void Print(Node node)
        {
            PrintNode(node, 0);
        }

        void PrintNode(Node node, int level)
        {
            var printer = this;
            printer.printIndent(level);
            printer.printf("<div ");

            if (node.printFunc != null) {
                node.printFunc(node);
            }

            if (PrintOptionsLayout) {
                printer.printf($"layout=\"");
                printer.printf($"width: {node.LayoutGetWidth()}; ");
                printer.printf($"height: {node.LayoutGetHeight()}; ");
                printer.printf($"top: {node.LayoutGetTop()}; ");
                printer.printf($"left: {node.LayoutGetLeft()};");
                printer.printf("\" ");
            }

            if (PrintOptionsStyle) {
                printer.printf("style=\"");
                if (node.nodeStyle.FlexDirection != Constant.nodeDefaults.nodeStyle.FlexDirection) {
                    printer.printf($"flex-direction: {Rockyfi.FlexDirectionToString(node.nodeStyle.FlexDirection)}; ");
                }
                if (node.nodeStyle.JustifyContent != Constant.nodeDefaults.nodeStyle.JustifyContent) {
                    printer.printf($"justify-content: {Rockyfi.JustifyToString(node.nodeStyle.JustifyContent)}; ");
                }
                if (node.nodeStyle.AlignItems != Constant.nodeDefaults.nodeStyle.AlignItems) {
                    printer.printf($"align-items: {Rockyfi.AlignToString(node.nodeStyle.AlignItems)}; ");
                }
                if (node.nodeStyle.AlignContent != Constant.nodeDefaults.nodeStyle.AlignContent) {
                    printer.printf($"align-content: {Rockyfi.AlignToString(node.nodeStyle.AlignContent)}; ");
                }
                if (node.nodeStyle.AlignSelf != Constant.nodeDefaults.nodeStyle.AlignSelf) {
                    printer.printf($"align-self: {Rockyfi.AlignToString(node.nodeStyle.AlignSelf)}; ");
                }

                printer.printFloatIfNotUndefined(node, "flex-grow", node.nodeStyle.FlexGrow);
                printer.printFloatIfNotUndefined(node, "flex-shrink", node.nodeStyle.FlexShrink);
                printer.printNumberIfNotAuto(node, "flex-basis", node.nodeStyle.FlexBasis);
                printer.printFloatIfNotUndefined(node, "flex", node.nodeStyle.Flex);

                if (node.nodeStyle.FlexWrap != Constant.nodeDefaults.nodeStyle.FlexWrap) {
                    printer.printf($"flexWrap: {Rockyfi.WrapToString(node.nodeStyle.FlexWrap)}; ");
                }

                if (node.nodeStyle.Overflow != Constant.nodeDefaults.nodeStyle.Overflow) {
                    printer.printf($"overflow: {Rockyfi.OverflowToString(node.nodeStyle.Overflow)}; ");
                }

                if (node.nodeStyle.Display != Constant.nodeDefaults.nodeStyle.Display) {
                    printer.printf($"display: {Rockyfi.DisplayToString(node.nodeStyle.Display)}; ");
                }

                printer.printEdges(node, "margin", node.nodeStyle.Margin);
                printer.printEdges(node, "padding", node.nodeStyle.Padding);
                printer.printEdges(node, "border", node.nodeStyle.Border);

                printer.printNumberIfNotAuto(node, "width", node.nodeStyle.Dimensions[(int)Dimension.Width]);
                printer.printNumberIfNotAuto(node, "height", node.nodeStyle.Dimensions[(int)Dimension.Height]);
                printer.printNumberIfNotAuto(node, "max-width", node.nodeStyle.MaxDimensions[(int)Dimension.Width]);
                printer.printNumberIfNotAuto(node, "max-height", node.nodeStyle.MaxDimensions[(int)Dimension.Height]);
                printer.printNumberIfNotAuto(node, "min-width", node.nodeStyle.MinDimensions[(int)Dimension.Width]);
                printer.printNumberIfNotAuto(node, "min-height", node.nodeStyle.MinDimensions[(int)Dimension.Height]);

                if (node.nodeStyle.PositionType != Constant.nodeDefaults.nodeStyle.PositionType) {
                    printer.printf($"position: {Rockyfi.PositionTypeToString(node.nodeStyle.PositionType)}; ");
                }

                printer.printEdgeIfNotUndefined(node, "left", node.nodeStyle.Position, Edge.Left);
                printer.printEdgeIfNotUndefined(node, "right", node.nodeStyle.Position, Edge.Right);
                printer.printEdgeIfNotUndefined(node, "top", node.nodeStyle.Position, Edge.Top);
                printer.printEdgeIfNotUndefined(node, "bottom", node.nodeStyle.Position, Edge.Bottom);
                printer.printf("\"");

                if (node.measureFunc != null) {
                    printer.printf(" has-custom-measure=\"true\"");
                }
            }
            printer.printf(">");

            var childCount = (node.Children.Count);
            if (PrintOptionsChildren && childCount > 0) {
                for (int i = 0; i < childCount; i++) {
                    printer.printf("\n");
                    printer.PrintNode(node.Children[(int)i], level+1);
                }
                printer.printIndent(level);
                printer.printf("\n");
            }
            if (childCount != 0) {
                printer.printIndent(level);
            }
            printer.printf("</div>");
        }

        void printEdges(Node node, string str, Value[] edges) {
            if (fourValuesEqual(edges)) {
                printNumberIfNotZero(node, str, edges[(int)Edge.Left]);
                // bugfix for issue #5
                // if we set EdgeAll, the values are
                // [{NaN 0} {NaN 0} {NaN 0} {NaN 0} {NaN 0} {NaN 0} {NaN 0} {NaN 0} {20 1}]
                // so EdgeLeft is not printed and we won't print padding
                // for simplicity, I assume that EdgeAll is exclusive with setting specific edges
                // so we can print both and only one should show up
                // C code has this bug: https://github.com/facebook/yoga/blob/26481a6553a33d9c005f2b8d24a7952fc58df32c/yoga/Yoga.c#L1036
                printNumberIfNotZero(node, str, edges[(int)Edge.All]);
            } else {
                for (var edge = (int)Edge.Left; edge < Constant.EdgeCount; edge++) {
                    var buf = $"{str}-{Rockyfi.EdgeToString((Edge)edge)}";
                    printNumberIfNotZero(node, buf, edges[edge]);
                }
            }
        }

        void printEdgeIfNotUndefined(Node node, string str, Value[] edges, Edge edge) {
            printNumberIfNotUndefined(node, str, Rockyfi.computedEdgeValue(edges, edge, Value.UndefinedValue));
        }

        void printFloatIfNotUndefined(Node node, string str, float number) {
            if (!float.IsNaN(number)) {
                printf($"{str}: {number}; ");
            }
        }

        void printNumberIfNotUndefined(Node node, string str, Value number) {
            if (number.unit != Unit.Undefined) {
                if (number.unit == Unit.Auto) {
                    printf($"{str}: auto; ");
                } else {
                    var unit = "%";

                    if (number.unit == Unit.Point) {
                        unit = "px";
                    }
                    printf($"{str}: {number.value}{unit}; ");
                }
            }
        }

        void printNumberIfNotAuto(Node node, string str, Value number) {
            if (number.unit != Unit.Auto) {
                printNumberIfNotUndefined(node, str, number);
            }
        }

        void printNumberIfNotZero(Node node, string str, Value number) {
            if (!Rockyfi.FloatsEqual(number.value, 0)) {
                printNumberIfNotUndefined(node, str, number);
            }
        }

        void printf(string str) {
            writer.Append(str);
        }

        void printIndent(int n)
        {
            for (int i = 0; i < n; i ++) writer.Append("  ");
        }

        bool fourValuesEqual(Value[] four) {
            return Rockyfi.ValueEqual(four[0], four[1]) && Rockyfi.ValueEqual(four[0], four[2]) &&
                Rockyfi.ValueEqual(four[0], four[3]);
        }

    }

    //public partial class Node
    //{
    //    public void PrintNode(Node node, PrintOptions print)
    //    {

    //    }
    //}
}
