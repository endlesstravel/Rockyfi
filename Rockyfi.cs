
#if true

using System.Collections.Generic;

namespace Rockyfi
{
    class Rockyfi
    {
        class Constant
        {
            public const int EdgeCount = 9;
            public const int ExperimentalFeatureCount = 1;
            public const int MeasureModeCount = 3;

            /// <summary>
            /// This value was chosen based on empiracle data. Even the most complicated
            /// layouts should not require more than 16 entries to fit within the cache.
            /// </summary>
            public const int MaxCachedResultCount = 16;
        }

        enum Align
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

        enum Dimension
        {
            Width,
            Height,
        }

        enum Direction
        {

            Inherit = 0,
            LTR,
            RTL,
            NeverUsed_1 = -1,
        }

        enum Display
        {
            Flex,
            None,
        }

        enum Edge: int
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
            Verbose,
            Fatal,
        }

        enum MeasureMode: int
        {
            Undefined = 0,
            Exactly,
            AtMost,
            NeverUsed_1 = -1,
        }

        enum NodeType
        {
            Default,
            Text,
        }

        enum Overflow
        {
            Visible,
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

        enum Wrap
        {
            NoWrap,
            Wrap,
            WrapReverse,
        }

        struct Size
        {
            float Width;
            float Height;
        }

        struct Value
        {
            public float value;
            public Unit unit;

            public Value(float v, Unit u)
            {
                this.value = v;
                this.unit = u;
            }
        }

        class CachedMeasurement
        {
            float availableWidth;
            float availableHeight;
            MeasureMode widthMeasureMode = MeasureMode.Undefined;
            MeasureMode heightMeasureMode = MeasureMode.Undefined;

            float computedWidth = -1;
            float computedHeight = -1;

            public void ResetToDefault()
            {
                this.availableHeight = 0;
                this.availableWidth = 0;
                this.widthMeasureMode = MeasureMode.Undefined;
                this.heightMeasureMode = MeasureMode.Undefined;
                this.computedWidth = -1;
                this.computedHeight = -1;
            }
        }

        class Layout
        {
            public readonly float[] Position = new float[4];
            public readonly float[] Dimensions = new float[2]{ float.NaN, float.NaN };
            public readonly float[] Margin = new float[6];
            public readonly float[] Border = new float[6];
            public readonly float[] Padding = new float[6];
            public Direction  Direction;
            public int computedFlexBasisGeneration;
            public float computedFlexBasis = float.NaN;
            public bool HadOverflow = false;
            
            // Instead of recomputing the entire layout every single time, we
            // cache some information to break early when nothing changed
            public int generationCount;
            public Direction lastParentDirection = Direction.NeverUsed_1;
            public int nextCachedMeasurementsIndex = 0;
            public readonly CachedMeasurement[] cachedMeasurements = new CachedMeasurement[Constant.MaxCachedResultCount]
            {
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
                new CachedMeasurement(),
            };
            public readonly float[] measuredDimensions = new float[2]{ float.NaN, float.NaN };
            readonly public CachedMeasurement cachedLayout = new CachedMeasurement();

            public void ResetToDefault()
            {
                for (int i = 0; i < this.Position.Length; i++)
                {
                    this.Position[i] = 0;
                }
                for (int i = 0; i < Dimensions.Length; i++)
                {
                    this.Dimensions[i] = float.NaN;
                }
                for (int i = 0; i < 6; i++)
                {
                    this.Margin[i] = 0;
                    this.Border[i] = 0;
                    this.Padding[i] = 0;
                }
                this.Direction = Direction.Inherit;
                this.computedFlexBasisGeneration = 0;
                this.computedFlexBasis = float.NaN;
                this.HadOverflow = false;
                this.generationCount = 0;
                this.lastParentDirection = Direction.NeverUsed_1;
                this.nextCachedMeasurementsIndex = 0;

                foreach (var cm in this.cachedMeasurements)
                {
                    cm.ResetToDefault();
                }

                for (int i = 0; i < measuredDimensions.Length; i++)
                {
                    this.measuredDimensions[i] = float.NaN;
                }

                cachedLayout.ResetToDefault();
            }
        }

        class Style
        {
            public Direction      Direction = Direction.Inherit;
            public FlexDirection  FlexDirection = FlexDirection.Column;
            public Justify JustifyContent = Justify.FlexStart;
            public Align AlignContent = Align.FlexStart;
            public Align AlignItems = Align.Stretch;
            public Align AlignSelf;
            public PositionType PositionType;
            public Wrap FlexWrap;
            public Overflow Overflow = Overflow.Visible;
            public Display Display = Display.Flex;
            public float Flex = float.NaN;
            public float FlexGrow = float.NaN;
            public float FlexShrink = float.NaN;
            public Value FlexBasis = autoValue;
            public readonly Value[] Margin = defaultEdgeValuesUnit();
            public readonly Value[] Position = defaultEdgeValuesUnit();
            public readonly Value[] Padding = defaultEdgeValuesUnit();
            public readonly Value[] Border = defaultEdgeValuesUnit();
            public readonly Value[] Dimensions = new Value[2]{ autoValue, autoValue };
            public readonly Value[] MinDimensions = new Value[2]{ undefinedValue, undefinedValue};
            public readonly Value[] MaxDimensions = new Value[2]{ undefinedValue, undefinedValue};
            // Yoga specific properties, not compatible with flexbox specification
            public float AspectRatio = float.NaN;


            static public void Copy(Style dest, Style src)
            {
                dest.Direction = src.Direction;
                dest.FlexDirection = src.FlexDirection;
                dest.JustifyContent = src.JustifyContent;
                dest.AlignContent = src.AlignContent;
                dest.AlignItems = src.AlignItems;
                dest.AlignSelf = src.AlignSelf;
                dest.PositionType = src.PositionType;
                dest.FlexWrap = src.FlexWrap;
                dest.Overflow = src.Overflow;
                dest.Display = src.Display;
                dest.Flex = src.Flex;
                dest.FlexGrow = src.FlexGrow;
                dest.FlexShrink = src.FlexShrink;

                for (int i = 0; i < src.Margin.Length; i++)
                {
                    dest.Margin[i] = src.Margin[i];
                    dest.Position[i] = src.Position[i];
                    dest.Padding[i] = src.Padding[i];
                    dest.Border[i] = src.Border[i];
                }

                for (int i = 0; i < 2; i++)
                {
                    dest.Dimensions[i] = src.Dimensions[i];
                    dest.MinDimensions[i] = src.MinDimensions[i];
                    dest.MaxDimensions[i] = src.MaxDimensions[i];
                }

                dest.AspectRatio = src.AspectRatio;
            }
        }

        class Config
        {
            readonly public bool[] experimentalFeatures = new bool[Constant.ExperimentalFeatureCount + 1];
            public bool UseWebDefaults = false;
            public bool UseLegacyStretchBehaviour = false;
            public float PointScaleFactor = 1;
            public LoggerFunc Logger = DefaultLog;
            public object Context = null;

            public static void Copy(Config dest, Config src)
            {
                dest.UseWebDefaults = src.UseWebDefaults;
                dest.UseLegacyStretchBehaviour = src.UseLegacyStretchBehaviour;
                dest.PointScaleFactor = src.PointScaleFactor;
                dest.Logger = src.Logger;
                dest.Context = src.Context;

                for (int i = 0; i < src.experimentalFeatures.Length; i++)
                {
                    dest.experimentalFeatures[i] = src.experimentalFeatures[i];
                }
            }
        }

        class Node
        {
            readonly public Style     Style = new Style();
            readonly public Layout    Layout = new Layout();
            public int lineIndex;

            public Node Parent = null;
            public readonly List<Node> Children = new List<Node>();

            public Node NextChild;

            public MeasureFunc Measure;
            public BaselineFunc Baseline;
            public PrintFunc Print;
            public Config Config;
            public object Context;

            public bool IsDirty = false;
            public bool hasNewLayout = true;
            public NodeType NodeType = NodeType.Default;

            public readonly Value[] resolvedDimensions = new Value[2]{ ValueUndefined, ValueUndefined };
        }

        // MeasureFunc describes function for measuring
        delegate Size MeasureFunc(Node node, float width, MeasureMode widthMode, float height, MeasureMode heightMode);

        // BaselineFunc describes function for baseline
        delegate float BaselineFunc(Node node, float width, float height);

        // PrintFunc defines function for printing
        delegate void PrintFunc(Node node);

        // Logger defines logging function
        delegate int LoggerFunc(Config config, Node node, LogLevel level, string format, params object[] args);

        readonly static Value undefinedValue = new Value(float.NaN, Unit.Undefined);
        readonly static Value autoValue = new Value(float.NaN, Unit.Auto);

        static Value[] defaultEdgeValuesUnit(){
            return new Value[Constant.EdgeCount]{
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
                undefinedValue,
            };
        } 

        const float defaultFlexGrow = 0;
        const float defaultFlexShrink = 0;
        const float webDefaultFlexShrink = 1;

        static Node nodeDefaults()
        {
            var node = new Node();
            return node;

        }

        readonly static Value ValueZero =  new Value(0, Unit.Point);
        readonly static Value ValueUndefined = new Value(float.NaN, Unit.Undefined);

        readonly static Value ValueAuto = new Value(float.NaN, Unit.Auto);

        static Config configDefaults()
        {
            return new Config();
        }

        static bool feq(float a, float b)
        {
            if (float.IsNaN(a) && float.IsNaN(b))
                return true;

            return a == b;
        }

        static bool valueEq(Value v1, Value v2)
        {
            if (v1.unit != v2.unit)
                return false;
            return feq(v1.value, v2.value);
        }

        static int DefaultLog(Config config, Node node, LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
            case LogLevel.Error:
            case LogLevel.Fatal:
                System.Console.WriteLine(format, args);
                return 0;
            case LogLevel.Warn:
            case LogLevel.Info:
            case LogLevel.Debug:
            case LogLevel.Verbose:
            default:
                System.Console.WriteLine(format, args);
                break;
            }

            return 0;
        }


        static Value computedEdgeValue(Value[] edges, Edge edge, Value defaultValue)
        {
            if (edges[(int)edge].unit != Unit.Undefined) {
                return edges[(int)edge];
            }

            bool isVertEdge = (edge == Edge.Top || edge == Edge.Bottom);
            if(isVertEdge && edges[(int)(Edge.Vertical)].unit != Unit.Undefined){
                return edges[(int)Edge.Vertical];
            }

            bool isHorizEdge = (edge == Edge.Left || edge == Edge.Right || edge == Edge.Start || edge == Edge.End);
            if (isHorizEdge && edges[(int)Edge.Horizontal].unit != Unit.Undefined) {
                return edges[(int)Edge.Horizontal];
            }

            if (edges[(int)Edge.All].unit != Unit.Undefined) {
                return edges[(int)Edge.All];
            }

            if (edge == Edge.Start || edge == Edge.End) {
                return ValueUndefined;
            }

            return defaultValue;
        }

        static float resolveValue(Value value, float parentSize)
        {
            switch (value.unit)
            {
                case Unit.Undefined:
                case Unit.Auto:
                    return float.NaN;
                case Unit.Point:
                    return value.value;
                case Unit.Percent:
                    return value.value * parentSize / 100f;
            }
            return float.NaN;
        }


        static float resolveValueMargin(Value value, float parentSize) 
        {
            if (value.unit == Unit.Auto)
            {
                return 0;
            }

            return resolveValue(value, parentSize);
        }

        // // NewNodeWithConfig creates new node with config
        static Node NewNodeWithConfig(Config config)
        {
            var node = nodeDefaults();

            if( config.UseWebDefaults)
            {
                node.Style.FlexDirection = FlexDirection.Row;
                node.Style.AlignContent = Align.Stretch;
            }
            node.Config = config;
            return node;
        }

        // // NewNode creates a new node
        static Node NewNode()
        {
            return NewNodeWithConfig(configDefaults());
        }

        // static int Len(Node[] array)
        // {
        //     return array == null ? 0 : array.Length;
        // }

        // // Reset resets a node
        static void Reset(ref Node node)
        {
            assertWithNode(node, node.Children.Count == 0, "Cannot reset a node which still has children attached");
            assertWithNode(node, node.Parent == null, "Cannot reset a node still attached to a parent");
            node.Children.Clear();

            var config = node.Config;
            node = nodeDefaults();
            if(config.UseWebDefaults)
            {
                node.Style.FlexDirection = FlexDirection.Row;
                node.Style.AlignContent = Align.Stretch;
            }
            node.Config = config;
        }

        // ConfigGetDefault returns default config, only for C#
        static Config ConfigGetDefault() {
            return configDefaults();
        }

        // NewConfig creates new config
        static Config NewConfig()
        {
            return configDefaults();
        }

        // ConfigCopy copies a config
        static void ConfigCopy(Config dest, Config src) 
        {
            Config.Copy(dest, src);
        }

        static void nodeMarkDirtyInternal(Node node) 
        {
            if (!node.IsDirty)
            {
                node.IsDirty = true;
                node.Layout.computedFlexBasis = float.NaN;
                if(node.Parent != null) 
                {
                    nodeMarkDirtyInternal(node.Parent);
                }
            }
        }

        // SetMeasureFunc sets measure function
        static void SetMeasureFunc(Node node, MeasureFunc measureFunc) 
        {
            if ( measureFunc == null )
            {
                node.Measure = null;
                // TODO: t18095186 Move nodeType to opt-in function and mark appropriate places in Litho
                node.NodeType = NodeType.Default;
            } else {
                assertWithNode(
                    node,
                    node.Children.Count == 0,
                    "Cannot set measure function: Nodes with measure functions cannot have children.");
                node.Measure = measureFunc;
                // TODO: t18095186 Move nodeType to opt-in function and mark appropriate places in Litho
                node.NodeType = NodeType.Text;
            }
        }

        // InsertChild inserts a child
        static void InsertChild(Node node, Node child, int idx) 
        {
            assertWithNode(node, child.Parent == null, "Child already has a parent, it must be removed first.");
            assertWithNode(node, node.Measure == null, "Cannot add child: Nodes with measure functions cannot have children.");

            node.Children.Insert(idx, child);
            child.Parent = node;
            nodeMarkDirtyInternal(node);
        }

        // RemoveChild removes child node
        static void RemoveChild(Node node, Node child) 
        {
            if (node.Children.Remove(child))
            {
                child.Layout.ResetToDefault(); // layout is no longer valid
                child.Parent = null;
                nodeMarkDirtyInternal(node);
            }
        }

        // GetChild returns a child at a given index
        static Node GetChild(Node node, int idx)
        {
            return idx < node.Children.Count ? node.Children[idx] : null;
        }

