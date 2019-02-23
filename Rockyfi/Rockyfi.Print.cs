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
                    printer.printf($"flex-direction: {FlexDirectionToString(node.nodeStyle.FlexDirection)}; ");
                }
                if (node.nodeStyle.JustifyContent != Constant.nodeDefaults.nodeStyle.JustifyContent) {
                    printer.printf($"justify-content: {JustifyToString(node.nodeStyle.JustifyContent)}; ");
                }
                if (node.nodeStyle.AlignItems != Constant.nodeDefaults.nodeStyle.AlignItems) {
                    printer.printf($"align-items: {AlignToString(node.nodeStyle.AlignItems)}; ");
                }
                if (node.nodeStyle.AlignContent != Constant.nodeDefaults.nodeStyle.AlignContent) {
                    printer.printf($"align-content: {AlignToString(node.nodeStyle.AlignContent)}; ");
                }
                if (node.nodeStyle.AlignSelf != Constant.nodeDefaults.nodeStyle.AlignSelf) {
                    printer.printf($"align-self: {AlignToString(node.nodeStyle.AlignSelf)}; ");
                }

                printer.printFloatIfNotUndefined(node, "flex-grow", node.nodeStyle.FlexGrow);
                printer.printFloatIfNotUndefined(node, "flex-shrink", node.nodeStyle.FlexShrink);
                printer.printNumberIfNotAuto(node, "flex-basis", node.nodeStyle.FlexBasis);
                printer.printFloatIfNotUndefined(node, "flex", node.nodeStyle.Flex);

                if (node.nodeStyle.FlexWrap != Constant.nodeDefaults.nodeStyle.FlexWrap) {
                    printer.printf($"flexWrap: {WrapToString(node.nodeStyle.FlexWrap)}; ");
                }

                if (node.nodeStyle.Overflow != Constant.nodeDefaults.nodeStyle.Overflow) {
                    printer.printf($"overflow: {OverflowToString(node.nodeStyle.Overflow)}; ");
                }

                if (node.nodeStyle.Display != Constant.nodeDefaults.nodeStyle.Display) {
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

                if (node.nodeStyle.PositionType != Constant.nodeDefaults.nodeStyle.PositionType) {
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


        #region XXXX_ToSring
        // AlignToString returns string version of Align enum
        public static string AlignToString(Align value)
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
        public static string DimensionToString(Dimension value)
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
        public static string DirectionToString(Direction value)
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
        public static string DisplayToString(Display value)
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
        public static string EdgeToString(Edge value)
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
        public static string ExperimentalFeatureToString(ExperimentalFeature value)
        {
            switch (value)
            {
                case ExperimentalFeature.WebFlexBasis:
                    return "web-flex-basis";
            }
            return "unknown";
        }

        // FlexDirectionToString returns string version of FlexDirection enum
        public static string FlexDirectionToString(FlexDirection value)
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
        public static string JustifyToString(Justify value)
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
        public static string LogLevelToString(LogLevel value)
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
        public static string MeasureModeToString(MeasureMode value)
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
        public static string NodeTypeToString(NodeType value)
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
        public static string OverflowToString(Overflow value)
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
        public static string PositionTypeToString(PositionType value)
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
        public static string PrintOptionsToString(PrintOptions value)
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
        public static string UnitToString(Unit value)
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
        public static string WrapToString(Wrap value)
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
        #endregion

        #region StringTo_XXXX

        // AlignToString returns string version of Align enum
        public static bool StringToAlign(string value, out Align result)
        {
            switch (value)
            {
                case "auto": result = Align.Auto; return true;
                case "flex-start": result = Align.FlexStart; return true;
                case "center": result = Align.Center; return true;
                case "flex-end": result = Align.FlexEnd; return true;
                case "stretch": result = Align.Stretch; return true;
                case "baseline": result = Align.Baseline; return true;
                case "space-between": result = Align.SpaceBetween; return true;
                case "space-around": result = Align.SpaceAround; return true;
            }
            result = Align.Auto;
            return true;
        }

        // DimensionToString returns string version of Dimension enum
        public static bool StringToDimension(string value, out Dimension result)
        {
            switch (value)
            {
                case "width": result = Dimension.Width; return true;
                case "height": result = Dimension.Height; return true;
            }
            result = Dimension.Width;
            return true;
        }

        // DirectionToString returns string version of Direction enum
        public static bool StringToDirection(string value, out Direction result)
        {
            switch (value)
            {
                case "inherit": result = Direction.Inherit; return true;
                case "ltr": result = Direction.LTR; return true;
                case "rtl": result = Direction.RTL; return true;
            }
            result = Direction.Inherit;
            return true;
        }

        // DisplayToString returns string version of Display enum
        public static bool StringToDisplay(string value, out Display result)
        {
            switch (value)
            {
                case "flex": result = Display.Flex; return true;
                case "none": result = Display.None; return true;
            }
            result = Display.Flex;
            return true;
        }

        // EdgeToString returns string version of Edge enum
        public static bool StringToEdge(string value, out Edge result)
        {
            switch (value)
            {
                case "left": result = Edge.Left; return true;
                case "top": result = Edge.Top; return true;
                case "right": result = Edge.Right; return true;
                case "bottom": result = Edge.Bottom; return true;
                case "start": result = Edge.Start; return true;
                case "end": result = Edge.End; return true;
                case "horizontal": result = Edge.Horizontal; return true;
                case "vertical": result = Edge.Vertical; return true;
                case "all": result = Edge.All; return true;
            }
            result = Edge.Left;
            return true;
        }

        // ExperimentalFeatureToString returns string version of ExperimentalFeature enum
        public static bool StringToExperimentalFeature(string value, out ExperimentalFeature result)
        {
            switch (value)
            {
                case "web-flex-basis": result = ExperimentalFeature.WebFlexBasis; return true;
            }
            result = ExperimentalFeature.WebFlexBasis;
            return true;
        }

        // FlexDirectionToString returns string version of FlexDirection enum
        public static bool StringToFlexDirection(string value, out FlexDirection result)
        {
            switch (value)
            {
                case "column": result = FlexDirection.Column; return true;
                case "column-reverse": result = FlexDirection.ColumnReverse; return true;
                case "row": result = FlexDirection.Row; return true;
                case "row-reverse": result = FlexDirection.RowReverse; return true;
            }
            result = FlexDirection.Column;
            return true;
        }

        // JustifyToString returns string version of Justify enum
        public static bool StringToJustify(string value, out Justify result)
        {
            switch (value)
            {
                case "flex-start": result = Justify.FlexStart; return true;
                case "center": result = Justify.Center; return true;
                case "flex-end": result = Justify.FlexEnd; return true;
                case "space-between": result = Justify.SpaceBetween; return true;
                case "space-around": result = Justify.SpaceAround; return true;
            }
            result = Justify.FlexStart;
            return true;
        }

        public static bool StringToLogLevel(string value, out LogLevel result)
        {
            switch (value)
            {
                case "error": result = LogLevel.Error; return true;
                case "warn": result = LogLevel.Warn; return true;
                case "info": result = LogLevel.Info; return true;
                case "debug": result = LogLevel.Debug; return true;
                case "verbose": result = LogLevel.Verbose; return true;
                case "fatal": result = LogLevel.Fatal; return true;
            }
            result = LogLevel.Error;
            return true;
        }

        public static bool StringToMeasureMode(string value, out MeasureMode result)
        {
            switch (value)
            {
                case "undefined": result = MeasureMode.Undefined; return true;
                case "exactly": result = MeasureMode.Exactly; return true;
                case "at-most": result = MeasureMode.AtMost; return true;
            }
            result = MeasureMode.Undefined;
            return true;
        }

        public static bool StringToNodeType(string value, out NodeType result)
        {
            switch (value)
            {
                case "default": result = NodeType.Default; return true;
                case "text": result = NodeType.Text; return true;
            }
            result = NodeType.Default;
            return true;
        }

        public static bool StringToOverflow(string value, out Overflow result)
        {
            switch (value)
            {
                case "visible": result = Overflow.Visible; return true;
                case "hidden": result = Overflow.Hidden; return true;
                case "scroll": result = Overflow.Scroll; return true;
            }
            result = Overflow.Visible;
            return true;
        }

        public static bool StringToPositionType(string value, out PositionType result)
        {
            switch (value)
            {
                case "relative": result = PositionType.Relative; return true;
                case "absolute": result = PositionType.Absolute; return true;
            }
            result = PositionType.Relative;
            return true;
        }

        // PrintOptionsToString returns string version of PrintOptions enum
        public static bool StringToPrintOptions(string value, out PrintOptions result)
        {
            switch (value)
            {
                case "layout": result = PrintOptions.Layout; return true;
                case "style": result = PrintOptions.Style; return true;
                case "children": result = PrintOptions.Children; return true;
            }
            result = PrintOptions.Layout;
            return true;
        }

        // UnitToString returns string version of Unit enum
        public static bool StringToUnit(string value, out Unit result)
        {
            switch (value)
            {
                case "undefined": result = Unit.Undefined; return true;
                case "point": result = Unit.Point; return true;
                case "percent": result = Unit.Percent; return true;
                case "auto": result = Unit.Auto; return true;
            }
            result = Unit.Undefined;
            return true;
        }

        // WrapToString returns string version of Wrap enum
        public static bool StringToWrap(string value, out Wrap result)
        {
            switch (value)
            {
                case "no-wrap": result = Wrap.NoWrap; return true;
                case "wrap": result = Wrap.Wrap; return true;
                case "wrap-reverse": result = Wrap.WrapReverse; return true;
            }
            result = Wrap.NoWrap;
            return true;
        }

        #endregion

    }

    public partial class Node
    {
        public void PrintNode(Node node, PrintOptions print)
        {

        }
    }
}
