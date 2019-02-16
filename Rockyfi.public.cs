
namespace Rockyfi
{
    partial class Node
    {
        // StyleSetWidth sets width
        func (node *Node) StyleSetWidth(float width) {
            dim := &node.Style.Dimensions[DimensionWidth]
            if dim.Value != width || dim.Unit != UnitPoint {
                dim.Value = width
                dim.Unit = UnitPoint
                if FloatIsUndefined(width) {
                    dim.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetWidthPercent sets width percent
        func (node *Node) StyleSetWidthPercent(float width) {
            dim := &node.Style.Dimensions[DimensionWidth]
            if dim.Value != width || dim.Unit != UnitPercent {
                dim.Value = width
                dim.Unit = UnitPercent
                if FloatIsUndefined(width) {
                    dim.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetWidthAuto sets width auto
        func (node *Node) StyleSetWidthAuto() {
            dim := &node.Style.Dimensions[DimensionWidth]
            if dim.Unit != UnitAuto {
                dim.Value = Undefined
                dim.Unit = UnitAuto
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetWidth gets width
        func (node *Node) StyleGetWidth() Value {
            return node.Style.Dimensions[DimensionWidth]
        }

        // StyleSetHeight sets height
        func (node *Node) StyleSetHeight(float height) {
            dim := &node.Style.Dimensions[DimensionHeight]
            if dim.Value != height || dim.Unit != UnitPoint {
                dim.Value = height
                dim.Unit = UnitPoint
                if FloatIsUndefined(height) {
                    dim.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetHeightPercent sets height percent
        func (node *Node) StyleSetHeightPercent(float height) {
            dim := &node.Style.Dimensions[DimensionHeight]
            if dim.Value != height || dim.Unit != UnitPercent {
                dim.Value = height
                dim.Unit = UnitPercent
                if FloatIsUndefined(height) {
                    dim.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetHeightAuto sets height auto
        func (node *Node) StyleSetHeightAuto() {
            dim := &node.Style.Dimensions[DimensionHeight]
            if dim.Unit != UnitAuto {
                dim.Value = Undefined
                dim.Unit = UnitAuto
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetHeight gets height
        func (node *Node) StyleGetHeight() Value {
            return node.Style.Dimensions[DimensionHeight]
        }

        // StyleSetPositionType sets position type
        func (node *Node) StyleSetPositionType(PositionType positionType) {
            if node.Style.PositionType != positionType {
                node.Style.PositionType = positionType
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetPosition sets position
        func (node *Node) StyleSetPosition(Edge edge, float position) {
            pos := &node.Style.Position[edge]
            if pos.Value != position || pos.Unit != UnitPoint {
                pos.Value = position
                pos.Unit = UnitPoint
                if FloatIsUndefined(position) {
                    pos.Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetPositionPercent sets position percent
        func (node *Node) StyleSetPositionPercent(Edge edge, float position) {
            pos := &node.Style.Position[edge]
            if pos.Value != position || pos.Unit != UnitPercent {
                pos.Value = position
                pos.Unit = UnitPercent
                if FloatIsUndefined(position) {
                    pos.Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetPosition gets position
        func (node *Node) StyleGetPosition(Edge edge) Value {
            return node.Style.Position[edge]
        }

        // StyleSetDirection sets direction
        func (node *Node) StyleSetDirection(Direction direction) {
            if node.Style.Direction != direction {
                node.Style.Direction = direction
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexDirection sets flex directions
        func (node *Node) StyleSetFlexDirection(FlexDirection flexDirection) {
            if node.Style.FlexDirection != flexDirection {
                node.Style.FlexDirection = flexDirection
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetJustifyContent sets justify content
        func (node *Node) StyleSetJustifyContent(Justify justifyContent) {
            if node.Style.JustifyContent != justifyContent {
                node.Style.JustifyContent = justifyContent
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetAlignContent sets align content
        func (node *Node) StyleSetAlignContent(Align alignContent) {
            if node.Style.AlignContent != alignContent {
                node.Style.AlignContent = alignContent
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetAlignItems sets align content
        func (node *Node) StyleSetAlignItems(Align alignItems) {
            if node.Style.AlignItems != alignItems {
                node.Style.AlignItems = alignItems
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetAlignSelf sets align self
        func (node *Node) StyleSetAlignSelf(Align alignSelf) {
            if node.Style.AlignSelf != alignSelf {
                node.Style.AlignSelf = alignSelf
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexWrap sets flex wrap
        func (node *Node) StyleSetFlexWrap(Wrap flexWrap) {
            if node.Style.FlexWrap != flexWrap {
                node.Style.FlexWrap = flexWrap
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetOverflow sets overflow
        func (node *Node) StyleSetOverflow(Overflow overflow) {
            if node.Style.Overflow != overflow {
                node.Style.Overflow = overflow
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetDisplay sets display
        func (node *Node) StyleSetDisplay(Display display) {
            if node.Style.Display != display {
                node.Style.Display = display
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlex sets flex
        func (node *Node) StyleSetFlex(float flex) {
            if node.Style.Flex != flex {
                node.Style.Flex = flex
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexGrow sets flex grow
        func (node *Node) StyleSetFlexGrow(float flexGrow) {
            if node.Style.FlexGrow != flexGrow {
                node.Style.FlexGrow = flexGrow
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexShrink sets flex shrink
        func (node *Node) StyleSetFlexShrink(float flexShrink) {
            if node.Style.FlexShrink != flexShrink {
                node.Style.FlexShrink = flexShrink
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexBasis sets flex basis
        func (node *Node) StyleSetFlexBasis(float flexBasis) {
            if node.Style.FlexBasis.Value != flexBasis ||
                node.Style.FlexBasis.Unit != UnitPoint {
                node.Style.FlexBasis.Value = flexBasis
                node.Style.FlexBasis.Unit = UnitPoint
                if FloatIsUndefined(flexBasis) {
                    node.Style.FlexBasis.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetFlexBasisPercent sets flex basis percent
        func (node *Node) StyleSetFlexBasisPercent(float flexBasis) {
            if node.Style.FlexBasis.Value != flexBasis ||
                node.Style.FlexBasis.Unit != UnitPercent {
                node.Style.FlexBasis.Value = flexBasis
                node.Style.FlexBasis.Unit = UnitPercent
                if FloatIsUndefined(flexBasis) {
                    node.Style.FlexBasis.Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // NodeStyleSetFlexBasisAuto sets flex basis auto
        func NodeStyleSetFlexBasisAuto(node *Node) {
            if node.Style.FlexBasis.Unit != UnitAuto {
                node.Style.FlexBasis.Value = Undefined
                node.Style.FlexBasis.Unit = UnitAuto
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMargin sets margin
        func (node *Node) StyleSetMargin(Edge edge, float margin) {
            if node.Style.Margin[edge].Value != margin ||
                node.Style.Margin[edge].Unit != UnitPoint {
                node.Style.Margin[edge].Value = margin
                node.Style.Margin[edge].Unit = UnitPoint
                if FloatIsUndefined(margin) {
                    node.Style.Margin[edge].Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMarginPercent sets margin percent
        func (node *Node) StyleSetMarginPercent(Edge edge, float margin) {
            if node.Style.Margin[edge].Value != margin ||
                node.Style.Margin[edge].Unit != UnitPercent {
                node.Style.Margin[edge].Value = margin
                node.Style.Margin[edge].Unit = UnitPercent
                if FloatIsUndefined(margin) {
                    node.Style.Margin[edge].Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetMargin gets margin
        func (node *Node) StyleGetMargin(Edge edge) Value {
            return node.Style.Margin[edge]
        }

        // StyleSetMarginAuto sets margin auto
        func (node *Node) StyleSetMarginAuto(Edge edge) {
            if node.Style.Margin[edge].Unit != UnitAuto {
                node.Style.Margin[edge].Value = Undefined
                node.Style.Margin[edge].Unit = UnitAuto
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetPadding sets padding
        func (node *Node) StyleSetPadding(Edge edge, float padding) {
            if node.Style.Padding[edge].Value != padding ||
                node.Style.Padding[edge].Unit != UnitPoint {
                node.Style.Padding[edge].Value = padding
                node.Style.Padding[edge].Unit = UnitPoint
                if FloatIsUndefined(padding) {
                    node.Style.Padding[edge].Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetPaddingPercent sets padding percent
        func (node *Node) StyleSetPaddingPercent(Edge edge, float padding) {
            if node.Style.Padding[edge].Value != padding ||
                node.Style.Padding[edge].Unit != UnitPercent {
                node.Style.Padding[edge].Value = padding
                node.Style.Padding[edge].Unit = UnitPercent
                if FloatIsUndefined(padding) {
                    node.Style.Padding[edge].Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetPadding gets padding
        func (node *Node) StyleGetPadding(Edge edge) Value {
            return node.Style.Padding[edge]
        }

        // StyleSetBorder sets border
        func (node *Node) StyleSetBorder(Edge edge, float border) {
            if node.Style.Border[edge].Value != border ||
                node.Style.Border[edge].Unit != UnitPoint {
                node.Style.Border[edge].Value = border
                node.Style.Border[edge].Unit = UnitPoint
                if FloatIsUndefined(border) {
                    node.Style.Border[edge].Unit = UnitUndefined
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetBorder gets border
        func (node *Node) StyleGetBorder(Edge edge) float {
            return node.Style.Border[edge].Value
        }

        // StyleSetMinWidth sets min width
        func (node *Node) StyleSetMinWidth(minWidth float) {
            if node.Style.MinDimensions[DimensionWidth].Value != minWidth ||
                node.Style.MinDimensions[DimensionWidth].Unit != UnitPoint {
                node.Style.MinDimensions[DimensionWidth].Value = minWidth
                node.Style.MinDimensions[DimensionWidth].Unit = UnitPoint
                if FloatIsUndefined(minWidth) {
                    node.Style.MinDimensions[DimensionWidth].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMinWidthPercent sets width percent
        func (node *Node) StyleSetMinWidthPercent(minWidth float) {
            if node.Style.MinDimensions[DimensionWidth].Value != minWidth ||
                node.Style.MinDimensions[DimensionWidth].Unit != UnitPercent {
                node.Style.MinDimensions[DimensionWidth].Value = minWidth
                node.Style.MinDimensions[DimensionWidth].Unit = UnitPercent
                if FloatIsUndefined(minWidth) {
                    node.Style.MinDimensions[DimensionWidth].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetMinWidth gets min width
        func (node *Node) StyleGetMinWidth() Value {
            return node.Style.MinDimensions[DimensionWidth]
        }

        // StyleSetMinHeight sets min width
        func (node *Node) StyleSetMinHeight(float minHeight) {
            if node.Style.MinDimensions[DimensionHeight].Value != minHeight ||
                node.Style.MinDimensions[DimensionHeight].Unit != UnitPoint {
                node.Style.MinDimensions[DimensionHeight].Value = minHeight
                node.Style.MinDimensions[DimensionHeight].Unit = UnitPoint
                if FloatIsUndefined(minHeight) {
                    node.Style.MinDimensions[DimensionHeight].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMinHeightPercent sets min height percent
        func (node *Node) StyleSetMinHeightPercent(float minHeight) {
            if node.Style.MinDimensions[DimensionHeight].Value != minHeight ||
                node.Style.MinDimensions[DimensionHeight].Unit != UnitPercent {
                node.Style.MinDimensions[DimensionHeight].Value = minHeight
                node.Style.MinDimensions[DimensionHeight].Unit = UnitPercent
                if FloatIsUndefined(minHeight) {
                    node.Style.MinDimensions[DimensionHeight].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetMinHeight gets min height
        func (node *Node) StyleGetMinHeight() Value {
            return node.Style.MinDimensions[DimensionHeight]
        }

        // StyleSetMaxWidth sets max width
        func (node *Node) StyleSetMaxWidth(float maxWidth) {
            if node.Style.MaxDimensions[DimensionWidth].Value != maxWidth ||
                node.Style.MaxDimensions[DimensionWidth].Unit != UnitPoint {
                node.Style.MaxDimensions[DimensionWidth].Value = maxWidth
                node.Style.MaxDimensions[DimensionWidth].Unit = UnitPoint
                if FloatIsUndefined(maxWidth) {
                    node.Style.MaxDimensions[DimensionWidth].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMaxWidthPercent sets max width percent
        func (node *Node) StyleSetMaxWidthPercent(float maxWidth) {
            if node.Style.MaxDimensions[DimensionWidth].Value != maxWidth ||
                node.Style.MaxDimensions[DimensionWidth].Unit != UnitPercent {
                node.Style.MaxDimensions[DimensionWidth].Value = maxWidth
                node.Style.MaxDimensions[DimensionWidth].Unit = UnitPercent
                if FloatIsUndefined(maxWidth) {
                    node.Style.MaxDimensions[DimensionWidth].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetMaxWidth gets max width
        func (node *Node) StyleGetMaxWidth() Value {
            return node.Style.MaxDimensions[DimensionWidth]
        }

        // StyleSetMaxHeight sets max width
        func (node *Node) StyleSetMaxHeight(float maxHeight) {
            if node.Style.MaxDimensions[DimensionHeight].Value != maxHeight ||
                node.Style.MaxDimensions[DimensionHeight].Unit != UnitPoint {
                node.Style.MaxDimensions[DimensionHeight].Value = maxHeight
                node.Style.MaxDimensions[DimensionHeight].Unit = UnitPoint
                if FloatIsUndefined(maxHeight) {
                    node.Style.MaxDimensions[DimensionHeight].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleSetMaxHeightPercent sets max height percent
        func (node *Node) StyleSetMaxHeightPercent(float maxHeight) {
            if node.Style.MaxDimensions[DimensionHeight].Value != maxHeight ||
                node.Style.MaxDimensions[DimensionHeight].Unit != UnitPercent {
                node.Style.MaxDimensions[DimensionHeight].Value = maxHeight
                node.Style.MaxDimensions[DimensionHeight].Unit = UnitPercent
                if FloatIsUndefined(maxHeight) {
                    node.Style.MaxDimensions[DimensionHeight].Unit = UnitAuto
                }
                nodeMarkDirtyInternal(node)
            }
        }

        // StyleGetMaxHeight gets max height
        func (node *Node) StyleGetMaxHeight() Value {
            return node.Style.MaxDimensions[DimensionHeight]
        }

        // StyleSetAspectRatio sets axpect ratio
        func (node *Node) StyleSetAspectRatio(aspectRatio float) {
            if node.Style.AspectRatio != aspectRatio {
                node.Style.AspectRatio = aspectRatio
                nodeMarkDirtyInternal(node)
            }
        }

        // LayoutGetLeft gets left
        func (node *Node) LayoutGetLeft() float {
            return node.Layout.Position[EdgeLeft]
        }

        // LayoutGetTop gets top
        func (node *Node) LayoutGetTop() float {
            return node.Layout.Position[EdgeTop]
        }

        // LayoutGetRight gets right
        func (node *Node) LayoutGetRight() float {
            return node.Layout.Position[EdgeRight]
        }

        // LayoutGetBottom gets bottom
        func (node *Node) LayoutGetBottom() float {
            return node.Layout.Position[EdgeBottom]
        }

        // LayoutGetWidth gets width
        func (node *Node) LayoutGetWidth() float {
            return node.Layout.Dimensions[DimensionWidth]
        }

        // LayoutGetHeight gets height
        func (node *Node) LayoutGetHeight() float {
            return node.Layout.Dimensions[DimensionHeight]
        }

        // LayoutGetMargin gets margin
        func (node *Node) LayoutGetMargin(Edge edge) float {
            assertWithNode(node, edge < EdgeEnd, "Cannot get layout properties of multi-edge shorthands")
            if edge == EdgeLeft {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Margin[EdgeEnd]
                }
                return node.Layout.Margin[EdgeStart]
            }
            if edge == EdgeRight {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Margin[EdgeStart]
                }
                return node.Layout.Margin[EdgeEnd]
            }
            return node.Layout.Margin[edge]
        }

        // LayoutGetBorder gets border
        func (node *Node) LayoutGetBorder(Edge edge) float {
            assertWithNode(node, edge < EdgeEnd,
                "Cannot get layout properties of multi-edge shorthands")
            if edge == EdgeLeft {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Border[EdgeEnd]
                }
                return node.Layout.Border[EdgeStart]
            }
            if edge == EdgeRight {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Border[EdgeStart]
                }
                return node.Layout.Border[EdgeEnd]
            }
            return node.Layout.Border[edge]
        }

        // LayoutGetPadding gets padding
        func (node *Node) LayoutGetPadding(Edge edge) float {
            assertWithNode(node, edge < EdgeEnd,
                "Cannot get layout properties of multi-edge shorthands")
            if edge == EdgeLeft {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Padding[EdgeEnd]
                }
                return node.Layout.Padding[EdgeStart]
            }
            if edge == EdgeRight {
                if node.Layout.Direction == DirectionRTL {
                    return node.Layout.Padding[EdgeStart]
                }
                return node.Layout.Padding[EdgeEnd]
            }
            return node.Layout.Padding[edge]
        }

    }
}