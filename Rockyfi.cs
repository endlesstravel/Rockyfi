
namespace Rockyfi
{
    public partial class Node
    {
        #region Style
        // StyleSetWidth sets width
        public void StyleSetWidth(float width) {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Width];
            if (dim.value != width || dim.unit != Unit.Point) {
                dim.value = width;
                dim.unit = Unit.Point;
                if (FloatIsUndefined(width)) {
                    dim.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetWidthPercent sets width percent
        public void StyleSetWidthPercent(float width) {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Width];
            if (dim.value != width || dim.unit != Unit.Percent) {
                dim.value = width;
                dim.unit = Unit.Percent;
                if (FloatIsUndefined(width)) {
                    dim.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetWidthAuto sets width auto
        public void StyleSetWidthAuto() {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Width];
            if (dim.unit != Unit.Auto) {
                dim.value = float.NaN;
                dim.unit = Unit.Auto;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetWidth gets width
        public Value StyleGetWidth() {
            return this.nodeStyle.Dimensions[(int)Dimension.Width];
        }

        // StyleSetHeight sets height
        public void StyleSetHeight(float height) {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Height];
            if (dim.value != height || dim.unit != Unit.Point) {
                dim.value = height;
                dim.unit = Unit.Point;
                if (FloatIsUndefined(height)) {
                    dim.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetHeightPercent sets height percent
        public void StyleSetHeightPercent(float height) {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Height];
            if (dim.value != height || dim.unit != Unit.Percent) {
                dim.value = height;
                dim.unit = Unit.Percent;
                if (FloatIsUndefined(height)) {
                    dim.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetHeightAuto sets height auto
        public void StyleSetHeightAuto() {
            var dim = this.nodeStyle.Dimensions[(int)Dimension.Height];
            if (dim.unit != Unit.Auto) {
                dim.value = float.NaN;
                dim.unit = Unit.Auto;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetHeight gets height
        public Value StyleGetHeight() {
            return this.nodeStyle.Dimensions[(int)Dimension.Height];
        }

        // StyleSetPositionType sets position type
        public void StyleSetPositionType(PositionType positionType) {
            if (this.nodeStyle.PositionType != positionType) {
                this.nodeStyle.PositionType = positionType;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetPosition sets position
        public void StyleSetPosition(Edge edge, float position) {
            var pos = this.nodeStyle.Position[(int)edge];
            if (pos.value != position || pos.unit != Unit.Point) {
                pos.value = position;
                pos.unit = Unit.Point;
                if (FloatIsUndefined(position)) {
                    pos.unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetPositionPercent sets position percent
        public void StyleSetPositionPercent(Edge edge, float position) {
            var pos = this.nodeStyle.Position[(int)edge];
            if (pos.value != position || pos.unit != Unit.Percent) {
                pos.value = position;
                pos.unit = Unit.Percent;
                if (FloatIsUndefined(position)) {
                    pos.unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetPosition gets position
        public Value StyleGetPosition(Edge edge) {
            return this.nodeStyle.Position[(int)edge];
        }

        // StyleSetDirection sets direction
        public void StyleSetDirection(Direction direction) {
            if (this.nodeStyle.Direction != direction) {
                this.nodeStyle.Direction = direction;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexDirection sets flex directions
        public void StyleSetFlexDirection(FlexDirection flexDirection) {
            if (this.nodeStyle.FlexDirection != flexDirection) {
                this.nodeStyle.FlexDirection = flexDirection;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetJustifyContent sets justify content
        public void StyleSetJustifyContent(Justify justifyContent) {
            if (this.nodeStyle.JustifyContent != justifyContent) {
                this.nodeStyle.JustifyContent = justifyContent;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetAlignContent sets align content
        public void StyleSetAlignContent(Align alignContent) {
            if (this.nodeStyle.AlignContent != alignContent) {
                this.nodeStyle.AlignContent = alignContent;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetAlignItems sets align content
        public void StyleSetAlignItems(Align alignItems) {
            if (this.nodeStyle.AlignItems != alignItems) {
                this.nodeStyle.AlignItems = alignItems;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetAlignSelf sets align self
        public void StyleSetAlignSelf(Align alignSelf) {
            if (this.nodeStyle.AlignSelf != alignSelf) {
                this.nodeStyle.AlignSelf = alignSelf;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexWrap sets flex wrap
        public void StyleSetFlexWrap(Wrap flexWrap) {
            if (this.nodeStyle.FlexWrap != flexWrap) {
                this.nodeStyle.FlexWrap = flexWrap;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetOverflow sets overflow
        public void StyleSetOverflow(Overflow overflow) {
            if (this.nodeStyle.Overflow != overflow) {
                this.nodeStyle.Overflow = overflow;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetDisplay sets display
        public void StyleSetDisplay(Display display) {
            if (this.nodeStyle.Display != display) {
                this.nodeStyle.Display = display;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlex sets flex
        public void StyleSetFlex(float flex) {
            if (this.nodeStyle.Flex != flex) {
                this.nodeStyle.Flex = flex;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexGrow sets flex grow
        public void StyleSetFlexGrow(float flexGrow) {
            if (this.nodeStyle.FlexGrow != flexGrow) {
                this.nodeStyle.FlexGrow = flexGrow;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexShrink sets flex shrink
        public void StyleSetFlexShrink(float flexShrink) {
            if (this.nodeStyle.FlexShrink != flexShrink) {
                this.nodeStyle.FlexShrink = flexShrink;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexBasis sets flex basis
        public void StyleSetFlexBasis(float flexBasis) {
            if (this.nodeStyle.FlexBasis.value != flexBasis ||
                this.nodeStyle.FlexBasis.unit != Unit.Point) {
                this.nodeStyle.FlexBasis.value = flexBasis;
                this.nodeStyle.FlexBasis.unit = Unit.Point;
                if (FloatIsUndefined(flexBasis)) {
                    this.nodeStyle.FlexBasis.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetFlexBasisPercent sets flex basis percent
        public void StyleSetFlexBasisPercent(float flexBasis) {
            if (this.nodeStyle.FlexBasis.value != flexBasis ||
                this.nodeStyle.FlexBasis.unit != Unit.Percent) {
                this.nodeStyle.FlexBasis.value = flexBasis;
                this.nodeStyle.FlexBasis.unit = Unit.Percent;
                if (FloatIsUndefined(flexBasis)) {
                    this.nodeStyle.FlexBasis.unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // NodeStyleSetFlexBasisAuto sets flex basis auto
        public void NodeStyleSetFlexBasisAuto() {
            if (this.nodeStyle.FlexBasis.unit != Unit.Auto) {
                this.nodeStyle.FlexBasis.value = float.NaN;
                this.nodeStyle.FlexBasis.unit = Unit.Auto;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMargin sets margin
        public void StyleSetMargin(Edge edge, float margin) {
            if (this.nodeStyle.Margin[(int)edge].value != margin ||
                this.nodeStyle.Margin[(int)edge].unit != Unit.Point) {
                this.nodeStyle.Margin[(int)edge].value = margin;
                this.nodeStyle.Margin[(int)edge].unit = Unit.Point;
                if (FloatIsUndefined(margin)) {
                    this.nodeStyle.Margin[(int)edge].unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMarginPercent sets margin percent
        public void StyleSetMarginPercent(Edge edge, float margin) {
            if (this.nodeStyle.Margin[(int)edge].value != margin ||
                this.nodeStyle.Margin[(int)edge].unit != Unit.Percent) {
                this.nodeStyle.Margin[(int)edge].value = margin;
                this.nodeStyle.Margin[(int)edge].unit = Unit.Percent;
                if (FloatIsUndefined(margin)) {
                    this.nodeStyle.Margin[(int)edge].unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetMargin gets margin
        public Value StyleGetMargin(Edge edge) {
            return this.nodeStyle.Margin[(int)edge];
        }

        // StyleSetMarginAuto sets margin auto
        public void StyleSetMarginAuto(Edge edge) {
            if (this.nodeStyle.Margin[(int)edge].unit != Unit.Auto) {
                this.nodeStyle.Margin[(int)edge].value = float.NaN;
                this.nodeStyle.Margin[(int)edge].unit = Unit.Auto;
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetPadding sets padding
        public void StyleSetPadding(Edge edge, float padding) {
            if (this.nodeStyle.Padding[(int)edge].value != padding ||
                this.nodeStyle.Padding[(int)edge].unit != Unit.Point) {
                this.nodeStyle.Padding[(int)edge].value = padding;
                this.nodeStyle.Padding[(int)edge].unit = Unit.Point;
                if (FloatIsUndefined(padding)) {
                    this.nodeStyle.Padding[(int)edge].unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetPaddingPercent sets padding percent
        public void StyleSetPaddingPercent(Edge edge, float padding) {
            if (this.nodeStyle.Padding[(int)edge].value != padding ||
                this.nodeStyle.Padding[(int)edge].unit != Unit.Percent) {
                this.nodeStyle.Padding[(int)edge].value = padding;
                this.nodeStyle.Padding[(int)edge].unit = Unit.Percent;
                if (FloatIsUndefined(padding)) {
                    this.nodeStyle.Padding[(int)edge].unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetPadding gets padding
        public Value StyleGetPadding(Edge edge) {
            return this.nodeStyle.Padding[(int)edge];
        }

        // StyleSetBorder sets border
        public void StyleSetBorder(Edge edge, float border) {
            if (this.nodeStyle.Border[(int)edge].value != border ||
                this.nodeStyle.Border[(int)edge].unit != Unit.Point) {
                this.nodeStyle.Border[(int)edge].value = border;
                this.nodeStyle.Border[(int)edge].unit = Unit.Point;
                if (FloatIsUndefined(border)) {
                    this.nodeStyle.Border[(int)edge].unit = Unit.Undefined;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetBorder gets border
        public float StyleGetBorder(Edge edge) {
            return this.nodeStyle.Border[(int)edge].value;
        }

        // StyleSetMinWidth sets min width
        public void StyleSetMinWidth(float minWidth) {
            if (this.nodeStyle.MinDimensions[(int)Dimension.Width].value != minWidth ||
                this.nodeStyle.MinDimensions[(int)Dimension.Width].unit != Unit.Point) {
                this.nodeStyle.MinDimensions[(int)Dimension.Width].value = minWidth;
                this.nodeStyle.MinDimensions[(int)Dimension.Width].unit = Unit.Point;
                if (FloatIsUndefined(minWidth)) {
                    this.nodeStyle.MinDimensions[(int)Dimension.Width].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMinWidthPercent sets width percent
        public void StyleSetMinWidthPercent(float minWidth) {
            if (this.nodeStyle.MinDimensions[(int)Dimension.Width].value != minWidth ||
                this.nodeStyle.MinDimensions[(int)Dimension.Width].unit != Unit.Percent) {
                this.nodeStyle.MinDimensions[(int)Dimension.Width].value = minWidth;
                this.nodeStyle.MinDimensions[(int)Dimension.Width].unit = Unit.Percent;
                if (FloatIsUndefined(minWidth)) {
                    this.nodeStyle.MinDimensions[(int)Dimension.Width].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetMinWidth gets min width
        public Value StyleGetMinWidth() {
            return this.nodeStyle.MinDimensions[(int)Dimension.Width];
        }

        // StyleSetMinHeight sets min width
        public void StyleSetMinHeight(float minHeight) {
            if (this.nodeStyle.MinDimensions[(int)Dimension.Height].value != minHeight ||
                this.nodeStyle.MinDimensions[(int)Dimension.Height].unit != Unit.Point) {
                this.nodeStyle.MinDimensions[(int)Dimension.Height].value = minHeight;
                this.nodeStyle.MinDimensions[(int)Dimension.Height].unit = Unit.Point;
                if (FloatIsUndefined(minHeight)) {
                    this.nodeStyle.MinDimensions[(int)Dimension.Height].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMinHeightPercent sets min height percent
        public void StyleSetMinHeightPercent(float minHeight) {
            if (this.nodeStyle.MinDimensions[(int)Dimension.Height].value != minHeight ||
                this.nodeStyle.MinDimensions[(int)Dimension.Height].unit != Unit.Percent) {
                this.nodeStyle.MinDimensions[(int)Dimension.Height].value = minHeight;
                this.nodeStyle.MinDimensions[(int)Dimension.Height].unit = Unit.Percent;
                if (FloatIsUndefined(minHeight)) {
                    this.nodeStyle.MinDimensions[(int)Dimension.Height].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetMinHeight gets min height
        public Value StyleGetMinHeight() {
            return this.nodeStyle.MinDimensions[(int)Dimension.Height];
        }

        // StyleSetMaxWidth sets max width
        public void StyleSetMaxWidth(float maxWidth) {
            if (this.nodeStyle.MaxDimensions[(int)Dimension.Width].value != maxWidth ||
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit != Unit.Point) {
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].value = maxWidth;
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit = Unit.Point;
                if (FloatIsUndefined(maxWidth)) {
                    this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMaxWidthPercent sets max width percent
        public void StyleSetMaxWidthPercent(float maxWidth) {
            if (this.nodeStyle.MaxDimensions[(int)Dimension.Width].value != maxWidth ||
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit != Unit.Percent) {
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].value = maxWidth;
                this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit = Unit.Percent;
                if (FloatIsUndefined(maxWidth)) {
                    this.nodeStyle.MaxDimensions[(int)Dimension.Width].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetMaxWidth gets max width
        public Value StyleGetMaxWidth() {
            return this.nodeStyle.MaxDimensions[(int)Dimension.Width];
        }

        // StyleSetMaxHeight sets max width
        public void StyleSetMaxHeight(float maxHeight) {
            if (this.nodeStyle.MaxDimensions[(int)Dimension.Height].value != maxHeight ||
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit != Unit.Point) {
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].value = maxHeight;
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit = Unit.Point;
                if (FloatIsUndefined(maxHeight)) {
                    this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleSetMaxHeightPercent sets max height percent
        public void StyleSetMaxHeightPercent(float maxHeight) {
            if (this.nodeStyle.MaxDimensions[(int)Dimension.Height].value != maxHeight ||
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit != Unit.Percent) {
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].value = maxHeight;
                this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit = Unit.Percent;
                if (FloatIsUndefined(maxHeight)) {
                    this.nodeStyle.MaxDimensions[(int)Dimension.Height].unit = Unit.Auto;
                }
                nodeMarkDirtyInternal(this);
            }
        }

        // StyleGetMaxHeight gets max height
        public Value StyleGetMaxHeight() {
            return this.nodeStyle.MaxDimensions[(int)Dimension.Height];
        }

        // StyleSetAspectRatio sets axpect ratio
        public void StyleSetAspectRatio(float aspectRatio) {
            if (this.nodeStyle.AspectRatio != aspectRatio) {
                this.nodeStyle.AspectRatio = aspectRatio;
                nodeMarkDirtyInternal(this);
            }
        }

        // LayoutGetLeft gets left
        public float LayoutGetLeft() {
            return this.nodeLayout.Position[(int)Edge.Left];
        }

        // LayoutGetTop gets top
        public float LayoutGetTop() {
            return this.nodeLayout.Position[(int)Edge.Top];
        }

        // LayoutGetRight gets right
        public float LayoutGetRight() {
            return this.nodeLayout.Position[(int)Edge.Right];
        }

        // LayoutGetBottom gets bottom
        public float LayoutGetBottom() {
            return this.nodeLayout.Position[(int)Edge.Bottom];
        }

        // LayoutGetWidth gets width
        public float LayoutGetWidth() {
            return this.nodeLayout.Dimensions[(int)Dimension.Width];
        }

        // LayoutGetHeight gets height
        public float LayoutGetHeight() {
            return this.nodeLayout.Dimensions[(int)Dimension.Height];
        }

        // LayoutGetMargin gets margin
        public float LayoutGetMargin(Edge edge) {
            assertWithNode(this, edge < Edge.End, "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Margin[(int)Edge.End];
                }
                return this.nodeLayout.Margin[(int)Edge.Start];
            }
            if (edge == Edge.Right) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Margin[(int)Edge.Start];
                }
                return this.nodeLayout.Margin[(int)Edge.End];
            }
            return this.nodeLayout.Margin[(int)edge];
        }

        // LayoutGetBorder gets border
        public float LayoutGetBorder(Edge edge) {
            assertWithNode(this, edge < Edge.End,
                "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Border[(int)Edge.End];
                }
                return this.nodeLayout.Border[(int)Edge.Start];
            }
            if (edge == Edge.Right) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Border[(int)Edge.Start];
                }
                return this.nodeLayout.Border[(int)Edge.End];
            }
            return this.nodeLayout.Border[(int)edge];
        }

        // LayoutGetPadding gets padding
        public float LayoutGetPadding(Edge edge) {
            assertWithNode(this, edge < Edge.End,
                "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Padding[(int)Edge.End];
                }
                return this.nodeLayout.Padding[(int)Edge.Start];
            }
            if (edge == Edge.Right) {
                if (this.nodeLayout.Direction == Direction.RTL) {
                    return this.nodeLayout.Padding[(int)Edge.Start];
                }
                return this.nodeLayout.Padding[(int)Edge.End];
            }
            return this.nodeLayout.Padding[(int)edge];
        }
        #endregion

        #region other props
        public void SetMeasureFunc(MeasureFunc func)
        {
            Node.SetMeasureFunc(this, func);
        }
        public void SetBaselineFunc(BaselineFunc func)
        {
            this.baselineFunc = func;
        }
        #endregion

        #region tree
        public void InsertChild(Node child, int idx)
        {
            Node.InsertChild(this, child, idx);
        }
        public void RemoveChild(Node child)
        {
            Node.RemoveChild(this, child);
        }
        #endregion
    }
}