using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rockyfi
{
    class NodePrinter
    {
        public NodePrinter(StringBuilder writer, bool PrintOptionsLayout,bool PrintOptionsStyle,bool PrintOptionsChildren)
        {
            this.writer = writer;
            this.PrintOptionsLayout = PrintOptionsLayout;
            this.PrintOptionsStyle = PrintOptionsStyle;
            this.PrintOptionsChildren = PrintOptionsChildren;
        }

        static Node nodeDefaults = Node.CreateDefaultNode();

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
                if (node.nodeStyle.FlexDirection != nodeDefaults.nodeStyle.FlexDirection) {
                    printer.printf($"flex-direction: {FlexDirectionToString(node.nodeStyle.FlexDirection)}; ");
                }
                if (node.nodeStyle.JustifyContent != nodeDefaults.nodeStyle.JustifyContent) {
                    printer.printf($"justify-content: {JustifyToString(node.nodeStyle.JustifyContent)}; ");
                }
                if (node.nodeStyle.AlignItems != nodeDefaults.nodeStyle.AlignItems) {
                    printer.printf($"align-items: {AlignToString(node.nodeStyle.AlignItems)}; ");
                }
                if (node.nodeStyle.AlignContent != nodeDefaults.nodeStyle.AlignContent) {
                    printer.printf($"align-content: {AlignToString(node.nodeStyle.AlignContent)}; ");
                }
                if (node.nodeStyle.AlignSelf != nodeDefaults.nodeStyle.AlignSelf) {
                    printer.printf($"align-self: {AlignToString(node.nodeStyle.AlignSelf)}; ");
                }

                printer.printFloatIfNotUndefined(node, "flex-grow", node.nodeStyle.FlexGrow);
                printer.printFloatIfNotUndefined(node, "flex-shrink", node.nodeStyle.FlexShrink);
                printer.printNumberIfNotAuto(node, "flex-basis", node.nodeStyle.FlexBasis);
                printer.printFloatIfNotUndefined(node, "flex", node.nodeStyle.Flex);

                if (node.nodeStyle.FlexWrap != nodeDefaults.nodeStyle.FlexWrap) {
                    printer.printf($"flexWrap: {WrapToString(node.nodeStyle.FlexWrap)}; ");
                }

                if (node.nodeStyle.Overflow != nodeDefaults.nodeStyle.Overflow) {
                    printer.printf($"overflow: {OverflowToString(node.nodeStyle.Overflow)}; ");
                }

                if (node.nodeStyle.Display != nodeDefaults.nodeStyle.Display) {
                    printer.printf($"display: {DisplayToString(node.nodeStyle.Display)}; ");
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

                if (node.nodeStyle.PositionType != nodeDefaults.nodeStyle.PositionType) {
                    printer.printf($"position: {PositionTypeToString(node.nodeStyle.PositionType)}; ");
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
                    var buf = $"{str}-{EdgeToString((Edge)edge)}";
                    printNumberIfNotZero(node, buf, edges[edge]);
                }
            }
        }

        void printEdgeIfNotUndefined(Node node, string str, Value[] edges, Edge edge) {
            printNumberIfNotUndefined(node, str, Node.computedEdgeValue(edges, edge, Value.UndefinedValue));
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
            if (!Node.FloatsEqual(number.value, 0)) {
                printNumberIfNotUndefined(node, str, number);
            }
        }

        void printf(string str) {
            writer.Append(str);
        }

        void printIndent(int n)
        {
            List<string> indent = new List<string>(n);
            for (int i = 0; i < n; i ++) indent.Add("");
            writer.Append(string.Join("  ", indent));
        }

        bool fourValuesEqual(Value[] four) {
            return Node.ValueEqual(four[0], four[1]) && Node.ValueEqual(four[0], four[2]) &&
                Node.ValueEqual(four[0], four[3]);
        }




        // AlignToString returns string version of Align enum
        static string AlignToString(Align value)
        {
            switch (value)
            {
                case Align.Auto:
                    return "auto";
                case Align.FlexStart:
                    return "flex-start";
                case Align.Center:
                    return "center";
                case Align.FlexEnd:
                    return "flex-end";
                case Align.Stretch:
                    return "stretch";
                case Align.Baseline:
                    return "baseline";
                case Align.SpaceBetween:
                    return "space-between";
                case Align.SpaceAround:
                    return "space-around";
            }
            return "unknown";
        }

        // DimensionToString returns string version of Dimension enum
        static string DimensionToString(Dimension value)
        {
            switch (value)
            {
                case Dimension.Width:
                    return "width";
                case Dimension.Height:
                    return "height";
            }
            return "unknown";
        }

        // DirectionToString returns string version of Direction enum
        static string DirectionToString(Direction value)
        {
            switch (value)
            {
                case Direction.Inherit:
                    return "inherit";
                case Direction.LTR:
                    return "ltr";
                case Direction.RTL:
                    return "rtl";
            }
            return "unknown";
        }

        // DisplayToString returns string version of Display enum
        static string DisplayToString(Display value)
        {
            switch (value)
            {
                case Display.Flex:
                    return "flex";
                case Display.None:
                    return "none";
            }
            return "unknown";
        }

        // EdgeToString returns string version of Edge enum
        static string EdgeToString(Edge value)
        {
            switch (value)
            {
                case Edge.Left:
                    return "left";
                case Edge.Top:
                    return "top";
                case Edge.Right:
                    return "right";
                case Edge.Bottom:
                    return "bottom";
                case Edge.Start:
                    return "start";
                case Edge.End:
                    return "end";
                case Edge.Horizontal:
                    return "horizontal";
                case Edge.Vertical:
                    return "vertical";
                case Edge.All:
                    return "all";
            }
            return "unknown";
        }

        // ExperimentalFeatureToString returns string version of ExperimentalFeature enum
        static string ExperimentalFeatureToString(ExperimentalFeature value)
        {
            switch (value)
            {
                case ExperimentalFeature.WebFlexBasis:
                    return "web-flex-basis";
            }
            return "unknown";
        }

        // FlexDirectionToString returns string version of FlexDirection enum
        static string FlexDirectionToString(FlexDirection value)
        {
            switch (value)
            {
                case FlexDirection.Column:
                    return "column";
                case FlexDirection.ColumnReverse:
                    return "column-reverse";
                case FlexDirection.Row:
                    return "row";
                case FlexDirection.RowReverse:
                    return "row-reverse";
            }
            return "unknown";
        }

        // JustifyToString returns string version of Justify enum
        static string JustifyToString(Justify value)
        {
            switch (value)
            {
                case Justify.FlexStart:
                    return "flex-start";
                case Justify.Center:
                    return "center";
                case Justify.FlexEnd:
                    return "flex-end";
                case Justify.SpaceBetween:
                    return "space-between";
                case Justify.SpaceAround:
                    return "space-around";
            }
            return "unknown";
        }

        // LogLevelToString returns string version of LogLevel enum
        static string LogLevelToString(LogLevel value)
        {
            switch (value)
            {
                case LogLevel.Error:
                    return "error";
                case LogLevel.Warn:
                    return "warn";
                case LogLevel.Info:
                    return "info";
                case LogLevel.Debug:
                    return "debug";
                case LogLevel.Verbose:
                    return "verbose";
                case LogLevel.Fatal:
                    return "fatal";
            }
            return "unknown";
        }

        // MeasureModeToString returns string version of MeasureMode enum
        static string MeasureModeToString(MeasureMode value)
        {
            switch (value)
            {
                case MeasureMode.Undefined:
                    return "undefined";
                case MeasureMode.Exactly:
                    return "exactly";
                case MeasureMode.AtMost:
                    return "at-most";
            }
            return "unknown";
        }

        // NodeTypeToString returns string version of NodeType enum
        static string NodeTypeToString(NodeType value)
        {
            switch (value)
            {
                case NodeType.Default:
                    return "default";
                case NodeType.Text:
                    return "text";
            }
            return "unknown";
        }

        // OverflowToString returns string version of Overflow enum
        static string OverflowToString(Overflow value)
        {
            switch (value)
            {
                case Overflow.Visible:
                    return "visible";
                case Overflow.Hidden:
                    return "hidden";
                case Overflow.Scroll:
                    return "scroll";
            }
            return "unknown";
        }

        // PositionType returns string version of PositionType enum
        static string PositionTypeToString(PositionType value)
        {
            switch (value)
            {
                case PositionType.Relative:
                    return "relative";
                case PositionType.Absolute:
                    return "absolute";
            }
            return "unknown";
        }

        // PrintOptionsToString returns string version of PrintOptions enum
        static string PrintOptionsToString(PrintOptions value)
        {
            switch (value)
            {
                case PrintOptions.Layout:
                    return "layout";
                case PrintOptions.Style:
                    return "style";
                case PrintOptions.Children:
                    return "children";
            }
            return "unknown";
        }

        // UnitToString returns string version of Unit enum
        static string UnitToString(Unit value)
        {
            switch (value)
            {
                case Unit.Undefined:
                    return "undefined";
                case Unit.Point:
                    return "point";
                case Unit.Percent:
                    return "percent";
                case Unit.Auto:
                    return "auto";
            }
            return "unknown";
        }

        // WrapToString returns string version of Wrap enum
        static string WrapToString(Wrap value)
        {
            switch (value)
            {
                case Wrap.NoWrap:
                    return "no-wrap";
                case Wrap.Wrap:
                    return "wrap";
                case Wrap.WrapReverse:
                    return "wrap-reverse";
            }
            return "unknown";
        }

    }

    public partial class Node
    {
        public void PrintNode(Node node, PrintOptions print)
        {

        }
    }
}