        // MarkDirty marks node as dirty
        static void MarkDirty(Node node) {
            assertWithNode(node, node.Measure != null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");
            nodeMarkDirtyInternal(node);
        }

        static bool styleEq(Style s1, Style s2) {
            if( s1.Direction != s2.Direction ||
                s1.FlexDirection != s2.FlexDirection ||
                s1.JustifyContent != s2.JustifyContent ||
                s1.AlignContent != s2.AlignContent ||
                s1.AlignItems != s2.AlignItems ||
                s1.AlignSelf != s2.AlignSelf ||
                s1.PositionType != s2.PositionType ||
                s1.FlexWrap != s2.FlexWrap ||
                s1.Overflow != s2.Overflow ||
                s1.Display != s2.Display ||
                !feq(s1.Flex, s2.Flex) ||
                !feq(s1.FlexGrow, s2.FlexGrow) ||
                !feq(s1.FlexShrink, s2.FlexShrink) ||
                !valueEq(s1.FlexBasis, s2.FlexBasis) ) {
                return false;
            }
            for (int i = 0; i < Constant.EdgeCount; i++) {
                if ( !valueEq(s1.Margin[i], s2.Margin[i]) ||
                    !valueEq(s1.Position[i], s2.Position[i]) ||
                    !valueEq(s1.Padding[i], s2.Padding[i]) ||
                    !valueEq(s1.Border[i], s2.Border[i]) ) {
                    return false;
                }
            }
            for (int i = 0; i < 2; i++) {
                if (!valueEq(s1.Dimensions[i], s2.Dimensions[i]) ||
                    !valueEq(s1.MinDimensions[i], s2.MinDimensions[i]) ||
                    !valueEq(s1.MaxDimensions[i], s2.MaxDimensions[i]) ) {
                    return false;
                }
            }
            return true;
        }

        // NodeCopyStyle copies style
        static void NodeCopyStyle(Node dstNode, Node srcNode) 
        {
            if (!styleEq(dstNode.Style, srcNode.Style)) 
            {
                Style.Copy(dstNode.Style, srcNode.Style);
                nodeMarkDirtyInternal(dstNode);
            }
        }

        static float resolveFlexGrow(Node node)
        {
            // Root nodes flexGrow should always be 0
            if (node.Parent == null) 
            {
                return 0;
            }
            if (!FloatIsUndefined(node.Style.FlexGrow))
            {
                return node.Style.FlexGrow;
            }
            if (!FloatIsUndefined(node.Style.Flex) && node.Style.Flex > 0) 
            {
                return node.Style.Flex;
            }
            return defaultFlexGrow;
        }

        // StyleGetFlexGrow gets flex grow
        static float StyleGetFlexGrow(Node node)
        {
            if (FloatIsUndefined(node.Style.FlexGrow))
            {
                return defaultFlexGrow;
            }
            return node.Style.FlexGrow;
        }

        // StyleGetFlexShrink gets flex shrink
        static float StyleGetFlexShrink(Node node) 
        {
            if (FloatIsUndefined(node.Style.FlexShrink))
            {
                if (node.Config.UseWebDefaults) 
                {
                    return webDefaultFlexShrink;
                }
                return defaultFlexShrink;
            }
            return node.Style.FlexShrink;
        }

        static float nodeResolveFlexShrink(Node node) 
        {
            // Root nodes flexShrink should always be 0
            if (node.Parent == null) 
            {
                return 0;
            }
            if (!FloatIsUndefined(node.Style.FlexShrink)) 
            {
                return node.Style.FlexShrink;
            }
            if (!node.Config.UseWebDefaults && !FloatIsUndefined(node.Style.Flex) &&
                node.Style.Flex < 0)
            {
                return -node.Style.Flex;
            }
            if (node.Config.UseWebDefaults) 
            {
                return webDefaultFlexShrink;
            }
            return defaultFlexShrink;
        }

        static Value nodeResolveFlexBasisPtr(Node node) 
        {
            var style = node.Style;
            if (style.FlexBasis.unit != Unit.Auto && style.FlexBasis.unit != Unit.Undefined) {
                return style.FlexBasis;
            }
            if (!FloatIsUndefined(style.Flex) && style.Flex > 0)
            {
                if (node.Config.UseWebDefaults)
                {
                    return ValueAuto;
                }
                return ValueZero;
            }
            return ValueAuto;
        }

        // // see yoga_props.go

        // var (
        //     currentGenerationCount = 0
        // )
        static int currentGenerationCount = 0;

        // FloatIsUndefined returns true if value is undefined
        static bool FloatIsUndefined(float value) 
        {
            return float.IsNaN(value);
        }

        // ValueEqual returns true if values are equal
        static bool ValueEqual(Value a, Value b) 
        {
            if (a.unit != b.unit) {
                return false;
            }

            if (a.unit == Unit.Undefined) {
                return true;
            }

            return System.Math.Abs(a.value - b.value) < 0.0001f;
        }

        static void resolveDimensions(Node node) 
        {
            for (int dim = (int)Dimension.Width; dim <= (int)Dimension.Height; dim++) {
                if (node.Style.MaxDimensions[dim].unit != Unit.Undefined &&
                    ValueEqual(node.Style.MaxDimensions[dim], node.Style.MinDimensions[dim])) {
                    node.resolvedDimensions[dim] = node.Style.MaxDimensions[dim];
                } else {
                    node.resolvedDimensions[dim] = node.Style.Dimensions[dim];
                }
            }
        }

        // FloatsEqual returns true if floats are approx. equal
        static bool FloatsEqual(float a, float b)  {
            if (FloatIsUndefined(a)) {
                return FloatIsUndefined(b);
            }
            return System.Math.Abs(a-b) < 0.0001f;
        }

        // // see print.go

        // var (
        // )
        readonly static Edge[] leading  = new Edge[4]{Edge.Top, Edge.Bottom, Edge.Left, Edge.Right};
        readonly static Edge[] trailing = new Edge[4]{Edge.Bottom, Edge.Top, Edge.Right, Edge.Left};
        readonly static Edge[] pos      = new Edge[4]{Edge.Top, Edge.Bottom, Edge.Left, Edge.Right};
        readonly static Dimension[] dim      = new Dimension[4]{Dimension.Height, Dimension.Height, Dimension.Width, Dimension.Width};

        static bool flexDirectionIsRow(FlexDirection flexDirection) {
            return flexDirection == FlexDirection.Row || flexDirection == FlexDirection.RowReverse;
        }

        static bool flexDirectionIsColumn(FlexDirection flexDirection) {
            return flexDirection == FlexDirection.Column || flexDirection == FlexDirection.ColumnReverse;
        }

        static float nodeLeadingMargin(Node node, FlexDirection axis, float widthSize) {
            if (flexDirectionIsRow(axis) && node.Style.Margin[(int)Edge.Start].unit != Unit.Undefined) {
                return resolveValueMargin(node.Style.Margin[(int)Edge.Start], widthSize);
            }

            var v = computedEdgeValue(node.Style.Margin, leading[(int)axis], ValueZero);
            return resolveValueMargin(v, widthSize);
        }

        static float nodeTrailingMargin(Node node, FlexDirection axis, float widthSize) {
            if (flexDirectionIsRow(axis) && node.Style.Margin[(int)Edge.End].unit != Unit.Undefined) {
                return resolveValueMargin(node.Style.Margin[(int)Edge.End], widthSize);
            }

            return resolveValueMargin(computedEdgeValue(node.Style.Margin, trailing[(int)axis], ValueZero),
                widthSize);
        }

        static float nodeLeadingPadding(Node node, FlexDirection axis, float widthSize) {
            if (flexDirectionIsRow(axis) && node.Style.Padding[(int)Edge.Start].unit != Unit.Undefined &&
                resolveValue(node.Style.Padding[(int)Edge.Start], widthSize) >= 0 ){
                return resolveValue(node.Style.Padding[(int)Edge.Start], widthSize);
            }

            return System.Math.Max(resolveValue(computedEdgeValue(node.Style.Padding, leading[(int)axis], ValueZero), widthSize), 0);
        }

        static float nodeTrailingPadding(Node node, FlexDirection axis, float widthSize) {
            if (flexDirectionIsRow(axis) && node.Style.Padding[(int)Edge.End].unit != Unit.Undefined &&
                resolveValue(node.Style.Padding[(int)Edge.End], widthSize) >= 0) {
                return resolveValue(node.Style.Padding[(int)Edge.End], widthSize);
            }

            return System.Math.Max(resolveValue(computedEdgeValue(node.Style.Padding, trailing[(int)axis], ValueZero), widthSize), 0);
        }

        static float nodeLeadingBorder(Node node, FlexDirection axis) {
            if (flexDirectionIsRow(axis) && node.Style.Border[(int)Edge.Start].unit != Unit.Undefined &&
                node.Style.Border[(int)Edge.Start].value >= 0) {
                return node.Style.Border[(int)Edge.Start].value;
            }

            return System.Math.Max(computedEdgeValue(node.Style.Border, leading[(int)axis], ValueZero).value, 0);
        }

        static float nodeTrailingBorder(Node node, FlexDirection axis) {
            if (flexDirectionIsRow(axis) && node.Style.Border[(int)Edge.End].unit != Unit.Undefined &&
                node.Style.Border[(int)Edge.End].value >= 0) {
                return node.Style.Border[(int)Edge.End].value;
            }

            return System.Math.Max(computedEdgeValue(node.Style.Border, trailing[(int)axis], ValueZero).value, 0);
        }

        static float nodeLeadingPaddingAndBorder(Node node, FlexDirection axis, float widthSize) {
            return nodeLeadingPadding(node, axis, widthSize) + nodeLeadingBorder(node, axis);
        }

        static float nodeTrailingPaddingAndBorder(Node node, FlexDirection axis, float widthSize) {
            return nodeTrailingPadding(node, axis, widthSize) + nodeTrailingBorder(node, axis);
        }

        static float nodeMarginForAxis(Node node, FlexDirection axis, float widthSize) {
            var leading = nodeLeadingMargin(node, axis, widthSize);
            var trailing = nodeTrailingMargin(node, axis, widthSize);
            return leading + trailing;
        }

        static float nodePaddingAndBorderForAxis(Node node, FlexDirection axis, float widthSize) {
            return nodeLeadingPaddingAndBorder(node, axis, widthSize) +
                nodeTrailingPaddingAndBorder(node, axis, widthSize);
        }

        static Align nodeAlignItem(Node node, Node child) {
            var align = child.Style.AlignSelf;
            if (child.Style.AlignSelf == Align.Auto) {
                align = node.Style.AlignItems;
            }
            if (align == Align.Baseline && flexDirectionIsColumn(node.Style.FlexDirection)) {
                return Align.FlexStart;
            }
            return align;
        }

        static Direction nodeResolveDirection(Node node, Direction parentDirection) {
            if (node.Style.Direction == Direction.Inherit) {
                if (parentDirection > Direction.Inherit) {
                    return parentDirection;
                }
                return Direction.LTR;
            }
            return node.Style.Direction;
        }

        // Baseline retuns baseline
        static float Baseline(Node node) {
            if (node.Baseline != null) {
                var baseline = node.Baseline(node, node.Layout.measuredDimensions[(int)Dimension.Width], node.Layout.measuredDimensions[(int)Dimension.Height]);
                assertWithNode(node, !FloatIsUndefined(baseline), "Expect custom baseline function to not return NaN");
                return baseline;
            } 
            else
            {
                Node baselineChild = null;
                foreach (var child in node.Children) {
                    if (child.lineIndex > 0) {
                        break;
                    }
                    if (child.Style.PositionType == PositionType.Absolute) {
                        continue;
                    }
                    if (nodeAlignItem(node, child) == Align.Baseline) {
                        baselineChild = child;
                        break;
                    }

                    if (baselineChild == null) {
                        baselineChild = child;
                    }
                }

                if (baselineChild == null) {
                    return node.Layout.measuredDimensions[(int)Dimension.Height];
                }

                var baseline = Baseline(baselineChild);
                return baseline + baselineChild.Layout.Position[(int)Edge.Top];
            }

        }

        static FlexDirection resolveFlexDirection(FlexDirection flexDirection, Direction direction) {
            if (direction == Direction.RTL) {
                if (flexDirection == FlexDirection.Row) {
                    return FlexDirection.RowReverse;
                } else if (flexDirection == FlexDirection.RowReverse) {
                    return FlexDirection.Row;
                }
            }
            return flexDirection;
        }

        static FlexDirection flexDirectionCross(FlexDirection flexDirection, Direction direction) {
            if (flexDirectionIsColumn(flexDirection)) {
                return resolveFlexDirection(FlexDirection.Row, direction);
            }
            return FlexDirection.Column;
        }

        static bool nodeIsFlex(Node node) {
            return (node.Style.PositionType == PositionType.Relative &&
                (resolveFlexGrow(node) != 0 || nodeResolveFlexShrink(node) != 0));
        }

        static bool isBaselineLayout(Node node) {
            if (flexDirectionIsColumn(node.Style.FlexDirection)) {
                return false;
            }
            if (node.Style.AlignItems == Align.Baseline) {
                return true;
            }
            foreach (var child in node.Children) {
                if (child.Style.PositionType == PositionType.Relative &&
                    child.Style.AlignSelf == Align.Baseline) {
                    return true;
                }
            }

            return false;
        }

        static float nodeDimWithMargin(Node node, FlexDirection axis, float widthSize) {
            return node.Layout.measuredDimensions[(int)dim[(int)axis]] + nodeLeadingMargin(node, axis, widthSize) +
                nodeTrailingMargin(node, axis, widthSize);
        }

        static bool nodeIsStyleDimDefined(Node node, FlexDirection axis, float parentSize) {
            var v = node.resolvedDimensions[(int)dim[(int)axis]];
            var isNotDefined = (v.unit == Unit.Auto ||
                v.unit == Unit.Undefined ||
                (v.unit == Unit.Point && v.value < 0) ||
                (v.unit == Unit.Percent && (v.value < 0 || FloatIsUndefined(parentSize))));
            return !isNotDefined;
        }

        // static bool nodeIsLayoutDimDefined(Node node, FlexDirection axis) {
        //     var value = node.Layout.measuredDimensions[dim[(int)axis]];
        //     return (!FloatIsUndefined(value) && value >= 0);
        // }

        // static bool nodeIsLeadingPosDefined(Node node, FlexDirection axis) {
        //     return (flexDirectionIsRow(axis) &&
        //         computedEdgeValue(node.Style.Position[:], Edge.Start, &ValueUndefined).Unit !=
        //             Unit.Undefined) ||
        //         computedEdgeValue(node.Style.Position[:], leading[axis], &ValueUndefined).Unit !=
        //             Unit.Undefined
        // }

        // static nodeIsTrailingPosDefined(Node node, FlexDirection axis) bool {
        //     return (flexDirectionIsRow(axis) &&
        //         computedEdgeValue(node.Style.Position[:], Edge.End, &ValueUndefined).Unit !=
        //             Unit.Undefined) ||
        //         computedEdgeValue(node.Style.Position[:], trailing[axis], &ValueUndefined).Unit !=
        //             Unit.Undefined
        // }

        // static nodeLeadingPosition(Node node, FlexDirection axis, axisSize float) float {
        //     if (flexDirectionIsRow(axis) ) {
        //         leadingPosition := computedEdgeValue(node.Style.Position[:], Edge.Start, &ValueUndefined)
        //         if (leadingPosition.Unit != Unit.Undefined ) {
        //             return resolveValue(leadingPosition, axisSize)
        //         }
        //     }

        //     leadingPosition := computedEdgeValue(node.Style.Position[:], leading[axis], &ValueUndefined)

        //     if (leadingPosition.Unit == Unit.Undefined ) {
        //         return 0
        //     }
        //     return resolveValue(leadingPosition, axisSize)
        // }

        // static nodeTrailingPosition(Node node, FlexDirection axis, axisSize float) float {
        //     if (flexDirectionIsRow(axis) ) {
        //         trailingPosition := computedEdgeValue(node.Style.Position[:], Edge.End, &ValueUndefined)
        //         if (trailingPosition.Unit != Unit.Undefined ) {
        //             return resolveValue(trailingPosition, axisSize)
        //         }
        //     }

        //     trailingPosition := computedEdgeValue(node.Style.Position[:], trailing[axis], &ValueUndefined)

        //     if (trailingPosition.Unit == Unit.Undefined ) {
        //         return 0
        //     }
        //     return resolveValue(trailingPosition, axisSize)
        // }

        // static nodeBoundAxisWithinMinAndMax(Node node, FlexDirection axis, value float, axisSize float) float {
        //     var min = Undefined;
        //     var max = Undefined;

        //     if (flexDirectionIsColumn(axis)) {
        //         min = resolveValue(&node.Style.MinDimensions[Dimension.Height], axisSize)
        //         max = resolveValue(&node.Style.MaxDimensions[Dimension.Height], axisSize)
        //     } else if (flexDirectionIsRow(axis) ) {
        //         min = resolveValue(&node.Style.MinDimensions[Dimension.Width], axisSize)
        //         max = resolveValue(&node.Style.MaxDimensions[Dimension.Width], axisSize)
        //     }

        //     boundValue := value

        //     if (!FloatIsUndefined(max) && max >= 0 && boundValue > max ) {
        //         boundValue = max
        //     }

        //     if (!FloatIsUndefined(min) && min >= 0 && boundValue < min ) {
        //         boundValue = min
        //     }

        //     return boundValue
        // }

        // static marginLeadingValue(Node node, FlexDirection axis) *Value {
        //     if (flexDirectionIsRow(axis) && node.Style.Margin[(int)Edge.Start].Unit != Unit.Undefined ) {
        //         return &node.Style.Margin[(int)Edge.Start]
        //     }
        //     return &node.Style.Margin[leading[axis]]
        // }

        // static marginTrailingValue(Node node, FlexDirection axis) *Value {
        //     if (flexDirectionIsRow(axis) && node.Style.Margin[(int)Edge.End].Unit != Unit.Undefined ) {
        //         return &node.Style.Margin[(int)Edge.End]
        //     }
        //     return &node.Style.Margin[trailing[axis]]

        // }

        // // nodeBoundAxis is like nodeBoundAxisWithinMinAndMax but also ensures that
        // // the value doesn't go below the padding and border amount.
        // static nodeBoundAxis(Node node, FlexDirection axis, value float, axisSize float, float widthSize) float {
        //     return System.Math.Max(nodeBoundAxisWithinMinAndMax(node, axis, value, axisSize),
        //         nodePaddingAndBorderForAxis(node, axis, widthSize))
        // }

        // static nodeSetChildTrailingPosition(Node node, Node child, FlexDirection axis) {
        //     size := child.Layout.measuredDimensions[dim[axis]]
        //     child.Layout.Position[trailing[axis]] =
        //         node.Layout.measuredDimensions[dim[axis]] - size - child.Layout.Position[pos[axis]]
        // }

        // // If both left and right are defined, then use left. Otherwise return
        // // +left or -right depending on which is defined.
        // static nodeRelativePosition(Node node, FlexDirection axis, axisSize float) float {
        //     if (nodeIsLeadingPosDefined(node, axis) ) {
        //         return nodeLeadingPosition(node, axis, axisSize)
        //     }
        //     return -nodeTrailingPosition(node, axis, axisSize)
        // }

        // static constrainMaxSizeForMode(Node node, FlexDirection axis, parentAxisSize float, parentWidth float, mode *MeasureMode, size *float) {
        //     maxSize := resolveValue(&node.Style.MaxDimensions[dim[axis]], parentAxisSize) +
        //         nodeMarginForAxis(node, axis, parentWidth)
        //     switch *mode {
        //     case MeasureModeExactly, MeasureModeAtMost:
        //         if (FloatIsUndefined(maxSize) || *size < maxSize ) {
        //             // TODO: this is redundant, but what is in original code
        //             //*size = *size
        //         } else {
        //             *size = maxSize
        //         }
        //     case MeasureModeUndefined:
        //         if (!FloatIsUndefined(maxSize) ) {
        //             *mode = MeasureModeAtMost
        //             *size = maxSize
        //         }
        //     }
        // }

        // static nodeSetPosition(Node node, Direction direction, mainSize float, crossSize float, parentWidth float) {
        //     /* Root nodes should be always layouted as LTR, so we don't return negative values. */
        //     directionRespectingRoot := DirectionLTR
        //     if (node.Parent != null ) {
        //         directionRespectingRoot = direction
        //     }

        //     mainAxis := resolveFlexDirection(node.Style.FlexDirection, directionRespectingRoot)
        //     crossAxis := flexDirectionCross(mainAxis, directionRespectingRoot)

        //     relativePositionMain := nodeRelativePosition(node, mainAxis, mainSize)
        //     relativePositionCross := nodeRelativePosition(node, crossAxis, crossSize)

        //     pos := &node.Layout.Position
        //     pos[leading[mainAxis]] = nodeLeadingMargin(node, mainAxis, parentWidth) + relativePositionMain
        //     pos[trailing[mainAxis]] = nodeTrailingMargin(node, mainAxis, parentWidth) + relativePositionMain
        //     pos[leading[crossAxis]] = nodeLeadingMargin(node, crossAxis, parentWidth) + relativePositionCross
        //     pos[trailing[crossAxis]] = nodeTrailingMargin(node, crossAxis, parentWidth) + relativePositionCross
        // }

        // static nodeComputeFlexBasisForChild(Node node,
        //     Node child,
        //     width float,
        //     widthMode MeasureMode,
        //     height float,
        //     parentWidth float,
        //     parentHeight float,
        //     heightMode MeasureMode,
        //     Direction direction,
        //     config *Config) {
        //     mainAxis := resolveFlexDirection(node.Style.FlexDirection, direction)
        //     isMainAxisRow := flexDirectionIsRow(mainAxis)
        //     mainAxisSize := height
        //     mainAxisParentSize := parentHeight
        //     if (isMainAxisRow ) {
        //         mainAxisSize = width
        //         mainAxisParentSize = parentWidth
        //     }

        //     var childWidth float
        //     var childHeight float
        //     var childWidthMeasureMode MeasureMode
        //     var childHeightMeasureMode MeasureMode

        //     resolvedFlexBasis := resolveValue(nodeResolveFlexBasisPtr(child), mainAxisParentSize)
        //     isRowStyleDimDefined := nodeIsStyleDimDefined(child, FlexDirection.Row, parentWidth)
        //     isColumnStyleDimDefined := nodeIsStyleDimDefined(child, FlexDirection.Column, parentHeight)

        //     if (!FloatIsUndefined(resolvedFlexBasis) && !FloatIsUndefined(mainAxisSize) ) {
        //         if FloatIsUndefined(child.Layout.computedFlexBasis) ||
        //             (child.Config.IsExperimentalFeatureEnabled(ExperimentalFeatureWebFlexBasis) &&
        //                 child.Layout.computedFlexBasisGeneration != currentGenerationCount) {
        //             child.Layout.computedFlexBasis =
        //                 System.Math.Max(resolvedFlexBasis, nodePaddingAndBorderForAxis(child, mainAxis, parentWidth))
        //         }
        //     } else if (isMainAxisRow && isRowStyleDimDefined ) {
        //         // The width is definite, so use that as the flex basis.
        //         child.Layout.computedFlexBasis =
        //             System.Math.Max(resolveValue(child.resolvedDimensions[Dimension.Width], parentWidth),
        //                 nodePaddingAndBorderForAxis(child, FlexDirection.Row, parentWidth))
        //     } else if (!isMainAxisRow && isColumnStyleDimDefined ) {
        //         // The height is definite, so use that as the flex basis.
        //         child.Layout.computedFlexBasis =
        //             System.Math.Max(resolveValue(child.resolvedDimensions[Dimension.Height], parentHeight),
        //                 nodePaddingAndBorderForAxis(child, FlexDirection.Column, parentWidth))
        //     } else {
        //         // Compute the flex basis and hypothetical main size (i.e. the clamped
        //         // flex basis).
        //         childWidth = Undefined
        //         childHeight = Undefined
        //         childWidthMeasureMode = MeasureModeUndefined
        //         childHeightMeasureMode = MeasureModeUndefined

        //         marginRow := nodeMarginForAxis(child, FlexDirection.Row, parentWidth)
        //         marginColumn := nodeMarginForAxis(child, FlexDirection.Column, parentWidth)

        //         if (isRowStyleDimDefined ) {
        //             childWidth =
        //                 resolveValue(child.resolvedDimensions[Dimension.Width], parentWidth) + marginRow
        //             childWidthMeasureMode = MeasureModeExactly
        //         }
        //         if (isColumnStyleDimDefined ) {
        //             childHeight =
        //                 resolveValue(child.resolvedDimensions[Dimension.Height], parentHeight) + marginColumn
        //             childHeightMeasureMode = MeasureModeExactly
        //         }

        //         // The W3C spec doesn't say anything about the 'overflow' property,
        //         // but all major browsers appear to implement the following logic.
        //         if (!isMainAxisRow && node.Style.Overflow == OverflowScroll) ||
        //             node.Style.Overflow != OverflowScroll {
        //             if (FloatIsUndefined(childWidth) && !FloatIsUndefined(width) ) {
        //                 childWidth = width
        //                 childWidthMeasureMode = MeasureModeAtMost
        //             }
        //         }

        //         if (isMainAxisRow && node.Style.Overflow == OverflowScroll) ||
        //             node.Style.Overflow != OverflowScroll {
        //             if (FloatIsUndefined(childHeight) && !FloatIsUndefined(height) ) {
        //                 childHeight = height
        //                 childHeightMeasureMode = MeasureModeAtMost
        //             }
        //         }

        //         // If child has no defined size in the cross axis and is set to stretch,
        //         // set the cross
        //         // axis to be measured exactly with the available inner width
        //         if !isMainAxisRow && !FloatIsUndefined(width) && !isRowStyleDimDefined &&
        //             widthMode == MeasureModeExactly && nodeAlignItem(node, child) == Align.Stretch {
        //             childWidth = width
        //             childWidthMeasureMode = MeasureModeExactly
        //         }
        //         if isMainAxisRow && !FloatIsUndefined(height) && !isColumnStyleDimDefined &&
        //             heightMode == MeasureModeExactly && nodeAlignItem(node, child) == Align.Stretch {
        //             childHeight = height
        //             childHeightMeasureMode = MeasureModeExactly
        //         }

        //         if (!FloatIsUndefined(child.Style.AspectRatio) ) {
        //             if (!isMainAxisRow && childWidthMeasureMode == MeasureModeExactly ) {
        //                 child.Layout.computedFlexBasis =
        //                     System.Math.Max((childWidth-marginRow)/child.Style.AspectRatio,
        //                         nodePaddingAndBorderForAxis(child, FlexDirection.Column, parentWidth))
        //                 return
        //             } else if (isMainAxisRow && childHeightMeasureMode == MeasureModeExactly ) {
        //                 child.Layout.computedFlexBasis =
        //                     System.Math.Max((childHeight-marginColumn)*child.Style.AspectRatio,
        //                         nodePaddingAndBorderForAxis(child, FlexDirection.Row, parentWidth))
        //                 return
        //             }
        //         }

        //         constrainMaxSizeForMode(
        //             child, FlexDirection.Row, parentWidth, parentWidth, &childWidthMeasureMode, &childWidth)
        //         constrainMaxSizeForMode(child,
        //             FlexDirection.Column,
        //             parentHeight,
        //             parentWidth,
        //             &childHeightMeasureMode,
        //             &childHeight)

        //         // Measure the child
        //         layoutNodeInternal(child,
        //             childWidth,
        //             childHeight,
        //             direction,
        //             childWidthMeasureMode,
        //             childHeightMeasureMode,
        //             parentWidth,
        //             parentHeight,
        //             false,
        //             "measure",
        //             config)

        //         child.Layout.computedFlexBasis =
        //             System.Math.Max(child.Layout.measuredDimensions[dim[mainAxis]],
        //                 nodePaddingAndBorderForAxis(child, mainAxis, parentWidth))
        //     }

        //     child.Layout.computedFlexBasisGeneration = currentGenerationCount
        // }

        // static nodeAbsoluteLayoutChild(Node node, Node child, width float, widthMode MeasureMode, height float, Direction direction, config *Config) {
        //     mainAxis := resolveFlexDirection(node.Style.FlexDirection, direction)
        //     crossAxis := flexDirectionCross(mainAxis, direction)
        //     isMainAxisRow := flexDirectionIsRow(mainAxis)

        //     childWidth := Undefined
        //     childHeight := Undefined
        //     childWidthMeasureMode := MeasureModeUndefined
        //     childHeightMeasureMode := MeasureModeUndefined

        //     marginRow := nodeMarginForAxis(child, FlexDirection.Row, width)
        //     marginColumn := nodeMarginForAxis(child, FlexDirection.Column, width)

        //     if (nodeIsStyleDimDefined(child, FlexDirection.Row, width) ) {
        //         childWidth = resolveValue(child.resolvedDimensions[Dimension.Width], width) + marginRow
        //     } else {
        //         // If the child doesn't have a specified width, compute the width based
        //         // on the left/right
        //         // offsets if they're defined.
        //         if nodeIsLeadingPosDefined(child, FlexDirection.Row) &&
        //             nodeIsTrailingPosDefined(child, FlexDirection.Row) {
        //             childWidth = node.Layout.measuredDimensions[Dimension.Width] -
        //                 (nodeLeadingBorder(node, FlexDirection.Row) +
        //                     nodeTrailingBorder(node, FlexDirection.Row)) -
        //                 (nodeLeadingPosition(child, FlexDirection.Row, width) +
        //                     nodeTrailingPosition(child, FlexDirection.Row, width))
        //             childWidth = nodeBoundAxis(child, FlexDirection.Row, childWidth, width, width)
        //         }
        //     }

        //     if (nodeIsStyleDimDefined(child, FlexDirection.Column, height) ) {
        //         childHeight =
        //             resolveValue(child.resolvedDimensions[Dimension.Height], height) + marginColumn
        //     } else {
        //         // If the child doesn't have a specified height, compute the height
        //         // based on the top/bottom
        //         // offsets if they're defined.
        //         if nodeIsLeadingPosDefined(child, FlexDirection.Column) &&
        //             nodeIsTrailingPosDefined(child, FlexDirection.Column) {
        //             childHeight = node.Layout.measuredDimensions[Dimension.Height] -
        //                 (nodeLeadingBorder(node, FlexDirection.Column) +
        //                     nodeTrailingBorder(node, FlexDirection.Column)) -
        //                 (nodeLeadingPosition(child, FlexDirection.Column, height) +
        //                     nodeTrailingPosition(child, FlexDirection.Column, height))
        //             childHeight = nodeBoundAxis(child, FlexDirection.Column, childHeight, height, width)
        //         }
        //     }

        //     // Exactly one dimension needs to be defined for us to be able to do aspect ratio
        //     // calculation. One dimension being the anchor and the other being flexible.
        //     if (FloatIsUndefined(childWidth) != FloatIsUndefined(childHeight) ) {
        //         if (!FloatIsUndefined(child.Style.AspectRatio) ) {
        //             if (FloatIsUndefined(childWidth) ) {
        //                 childWidth =
        //                     marginRow + System.Math.Max((childHeight-marginColumn)*child.Style.AspectRatio,
        //                         nodePaddingAndBorderForAxis(child, FlexDirection.Column, width))
        //             } else if (FloatIsUndefined(childHeight) ) {
        //                 childHeight =
        //                     marginColumn + System.Math.Max((childWidth-marginRow)/child.Style.AspectRatio,
        //                         nodePaddingAndBorderForAxis(child, FlexDirection.Row, width))
        //             }
        //         }
        //     }

        //     // If we're still missing one or the other dimension, measure the content.
        //     if (FloatIsUndefined(childWidth) || FloatIsUndefined(childHeight) ) {
        //         childWidthMeasureMode = MeasureModeExactly
        //         if (FloatIsUndefined(childWidth) ) {
        //             childWidthMeasureMode = MeasureModeUndefined
        //         }
        //         childHeightMeasureMode = MeasureModeExactly
        //         if (FloatIsUndefined(childHeight) ) {
        //             childHeightMeasureMode = MeasureModeUndefined
        //         }

        //         // If the size of the parent is defined then try to rain the absolute child to that size
        //         // as well. This allows text within the absolute child to wrap to the size of its parent.
        //         // This is the same behavior as many browsers implement.
        //         if !isMainAxisRow && FloatIsUndefined(childWidth) && widthMode != MeasureModeUndefined &&
        //             width > 0 {
        //             childWidth = width
        //             childWidthMeasureMode = MeasureModeAtMost
        //         }

        //         layoutNodeInternal(child,
        //             childWidth,
        //             childHeight,
        //             direction,
        //             childWidthMeasureMode,
        //             childHeightMeasureMode,
        //             childWidth,
        //             childHeight,
        //             false,
        //             "abs-measure",
        //             config)
        //         childWidth = child.Layout.measuredDimensions[Dimension.Width] +
        //             nodeMarginForAxis(child, FlexDirection.Row, width)
        //         childHeight = child.Layout.measuredDimensions[Dimension.Height] +
        //             nodeMarginForAxis(child, FlexDirection.Column, width)
        //     }

        //     layoutNodeInternal(child,
        //         childWidth,
        //         childHeight,
        //         direction,
        //         MeasureModeExactly,
        //         MeasureModeExactly,
        //         childWidth,
        //         childHeight,
        //         true,
        //         "abs-layout",
        //         config)

        //     if (nodeIsTrailingPosDefined(child, mainAxis) && !nodeIsLeadingPosDefined(child, mainAxis) ) {
        //         axisSize := height
        //         if (isMainAxisRow ) {
        //             axisSize = width
        //         }
        //         child.Layout.Position[leading[mainAxis]] = node.Layout.measuredDimensions[dim[mainAxis]] -
        //             child.Layout.measuredDimensions[dim[mainAxis]] -
        //             nodeTrailingBorder(node, mainAxis) -
        //             nodeTrailingMargin(child, mainAxis, width) -
        //             nodeTrailingPosition(child, mainAxis, axisSize)
        //     } else if !nodeIsLeadingPosDefined(child, mainAxis) &&
        //         node.Style.JustifyContent == JustifyCenter {
        //         child.Layout.Position[leading[mainAxis]] = (node.Layout.measuredDimensions[dim[mainAxis]] -
        //             child.Layout.measuredDimensions[dim[mainAxis]]) /
        //             2.0
        //     } else if !nodeIsLeadingPosDefined(child, mainAxis) &&
        //         node.Style.JustifyContent == JustifyFlexEnd {
        //         child.Layout.Position[leading[mainAxis]] = (node.Layout.measuredDimensions[dim[mainAxis]] -
        //             child.Layout.measuredDimensions[dim[mainAxis]])
        //     }

        //     if nodeIsTrailingPosDefined(child, crossAxis) &&
        //         !nodeIsLeadingPosDefined(child, crossAxis) {
        //         axisSize := width
        //         if (isMainAxisRow ) {
        //             axisSize = height
        //         }

        //         child.Layout.Position[leading[crossAxis]] = node.Layout.measuredDimensions[dim[crossAxis]] -
        //             child.Layout.measuredDimensions[dim[crossAxis]] -
        //             nodeTrailingBorder(node, crossAxis) -
        //             nodeTrailingMargin(child, crossAxis, width) -
        //             nodeTrailingPosition(child, crossAxis, axisSize)
        //     } else if !nodeIsLeadingPosDefined(child, crossAxis) &&
        //         nodeAlignItem(node, child) == AlignCenter {
        //         child.Layout.Position[leading[crossAxis]] =
        //             (node.Layout.measuredDimensions[dim[crossAxis]] -
        //                 child.Layout.measuredDimensions[dim[crossAxis]]) /
        //                 2.0
        //     } else if !nodeIsLeadingPosDefined(child, crossAxis) &&
        //         ((nodeAlignItem(node, child) == AlignFlexEnd) != (node.Style.FlexWrap == WrapWrapReverse)) {
        //         child.Layout.Position[leading[crossAxis]] = (node.Layout.measuredDimensions[dim[crossAxis]] -
        //             child.Layout.measuredDimensions[dim[crossAxis]])
        //     }
        // }

        // // nodeWithMeasureFuncSetMeasuredDimensions sets measure dimensions for node with measure func
        // static nodeWithMeasureFuncSetMeasuredDimensions(Node node, availableWidth float, availableHeight float, widthMeasureMode MeasureMode, heightMeasureMode MeasureMode, parentWidth float, parentHeight float) {
        //     assertWithNode(node, node.Measure != null, "Expected node to have custom measure function")

        //     paddingAndBorderAxisRow := nodePaddingAndBorderForAxis(node, FlexDirection.Row, availableWidth)
        //     paddingAndBorderAxisColumn := nodePaddingAndBorderForAxis(node, FlexDirection.Column, availableWidth)
        //     marginAxisRow := nodeMarginForAxis(node, FlexDirection.Row, availableWidth)
        //     marginAxisColumn := nodeMarginForAxis(node, FlexDirection.Column, availableWidth)

        //     // We want to make sure we don't call measure with negative size
        //     innerWidth := System.Math.Max(0, availableWidth-marginAxisRow-paddingAndBorderAxisRow)
        //     if (FloatIsUndefined(availableWidth) ) {
        //         innerWidth = availableWidth
        //     }
        //     innerHeight := System.Math.Max(0, availableHeight-marginAxisColumn-paddingAndBorderAxisColumn)
        //     if (FloatIsUndefined(availableHeight) ) {
        //         innerHeight = availableHeight
        //     }

        //     if (widthMeasureMode == MeasureModeExactly && heightMeasureMode == MeasureModeExactly ) {
        //         // Don't bother sizing the text if both dimensions are already defined.
        //         node.Layout.measuredDimensions[Dimension.Width] = nodeBoundAxis(
        //             node, FlexDirection.Row, availableWidth-marginAxisRow, parentWidth, parentWidth)
        //         node.Layout.measuredDimensions[Dimension.Height] = nodeBoundAxis(
        //             node, FlexDirection.Column, availableHeight-marginAxisColumn, parentHeight, parentWidth)
        //     } else {
        //         // Measure the text under the current raints.
        //         measuredSize := node.Measure(node, innerWidth, widthMeasureMode, innerHeight, heightMeasureMode)

        //         width := availableWidth - marginAxisRow
        //         if widthMeasureMode == MeasureModeUndefined ||
        //             widthMeasureMode == MeasureModeAtMost {
        //             width = measuredSize.Width + paddingAndBorderAxisRow

        //         }

        //         node.Layout.measuredDimensions[Dimension.Width] = nodeBoundAxis(node, FlexDirection.Row, width, availableWidth, availableWidth)

        //         height := availableHeight - marginAxisColumn
        //         if heightMeasureMode == MeasureModeUndefined ||
        //             heightMeasureMode == MeasureModeAtMost {
        //             height = measuredSize.Height + paddingAndBorderAxisColumn
        //         }

        //         node.Layout.measuredDimensions[Dimension.Height] = nodeBoundAxis(node, FlexDirection.Column, height, availableHeight, availableWidth)
        //     }
        // }

        // // nodeEmptyContainerSetMeasuredDimensions sets measure dimensions for empty container
        // // For nodes with no children, use the available values if they were provided,
        // // or the minimum size as indicated by the padding and border sizes.
        // static nodeEmptyContainerSetMeasuredDimensions(Node node, availableWidth float, availableHeight float, widthMeasureMode MeasureMode, heightMeasureMode MeasureMode, parentWidth float, parentHeight float) {
        //     paddingAndBorderAxisRow := nodePaddingAndBorderForAxis(node, FlexDirection.Row, parentWidth)
        //     paddingAndBorderAxisColumn := nodePaddingAndBorderForAxis(node, FlexDirection.Column, parentWidth)
        //     marginAxisRow := nodeMarginForAxis(node, FlexDirection.Row, parentWidth)
        //     marginAxisColumn := nodeMarginForAxis(node, FlexDirection.Column, parentWidth)

        //     width := availableWidth - marginAxisRow
        //     if (widthMeasureMode == MeasureModeUndefined || widthMeasureMode == MeasureModeAtMost ) {
        //         width = paddingAndBorderAxisRow
        //     }
        //     node.Layout.measuredDimensions[Dimension.Width] = nodeBoundAxis(node, FlexDirection.Row, width, parentWidth, parentWidth)

        //     height := availableHeight - marginAxisColumn
        //     if (heightMeasureMode == MeasureModeUndefined || heightMeasureMode == MeasureModeAtMost ) {
        //         height = paddingAndBorderAxisColumn
        //     }
        //     node.Layout.measuredDimensions[Dimension.Height] = nodeBoundAxis(node, FlexDirection.Column, height, parentHeight, parentWidth)
        // }

        // static nodeFixedSizeSetMeasuredDimensions(Node node,
        //     availableWidth float,
        //     availableHeight float,
        //     widthMeasureMode MeasureMode,
        //     heightMeasureMode MeasureMode,
        //     parentWidth float,
        //     parentHeight float) bool {
        //     if (widthMeasureMode == MeasureModeAtMost && availableWidth <= 0) ||
        //         (heightMeasureMode == MeasureModeAtMost && availableHeight <= 0) ||
        //         (widthMeasureMode == MeasureModeExactly && heightMeasureMode == MeasureModeExactly) {
        //         marginAxisColumn := nodeMarginForAxis(node, FlexDirection.Column, parentWidth)
        //         marginAxisRow := nodeMarginForAxis(node, FlexDirection.Row, parentWidth)

        //         width := availableWidth - marginAxisRow
        //         if (FloatIsUndefined(availableWidth) || (widthMeasureMode == MeasureModeAtMost && availableWidth < 0) ) {
        //             width = 0
        //         }
        //         node.Layout.measuredDimensions[Dimension.Width] =
        //             nodeBoundAxis(node, FlexDirection.Row, width, parentWidth, parentWidth)

        //         height := availableHeight - marginAxisColumn
        //         if (FloatIsUndefined(availableHeight) || (heightMeasureMode == MeasureModeAtMost && availableHeight < 0) ) {
        //             height = 0
        //         }
        //         node.Layout.measuredDimensions[Dimension.Height] =
        //             nodeBoundAxis(node, FlexDirection.Column, height, parentHeight, parentWidth)

        //         return true
        //     }

        //     return false
        // }

        // // zeroOutLayoutRecursivly zeros out layout recursively
        // static zeroOutLayoutRecursivly(Node node) {
        //     node.Layout.Dimensions[Dimension.Height] = 0
        //     node.Layout.Dimensions[Dimension.Width] = 0
        //     node.Layout.Position[Edge.Top] = 0
        //     node.Layout.Position[Edge.Bottom] = 0
        //     node.Layout.Position[Edge.Left] = 0
        //     node.Layout.Position[Edge.Right] = 0
        //     node.Layout.cachedLayout.availableHeight = 0
        //     node.Layout.cachedLayout.availableWidth = 0
        //     node.Layout.cachedLayout.heightMeasureMode = MeasureModeExactly
        //     node.Layout.cachedLayout.widthMeasureMode = MeasureModeExactly
        //     node.Layout.cachedLayout.computedWidth = 0
        //     node.Layout.cachedLayout.computedHeight = 0
        //     node.hasNewLayout = true
        //     childCount := len(node.Children)
        //     for i := 0; i < childCount; i++ {
        //         child := node.Children[i]
        //         zeroOutLayoutRecursivly(child)
        //     }
        // }

        // // This is the main routine that implements a subset of the flexbox layout
        // // algorithm
        // // described in the W3C YG documentation: https://www.w3.org/TR/YG3-flexbox/.
        // //
        // // Limitations of this algorithm, compared to the full standard:
        // //  * Display property is always assumed to be 'flex' except for Text nodes,
        // //  which
        // //    are assumed to be 'inline-flex'.
        // //  * The 'zIndex' property (or any form of z ordering) is not supported. Nodes
        // //  are
        // //    stacked in document order.
        // //  * The 'order' property is not supported. The order of flex items is always
        // //  defined
        // //    by document order.
        // //  * The 'visibility' property is always assumed to be 'visible'. Values of
        // //  'collapse'
        // //    and 'hidden' are not supported.
        // //  * There is no support for forced breaks.
        // //  * It does not support vertical inline directions (top-to-bottom or
        // //  bottom-to-top text).
        // //
        // // Deviations from standard:
        // //  * Section 4.5 of the spec indicates that all flex items have a default
        // //  minimum
        // //    main size. For text blocks, for example, this is the width of the widest
        // //    word.
        // //    Calculating the minimum width is expensive, so we forego it and assume a
        // //    default
        // //    minimum main size of 0.
        // //  * Min/Max sizes in the main axis are not honored when resolving flexible
        // //  lengths.
        // //  * The spec indicates that the default value for 'flexDirection' is 'row',
        // //  but
        // //    the algorithm below assumes a default of 'column'.
        // //
        // // Input parameters:
        // //    - node: current node to be sized and layed out
        // //    - availableWidth & availableHeight: available size to be used for sizing
        // //    the node
        // //      or Undefined if the size is not available; interpretation depends on
        // //      layout
        // //      flags
        // //    - parentDirection: the inline (text) direction within the parent
        // //    (left-to-right or
        // //      right-to-left)
        // //    - widthMeasureMode: indicates the sizing rules for the width (see below
        // //    for explanation)
        // //    - heightMeasureMode: indicates the sizing rules for the height (see below
        // //    for explanation)
        // //    - performLayout: specifies whether the caller is interested in just the
        // //    dimensions
        // //      of the node or it requires the entire node and its subtree to be layed
        // //      out
        // //      (with final positions)
        // //
        // // Details:
        // //    This routine is called recursively to lay out subtrees of flexbox
        // //    elements. It uses the
        // //    information in node.style, which is treated as a read-only input. It is
        // //    responsible for
        // //    setting the layout.direction and layout.measuredDimensions fields for the
        // //    input node as well
        // //    as the layout.position and layout.lineIndex fields for its child nodes.
        // //    The
        // //    layout.measuredDimensions field includes any border or padding for the
        // //    node but does
        // //    not include margins.
        // //
        // //    The spec describes four different layout modes: "fill available", "max
        // //    content", "min
        // //    content",
        // //    and "fit content". Of these, we don't use "min content" because we don't
        // //    support default
        // //    minimum main sizes (see above for details). Each of our measure modes maps
        // //    to a layout mode
        // //    from the spec (https://www.w3.org/TR/YG3-sizing/#terms):
        // //      - YGMeasureModeUndefined: max content
        // //      - YGMeasureModeExactly: fill available
        // //      - YGMeasureModeAtMost: fit content
        // //
        // //    When calling nodelayoutImpl and YGLayoutNodeInternal, if the caller passes
        // //    an available size of
        // //    undefined then it must also pass a measure mode of YGMeasureModeUndefined
        // //    in that dimension.
        // static nodelayoutImpl(Node node, availableWidth float, availableHeight float,
        //     parentDirection Direction, widthMeasureMode MeasureMode,
        //     heightMeasureMode MeasureMode, parentWidth float, parentHeight float,
        //     performLayout bool, config *Config) {
        //     // assertWithNode(node, YGFloatIsUndefined(availableWidth) ? widthMeasureMode == YGMeasureModeUndefined : true, "availableWidth is indefinite so widthMeasureMode must be YGMeasureModeUndefined");
        //     //assertWithNode(node, YGFloatIsUndefined(availableHeight) ? heightMeasureMode == YGMeasureModeUndefined : true, "availableHeight is indefinite so heightMeasureMode must be YGMeasureModeUndefined");

        //     // Set the resolved resolution in the node's layout.
        //     direction := nodeResolveDirection(node, parentDirection)
        //     node.Layout.Direction = direction

        //     flexRowDirection := resolveFlexDirection(FlexDirection.Row, direction)
        //     flexColumnDirection := resolveFlexDirection(FlexDirection.Column, direction)

        //     node.Layout.Margin[(int)Edge.Start] = nodeLeadingMargin(node, flexRowDirection, parentWidth)
        //     node.Layout.Margin[(int)Edge.End] = nodeTrailingMargin(node, flexRowDirection, parentWidth)
        //     node.Layout.Margin[Edge.Top] = nodeLeadingMargin(node, flexColumnDirection, parentWidth)
        //     node.Layout.Margin[Edge.Bottom] = nodeTrailingMargin(node, flexColumnDirection, parentWidth)

        //     node.Layout.Border[(int)Edge.Start] = nodeLeadingBorder(node, flexRowDirection)
        //     node.Layout.Border[(int)Edge.End] = nodeTrailingBorder(node, flexRowDirection)
        //     node.Layout.Border[Edge.Top] = nodeLeadingBorder(node, flexColumnDirection)
        //     node.Layout.Border[Edge.Bottom] = nodeTrailingBorder(node, flexColumnDirection)

        //     node.Layout.Padding[(int)Edge.Start] = nodeLeadingPadding(node, flexRowDirection, parentWidth)
        //     node.Layout.Padding[(int)Edge.End] = nodeTrailingPadding(node, flexRowDirection, parentWidth)
        //     node.Layout.Padding[Edge.Top] = nodeLeadingPadding(node, flexColumnDirection, parentWidth)
        //     node.Layout.Padding[Edge.Bottom] = nodeTrailingPadding(node, flexColumnDirection, parentWidth)

        //     if (node.Measure != null ) {
        //         nodeWithMeasureFuncSetMeasuredDimensions(node, availableWidth, availableHeight, widthMeasureMode, heightMeasureMode, parentWidth, parentHeight)
        //         return
        //     }

        //     childCount := len(node.Children)
        //     if (childCount == 0 ) {
        //         nodeEmptyContainerSetMeasuredDimensions(node, availableWidth, availableHeight, widthMeasureMode, heightMeasureMode, parentWidth, parentHeight)
        //         return
        //     }

        //     // If we're not being asked to perform a full layout we can skip the algorithm if we already know
        //     // the size
        //     if (!performLayout && nodeFixedSizeSetMeasuredDimensions(node, availableWidth, availableHeight, widthMeasureMode, heightMeasureMode, parentWidth, parentHeight) ) {
        //         return
        //     }

        //     // Reset layout flags, as they could have changed.
        //     node.Layout.HadOverflow = false

        //     // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
        //     mainAxis := resolveFlexDirection(node.Style.FlexDirection, direction)
        //     crossAxis := flexDirectionCross(mainAxis, direction)
        //     isMainAxisRow := flexDirectionIsRow(mainAxis)
        //     justifyContent := node.Style.JustifyContent
        //     isNodeFlexWrap := node.Style.FlexWrap != WrapNoWrap

        //     mainAxisParentSize := parentHeight
        //     crossAxisParentSize := parentWidth
        //     if (isMainAxisRow ) {
        //         mainAxisParentSize = parentWidth
        //         crossAxisParentSize = parentHeight
        //     }

        //     var firstAbsoluteChild *Node
        //     var currentAbsoluteChild *Node

        //     leadingPaddingAndBorderMain := nodeLeadingPaddingAndBorder(node, mainAxis, parentWidth)
        //     trailingPaddingAndBorderMain := nodeTrailingPaddingAndBorder(node, mainAxis, parentWidth)
        //     leadingPaddingAndBorderCross := nodeLeadingPaddingAndBorder(node, crossAxis, parentWidth)
        //     paddingAndBorderAxisMain := nodePaddingAndBorderForAxis(node, mainAxis, parentWidth)
        //     paddingAndBorderAxisCross := nodePaddingAndBorderForAxis(node, crossAxis, parentWidth)

        //     measureModeMainDim := heightMeasureMode
        //     measureModeCrossDim := widthMeasureMode

        //     if (isMainAxisRow ) {
        //         measureModeMainDim = widthMeasureMode
        //         measureModeCrossDim = heightMeasureMode
        //     }

        //     paddingAndBorderAxisRow := paddingAndBorderAxisCross
        //     paddingAndBorderAxisColumn := paddingAndBorderAxisMain
        //     if (isMainAxisRow ) {
        //         paddingAndBorderAxisRow = paddingAndBorderAxisMain
        //         paddingAndBorderAxisColumn = paddingAndBorderAxisCross
        //     }

        //     marginAxisRow := nodeMarginForAxis(node, FlexDirection.Row, parentWidth)
        //     marginAxisColumn := nodeMarginForAxis(node, FlexDirection.Column, parentWidth)

        //     // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS
        //     minInnerWidth := resolveValue(&node.Style.MinDimensions[Dimension.Width], parentWidth) - marginAxisRow -
        //         paddingAndBorderAxisRow
        //     maxInnerWidth := resolveValue(&node.Style.MaxDimensions[Dimension.Width], parentWidth) - marginAxisRow -
        //         paddingAndBorderAxisRow
        //     minInnerHeight := resolveValue(&node.Style.MinDimensions[Dimension.Height], parentHeight) -
        //         marginAxisColumn - paddingAndBorderAxisColumn
        //     maxInnerHeight := resolveValue(&node.Style.MaxDimensions[Dimension.Height], parentHeight) -
        //         marginAxisColumn - paddingAndBorderAxisColumn

        //     minInnerMainDim := minInnerHeight
        //     maxInnerMainDim := maxInnerHeight
        //     if (isMainAxisRow ) {
        //         minInnerMainDim = minInnerWidth
        //         maxInnerMainDim = maxInnerWidth
        //     }

        //     // Max dimension overrides predefined dimension value; Min dimension in turn overrides both of the
        //     // above
        //     availableInnerWidth := availableWidth - marginAxisRow - paddingAndBorderAxisRow
        //     if (!FloatIsUndefined(availableInnerWidth) ) {
        //         // We want to make sure our available width does not violate min and max raints
        //         availableInnerWidth = System.Math.Max(fminf(availableInnerWidth, maxInnerWidth), minInnerWidth)
        //     }

        //     availableInnerHeight := availableHeight - marginAxisColumn - paddingAndBorderAxisColumn
        //     if (!FloatIsUndefined(availableInnerHeight) ) {
        //         // We want to make sure our available height does not violate min and max raints
        //         availableInnerHeight = System.Math.Max(fminf(availableInnerHeight, maxInnerHeight), minInnerHeight)
        //     }

        //     availableInnerMainDim := availableInnerHeight
        //     availableInnerCrossDim := availableInnerWidth
        //     if (isMainAxisRow ) {
        //         availableInnerMainDim = availableInnerWidth
        //         availableInnerCrossDim = availableInnerHeight
        //     }

        //     // If there is only one child with flexGrow + flexShrink it means we can set the
        //     // computedFlexBasis to 0 instead of measuring and shrinking / flexing the child to exactly
        //     // match the remaining space
        //     var singleFlexChild *Node
        //     if (measureModeMainDim == MeasureModeExactly ) {
        //         for i := 0; i < childCount; i++ {
        //             child := node.GetChild(i)
        //             if (singleFlexChild != null ) {
        //                 if (nodeIsFlex(child) ) {
        //                     // There is already a flexible child, abort.
        //                     singleFlexChild = null
        //                     break
        //                 }
        //             } else if (resolveFlexGrow(child) > 0 && nodeResolveFlexShrink(child) > 0 ) {
        //                 singleFlexChild = child
        //             }
        //         }
        //     }

        //     var totalOuterFlexBasis float

        //     // STEP 3: DETERMINE FLEX BASIS FOR EACH ITEM
        //     for i := 0; i < childCount; i++ {
        //         child := node.Children[i]
        //         if (child.Style.Display == DisplayNone ) {
        //             zeroOutLayoutRecursivly(child)
        //             child.hasNewLayout = true
        //             child.IsDirty = false
        //             continue
        //         }
        //         resolveDimensions(child)
        //         if (performLayout ) {
        //             // Set the initial position (relative to the parent).
        //             childDirection := nodeResolveDirection(child, direction)
        //             nodeSetPosition(child,
        //                 childDirection,
        //                 availableInnerMainDim,
        //                 availableInnerCrossDim,
        //                 availableInnerWidth)
        //         }

        //         // Absolute-positioned children don't participate in flex layout. Add them
        //         // to a list that we can process later.
        //         if (child.Style.PositionType == PositionType.Absolute ) {
        //             // Store a private linked list of absolutely positioned children
        //             // so that we can efficiently traverse them later.
        //             if (firstAbsoluteChild == null ) {
        //                 firstAbsoluteChild = child
        //             }
        //             if (currentAbsoluteChild != null ) {
        //                 currentAbsoluteChild.NextChild = child
        //             }
        //             currentAbsoluteChild = child
        //             child.NextChild = null
        //         } else {
        //             if (child == singleFlexChild ) {
        //                 child.Layout.computedFlexBasisGeneration = currentGenerationCount
        //                 child.Layout.computedFlexBasis = 0
        //             } else {
        //                 nodeComputeFlexBasisForChild(node,
        //                     child,
        //                     availableInnerWidth,
        //                     widthMeasureMode,
        //                     availableInnerHeight,
        //                     availableInnerWidth,
        //                     availableInnerHeight,
        //                     heightMeasureMode,
        //                     direction,
        //                     config)
        //             }
        //         }

        //         totalOuterFlexBasis +=
        //             child.Layout.computedFlexBasis + nodeMarginForAxis(child, mainAxis, availableInnerWidth)

        //     }

        //     flexBasisOverflows := totalOuterFlexBasis > availableInnerMainDim
        //     if (measureModeMainDim == MeasureModeUndefined ) {
        //         flexBasisOverflows = false
        //     }
        //     if (isNodeFlexWrap && flexBasisOverflows && measureModeMainDim == MeasureModeAtMost ) {
        //         measureModeMainDim = MeasureModeExactly
        //     }

        //     // STEP 4: COLLECT FLEX ITEMS INTO FLEX LINES

        //     // Indexes of children that represent the first and last items in the line.
        //     startOfLineIndex := 0
        //     endOfLineIndex := 0

        //     // Number of lines.
        //     lineCount := 0

        //     // Accumulated cross dimensions of all lines so far.
        //     var totalLineCrossDim float

        //     // Max main dimension of all the lines.
        //     var maxLineMainDim float

        //     for endOfLineIndex < childCount {
        //         // Number of items on the currently line. May be different than the
        //         // difference
        //         // between start and end indicates because we skip over absolute-positioned
        //         // items.
        //         itemsOnLine := 0

        //         // sizeConsumedOnCurrentLine is accumulation of the dimensions and margin
        //         // of all the children on the current line. This will be used in order to
        //         // either set the dimensions of the node if none already exist or to compute
        //         // the remaining space left for the flexible children.
        //         var sizeConsumedOnCurrentLine float
        //         var sizeConsumedOnCurrentLineIncludingMinConstraint float

        //         var totalFlexGrowFactors float
        //         var totalFlexShrinkScaledFactors float

        //         // Maintain a linked list of the child nodes that can shrink and/or grow.
        //         var firstRelativeChild *Node
        //         var currentRelativeChild *Node

        //         // Add items to the current line until it's full or we run out of items.
        //         for i := startOfLineIndex; i < childCount; i++ {
        //             child := node.Children[i]
        //             if (child.Style.Display == DisplayNone ) {
        //                 endOfLineIndex++
        //                 continue
        //             }
        //             child.lineIndex = lineCount

        //             if (child.Style.PositionType != PositionType.Absolute ) {
        //                 childMarginMainAxis := nodeMarginForAxis(child, mainAxis, availableInnerWidth)
        //                 flexBasisWithMaxConstraints := fminf(resolveValue(&child.Style.MaxDimensions[dim[mainAxis]], mainAxisParentSize), child.Layout.computedFlexBasis)
        //                 flexBasisWithMinAndMaxConstraints := System.Math.Max(resolveValue(&child.Style.MinDimensions[dim[mainAxis]], mainAxisParentSize), flexBasisWithMaxConstraints)

        //                 // If this is a multi-line flow and this item pushes us over the
        //                 // available size, we've
        //                 // hit the end of the current line. Break out of the loop and lay out
        //                 // the current line.
        //                 if sizeConsumedOnCurrentLineIncludingMinConstraint+flexBasisWithMinAndMaxConstraints+
        //                     childMarginMainAxis >
        //                     availableInnerMainDim &&
        //                     isNodeFlexWrap && itemsOnLine > 0 {
        //                     break
        //                 }

        //                 sizeConsumedOnCurrentLineIncludingMinConstraint +=
        //                     flexBasisWithMinAndMaxConstraints + childMarginMainAxis
        //                 sizeConsumedOnCurrentLine += flexBasisWithMinAndMaxConstraints + childMarginMainAxis
        //                 itemsOnLine++

        //                 if (nodeIsFlex(child) ) {
        //                     totalFlexGrowFactors += resolveFlexGrow(child)

        //                     // Unlike the grow factor, the shrink factor is scaled relative to the child dimension.
        //                     totalFlexShrinkScaledFactors +=
        //                         -nodeResolveFlexShrink(child) * child.Layout.computedFlexBasis
        //                 }

        //                 // Store a private linked list of children that need to be layed out.
        //                 if (firstRelativeChild == null ) {
        //                     firstRelativeChild = child
        //                 }
        //                 if (currentRelativeChild != null ) {
        //                     currentRelativeChild.NextChild = child
        //                 }
        //                 currentRelativeChild = child
        //                 child.NextChild = null
        //             }
        //             endOfLineIndex++
        //         }

        //         // The total flex factor needs to be floored to 1.
        //         if (totalFlexGrowFactors > 0 && totalFlexGrowFactors < 1 ) {
        //             totalFlexGrowFactors = 1
        //         }

        //         // The total flex shrink factor needs to be floored to 1.
        //         if (totalFlexShrinkScaledFactors > 0 && totalFlexShrinkScaledFactors < 1 ) {
        //             totalFlexShrinkScaledFactors = 1
        //         }

        //         // If we don't need to measure the cross axis, we can skip the entire flex
        //         // step.
        //         canSkipFlex := !performLayout && measureModeCrossDim == MeasureModeExactly

        //         // In order to position the elements in the main axis, we have two
        //         // controls. The space between the beginning and the first element
        //         // and the space between each two elements.
        //         var leadingMainDim float
        //         var betweenMainDim float

        //         // STEP 5: RESOLVING FLEXIBLE LENGTHS ON MAIN AXIS
        //         // Calculate the remaining available space that needs to be allocated.
        //         // If the main dimension size isn't known, it is computed based on
        //         // the line length, so there's no more space left to distribute.

        //         // If we don't measure with exact main dimension we want to ensure we don't violate min and max
        //         if (measureModeMainDim != MeasureModeExactly ) {
        //             if (!FloatIsUndefined(minInnerMainDim) && sizeConsumedOnCurrentLine < minInnerMainDim ) {
        //                 availableInnerMainDim = minInnerMainDim
        //             } else if !FloatIsUndefined(maxInnerMainDim) &&
        //                 sizeConsumedOnCurrentLine > maxInnerMainDim {
        //                 availableInnerMainDim = maxInnerMainDim
        //             } else {
        //                 if !node.Config.UseLegacyStretchBehaviour &&
        //                     (totalFlexGrowFactors == 0 || resolveFlexGrow(node) == 0) {
        //                     // If we don't have any children to flex or we can't flex the node itself,
        //                     // space we've used is all space we need. Root node also should be shrunk to minimum
        //                     availableInnerMainDim = sizeConsumedOnCurrentLine
        //                 }
        //             }
        //         }

        //         var remainingFreeSpace float
        //         if (!FloatIsUndefined(availableInnerMainDim) ) {
        //             remainingFreeSpace = availableInnerMainDim - sizeConsumedOnCurrentLine
        //         } else if (sizeConsumedOnCurrentLine < 0 ) {
        //             // availableInnerMainDim is indefinite which means the node is being sized based on its
        //             // content.
        //             // sizeConsumedOnCurrentLine is negative which means the node will allocate 0 points for
        //             // its content. Consequently, remainingFreeSpace is 0 - sizeConsumedOnCurrentLine.
        //             remainingFreeSpace = -sizeConsumedOnCurrentLine
        //         }

        //         originalRemainingFreeSpace := remainingFreeSpace
        //         var deltaFreeSpace float

        //         if (!canSkipFlex ) {
        //             var childFlexBasis float
        //             var flexShrinkScaledFactor float
        //             var flexGrowFactor float
        //             var baseMainSize float
        //             var boundMainSize float

        //             // Do two passes over the flex items to figure out how to distribute the
        //             // remaining space.
        //             // The first pass finds the items whose min/max raints trigger,
        //             // freezes them at those
        //             // sizes, and excludes those sizes from the remaining space. The second
        //             // pass sets the size
        //             // of each flexible item. It distributes the remaining space amongst the
        //             // items whose min/max
        //             // raints didn't trigger in pass 1. For the other items, it sets
        //             // their sizes by forcing
        //             // their min/max raints to trigger again.
        //             //
        //             // This two pass approach for resolving min/max raints deviates from
        //             // the spec. The
        //             // spec (https://www.w3.org/TR/YG-flexbox-1/#resolve-flexible-lengths)
        //             // describes a process
        //             // that needs to be repeated a variable number of times. The algorithm
        //             // implemented here
        //             // won't handle all cases but it was simpler to implement and it mitigates
        //             // performance
        //             // concerns because we know exactly how many passes it'll do.

        //             // First pass: detect the flex items whose min/max raints trigger
        //             var deltaFlexShrinkScaledFactors float
        //             var deltaFlexGrowFactors float
        //             currentRelativeChild = firstRelativeChild
        //             for currentRelativeChild != null {
        //                 childFlexBasis =
        //                     fminf(resolveValue(&currentRelativeChild.Style.MaxDimensions[dim[mainAxis]],
        //                         mainAxisParentSize),
        //                         System.Math.Max(resolveValue(&currentRelativeChild.Style.MinDimensions[dim[mainAxis]],
        //                             mainAxisParentSize),
        //                             currentRelativeChild.Layout.computedFlexBasis))

        //                 if (remainingFreeSpace < 0 ) {
        //                     flexShrinkScaledFactor = -nodeResolveFlexShrink(currentRelativeChild) * childFlexBasis

        //                     // Is this child able to shrink?
        //                     if (flexShrinkScaledFactor != 0 ) {
        //                         baseMainSize =
        //                             childFlexBasis +
        //                                 remainingFreeSpace/totalFlexShrinkScaledFactors*flexShrinkScaledFactor
        //                         boundMainSize = nodeBoundAxis(currentRelativeChild,
        //                             mainAxis,
        //                             baseMainSize,
        //                             availableInnerMainDim,
        //                             availableInnerWidth)
        //                         if (baseMainSize != boundMainSize ) {
        //                             // By excluding this item's size and flex factor from remaining,
        //                             // this item's
        //                             // min/max raints should also trigger in the second pass
        //                             // resulting in the
        //                             // item's size calculation being identical in the first and second
        //                             // passes.
        //                             deltaFreeSpace -= boundMainSize - childFlexBasis
        //                             deltaFlexShrinkScaledFactors -= flexShrinkScaledFactor
        //                         }
        //                     }
        //                 } else if (remainingFreeSpace > 0 ) {
        //                     flexGrowFactor = resolveFlexGrow(currentRelativeChild)

        //                     // Is this child able to grow?
        //                     if (flexGrowFactor != 0 ) {
        //                         baseMainSize =
        //                             childFlexBasis + remainingFreeSpace/totalFlexGrowFactors*flexGrowFactor
        //                         boundMainSize = nodeBoundAxis(currentRelativeChild,
        //                             mainAxis,
        //                             baseMainSize,
        //                             availableInnerMainDim,
        //                             availableInnerWidth)

        //                         if (baseMainSize != boundMainSize ) {
        //                             // By excluding this item's size and flex factor from remaining,
        //                             // this item's
        //                             // min/max raints should also trigger in the second pass
        //                             // resulting in the
        //                             // item's size calculation being identical in the first and second
        //                             // passes.
        //                             deltaFreeSpace -= boundMainSize - childFlexBasis
        //                             deltaFlexGrowFactors -= flexGrowFactor
        //                         }
        //                     }
        //                 }

        //                 currentRelativeChild = currentRelativeChild.NextChild
        //             }

        //             totalFlexShrinkScaledFactors += deltaFlexShrinkScaledFactors
        //             totalFlexGrowFactors += deltaFlexGrowFactors
        //             remainingFreeSpace += deltaFreeSpace

        //             // Second pass: resolve the sizes of the flexible items
        //             deltaFreeSpace = 0
        //             currentRelativeChild = firstRelativeChild
        //             for currentRelativeChild != null {
        //                 childFlexBasis =
        //                     fminf(resolveValue(&currentRelativeChild.Style.MaxDimensions[dim[mainAxis]],
        //                         mainAxisParentSize),
        //                         System.Math.Max(resolveValue(&currentRelativeChild.Style.MinDimensions[dim[mainAxis]],
        //                             mainAxisParentSize),
        //                             currentRelativeChild.Layout.computedFlexBasis))
        //                 updatedMainSize := childFlexBasis

        //                 if (remainingFreeSpace < 0 ) {
        //                     flexShrinkScaledFactor = -nodeResolveFlexShrink(currentRelativeChild) * childFlexBasis
        //                     // Is this child able to shrink?
        //                     if (flexShrinkScaledFactor != 0 ) {
        //                         var childSize float

        //                         if (totalFlexShrinkScaledFactors == 0 ) {
        //                             childSize = childFlexBasis + flexShrinkScaledFactor
        //                         } else {
        //                             childSize =
        //                                 childFlexBasis +
        //                                     (remainingFreeSpace/totalFlexShrinkScaledFactors)*flexShrinkScaledFactor
        //                         }

        //                         updatedMainSize = nodeBoundAxis(currentRelativeChild,
        //                             mainAxis,
        //                             childSize,
        //                             availableInnerMainDim,
        //                             availableInnerWidth)
        //                     }
        //                 } else if (remainingFreeSpace > 0 ) {
        //                     flexGrowFactor = resolveFlexGrow(currentRelativeChild)

        //                     // Is this child able to grow?
        //                     if (flexGrowFactor != 0 ) {
        //                         updatedMainSize =
        //                             nodeBoundAxis(currentRelativeChild,
        //                                 mainAxis,
        //                                 childFlexBasis+
        //                                     remainingFreeSpace/totalFlexGrowFactors*flexGrowFactor,
        //                                 availableInnerMainDim,
        //                                 availableInnerWidth)
        //                     }
        //                 }

        //                 deltaFreeSpace -= updatedMainSize - childFlexBasis

        //                 marginMain := nodeMarginForAxis(currentRelativeChild, mainAxis, availableInnerWidth)
        //                 marginCross := nodeMarginForAxis(currentRelativeChild, crossAxis, availableInnerWidth)

        //                 var childCrossSize float
        //                 childMainSize := updatedMainSize + marginMain
        //                 var childCrossMeasureMode MeasureMode
        //                 childMainMeasureMode := MeasureModeExactly

        //                 if !FloatIsUndefined(availableInnerCrossDim) &&
        //                     !nodeIsStyleDimDefined(currentRelativeChild, crossAxis, availableInnerCrossDim) &&
        //                     measureModeCrossDim == MeasureModeExactly &&
        //                     !(isNodeFlexWrap && flexBasisOverflows) &&
        //                     nodeAlignItem(node, currentRelativeChild) == Align.Stretch {
        //                     childCrossSize = availableInnerCrossDim
        //                     childCrossMeasureMode = MeasureModeExactly
        //                 } else if !nodeIsStyleDimDefined(currentRelativeChild,
        //                     crossAxis,
        //                     availableInnerCrossDim) {
        //                     childCrossSize = availableInnerCrossDim
        //                     childCrossMeasureMode = MeasureModeAtMost
        //                     if (FloatIsUndefined(childCrossSize) ) {
        //                         childCrossMeasureMode = MeasureModeUndefined
        //                     }
        //                 } else {
        //                     childCrossSize = resolveValue(currentRelativeChild.resolvedDimensions[dim[crossAxis]],
        //                         availableInnerCrossDim) +
        //                         marginCross
        //                     isLoosePercentageMeasurement := currentRelativeChild.resolvedDimensions[dim[crossAxis]].Unit == UnitPercent &&
        //                         measureModeCrossDim != MeasureModeExactly
        //                     childCrossMeasureMode = MeasureModeExactly
        //                     if (FloatIsUndefined(childCrossSize) || isLoosePercentageMeasurement ) {
        //                         childCrossMeasureMode = MeasureModeUndefined
        //                     }
        //                 }

        //                 if (!FloatIsUndefined(currentRelativeChild.Style.AspectRatio) ) {
        //                     v := (childMainSize - marginMain) * currentRelativeChild.Style.AspectRatio
        //                     if (isMainAxisRow ) {
        //                         v = (childMainSize - marginMain) / currentRelativeChild.Style.AspectRatio
        //                     }
        //                     childCrossSize = System.Math.Max(v, nodePaddingAndBorderForAxis(currentRelativeChild, crossAxis, availableInnerWidth))
        //                     childCrossMeasureMode = MeasureModeExactly

        //                     // Parent size raint should have higher priority than flex
        //                     if (nodeIsFlex(currentRelativeChild) ) {
        //                         childCrossSize = fminf(childCrossSize-marginCross, availableInnerCrossDim)
        //                         childMainSize = marginMain
        //                         if (isMainAxisRow ) {
        //                             childMainSize += childCrossSize * currentRelativeChild.Style.AspectRatio
        //                         } else {
        //                             childMainSize += childCrossSize / currentRelativeChild.Style.AspectRatio
        //                         }
        //                     }

        //                     childCrossSize += marginCross
        //                 }

        //                 constrainMaxSizeForMode(currentRelativeChild,
        //                     mainAxis,
        //                     availableInnerMainDim,
        //                     availableInnerWidth,
        //                     &childMainMeasureMode,
        //                     &childMainSize)
        //                 constrainMaxSizeForMode(currentRelativeChild,
        //                     crossAxis,
        //                     availableInnerCrossDim,
        //                     availableInnerWidth,
        //                     &childCrossMeasureMode,
        //                     &childCrossSize)

        //                 requiresStretchLayout := !nodeIsStyleDimDefined(currentRelativeChild, crossAxis, availableInnerCrossDim) &&
        //                     nodeAlignItem(node, currentRelativeChild) == Align.Stretch

        //                 childWidth := childCrossSize
        //                 if (isMainAxisRow ) {
        //                     childWidth = childMainSize
        //                 }
        //                 childHeight := childCrossSize
        //                 if (!isMainAxisRow ) {
        //                     childHeight = childMainSize
        //                 }

        //                 childWidthMeasureMode := childCrossMeasureMode
        //                 if (isMainAxisRow ) {
        //                     childWidthMeasureMode = childMainMeasureMode
        //                 }
        //                 childHeightMeasureMode := childCrossMeasureMode
        //                 if (!isMainAxisRow ) {
        //                     childHeightMeasureMode = childMainMeasureMode
        //                 }

        //                 // Recursively call the layout algorithm for this child with the updated
        //                 // main size.
        //                 layoutNodeInternal(currentRelativeChild,
        //                     childWidth,
        //                     childHeight,
        //                     direction,
        //                     childWidthMeasureMode,
        //                     childHeightMeasureMode,
        //                     availableInnerWidth,
        //                     availableInnerHeight,
        //                     performLayout && !requiresStretchLayout,
        //                     "flex",
        //                     config)
        //                 if (currentRelativeChild.Layout.HadOverflow ) {
        //                     node.Layout.HadOverflow = true
        //                 }

        //                 currentRelativeChild = currentRelativeChild.NextChild
        //             }
        //         }

        //         remainingFreeSpace = originalRemainingFreeSpace + deltaFreeSpace
        //         if (remainingFreeSpace < 0 ) {
        //             node.Layout.HadOverflow = true
        //         }

        //         // STEP 6: MAIN-AXIS JUSTIFICATION & CROSS-AXIS SIZE DETERMINATION

        //         // At this point, all the children have their dimensions set in the main
        //         // axis.
        //         // Their dimensions are also set in the cross axis with the exception of
        //         // items
        //         // that are aligned "stretch". We need to compute these stretch values and
        //         // set the final positions.

        //         // If we are using "at most" rules in the main axis. Calculate the remaining space when
        //         // raint by the min size defined for the main axis.

        //         if (measureModeMainDim == MeasureModeAtMost && remainingFreeSpace > 0 ) {
        //             if node.Style.MinDimensions[dim[mainAxis]].Unit != Unit.Undefined &&
        //                 resolveValue(&node.Style.MinDimensions[dim[mainAxis]], mainAxisParentSize) >= 0 {
        //                 remainingFreeSpace =
        //                     System.Math.Max(0,
        //                         resolveValue(&node.Style.MinDimensions[dim[mainAxis]], mainAxisParentSize)-
        //                             (availableInnerMainDim-remainingFreeSpace))
        //             } else {
        //                 remainingFreeSpace = 0
        //             }
        //         }

        //         numberOfAutoMarginsOnCurrentLine := 0
        //         for i := startOfLineIndex; i < endOfLineIndex; i++ {
        //             child := node.Children[i]
        //             if (child.Style.PositionType == PositionType.Relative ) {
        //                 if (marginLeadingValue(child, mainAxis).Unit == UnitAuto ) {
        //                     numberOfAutoMarginsOnCurrentLine++
        //                 }
        //                 if (marginTrailingValue(child, mainAxis).Unit == UnitAuto ) {
        //                     numberOfAutoMarginsOnCurrentLine++
        //                 }
        //             }
        //         }

        //         if (numberOfAutoMarginsOnCurrentLine == 0 ) {
        //             switch justifyContent {
        //             case JustifyCenter:
        //                 leadingMainDim = remainingFreeSpace / 2
        //             case JustifyFlexEnd:
        //                 leadingMainDim = remainingFreeSpace
        //             case JustifySpaceBetween:
        //                 if (itemsOnLine > 1 ) {
        //                     betweenMainDim = System.Math.Max(remainingFreeSpace, 0) / float(itemsOnLine-1)
        //                 } else {
        //                     betweenMainDim = 0
        //                 }
        //             case JustifySpaceAround:
        //                 // Space on the edges is half of the space between elements
        //                 betweenMainDim = remainingFreeSpace / float(itemsOnLine)
        //                 leadingMainDim = betweenMainDim / 2
        //             case Justify.FlexStart:
        //             }
        //         }

        //         mainDim := leadingPaddingAndBorderMain + leadingMainDim
        //         var crossDim float

        //         for i := startOfLineIndex; i < endOfLineIndex; i++ {
        //             child := node.Children[i]
        //             if (child.Style.Display == DisplayNone ) {
        //                 continue
        //             }
        //             if child.Style.PositionType == PositionType.Absolute &&
        //                 nodeIsLeadingPosDefined(child, mainAxis) {
        //                 if (performLayout ) {
        //                     // In case the child is position absolute and has left/top being
        //                     // defined, we override the position to whatever the user said
        //                     // (and margin/border).
        //                     child.Layout.Position[pos[mainAxis]] =
        //                         nodeLeadingPosition(child, mainAxis, availableInnerMainDim) +
        //                             nodeLeadingBorder(node, mainAxis) +
        //                             nodeLeadingMargin(child, mainAxis, availableInnerWidth)
        //                 }
        //             } else {
        //                 // Now that we placed the element, we need to update the variables.
        //                 // We need to do that only for relative elements. Absolute elements
        //                 // do not take part in that phase.
        //                 if (child.Style.PositionType == PositionType.Relative ) {
        //                     if (marginLeadingValue(child, mainAxis).Unit == UnitAuto ) {
        //                         mainDim += remainingFreeSpace / float(numberOfAutoMarginsOnCurrentLine)
        //                     }

        //                     if (performLayout ) {
        //                         child.Layout.Position[pos[mainAxis]] += mainDim
        //                     }

        //                     if (marginTrailingValue(child, mainAxis).Unit == UnitAuto ) {
        //                         mainDim += remainingFreeSpace / float(numberOfAutoMarginsOnCurrentLine)
        //                     }

        //                     if (canSkipFlex ) {
        //                         // If we skipped the flex step, then we can't rely on the
        //                         // measuredDims because
        //                         // they weren't computed. This means we can't call YGNodeDimWithMargin.
        //                         mainDim += betweenMainDim + nodeMarginForAxis(child, mainAxis, availableInnerWidth) +
        //                             child.Layout.computedFlexBasis
        //                         crossDim = availableInnerCrossDim
        //                     } else {
        //                         // The main dimension is the sum of all the elements dimension plus the spacing.
        //                         mainDim += betweenMainDim + nodeDimWithMargin(child, mainAxis, availableInnerWidth)

        //                         // The cross dimension is the max of the elements dimension since
        //                         // there can only be one element in that cross dimension.
        //                         crossDim = System.Math.Max(crossDim, nodeDimWithMargin(child, crossAxis, availableInnerWidth))
        //                     }
        //                 } else if (performLayout ) {
        //                     child.Layout.Position[pos[mainAxis]] +=
        //                         nodeLeadingBorder(node, mainAxis) + leadingMainDim
        //                 }
        //             }
        //         }

        //         mainDim += trailingPaddingAndBorderMain

        //         containerCrossAxis := availableInnerCrossDim
        //         if measureModeCrossDim == MeasureModeUndefined ||
        //             measureModeCrossDim == MeasureModeAtMost {
        //             // Compute the cross axis from the max cross dimension of the children.
        //             containerCrossAxis = nodeBoundAxis(node,
        //                 crossAxis,
        //                 crossDim+paddingAndBorderAxisCross,
        //                 crossAxisParentSize,
        //                 parentWidth) -
        //                 paddingAndBorderAxisCross
        //         }

        //         // If there's no flex wrap, the cross dimension is defined by the container.
        //         if (!isNodeFlexWrap && measureModeCrossDim == MeasureModeExactly ) {
        //             crossDim = availableInnerCrossDim
        //         }

        //         // Clamp to the min/max size specified on the container.
        //         crossDim = nodeBoundAxis(node,
        //             crossAxis,
        //             crossDim+paddingAndBorderAxisCross,
        //             crossAxisParentSize,
        //             parentWidth) -
        //             paddingAndBorderAxisCross

        //         // STEP 7: CROSS-AXIS ALIGNMENT
        //         // We can skip child alignment if we're just measuring the container.
        //         if (performLayout ) {
        //             for i := startOfLineIndex; i < endOfLineIndex; i++ {
        //                 child := node.Children[i]
        //                 if (child.Style.Display == DisplayNone ) {
        //                     continue
        //                 }
        //                 if (child.Style.PositionType == PositionType.Absolute ) {
        //                     // If the child is absolutely positioned and has a
        //                     // top/left/bottom/right
        //                     // set, override all the previously computed positions to set it
        //                     // correctly.
        //                     if (nodeIsLeadingPosDefined(child, crossAxis) ) {
        //                         child.Layout.Position[pos[crossAxis]] =
        //                             nodeLeadingPosition(child, crossAxis, availableInnerCrossDim) +
        //                                 nodeLeadingBorder(node, crossAxis) +
        //                                 nodeLeadingMargin(child, crossAxis, availableInnerWidth)
        //                     } else {
        //                         child.Layout.Position[pos[crossAxis]] =
        //                             nodeLeadingBorder(node, crossAxis) +
        //                                 nodeLeadingMargin(child, crossAxis, availableInnerWidth)
        //                     }
        //                 } else {
        //                     leadingCrossDim := leadingPaddingAndBorderCross

        //                     // For a relative children, we're either using alignItems (parent) or
        //                     // alignSelf (child) in order to determine the position in the cross
        //                     // axis
        //                     alignItem := nodeAlignItem(node, child)

        //                     // If the child uses align stretch, we need to lay it out one more
        //                     // time, this time
        //                     // forcing the cross-axis size to be the computed cross size for the
        //                     // current line.
        //                     if alignItem == Align.Stretch &&
        //                         marginLeadingValue(child, crossAxis).Unit != UnitAuto &&
        //                         marginTrailingValue(child, crossAxis).Unit != UnitAuto {
        //                         // If the child defines a definite size for its cross axis, there's
        //                         // no need to stretch.
        //                         if (!nodeIsStyleDimDefined(child, crossAxis, availableInnerCrossDim) ) {
        //                             childMainSize := child.Layout.measuredDimensions[dim[mainAxis]]
        //                             childCrossSize := crossDim
        //                             if (!FloatIsUndefined(child.Style.AspectRatio) ) {
        //                                 childCrossSize = nodeMarginForAxis(child, crossAxis, availableInnerWidth)
        //                                 if (isMainAxisRow ) {
        //                                     childCrossSize += childMainSize / child.Style.AspectRatio
        //                                 } else {
        //                                     childCrossSize += childMainSize * child.Style.AspectRatio
        //                                 }
        //                             }

        //                             childMainSize += nodeMarginForAxis(child, mainAxis, availableInnerWidth)

        //                             childMainMeasureMode := MeasureModeExactly
        //                             childCrossMeasureMode := MeasureModeExactly
        //                             constrainMaxSizeForMode(child,
        //                                 mainAxis,
        //                                 availableInnerMainDim,
        //                                 availableInnerWidth,
        //                                 &childMainMeasureMode,
        //                                 &childMainSize)
        //                             constrainMaxSizeForMode(child,
        //                                 crossAxis,
        //                                 availableInnerCrossDim,
        //                                 availableInnerWidth,
        //                                 &childCrossMeasureMode,
        //                                 &childCrossSize)

        //                             childWidth := childCrossSize
        //                             if (isMainAxisRow ) {
        //                                 childWidth = childMainSize
        //                             }
        //                             childHeight := childCrossSize
        //                             if (!isMainAxisRow ) {
        //                                 childHeight = childMainSize
        //                             }

        //                             childWidthMeasureMode := MeasureModeExactly
        //                             if (FloatIsUndefined(childWidth) ) {
        //                                 childWidthMeasureMode = MeasureModeUndefined
        //                             }

        //                             childHeightMeasureMode := MeasureModeExactly
        //                             if (FloatIsUndefined(childHeight) ) {
        //                                 childHeightMeasureMode = MeasureModeUndefined
        //                             }

        //                             layoutNodeInternal(child,
        //                                 childWidth,
        //                                 childHeight,
        //                                 direction,
        //                                 childWidthMeasureMode,
        //                                 childHeightMeasureMode,
        //                                 availableInnerWidth,
        //                                 availableInnerHeight,
        //                                 true,
        //                                 "stretch",
        //                                 config)
        //                         }
        //                     } else {
        //                         remainingCrossDim := containerCrossAxis - nodeDimWithMargin(child, crossAxis, availableInnerWidth)

        //                         if marginLeadingValue(child, crossAxis).Unit == UnitAuto &&
        //                             marginTrailingValue(child, crossAxis).Unit == UnitAuto {
        //                             leadingCrossDim += System.Math.Max(0, remainingCrossDim/2)
        //                         } else if (marginTrailingValue(child, crossAxis).Unit == UnitAuto ) {
        //                             // No-Op
        //                         } else if (marginLeadingValue(child, crossAxis).Unit == UnitAuto ) {
        //                             leadingCrossDim += System.Math.Max(0, remainingCrossDim)
        //                         } else if (alignItem == Align.FlexStart ) {
        //                             // No-Op
        //                         } else if (alignItem == AlignCenter ) {
        //                             leadingCrossDim += remainingCrossDim / 2
        //                         } else {
        //                             leadingCrossDim += remainingCrossDim
        //                         }
        //                     }
        //                     // And we apply the position
        //                     child.Layout.Position[pos[crossAxis]] += totalLineCrossDim + leadingCrossDim
        //                 }
        //             }
        //         }

        //         totalLineCrossDim += crossDim
        //         maxLineMainDim = System.Math.Max(maxLineMainDim, mainDim)

        //         lineCount++
        //         startOfLineIndex = endOfLineIndex

        //     }

        //     // STEP 8: MULTI-LINE CONTENT ALIGNMENT
        //     if performLayout && (lineCount > 1 || isBaselineLayout(node)) &&
        //         !FloatIsUndefined(availableInnerCrossDim) {
        //         remainingAlignContentDim := availableInnerCrossDim - totalLineCrossDim

        //         var crossDimLead float
        //         currentLead := leadingPaddingAndBorderCross

        //         switch node.Style.AlignContent {
        //         case AlignFlexEnd:
        //             currentLead += remainingAlignContentDim
        //         case AlignCenter:
        //             currentLead += remainingAlignContentDim / 2
        //         case Align.Stretch:
        //             if (availableInnerCrossDim > totalLineCrossDim ) {
        //                 crossDimLead = remainingAlignContentDim / float(lineCount)
        //             }
        //         case AlignSpaceAround:
        //             if (availableInnerCrossDim > totalLineCrossDim ) {
        //                 currentLead += remainingAlignContentDim / float(2*lineCount)
        //                 if (lineCount > 1 ) {
        //                     crossDimLead = remainingAlignContentDim / float(lineCount)
        //                 }
        //             } else {
        //                 currentLead += remainingAlignContentDim / 2
        //             }
        //         case AlignSpaceBetween:
        //             if (availableInnerCrossDim > totalLineCrossDim && lineCount > 1 ) {
        //                 crossDimLead = remainingAlignContentDim / float(lineCount-1)
        //             }
        //         case Align.Auto:
        //         case Align.FlexStart:
        //         case Align.Baseline:.
        //         }

//         //         endIndex := 0
//         //         for i := 0; i < lineCount++  ; {
//         //             startIndex (:= endInd)ex
//         //             (var ii i)nt
// ;
//         //             // compute the line's height and find the endInde.x
//  ;       //             var lineHeight float
//         //             var maxAscentForCurrentLine float;
//         //             var maxDescentForCurrentLine float
        //             for ii = startIndex; ii < childCount; ii++ {
        //                 child := node.Children[ii]
        //                 if (child.Style.Display == DisplayNone ) {
        //                     continue
        //                 }
        //                 if (child.Style.PositionType == PositionType.Relative ) {
        //                     if (child.lineIndex != i ) {
        //                         break
        //                     }
        //                     if (nodeIsLayoutDimDefined(child, crossAxis) ) {
        //                         lineHeight = System.Math.Max(lineHeight,
        //                             child.Layout.measuredDimensions[dim[crossAxis]]+
        //                                 nodeMarginForAxis(child, crossAxis, availableInnerWidth))
        //                     }
        //                     if (nodeAlignItem(node, child) == Align.Baseline ) {
        //                         ascent := Baseline(child) + nodeLeadingMargin(child, FlexDirection.Column, availableInnerWidth)
        //                         descent := child.Layout.measuredDimensions[Dimension.Height] + nodeMarginForAxis(child, FlexDirection.Column, availableInnerWidth) - ascent
        //                         maxAscentForCurrentLine = System.Math.Max(maxAscentForCurrentLine, ascent)
        //                         maxDescentForCurrentLine = System.Math.Max(maxDescentForCurrentLine, descent)
        //                         lineHeight = System.Math.Max(lineHeight, maxAscentForCurrentLine+maxDescentForCurrentLine)
        //                     }
        //                 }
        //             }
        //             endIndex = ii
        //             lineHeight += crossDimLead

        //             if (performLayout ) {
        //                 for ii = startIndex; ii < endIndex; ii++ {
        //                     child := node.Children[ii]
        //                     if (child.Style.Display == DisplayNone ) {
        //                         continue
        //                     }
        //                     if (child.Style.PositionType == PositionType.Relative ) {
        //                         switch nodeAlignItem(node, child) {
        //                         case Align.FlexStart:
        //                             {
        //                                 child.Layout.Position[pos[crossAxis]] =
        //                                     currentLead + nodeLeadingMargin(child, crossAxis, availableInnerWidth)
        //                             }
        //                         case AlignFlexEnd:
        //                             {
        //                                 child.Layout.Position[pos[crossAxis]] =
        //                                     currentLead + lineHeight -
        //                                         nodeTrailingMargin(child, crossAxis, availableInnerWidth) -
        //                                         child.Layout.measuredDimensions[dim[crossAxis]]
        //                             }
        //                         case AlignCenter:
        //                             {
        //                                 childHeight := child.Layout.measuredDimensions[dim[crossAxis]]
        //                                 child.Layout.Position[pos[crossAxis]] = currentLead + (lineHeight-childHeight)/2
        //                             }
        //                         case Align.Stretch:
        //                             {
        //                                 child.Layout.Position[pos[crossAxis]] =
        //                                     currentLead + nodeLeadingMargin(child, crossAxis, availableInnerWidth)

        //                                 // Remeasure child with the line height as it as been only measured with the
        //                                 // parents height yet.
        //                                 if (!nodeIsStyleDimDefined(child, crossAxis, availableInnerCrossDim) ) {
        //                                     childWidth := lineHeight
        //                                     if (isMainAxisRow ) {
        //                                         childWidth = child.Layout.measuredDimensions[Dimension.Width] +
        //                                             nodeMarginForAxis(child, mainAxis, availableInnerWidth)
        //                                     }

        //                                     childHeight := lineHeight
        //                                     if (!isMainAxisRow ) {
        //                                         childHeight = child.Layout.measuredDimensions[Dimension.Height] +
        //                                             nodeMarginForAxis(child, crossAxis, availableInnerWidth)
        //                                     }

        //                                     if !(FloatsEqual(childWidth,
        //                                         child.Layout.measuredDimensions[Dimension.Width]) &&
        //                                         FloatsEqual(childHeight,
        //                                             child.Layout.measuredDimensions[Dimension.Height])) {
        //                                         layoutNodeInternal(child,
        //                                             childWidth,
        //                                             childHeight,
        //                                             direction,
        //                                             MeasureModeExactly,
        //                                             MeasureModeExactly,
        //                                             availableInnerWidth,
        //                                             availableInnerHeight,
        //                                             true,
        //                                             "multiline-stretch",
        //                                             config)
        //                                     }
        //                                 }
        //                             }
        //                         case Align.Baseline:
        //                             {
        //                                 child.Layout.Position[Edge.Top] =
        //                                     currentLead + maxAscentForCurrentLine - Baseline(child) +
        //                                         nodeLeadingPosition(child, FlexDirection.Column, availableInnerCrossDim)
        //                             }
        //                         case Align.Auto:
        //                         case AlignSpaceBetween:
        //                         case AlignSpaceAround:.
        //                         }
        //                     }
        //                 }
        //             }

        // currentLead  (//            += lineHeig)ht
        // //         (}
        // //    ) };

        // //     .// ;STEP 9: COMPUTING FINAL DIMENSIONS
        // //     node.Layout.measuredDimensions[Dimension.Width] = nodeBoundAxis(
        // //         node, FlexDirection.Row, availableWidth-marginAxisRow, parentWidth, parentWidth);
        // //     node.Layout.measuredDimensions[Dimension.Height] = nodeBoundAxis(
        //         node, FlexDirection.Column, availableHeight-marginAxisColumn, parentHeight, parentWidth)

        //     // If the user didn't specify a width or height for the node, set the
        //     // dimensions based on the children.
        //     if measureModeMainDim == MeasureModeUndefined ||
        //         (node.Style.Overflow != OverflowScroll && measureModeMainDim == MeasureModeAtMost) {
        //         // Clamp the size to the min/max size, if specified, and make sure it
        //         // doesn't go below the padding and border amount.
        //         node.Layout.measuredDimensions[dim[mainAxis]] =
        //             nodeBoundAxis(node, mainAxis, maxLineMainDim, mainAxisParentSize, parentWidth)
        //     } else if measureModeMainDim == MeasureModeAtMost &&
        //         node.Style.Overflow == OverflowScroll {
        //         node.Layout.measuredDimensions[dim[mainAxis]] = System.Math.Max(
        //             fminf(availableInnerMainDim+paddingAndBorderAxisMain,
        //                 nodeBoundAxisWithinMinAndMax(node, mainAxis, maxLineMainDim, mainAxisParentSize)),
        //             paddingAndBorderAxisMain)
        //     }

        //     if measureModeCrossDim == MeasureModeUndefined ||
        //         (node.Style.Overflow != OverflowScroll && measureModeCrossDim == MeasureModeAtMost) {
        //         // Clamp the size to the min/max size, if specified, and make sure it
        //         // doesn't go below the padding and border amount.
        //         node.Layout.measuredDimensions[dim[crossAxis]] =
        //             nodeBoundAxis(node,
        //                 crossAxis,
        //                 totalLineCrossDim+paddingAndBorderAxisCross,
        //                 crossAxisParentSize,
        //                 parentWidth)
        //     } else if measureModeCrossDim == MeasureModeAtMost &&
        //         node.Style.Overflow == OverflowScroll {
        //         node.Layout.measuredDimensions[dim[crossAxis]] =
        //             System.Math.Max(fminf(availableInnerCrossDim+paddingAndBorderAxisCross,
        //                 nodeBoundAxisWithinMinAndMax(node,
        //                     crossAxis,
        //                     totalLineCrossDim+paddingAndBorderAxisCross,
        //                     crossAxisParentSize)),
        //                 paddingAndBorderAxisCross)
        //     }

        //     // As we only wrapped in normal direction yet, we need to reverse the positions on wrap-reverse.
        //     if (performLayout && node.Style.FlexWrap == WrapWrapReverse ) {
        //         for i := 0; i < childCount; i++ {
        //             child := node.GetChild(i)
        //             if (child.Style.PositionType == PositionType.Relative ) {
        //                 child.Layout.Position[pos[crossAxis]] = node.Layout.measuredDimensions[dim[crossAxis]] -
        //                     child.Layout.Position[pos[crossAxis]] -
        //                     child.Layout.measuredDimensions[dim[crossAxis]]
        //             }
        //         }
        //     }

        //     if (performLayout ) {
        //         // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
        //         for currentAbsoluteChild = firstAbsoluteChild; currentAbsoluteChild != null; currentAbsoluteChild = currentAbsoluteChild.NextChild {
        //             mode := measureModeCrossDim
        //             if (isMainAxisRow ) {
        //                 mode = measureModeMainDim
        //             }

        //             nodeAbsoluteLayoutChild(node,
        //                 currentAbsoluteChild,
        //                 availableInnerWidth,
        //                 mode,
        //                 availableInnerHeight,
        //                 direction,
        //                 config)
        //         }

        //         // STEP 11: SETTING TRAILING POSITIONS FOR CHILDREN
        //         needsMainTrailingPos := mainAxis == FlexDirection.RowReverse || mainAxis == FlexDirectionColumnReverse
        //         needsCrossTrailingPos := crossAxis == FlexDirection.RowReverse || crossAxis == FlexDirectionColumnReverse

        //         // Set trailing position if necessary.
        //         if (needsMainTrailingPos || needsCrossTrailingPos ) {
        //             for i := 0; i < childCount; i++ {
        //                 child := node.Children[i]
        //                 if (child.Style.Display == DisplayNone ) {
        //                     continue
        //                 }
        //                 if (needsMainTrailingPos ) {
        //                     nodeSetChildTrailingPosition(node, child, mainAxis)
        //                 }

        //                 if (needsCrossTrailingPos ) {
        //                     nodeSetChildTrailingPosition(node, child, crossAxis)
        //                 }
        //             }
        //         }
        //     }
        // }

        // var (
        //     gDepth        = 0
        //     gPrintTree    = false
        //     gPrintChanges = false
        //     gPrintSkips   = false
        // )

        // const (
        //     spacerStr = "                                                            "
        // )

        // // spacer returns spacer string
        // static spacer(level int) string {
        //     n := len(spacerStr)
        //     if (level > n ) {
        //         level = n
        //     }
        //     return spacerStr[:level]
        // }

        // var (
        //     measureModeNames = [measureModeCount]string{"UNDEFINED", "EXACTLY", "AT_MOST"}
        //     layoutModeNames  = [measureModeCount]string{"LAY_UNDEFINED", "LAY_EXACTLY", "LAY_AT_MOST"}
        // )

        // // measureModeName returns name of measure mode
        // static measureModeName(mode MeasureMode, performLayout bool) string {

        //     if (mode >= measureModeCount ) {
        //         return ""
        //     }

        //     if (performLayout ) {
        //         return layoutModeNames[mode]
        //     }
        //     return measureModeNames[mode]
        // }

        // static measureModeSizeIsExactAndMatchesOldMeasuredSize(sizeMode MeasureMode, size float, lastComputedSize float) bool {
        //     return sizeMode == MeasureModeExactly && FloatsEqual(size, lastComputedSize)
        // }

        // static measureModeOldSizeIsUnspecifiedAndStillFits(sizeMode MeasureMode, size float, lastSizeMode MeasureMode, lastComputedSize float) bool {
        //     return sizeMode == MeasureModeAtMost && lastSizeMode == MeasureModeUndefined &&
        //         (size >= lastComputedSize || FloatsEqual(size, lastComputedSize))
        // }

        // static measureModeNewMeasureSizeIsStricterAndStillValid(sizeMode MeasureMode, size float, lastSizeMode MeasureMode, lastSize float, lastComputedSize float) bool {
        //     return lastSizeMode == MeasureModeAtMost && sizeMode == MeasureModeAtMost &&
        //         lastSize > size && (lastComputedSize <= size || FloatsEqual(size, lastComputedSize))
        // }

        // // roundValueToPixelGrid rounds value to pixel grid
        // static roundValueToPixelGrid(value float, pointScaleFactor float, forceCeil bool, forceFloor bool) float {
        //     scaledValue := value * pointScaleFactor
        //     fractial := fmodf(scaledValue, 1.0)
        //     if (FloatsEqual(fractial, 0) ) {
        //         // First we check if the value is already rounded
        //         scaledValue = scaledValue - fractial
        //     } else if (FloatsEqual(fractial, 1.0) ) {
        //         scaledValue = scaledValue - fractial + 1.0
        //     } else if (forceCeil ) {
        //         // Next we check if we need to use forced rounding
        //         scaledValue = scaledValue - fractial + 1.0
        //     } else if (forceFloor ) {
        //         scaledValue = scaledValue - fractial
        //     } else {
        //         // Finally we just round the value
        //         var f float
        //         if (fractial >= 0.5 ) {
        //             f = 1.0
        //         }
        //         scaledValue = scaledValue - fractial + f
        //     }
        //     return scaledValue / pointScaleFactor
        // }

        // // nodeCanUseCachedMeasurement returns true if can use cached measurement
        // static nodeCanUseCachedMeasurement(widthMode MeasureMode, width float, heightMode MeasureMode, height float, lastWidthMode MeasureMode, lastWidth float, lastHeightMode MeasureMode, lastHeight float, lastComputedWidth float, lastComputedHeight float, marginRow float, marginColumn float, config *Config) bool {
        //     if (lastComputedHeight < 0 || lastComputedWidth < 0 ) {
        //         return false
        //     }
        //     useRoundedComparison := config != null && config.PointScaleFactor != 0
        //     effectiveWidth := width
        //     effectiveHeight := height
        //     effectiveLastWidth := lastWidth
        //     effectiveLastHeight := lastHeight

        //     if (useRoundedComparison ) {
        //         effectiveWidth = roundValueToPixelGrid(width, config.PointScaleFactor, false, false)
        //         effectiveHeight = roundValueToPixelGrid(height, config.PointScaleFactor, false, false)
        //         effectiveLastWidth = roundValueToPixelGrid(lastWidth, config.PointScaleFactor, false, false)
        //         effectiveLastHeight = roundValueToPixelGrid(lastHeight, config.PointScaleFactor, false, false)
        //     }

        //     hasSameWidthSpec := lastWidthMode == widthMode && FloatsEqual(effectiveLastWidth, effectiveWidth)
        //     hasSameHeightSpec := lastHeightMode == heightMode && FloatsEqual(effectiveLastHeight, effectiveHeight)

        //     widthIsCompatible :=
        //         hasSameWidthSpec || measureModeSizeIsExactAndMatchesOldMeasuredSize(widthMode,
        //             width-marginRow,
        //             lastComputedWidth) ||
        //             measureModeOldSizeIsUnspecifiedAndStillFits(widthMode,
        //                 width-marginRow,
        //                 lastWidthMode,
        //                 lastComputedWidth) ||
        //             measureModeNewMeasureSizeIsStricterAndStillValid(
        //                 widthMode, width-marginRow, lastWidthMode, lastWidth, lastComputedWidth)

        //     heightIsCompatible :=
        //         hasSameHeightSpec || measureModeSizeIsExactAndMatchesOldMeasuredSize(heightMode,
        //             height-marginColumn,
        //             lastComputedHeight) ||
        //             measureModeOldSizeIsUnspecifiedAndStillFits(heightMode,
        //                 height-marginColumn,
        //                 lastHeightMode,
        //                 lastComputedHeight) ||
        //             measureModeNewMeasureSizeIsStricterAndStillValid(
        //                 heightMode, height-marginColumn, lastHeightMode, lastHeight, lastComputedHeight)

        //     return widthIsCompatible && heightIsCompatible
        // }

        // // layoutNodeInternal is a wrapper around the YGNodelayoutImpl function. It determines
        // // whether the layout request is redundant and can be skipped.
        // //
        // // Parameters:
        // //  Input parameters are the same as YGNodelayoutImpl (see above)
        // //  Return parameter is true if layout was performed, false if skipped
        // static layoutNodeInternal(Node node, availableWidth float, availableHeight float,
        //     parentDirection Direction, widthMeasureMode MeasureMode,
        //     heightMeasureMode MeasureMode, parentWidth float, parentHeight float,
        //     performLayout bool, reason string, config *Config) bool {
        //     layout := &node.Layout

        //     gDepth++

        //     needToVisitNode :=
        //         (node.IsDirty && layout.generationCount != currentGenerationCount) ||
        //             layout.lastParentDirection != parentDirection

        //     if (needToVisitNode ) {
        //         // Invalidate the cached results.
        //         layout.nextCachedMeasurementsIndex = 0
        //         layout.cachedLayout.widthMeasureMode = MeasureMode(-1)
        //         layout.cachedLayout.heightMeasureMode = MeasureMode(-1)
        //         layout.cachedLayout.computedWidth = -1
        //         layout.cachedLayout.computedHeight = -1
        //     }

        //     var cachedResults *CachedMeasurement

        //     // Determine whether the results are already cached. We maintain a separate
        //     // cache for layouts and measurements. A layout operation modifies the
        //     // positions
        //     // and dimensions for nodes in the subtree. The algorithm assumes that each
        //     // node
        //     // gets layed out a maximum of one time per tree layout, but multiple
        //     // measurements
        //     // may be required to resolve all of the flex dimensions.
        //     // We handle nodes with measure functions specially here because they are the
        //     // most
        //     // expensive to measure, so it's worth avoiding redundant measurements if at
        //     // all possible.
        //     if (node.Measure != null ) {
        //         marginAxisRow := nodeMarginForAxis(node, FlexDirection.Row, parentWidth)
        //         marginAxisColumn := nodeMarginForAxis(node, FlexDirection.Column, parentWidth)

        //         // First, try to use the layout cache.
        //         if nodeCanUseCachedMeasurement(widthMeasureMode,
        //             availableWidth,
        //             heightMeasureMode,
        //             availableHeight,
        //             layout.cachedLayout.widthMeasureMode,
        //             layout.cachedLayout.availableWidth,
        //             layout.cachedLayout.heightMeasureMode,
        //             layout.cachedLayout.availableHeight,
        //             layout.cachedLayout.computedWidth,
        //             layout.cachedLayout.computedHeight,
        //             marginAxisRow,
        //             marginAxisColumn,
        //             config) {
        //             cachedResults = &layout.cachedLayout
        //         } else {
        //             // Try to use the measurement cache.
        //             for i := 0; i < layout.nextCachedMeasurementsIndex; i++ {
        //                 if nodeCanUseCachedMeasurement(widthMeasureMode,
        //                     availableWidth,
        //                     heightMeasureMode,
        //                     availableHeight,
        //                     layout.cachedMeasurements[i].widthMeasureMode,
        //                     layout.cachedMeasurements[i].availableWidth,
        //                     layout.cachedMeasurements[i].heightMeasureMode,
        //                     layout.cachedMeasurements[i].availableHeight,
        //                     layout.cachedMeasurements[i].computedWidth,
        //                     layout.cachedMeasurements[i].computedHeight,
        //                     marginAxisRow,
        //                     marginAxisColumn,
        //                     config) {
        //                     cachedResults = &layout.cachedMeasurements[i]
        //                     break
        //                 }
        //             }
        //         }
        //     } else if (performLayout ) {
        //         if FloatsEqual(layout.cachedLayout.availableWidth, availableWidth) &&
        //             FloatsEqual(layout.cachedLayout.availableHeight, availableHeight) &&
        //             layout.cachedLayout.widthMeasureMode == widthMeasureMode &&
        //             layout.cachedLayout.heightMeasureMode == heightMeasureMode {
        //             cachedResults = &layout.cachedLayout
        //         }
        //     } else {
        //         for i := 0; i < layout.nextCachedMeasurementsIndex; i++ {
        //             if FloatsEqual(layout.cachedMeasurements[i].availableWidth, availableWidth) &&
        //                 FloatsEqual(layout.cachedMeasurements[i].availableHeight, availableHeight) &&
        //                 layout.cachedMeasurements[i].widthMeasureMode == widthMeasureMode &&
        //                 layout.cachedMeasurements[i].heightMeasureMode == heightMeasureMode {
        //                 cachedResults = &layout.cachedMeasurements[i]
        //                 break
        //             }
        //         }
        //     }

        //     if (!needToVisitNode && cachedResults != null ) {
        //         layout.measuredDimensions[Dimension.Width] = cachedResults.computedWidth
        //         layout.measuredDimensions[Dimension.Height] = cachedResults.computedHeight

        //         if (gPrintChanges && gPrintSkips ) {
        //             fmt.Printf("%s%d.{[skipped] ", spacer(gDepth), gDepth)
        //             if (node.Print != null ) {
        //                 node.Print(node)
        //             }
        //             fmt.Printf("wm: %s, hm: %s, aw: %f ah: %f => d: (%f, %f) %s\n",
        //                 measureModeName(widthMeasureMode, performLayout),
        //                 measureModeName(heightMeasureMode, performLayout),
        //                 availableWidth,
        //                 availableHeight,
        //                 cachedResults.computedWidth,
        //                 cachedResults.computedHeight,
        //                 reason)
        //         }
        //     } else {
        //         if (gPrintChanges ) {
        //             s := ""
        //             if (needToVisitNode ) {
        //                 s = "*"
        //             }
        //             fmt.Printf("%s%d.{%s", spacer(gDepth), gDepth, s)
        //             if (node.Print != null ) {
        //                 node.Print(node)
        //             }
        //             fmt.Printf("wm: %s, hm: %s, aw: %f ah: %f %s\n",
        //                 measureModeName(widthMeasureMode, performLayout),
        //                 measureModeName(heightMeasureMode, performLayout),
        //                 availableWidth,
        //                 availableHeight,
        //                 reason)
        //         }

        //         nodelayoutImpl(node,
        //             availableWidth,
        //             availableHeight,
        //             parentDirection,
        //             widthMeasureMode,
        //             heightMeasureMode,
        //             parentWidth,
        //             parentHeight,
        //             performLayout,
        //             config)

        //         if (gPrintChanges ) {
        //             s := ""
        //             if (needToVisitNode ) {
        //                 s = "*"
        //             }
        //             fmt.Printf("%s%d.}%s", spacer(gDepth), gDepth, s)
        //             if (node.Print != null ) {
        //                 node.Print(node)
        //             }
        //             fmt.Printf("wm: %s, hm: %s, d: (%f, %f) %s\n",
        //                 measureModeName(widthMeasureMode, performLayout),
        //                 measureModeName(heightMeasureMode, performLayout),
        //                 layout.measuredDimensions[Dimension.Width],
        //                 layout.measuredDimensions[Dimension.Height],
        //                 reason)
        //         }

        //         layout.lastParentDirection = parentDirection

        //         if (cachedResults == null ) {
        //             if (layout.nextCachedMeasurementsIndex == maxCachedResultCount ) {
        //                 if (gPrintChanges ) {
        //                     fmt.Printf("Out of cache entries!\n")
        //                 }
        //                 layout.nextCachedMeasurementsIndex = 0
        //             }

        //             var newCacheEntry *CachedMeasurement
        //             if (performLayout ) {
        //                 // Use the single layout cache entry.
        //                 newCacheEntry = &layout.cachedLayout
        //             } else {
        //                 // Allocate a new measurement cache entry.
        //                 newCacheEntry = &layout.cachedMeasurements[layout.nextCachedMeasurementsIndex]
        //                 layout.nextCachedMeasurementsIndex++
        //             }

        //             newCacheEntry.availableWidth = availableWidth
        //             newCacheEntry.availableHeight = availableHeight
        //             newCacheEntry.widthMeasureMode = widthMeasureMode
        //             newCacheEntry.heightMeasureMode = heightMeasureMode
        //             newCacheEntry.computedWidth = layout.measuredDimensions[Dimension.Width]
        //             newCacheEntry.computedHeight = layout.measuredDimensions[Dimension.Height]
        //         }
        //     }

        //     if (performLayout ) {
        //         node.Layout.Dimensions[Dimension.Width] = node.Layout.measuredDimensions[Dimension.Width]
        //         node.Layout.Dimensions[Dimension.Height] = node.Layout.measuredDimensions[Dimension.Height]
        //         node.hasNewLayout = true
        //         node.IsDirty = false
        //     }

        //     gDepth--
        //     layout.generationCount = currentGenerationCount
        //     return needToVisitNode || cachedResults == null
        // }

        // // SetPointScaleFactor sets scale factor
        // static (config *Config) SetPointScaleFactor(pixelsInPoint float) {
        //     assertWithConfig(config, pixelsInPoint >= 0, "Scale factor should not be less than zero")

        //     // We store points for Pixel as we will use it for rounding
        //     if (pixelsInPoint == 0 ) {
        //         // Zero is used to skip rounding
        //         config.PointScaleFactor = 0
        //     } else {
        //         config.PointScaleFactor = pixelsInPoint
        //     }
        // }

        // static roundToPixelGrid(Node node, pointScaleFactor float, absoluteLeft float, absoluteTop float) {
        //     if (pointScaleFactor == 0.0 ) {
        //         return
        //     }

        //     nodeLeft := node.Layout.Position[Edge.Left]
        //     nodeTop := node.Layout.Position[Edge.Top]

        //     nodeWidth := node.Layout.Dimensions[Dimension.Width]
        //     nodeHeight := node.Layout.Dimensions[Dimension.Height]

        //     absoluteNodeLeft := absoluteLeft + nodeLeft
        //     absoluteNodeTop := absoluteTop + nodeTop

        //     absoluteNodeRight := absoluteNodeLeft + nodeWidth
        //     absoluteNodeBottom := absoluteNodeTop + nodeHeight

        //     // If a node has a custom measure function we never want to round down its size as this could
        //     // lead to unwanted text truncation.
        //     textRounding := node.NodeType == NodeTypeText

        //     node.Layout.Position[Edge.Left] = roundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding)
        //     node.Layout.Position[Edge.Top] = roundValueToPixelGrid(nodeTop, pointScaleFactor, false, textRounding)

        //     // We multiply dimension by scale factor and if the result is close to the whole number, we don't have any fraction
        //     // To verify if the result is close to whole number we want to check both floor and ceil numbers
        //     hasFractionalWidth := !FloatsEqual(fmodf(nodeWidth*pointScaleFactor, 1), 0) &&
        //         !FloatsEqual(fmodf(nodeWidth*pointScaleFactor, 1), 1)
        //     hasFractionalHeight := !FloatsEqual(fmodf(nodeHeight*pointScaleFactor, 1), 0) &&
        //         !FloatsEqual(fmodf(nodeHeight*pointScaleFactor, 1), 1)

        //     node.Layout.Dimensions[Dimension.Width] =
        //         roundValueToPixelGrid(
        //             absoluteNodeRight,
        //             pointScaleFactor,
        //             (textRounding && hasFractionalWidth),
        //             (textRounding && !hasFractionalWidth)) -
        //             roundValueToPixelGrid(absoluteNodeLeft, pointScaleFactor, false, textRounding)
        //     node.Layout.Dimensions[Dimension.Height] =
        //         roundValueToPixelGrid(
        //             absoluteNodeBottom,
        //             pointScaleFactor,
        //             (textRounding && hasFractionalHeight),
        //             (textRounding && !hasFractionalHeight)) -
        //             roundValueToPixelGrid(absoluteNodeTop, pointScaleFactor, false, textRounding)

        //     for _, child := range node.Children {
        //         roundToPixelGrid(child, pointScaleFactor, absoluteNodeLeft, absoluteNodeTop)
        //     }
        // }

        // static calcStartWidth(Node node, parentWidth float) (float, MeasureMode) {
        //     if (nodeIsStyleDimDefined(node, FlexDirection.Row, parentWidth) ) {
        //         width := resolveValue(node.resolvedDimensions[dim[FlexDirection.Row]], parentWidth)
        //         margin := nodeMarginForAxis(node, FlexDirection.Row, parentWidth)
        //         return width + margin, MeasureModeExactly
        //     }
        //     if (resolveValue(&node.Style.MaxDimensions[Dimension.Width], parentWidth) >= 0.0 ) {
        //         width := resolveValue(&node.Style.MaxDimensions[Dimension.Width], parentWidth)
        //         return width, MeasureModeAtMost
        //     }

        //     width := parentWidth
        //     widthMeasureMode := MeasureModeExactly
        //     if (FloatIsUndefined(width) ) {
        //         widthMeasureMode = MeasureModeUndefined
        //     }
        //     return width, widthMeasureMode
        // }
        // static calcStartHeight(Node node, parentWidth, parentHeight float) (float, MeasureMode) {
        //     if (nodeIsStyleDimDefined(node, FlexDirection.Column, parentHeight) ) {
        //         height := resolveValue(node.resolvedDimensions[dim[FlexDirection.Column]], parentHeight)
        //         margin := nodeMarginForAxis(node, FlexDirection.Column, parentWidth)
        //         return height + margin, MeasureModeExactly
        //     }
        //     if (resolveValue(&node.Style.MaxDimensions[Dimension.Height], parentHeight) >= 0 ) {
        //         height := resolveValue(&node.Style.MaxDimensions[Dimension.Height], parentHeight)
        //         return height, MeasureModeAtMost
        //     }
        //     height := parentHeight
        //     heightMeasureMode := MeasureModeExactly
        //     if (FloatIsUndefined(height) ) {
        //         heightMeasureMode = MeasureModeUndefined
        //     }
        //     return height, heightMeasureMode
        // }

        // // CalculateLayout calculates layout
        // static CalculateLayout(Node node, parentWidth float, parentHeight float, parentDirection Direction) {
        //     // Increment the generation count. This will force the recursive routine to
        //     // visit
        //     // all dirty nodes at least once. Subsequent visits will be skipped if the
        //     // input
        //     // parameters don't change.
        //     currentGenerationCount++

        //     resolveDimensions(node)

        //     width, widthMeasureMode := calcStartWidth(node, parentWidth)
        //     height, heightMeasureMode := calcStartHeight(node, parentWidth, parentHeight)

        //     if layoutNodeInternal(node, width, height, parentDirection,
        //         widthMeasureMode, heightMeasureMode, parentWidth, parentHeight,
        //         true, "initial", node.Config) {
        //         nodeSetPosition(node, node.Layout.Direction, parentWidth, parentHeight, parentWidth)
        //         roundToPixelGrid(node, node.Config.PointScaleFactor, 0, 0)

        //         if (gPrintTree ) {
        //             NodePrint(node, PrintOptionsLayout|PrintOptionsChildren|PrintOptionsStyle)
        //         }
        //     }
        // }

        // // SetExperimentalFeatureEnabled enables experimental feature
        // static (config *Config) SetExperimentalFeatureEnabled(feature ExperimentalFeature, enabled bool) {
        //     config.experimentalFeatures[feature] = enabled
        // }

        // // IsExperimentalFeatureEnabled returns if experimental feature is enabled
        // static (config *Config) IsExperimentalFeatureEnabled(feature ExperimentalFeature) bool {
        //     return config.experimentalFeatures[feature]
        // }

        // static log(Node node, level LogLevel, format string, args ...interface{}) {
        //     fmt.Printf(format, args...)
        // }

        static void assertCond(bool cond, string format, params object[] args)
        {
            if (!cond) 
            {
                throw new System.Exception(string.Format(format, args));
            }
        }

        static void assertWithNode(Node node, bool cond, string format, params object[] args)
        {
            assertCond(cond, format, args);
        }

        // static assertWithConfig(config *Config, condition bool, message string) {
        //     if (!condition ) {
        //         panic(message)
        //     }
        // }

    }

}
#endif