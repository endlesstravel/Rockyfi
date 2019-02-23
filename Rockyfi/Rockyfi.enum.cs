
namespace Rockyfi
{

    public enum Align
    {
        Auto,
        FlexStart,
        Center,
        FlexEnd,
        Stretch,
        Baseline,
        SpaceBetween,
        SpaceAround,
    }

    public enum Dimension
    {
        Width,
        Height,
    }

    public enum Direction
    {

        Inherit = 0,
        LTR,
        RTL,
        NeverUsed_1 = -1,
    }

    public enum Display
    {
        Flex,
        None,
    }

    public enum Edge: int
    {
        Left,
        Top,
        Right,
        Bottom,
        Start,
        End,
        Horizontal,
        Vertical,
        All,
    }


    // int Edge.Left  = 0;
    // int Edge.Top = 1;
    // int Edge.Right = 2;
    // int Edge.Bottom = 3;
    // int Edge.Start = 4;
    // int Edge.End = 5;
    // int EdgeHorizontal = 6;
    // int EdgeVertical = 7;
    // int EdgeAll = 8;

    public enum ExperimentalFeature
    {
        WebFlexBasis,
    }

    public enum FlexDirection
    {
        Column,
        ColumnReverse,
        Row,
        RowReverse,
    }

    public enum Justify
    {
        FlexStart,
        Center,
        FlexEnd,
        SpaceBetween,
        SpaceAround,
    }

    public enum LogLevel
    {
        Error,
        Warn,
        Info,
        Debug,
        Verbose,
        Fatal,
    }

    public enum MeasureMode: int
    {
        Undefined = 0,
        Exactly,
        AtMost,
        NeverUsed_1 = -1,
    }

    public enum NodeType
    {
        Default,
        Text,
    }

    public enum Overflow
    {
        Visible,
        Hidden,
        Scroll,
    }

    public enum PositionType
    {
        Relative,
        Absolute,
    }

    public enum PrintOptions
    {
        Layout,
        Style,
        Children,
    }

    public enum Unit
    {
        Undefined,
        Point,
        Percent,
        Auto,
    }

    public enum Wrap
    {
        NoWrap,
        Wrap,
        WrapReverse,
    }
}