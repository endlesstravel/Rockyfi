using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rockyfi
{
    public class Size
    {
        public float Width;
        public float Height;

        public Size(float w, float h)
        {
            Width = w;
            Height = h;
        }
    }

    public class Value
    {
        public float value;
        public Unit unit;

        public Value(float v, Unit u)
        {
            this.value = v;
            this.unit = u;
        }

        public static Value UndefinedValue
        {
            get
            {
                return new Value(float.NaN, Unit.Undefined);
            }
        }

        public static void CopyValue(Value[] dest, Value[] src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                dest[i].value = src[i].value;
                dest[i].unit = src[i].unit;
            }
        }
    }

    public class Config
    {
        public bool UseWebDefaults = false;
        public object Context = null;
        public LoggerFunc Logger = DefaultLog;

        readonly internal bool[] experimentalFeatures = new bool[Constant.ExperimentalFeatureCount + 1];
        internal bool UseLegacyStretchBehaviour = false;
        internal float PointScaleFactor = 1;

        public static int DefaultLog(Config config, Node node, LogLevel level, string format, params object[] args)
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

        // SetExperimentalFeatureEnabled enables experimental feature
        public void SetExperimentalFeatureEnabled(ExperimentalFeature feature, bool enabled)
        {
            this.experimentalFeatures[(int)feature] = enabled;
        }

        // IsExperimentalFeatureEnabled returns if experimental feature is enabled
        public bool IsExperimentalFeatureEnabled(ExperimentalFeature feature)
        {
            return this.experimentalFeatures[(int)feature];
        }


        // SetPointScaleFactor sets scale factor
        public void SetPointScaleFactor(float pixelsInPoint)
        {
            assertWithConfig(this, pixelsInPoint >= 0, "Scale factor should not be less than zero");

            // We store points for Pixel as we will use it for rounding
            if (pixelsInPoint == 0)
            {
                // Zero is used to skip rounding
                this.PointScaleFactor = 0;
            }
            else
            {
                this.PointScaleFactor = pixelsInPoint;
            }
        }

        internal static void assertWithConfig(Config config, bool condition, string message)
        {
            if (!condition)
            {
                throw new System.Exception(message);
            }
        }
    }

    public partial class Node
    {
        // FloatsEqual returns true if floats are approx. equal
        public static bool FloatsEqual(float a, float b)
        {
            if (FloatIsUndefined(a))
            {
                return FloatIsUndefined(b);
            }
            return System.Math.Abs(a - b) < 0.0001f;
        }
        // roundValueToPixelGrid rounds value to pixel grid
        public static float roundValueToPixelGrid(float value, float pointScaleFactor, bool forceCeil, bool forceFloor)
        {
            var scaledValue = value * pointScaleFactor;
            var fractial = fmodf(scaledValue, 1f);
            if (FloatsEqual(fractial, 0))
            {
                // First we check if the value is already rounded
                scaledValue = scaledValue - fractial;
            }
            else if (FloatsEqual(fractial, 1))
            {
                scaledValue = scaledValue - fractial + 1;
            }
            else if (forceCeil)
            {
                // Next we check if we need to use forced rounding
                scaledValue = scaledValue - fractial + 1;
            }
            else if (forceFloor)
            {
                scaledValue = scaledValue - fractial;
            }
            else
            {
                // Finally we just round the value
                float f = 0;
                if (fractial >= 0.5f)
                {
                    f = 1.0f;
                }
                scaledValue = scaledValue - fractial + f;
            }
            return scaledValue / pointScaleFactor;
        }

        // NodeCopyStyle copies style
        public static void NodeCopyStyle(Node dstNode, Node srcNode)
        {
            if (!styleEq(dstNode.nodeStyle, srcNode.nodeStyle))
            {
                Style.Copy(dstNode.nodeStyle, srcNode.nodeStyle);
                nodeMarkDirtyInternal(dstNode);
            }
        }


        // // Reset resets a node
        public static void Reset(ref Node node)
        {
            assertWithNode(node, node.Children.Count == 0, "Cannot reset a node which still has children attached");
            assertWithNode(node, node.Parent == null, "Cannot reset a node still attached to a parent");
            node.Children.Clear();

            var config = node.config;
            node = CreateDefaultNode();
            if (config.UseWebDefaults)
            {
                node.nodeStyle.FlexDirection = FlexDirection.Row;
                node.nodeStyle.AlignContent = Align.Stretch;
            }
            node.config = config;
        }


        public static Node CreateDefaultNode()
        {
            var node = new Node();
            return node;
        }

        public static Node CreateDefaultNode(Config config)
        {
            var node = new Node();
            if (config.UseWebDefaults)
            {
                node.nodeStyle.FlexDirection = FlexDirection.Row;
                node.nodeStyle.AlignContent = Align.Stretch;
            }
            node.config = config;
            return node;
        }

        public static Config CreateDefaultConfig()
        {
            return new Config();
        }

        // CalculateLayout calculates layout
        public static void CalculateLayout(Node node, float parentWidth, float parentHeight, Direction parentDirection)
        {
            // Increment the generation count. This will force the recursive routine to
            // visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the
            // input
            // parameters don't change.
            currentGenerationCount++;

            resolveDimensions(node);

            calcStartWidth(node, parentWidth, out float width, out MeasureMode widthMeasureMode);
            calcStartHeight(node, parentWidth, parentHeight, out float height, out MeasureMode heightMeasureMode);

            if (layoutNodeInternal(node, width, height, parentDirection,
                widthMeasureMode, heightMeasureMode, parentWidth, parentHeight,
                true, "initial", node.config))
            {
                nodeSetPosition(node, node.nodeLayout.Direction, parentWidth, parentHeight, parentWidth);
                roundToPixelGrid(node, node.config.PointScaleFactor, 0, 0);

                if (gPrintTree)
                {
                    // NodePrint(node, PrintOptionsLayout|PrintOptionsChildren|PrintOptionsStyle);
                    System.Console.WriteLine("NodePrint(node, PrintOptionsLayout|PrintOptionsChildren|PrintOptionsStyle);");
                }
            }
        }
    }
}
