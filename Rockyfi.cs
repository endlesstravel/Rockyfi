

namespace Rockyfi
{

    class Constant
    {
        const int EdgeCount = 9;
        const int ExperimentalFeatureCount = 1;
        const int MeasureModeCount = 3;
    }

    enum Align
    {
        Auto,
        FlexStart,
        Center,
        FlexEnd,
        Strech,
        Baseline,
        SpaceBetween,
        SpaceAround,
    }

    enum Dimension
    {
        Width,
        Height,
    }

    enum Direction
    {
        Inherit,
        LTR,
        RTL,
    }

    enum Display
    {
        Flex,
        None,
    }

    enum Edge
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

    enum ExperimentalFeature
    {
        WebFlexBasis,
    }

    enum FlexDirection
    {
        Column,
        ColumnReverse,
        Row,
        RowReverse,
    }

    enum Justify
    {
        FlexStart,
        Center,
        FlexEnd,
        SpaceBetween,
        SpaceAround,
    }

    enum LogLevel
    {
        Error,
        Warn,
        Info,
        Debug,
        Berbose,
        Fatal,
    }

    enum MeasureMode
    {
        Undefined,
        Exactly,
        AtMost,
    }

    enum NodeType
    {
        Default,
        Text,
    }

    enum Overflow
    {
        Bisible,
        Hidden,
        Scroll,
    }

    enum PositionType
    {
        Relative,
        Absolute,
    }

    enum PrintOptions
    {
        Layout,
        Style,
        Children,
    }

    enum Unit
    {
        Undefined,
        Point,
        Percent,
        Auto,
    }

    enum Warp
    {
        NoWrap,
        Wrap,
        WrapReverse,
    }

}