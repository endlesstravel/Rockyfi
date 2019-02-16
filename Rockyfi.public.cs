
namespace Rockyfi
{
    partial class Rockyfi
    {
        partial class Node
        {
            Node node;

            // StyleSetWidth sets width
            public void StyleSetWidth(float width) {
                var dim = node.Style.Dimensions[(int)Dimension.Width];
                if (dim.value != width || dim.unit != Unit.Point) {
                    dim.value = width;
                    dim.unit = Unit.Point;
                    if (FloatIsUndefined(width)) {
                        dim.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetWidthPercent sets width percent
            public void StyleSetWidthPercent(float width) {
                var dim = node.Style.Dimensions[(int)Dimension.Width];
                if (dim.value != width || dim.unit != Unit.Percent) {
                    dim.value = width;
                    dim.unit = Unit.Percent;
                    if (FloatIsUndefined(width)) {
                        dim.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetWidthAuto sets width auto
            public void StyleSetWidthAuto() {
                var dim = node.Style.Dimensions[(int)Dimension.Width];
                if (dim.unit != Unit.Auto) {
                    dim.value = float.NaN;
                    dim.unit = Unit.Auto;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetWidth gets width
            public Value StyleGetWidth() {
                return node.Style.Dimensions[(int)Dimension.Width];
            }

            // StyleSetHeight sets height
            public void StyleSetHeight(float height) {
                var dim = node.Style.Dimensions[(int)Dimension.Height];
                if (dim.value != height || dim.unit != Unit.Point) {
                    dim.value = height;
                    dim.unit = Unit.Point;
                    if (FloatIsUndefined(height)) {
                        dim.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetHeightPercent sets height percent
            public void StyleSetHeightPercent(float height) {
                var dim = node.Style.Dimensions[(int)Dimension.Height];
                if (dim.value != height || dim.unit != Unit.Percent) {
                    dim.value = height;
                    dim.unit = Unit.Percent;
                    if (FloatIsUndefined(height)) {
                        dim.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetHeightAuto sets height auto
            public void StyleSetHeightAuto() {
                var dim = node.Style.Dimensions[(int)Dimension.Height];
                if (dim.unit != Unit.Auto) {
                    dim.value = float.NaN;
                    dim.unit = Unit.Auto;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetHeight gets height
            public Value StyleGetHeight() {
                return node.Style.Dimensions[(int)Dimension.Height];
            }

            // StyleSetPositionType sets position type
            public void StyleSetPositionType(PositionType positionType) {
                if (node.Style.PositionType != positionType) {
                    node.Style.PositionType = positionType;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetPosition sets position
            public void StyleSetPosition(Edge edge, float position) {
                var pos = node.Style.Position[(int)edge];
                if (pos.value != position || pos.unit != Unit.Point) {
                    pos.value = position;
                    pos.unit = Unit.Point;
                    if (FloatIsUndefined(position)) {
                        pos.unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetPositionPercent sets position percent
            public void StyleSetPositionPercent(Edge edge, float position) {
                var pos = node.Style.Position[(int)edge];
                if (pos.value != position || pos.unit != Unit.Percent) {
                    pos.value = position;
                    pos.unit = Unit.Percent;
                    if (FloatIsUndefined(position)) {
                        pos.unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetPosition gets position
            public Value StyleGetPosition(Edge edge) {
                return node.Style.Position[(int)edge];
            }

            // StyleSetDirection sets direction
            public void StyleSetDirection(Direction direction) {
                if (node.Style.Direction != direction) {
                    node.Style.Direction = direction;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexDirection sets flex directions
            public void StyleSetFlexDirection(FlexDirection flexDirection) {
                if (node.Style.FlexDirection != flexDirection) {
                    node.Style.FlexDirection = flexDirection;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetJustifyContent sets justify content
            public void StyleSetJustifyContent(Justify justifyContent) {
                if (node.Style.JustifyContent != justifyContent) {
                    node.Style.JustifyContent = justifyContent;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetAlignContent sets align content
            public void StyleSetAlignContent(Align alignContent) {
                if (node.Style.AlignContent != alignContent) {
                    node.Style.AlignContent = alignContent;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetAlignItems sets align content
            public void StyleSetAlignItems(Align alignItems) {
                if (node.Style.AlignItems != alignItems) {
                    node.Style.AlignItems = alignItems;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetAlignSelf sets align self
            public void StyleSetAlignSelf(Align alignSelf) {
                if (node.Style.AlignSelf != alignSelf) {
                    node.Style.AlignSelf = alignSelf;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexWrap sets flex wrap
            public void StyleSetFlexWrap(Wrap flexWrap) {
                if (node.Style.FlexWrap != flexWrap) {
                    node.Style.FlexWrap = flexWrap;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetOverflow sets overflow
            public void StyleSetOverflow(Overflow overflow) {
                if (node.Style.Overflow != overflow) {
                    node.Style.Overflow = overflow;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetDisplay sets display
            public void StyleSetDisplay(Display display) {
                if (node.Style.Display != display) {
                    node.Style.Display = display;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlex sets flex
            public void StyleSetFlex(float flex) {
                if (node.Style.Flex != flex) {
                    node.Style.Flex = flex;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexGrow sets flex grow
            public void StyleSetFlexGrow(float flexGrow) {
                if (node.Style.FlexGrow != flexGrow) {
                    node.Style.FlexGrow = flexGrow;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexShrink sets flex shrink
            public void StyleSetFlexShrink(float flexShrink) {
                if (node.Style.FlexShrink != flexShrink) {
                    node.Style.FlexShrink = flexShrink;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexBasis sets flex basis
            public void StyleSetFlexBasis(float flexBasis) {
                if (node.Style.FlexBasis.value != flexBasis ||
                    node.Style.FlexBasis.unit != Unit.Point) {
                    node.Style.FlexBasis.value = flexBasis;
                    node.Style.FlexBasis.unit = Unit.Point;
                    if (FloatIsUndefined(flexBasis)) {
                        node.Style.FlexBasis.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetFlexBasisPercent sets flex basis percent
            public void StyleSetFlexBasisPercent(float flexBasis) {
                if (node.Style.FlexBasis.value != flexBasis ||
                    node.Style.FlexBasis.unit != Unit.Percent) {
                    node.Style.FlexBasis.value = flexBasis;
                    node.Style.FlexBasis.unit = Unit.Percent;
                    if (FloatIsUndefined(flexBasis)) {
                        node.Style.FlexBasis.unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // NodeStyleSetFlexBasisAuto sets flex basis auto
            public void NodeStyleSetFlexBasisAuto() {
                if (node.Style.FlexBasis.unit != Unit.Auto) {
                    node.Style.FlexBasis.value = float.NaN;
                    node.Style.FlexBasis.unit = Unit.Auto;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMargin sets margin
            public void StyleSetMargin(Edge edge, float margin) {
                if (node.Style.Margin[(int)edge].value != margin ||
                    node.Style.Margin[(int)edge].unit != Unit.Point) {
                    node.Style.Margin[(int)edge].value = margin;
                    node.Style.Margin[(int)edge].unit = Unit.Point;
                    if (FloatIsUndefined(margin)) {
                        node.Style.Margin[(int)edge].unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMarginPercent sets margin percent
            public void StyleSetMarginPercent(Edge edge, float margin) {
                if (node.Style.Margin[(int)edge].value != margin ||
                    node.Style.Margin[(int)edge].unit != Unit.Percent) {
                    node.Style.Margin[(int)edge].value = margin;
                    node.Style.Margin[(int)edge].unit = Unit.Percent;
                    if (FloatIsUndefined(margin)) {
                        node.Style.Margin[(int)edge].unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetMargin gets margin
            public Value StyleGetMargin(Edge edge) {
                return node.Style.Margin[(int)edge];
            }

            // StyleSetMarginAuto sets margin auto
            public void StyleSetMarginAuto(Edge edge) {
                if (node.Style.Margin[(int)edge].unit != Unit.Auto) {
                    node.Style.Margin[(int)edge].value = float.NaN;
                    node.Style.Margin[(int)edge].unit = Unit.Auto;
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetPadding sets padding
            public void StyleSetPadding(Edge edge, float padding) {
                if (node.Style.Padding[(int)edge].value != padding ||
                    node.Style.Padding[(int)edge].unit != Unit.Point) {
                    node.Style.Padding[(int)edge].value = padding;
                    node.Style.Padding[(int)edge].unit = Unit.Point;
                    if (FloatIsUndefined(padding)) {
                        node.Style.Padding[(int)edge].unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetPaddingPercent sets padding percent
            public void StyleSetPaddingPercent(Edge edge, float padding) {
                if (node.Style.Padding[(int)edge].value != padding ||
                    node.Style.Padding[(int)edge].unit != Unit.Percent) {
                    node.Style.Padding[(int)edge].value = padding;
                    node.Style.Padding[(int)edge].unit = Unit.Percent;
                    if (FloatIsUndefined(padding)) {
                        node.Style.Padding[(int)edge].unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetPadding gets padding
            public Value StyleGetPadding(Edge edge) {
                return node.Style.Padding[(int)edge];
            }

            // StyleSetBorder sets border
            public void StyleSetBorder(Edge edge, float border) {
                if (node.Style.Border[(int)edge].value != border ||
                    node.Style.Border[(int)edge].unit != Unit.Point) {
                    node.Style.Border[(int)edge].value = border;
                    node.Style.Border[(int)edge].unit = Unit.Point;
                    if (FloatIsUndefined(border)) {
                        node.Style.Border[(int)edge].unit = Unit.Undefined;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetBorder gets border
            public float StyleGetBorder(Edge edge) {
                return node.Style.Border[(int)edge].value;
            }

            // StyleSetMinWidth sets min width
            public void StyleSetMinWidth(float minWidth) {
                if (node.Style.MinDimensions[(int)Dimension.Width].value != minWidth ||
                    node.Style.MinDimensions[(int)Dimension.Width].unit != Unit.Point) {
                    node.Style.MinDimensions[(int)Dimension.Width].value = minWidth;
                    node.Style.MinDimensions[(int)Dimension.Width].unit = Unit.Point;
                    if (FloatIsUndefined(minWidth)) {
                        node.Style.MinDimensions[(int)Dimension.Width].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMinWidthPercent sets width percent
            public void StyleSetMinWidthPercent(float minWidth) {
                if (node.Style.MinDimensions[(int)Dimension.Width].value != minWidth ||
                    node.Style.MinDimensions[(int)Dimension.Width].unit != Unit.Percent) {
                    node.Style.MinDimensions[(int)Dimension.Width].value = minWidth;
                    node.Style.MinDimensions[(int)Dimension.Width].unit = Unit.Percent;
                    if (FloatIsUndefined(minWidth)) {
                        node.Style.MinDimensions[(int)Dimension.Width].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetMinWidth gets min width
            public Value StyleGetMinWidth() {
                return node.Style.MinDimensions[(int)Dimension.Width];
            }

            // StyleSetMinHeight sets min width
            public void StyleSetMinHeight(float minHeight) {
                if (node.Style.MinDimensions[(int)Dimension.Height].value != minHeight ||
                    node.Style.MinDimensions[(int)Dimension.Height].unit != Unit.Point) {
                    node.Style.MinDimensions[(int)Dimension.Height].value = minHeight;
                    node.Style.MinDimensions[(int)Dimension.Height].unit = Unit.Point;
                    if (FloatIsUndefined(minHeight)) {
                        node.Style.MinDimensions[(int)Dimension.Height].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMinHeightPercent sets min height percent
            public void StyleSetMinHeightPercent(float minHeight) {
                if (node.Style.MinDimensions[(int)Dimension.Height].value != minHeight ||
                    node.Style.MinDimensions[(int)Dimension.Height].unit != Unit.Percent) {
                    node.Style.MinDimensions[(int)Dimension.Height].value = minHeight;
                    node.Style.MinDimensions[(int)Dimension.Height].unit = Unit.Percent;
                    if (FloatIsUndefined(minHeight)) {
                        node.Style.MinDimensions[(int)Dimension.Height].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetMinHeight gets min height
            public Value StyleGetMinHeight() {
                return node.Style.MinDimensions[(int)Dimension.Height];
            }

            // StyleSetMaxWidth sets max width
            public void StyleSetMaxWidth(float maxWidth) {
                if (node.Style.MaxDimensions[(int)Dimension.Width].value != maxWidth ||
                    node.Style.MaxDimensions[(int)Dimension.Width].unit != Unit.Point) {
                    node.Style.MaxDimensions[(int)Dimension.Width].value = maxWidth;
                    node.Style.MaxDimensions[(int)Dimension.Width].unit = Unit.Point;
                    if (FloatIsUndefined(maxWidth)) {
                        node.Style.MaxDimensions[(int)Dimension.Width].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMaxWidthPercent sets max width percent
            public void StyleSetMaxWidthPercent(float maxWidth) {
                if (node.Style.MaxDimensions[(int)Dimension.Width].value != maxWidth ||
                    node.Style.MaxDimensions[(int)Dimension.Width].unit != Unit.Percent) {
                    node.Style.MaxDimensions[(int)Dimension.Width].value = maxWidth;
                    node.Style.MaxDimensions[(int)Dimension.Width].unit = Unit.Percent;
                    if (FloatIsUndefined(maxWidth)) {
                        node.Style.MaxDimensions[(int)Dimension.Width].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetMaxWidth gets max width
            public Value StyleGetMaxWidth() {
                return node.Style.MaxDimensions[(int)Dimension.Width];
            }

            // StyleSetMaxHeight sets max width
            public void StyleSetMaxHeight(float maxHeight) {
                if (node.Style.MaxDimensions[(int)Dimension.Height].value != maxHeight ||
                    node.Style.MaxDimensions[(int)Dimension.Height].unit != Unit.Point) {
                    node.Style.MaxDimensions[(int)Dimension.Height].value = maxHeight;
                    node.Style.MaxDimensions[(int)Dimension.Height].unit = Unit.Point;
                    if (FloatIsUndefined(maxHeight)) {
                        node.Style.MaxDimensions[(int)Dimension.Height].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleSetMaxHeightPercent sets max height percent
            public void StyleSetMaxHeightPercent(float maxHeight) {
                if (node.Style.MaxDimensions[(int)Dimension.Height].value != maxHeight ||
                    node.Style.MaxDimensions[(int)Dimension.Height].unit != Unit.Percent) {
                    node.Style.MaxDimensions[(int)Dimension.Height].value = maxHeight;
                    node.Style.MaxDimensions[(int)Dimension.Height].unit = Unit.Percent;
                    if (FloatIsUndefined(maxHeight)) {
                        node.Style.MaxDimensions[(int)Dimension.Height].unit = Unit.Auto;
                    }
                    nodeMarkDirtyInternal(node);
                }
            }

            // StyleGetMaxHeight gets max height
            public Value StyleGetMaxHeight() {
                return node.Style.MaxDimensions[(int)Dimension.Height];
            }

            // StyleSetAspectRatio sets axpect ratio
            public void StyleSetAspectRatio(float aspectRatio) {
                if (node.Style.AspectRatio != aspectRatio) {
                    node.Style.AspectRatio = aspectRatio;
                    nodeMarkDirtyInternal(node);
                }
            }

            // LayoutGetLeft gets left
            public float LayoutGetLeft() {
                return node.Layout.Position[(int)Edge.Left];
            }

            // LayoutGetTop gets top
            public float LayoutGetTop() {
                return node.Layout.Position[(int)Edge.Top];
            }

            // LayoutGetRight gets right
            public float LayoutGetRight() {
                return node.Layout.Position[(int)Edge.Right];
            }

            // LayoutGetBottom gets bottom
            public float LayoutGetBottom() {
                return node.Layout.Position[(int)Edge.Bottom];
            }

            // LayoutGetWidth gets width
            public float LayoutGetWidth() {
                return node.Layout.Dimensions[(int)Dimension.Width];
            }

            // LayoutGetHeight gets height
            public float LayoutGetHeight() {
                return node.Layout.Dimensions[(int)Dimension.Height];
            }

            // LayoutGetMargin gets margin
            public float LayoutGetMargin(Edge edge) {
                assertWithNode(node, edge < Edge.End, "Cannot get layout properties of multi-edge shorthands");
                if (edge == Edge.Left) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Margin[(int)Edge.End];
                    }
                    return node.Layout.Margin[(int)Edge.Start];
                }
                if (edge == Edge.Right) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Margin[(int)Edge.Start];
                    }
                    return node.Layout.Margin[(int)Edge.End];
                }
                return node.Layout.Margin[(int)edge];
            }

            // LayoutGetBorder gets border
            public float LayoutGetBorder(Edge edge) {
                assertWithNode(node, edge < Edge.End,
                    "Cannot get layout properties of multi-edge shorthands");
                if (edge == Edge.Left) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Border[(int)Edge.End];
                    }
                    return node.Layout.Border[(int)Edge.Start];
                }
                if (edge == Edge.Right) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Border[(int)Edge.Start];
                    }
                    return node.Layout.Border[(int)Edge.End];
                }
                return node.Layout.Border[(int)edge];
            }

            // LayoutGetPadding gets padding
            public float LayoutGetPadding(Edge edge) {
                assertWithNode(node, edge < Edge.End,
                    "Cannot get layout properties of multi-edge shorthands");
                if (edge == Edge.Left) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Padding[(int)Edge.End];
                    }
                    return node.Layout.Padding[(int)Edge.Start];
                }
                if (edge == Edge.Right) {
                    if (node.Layout.Direction == Direction.RTL) {
                        return node.Layout.Padding[(int)Edge.Start];
                    }
                    return node.Layout.Padding[(int)Edge.End];
                }
                return node.Layout.Padding[(int)edge];
            }

        }
    }
}