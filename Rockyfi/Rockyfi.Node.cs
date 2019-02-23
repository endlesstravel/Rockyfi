using System.Collections.Generic;

namespace Rockyfi
{
    public partial class Node
    {
        readonly internal Style nodeStyle = new Style();
        readonly internal Rockyfi.Layout nodeLayout = new Rockyfi.Layout();
        internal int lineIndex;

        internal Node Parent = null;
        internal readonly List<Node> Children = new List<Node>();

        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }
        internal Node NextChild;

        internal MeasureFunc measureFunc;
        internal BaselineFunc baselineFunc;
        internal PrintFunc printFunc;
        internal Config config = Constant.configDefaults;

        public bool IsDirty
        {
            get;
            internal set;
        }
        internal bool hasNewLayout = true;
        internal NodeType NodeType = NodeType.Default;

        internal readonly Value[] resolvedDimensions = new Value[2] { Rockyfi.ValueUndefined, Rockyfi.ValueUndefined };



        public object Context;
        public readonly Dictionary<string, object> Atrribute = new Dictionary<string, object>();

        public void CalculateLayout(float parentWidth, float parentHeight, Direction parentDirection)
        {
            Rockyfi.CalculateLayout(this, parentWidth, parentHeight, parentDirection);
        }
    }



}