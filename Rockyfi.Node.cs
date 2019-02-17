using System.Collections.Generic;

namespace Rockyfi
{
    public partial class Node
    {
        readonly Style nodeStyle = new Style();
        readonly Layout nodeLayout = new Layout();
        internal int lineIndex;

        internal Node Parent = null;
        internal readonly List<Node> Children = new List<Node>();

        internal Node NextChild;

        internal MeasureFunc measureFunc;
        internal BaselineFunc baselineFunc;
        internal PrintFunc printFunc;
        internal Config config = CreateDefaultConfig();

        internal bool IsDirty = false;
        internal bool hasNewLayout = true;
        internal NodeType NodeType = NodeType.Default;

        internal readonly Value[] resolvedDimensions = new Value[2] { ValueUndefined, ValueUndefined };



        public object Context;
        public readonly Dictionary<string, object> Atrribute = new Dictionary<string, object>();

        public void Update(float parentWidth, float parentHeight, Direction parentDirection)
        {
            CalculateLayout(this, parentWidth, parentHeight, parentDirection);
        }



        internal void Helper_SetDimensions(Value value, Dimension dimension)
        {
            if (dimension == Dimension.Width)
            {
                if (value.unit == Unit.Auto)
                    StyleSetWidthAuto();
                else if (value.unit == Unit.Percent)
                    StyleSetWidthPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetWidth(value.value);
            }
            else
            {
                if (value.unit == Unit.Auto)
                    StyleSetHeightAuto();
                else if (value.unit == Unit.Percent)
                    StyleSetHeightPercent(value.value);
                else if (value.unit == Unit.Point)
                    StyleSetHeight(value.value);
            }
        }
    }



}