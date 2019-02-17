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
        readonly internal bool[] experimentalFeatures = new bool[Constant.ExperimentalFeatureCount + 1];
        internal bool UseWebDefaults = false;
        internal bool UseLegacyStretchBehaviour = false;
        internal float PointScaleFactor = 1;
        internal LoggerFunc Logger = DefaultLog;
        internal object Context = null;

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

        internal static void Copy(Config dest, Config src)
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
        internal void SetExperimentalFeatureEnabled(ExperimentalFeature feature, bool enabled)
        {
            this.experimentalFeatures[(int)feature] = enabled;
        }

        // IsExperimentalFeatureEnabled returns if experimental feature is enabled
        internal bool IsExperimentalFeatureEnabled(ExperimentalFeature feature)
        {
            return this.experimentalFeatures[(int)feature];
        }


        // SetPointScaleFactor sets scale factor
        internal void SetPointScaleFactor(float pixelsInPoint)
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
}
