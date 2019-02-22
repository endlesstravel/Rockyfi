
namespace Rockyfi
{
    class TestCC
    {
        static void assertFloatEqual(float a, float b)
        {

        }



        #region border_test.go
        void TestBorder_no_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetBorder(Edge.Left, 10);
	        root.StyleSetBorder(Edge.Top, 10);
	        root.StyleSetBorder(Edge.Right, 10);
	        root.StyleSetBorder(Edge.Bottom, 10);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(20, root.LayoutGetWidth());
	        assertFloatEqual(20, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(20, root.LayoutGetWidth());
	        assertFloatEqual(20, root.LayoutGetHeight());
        }

        void TestBorder_container_match_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetBorder(Edge.Left, 10);
	        root.StyleSetBorder(Edge.Top, 10);
	        root.StyleSetBorder(Edge.Right, 10);
	        root.StyleSetBorder(Edge.Bottom, 10);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestBorder_flex_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetBorder(Edge.Left, 10);
	        root.StyleSetBorder(Edge.Top, 10);
	        root.StyleSetBorder(Edge.Right, 10);
	        root.StyleSetBorder(Edge.Bottom, 10);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());
        }

        void TestBorder_stretch_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetBorder(Edge.Left, 10);
	        root.StyleSetBorder(Edge.Top, 10);
	        root.StyleSetBorder(Edge.Right, 10);
	        root.StyleSetBorder(Edge.Bottom, 10);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestBorder_center_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetBorder(Edge.Start, 10);
	        root.StyleSetBorder(Edge.End, 20);
	        root.StyleSetBorder(Edge.Bottom, 20);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(40, rootChild0.LayoutGetLeft());
	        assertFloatEqual(35, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(35, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        #endregion


#if false
        #region compute_margin_test.go
        void TestComputed_layout_margin() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);
	        root.StyleSetMarginPercent(Edge.Start, 10);

	        CalculateLayout(root, 100, 100, DirectionLTR);

	        assertFloatEqual(10, root.LayoutGetMargin(Edge.Left));
	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Right));

	        CalculateLayout(root, 100, 100, DirectionRTL);

	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Left));
	        assertFloatEqual(10, root.LayoutGetMargin(Edge.Right));
        }

        #endregion


        #region compute_padding_test.go

        void TestComputed_layout_padding() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);
	        root.StyleSetPaddingPercent(Edge.Start, 10);

	        CalculateLayout(root, 100, 100, DirectionLTR);

	        assertFloatEqual(10, root.LayoutGetPadding(Edge.Left));
	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Right));

	        CalculateLayout(root, 100, 100, DirectionRTL);

	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Left));
	        assertFloatEqual(10, root.LayoutGetPadding(Edge.Right));
        }

        #endregion


        #region default_values_test.go
        void TestAssert_default_values() {
	        var root = Node.CreateDefaultNode();

	        assert.Equal(t, 0, len(root.Children));
	        var nilNode *Node
	        assert.Equal(t, nilNode, root.GetChild(1));
	        assert.Equal(t, nilNode, root.GetChild(0));

	        assert.Equal(t, DirectionInherit, root.Style.Direction);
	        assert.Equal(t, FlexDirection.Column, root.Style.FlexDirection);
	        assert.Equal(t, Justify.FlexStart, root.Style.JustifyContent);
	        assert.Equal(t, Align.FlexStart, root.Style.AlignContent);
	        assert.Equal(t, Align.Stretch, root.Style.AlignItems);
	        assert.Equal(t, Align.Auto, root.Style.AlignSelf);
	        assert.Equal(t, PositionType.Relative, root.Style.PositionType);
	        assert.Equal(t, Wrap.NoWrap, root.Style.FlexWrap);
	        assert.Equal(t, Overflow.Visible, root.Style.Overflow);
	        assertFloatEqual(0, root.StyleGetFlexGrow());
	        assertFloatEqual(0, root.StyleGetFlexShrink());
	        assert.Equal(t, root.Style.FlexBasis.Unit, Unit.Auto);

	        assert.Equal(t, root.StyleGetPosition(Edge.Left).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPosition(Edge.Top).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPosition(Edge.Right).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPosition(Edge.Bottom).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPosition(Edge.Start).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPosition(Edge.End).Unit, Unit.Undefined);

	        assert.Equal(t, root.StyleGetMargin(Edge.Left).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMargin(Edge.Top).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMargin(Edge.Right).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMargin(Edge.Bottom).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMargin(Edge.Start).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMargin(Edge.End).Unit, Unit.Undefined);

	        assert.Equal(t, root.StyleGetPadding(Edge.Left).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPadding(Edge.Top).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPadding(Edge.Right).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPadding(Edge.Bottom).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPadding(Edge.Start).Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetPadding(Edge.End).Unit, Unit.Undefined);

	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.Left)));
	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.Top)));
	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.Right)));
	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.Bottom)));
	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.Start)));
	        assert.True(t, FloatIsUndefined(root.StyleGetBorder(Edge.End)));

	        assert.Equal(t, root.StyleGetWidth().Unit, Unit.Auto);
	        assert.Equal(t, root.StyleGetHeight().Unit, Unit.Auto);
	        assert.Equal(t, root.StyleGetMinWidth().Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMinHeight().Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMaxWidth().Unit, Unit.Undefined);
	        assert.Equal(t, root.StyleGetMaxHeight().Unit, Unit.Undefined);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetRight());
	        assertFloatEqual(0, root.LayoutGetBottom());

	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Left));
	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Top));
	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Right));
	        assertFloatEqual(0, root.LayoutGetMargin(Edge.Bottom));

	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Left));
	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Top));
	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Right));
	        assertFloatEqual(0, root.LayoutGetPadding(Edge.Bottom));

	        assertFloatEqual(0, root.LayoutGetBorder(Edge.Left));
	        assertFloatEqual(0, root.LayoutGetBorder(Edge.Top));
	        assertFloatEqual(0, root.LayoutGetBorder(Edge.Right));
	        assertFloatEqual(0, root.LayoutGetBorder(Edge.Bottom));

	        assert.True(t, FloatIsUndefined(root.LayoutGetWidth()));
	        assert.True(t, FloatIsUndefined(root.LayoutGetHeight()));
	        assert.Equal(t, DirectionInherit, root.Layout.Direction);

        }

        void TestAssert_webdefault_values() {

	        config.UseWebDefaults = true
	        var root = Node.CreateDefaultNode();

	        assert.Equal(t, FlexDirection.Row, root.Style.FlexDirection);
	        assert.Equal(t, Align.Stretch, root.Style.AlignContent);
	        assertFloatEqual(1, root.StyleGetFlexShrink());

        }

        void TestAssert_webdefault_values_reset() {

	        config.UseWebDefaults = true
	        var root = Node.CreateDefaultNode();
	        root.Reset();

	        assert.Equal(t, FlexDirection.Row, root.Style.FlexDirection);
	        assert.Equal(t, Align.Stretch, root.Style.AlignContent);
	        assertFloatEqual(1, root.StyleGetFlexShrink());

        }

        #endregion


        #region dimension_test.go
        void TestWrap_child() {

	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestWrap_grandchild() {


	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(100);
	        rootChild0Child0.StyleSetHeight(100);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());
        }

        #endregion


        #region dirty_marking_test.go
        void TestDirty_propagation() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        rootChild0.StyleSetWidth(20);

	        assert.True(t, rootChild0.IsDirty);
	        assert.False(t, rootChild1.IsDirty);
	        assert.True(t, root.IsDirty);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.False(t, rootChild0.IsDirty);
	        assert.False(t, rootChild1.IsDirty);
	        assert.False(t, root.IsDirty);

        }

        void TestDirty_propagation_only_if_prop_changed() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        rootChild0.StyleSetWidth(50);

	        assert.False(t, rootChild0.IsDirty);
	        assert.False(t, rootChild1.IsDirty);
	        assert.False(t, root.IsDirty);

        }

        void TestDirty_mark_all_children_as_dirty_when_display_changes() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetHeight(100);

	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetFlexGrow(1);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetFlexGrow(1);

	        var child1Child0 = Node.CreateDefaultNode();
	        var child1Child0Child0 = Node.CreateDefaultNode();
	        child1Child0Child0.StyleSetWidth(8);
	        child1Child0Child0.StyleSetHeight(16);

	        child1Child0.InsertChild(child1Child0Child0, 0);

	        child1.InsertChild(child1Child0, 0);
	        root.InsertChild(child0, 0);
	        root.InsertChild(child1, 0);

	        child0.StyleSetDisplay(DisplayFlex);
	        child1.StyleSetDisplay(DisplayNone);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(0, child1Child0Child0.LayoutGetWidth());
	        assertFloatEqual(0, child1Child0Child0.LayoutGetHeight());

	        child0.StyleSetDisplay(DisplayNone);
	        child1.StyleSetDisplay(DisplayFlex);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(8, child1Child0Child0.LayoutGetWidth());
	        assertFloatEqual(16, child1Child0Child0.LayoutGetHeight());

	        child0.StyleSetDisplay(DisplayFlex);
	        child1.StyleSetDisplay(DisplayNone);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(0, child1Child0Child0.LayoutGetWidth());
	        assertFloatEqual(0, child1Child0Child0.LayoutGetHeight());

	        child0.StyleSetDisplay(DisplayNone);
	        child1.StyleSetDisplay(DisplayFlex);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(8, child1Child0Child0.LayoutGetWidth());
	        assertFloatEqual(16, child1Child0Child0.LayoutGetHeight());
        }

        void TestDirty_node_only_if_children_are_actually_removed() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(50);
	        child0.StyleSetHeight(25);
	        root.InsertChild(child0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        var child1 = Node.CreateDefaultNode();
	        root.RemoveChild(child1);
	        assert.False(t, root.IsDirty);

	        root.RemoveChild(child0);
	        assert.True(t, root.IsDirty);
        }

        #endregion


        #region display_test.go
        void TestDisplay_none() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetDisplay(DisplayNone);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());
        }

        void TestDisplay_none_fixed_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(20);
	        rootChild1.StyleSetHeight(20);
	        rootChild1.StyleSetDisplay(DisplayNone);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());
        }

        void TestDisplay_none_with_margin() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Left, 10);
	        rootChild0.StyleSetMargin(Edge.Top, 10);
	        rootChild0.StyleSetMargin(Edge.Right, 10);
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        rootChild0.StyleSetWidth(20);
	        rootChild0.StyleSetHeight(20);
	        rootChild0.StyleSetDisplay(DisplayNone);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());
        }

        void TestDisplay_none_with_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasisPercent(0);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetFlexShrink(1);
	        rootChild1.StyleSetFlexBasisPercent(0);
	        rootChild1.StyleSetDisplay(DisplayNone);
	        root.InsertChild(rootChild1, 1);

	        var rootChild1child0 = Node.CreateDefaultNode();
	        rootChild1child0.StyleSetFlexGrow(1);
	        rootChild1child0.StyleSetFlexShrink(1);
	        rootChild1child0.StyleSetFlexBasisPercent(0);
	        rootChild1child0.StyleSetWidth(20);
	        rootChild1child0.StyleSetMinWidth(0);
	        rootChild1child0.StyleSetMinHeight(0);
	        rootChild1.InsertChild(rootChild1child0, 0);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetFlexShrink(1);
	        rootChild2.StyleSetFlexBasisPercent(0);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(0, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(0, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestDisplay_none_with_position() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetPosition(Edge.Top, 10);
	        rootChild1.StyleSetDisplay(DisplayNone);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());
        }

        #endregion


        #region edge_test.go
        void TestStart_overrides() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Start, 10);
	        rootChild0.StyleSetMargin(Edge.Left, 20);
	        rootChild0.StyleSetMargin(Edge.Right, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetRight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);
	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetRight());
        }

        void TestEnd_overrides() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.End, 10);
	        rootChild0.StyleSetMargin(Edge.Left, 20);
	        rootChild0.StyleSetMargin(Edge.Right, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetRight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);
	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetRight());
        }

        void TestHorizontal_overridden() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Horizontal, 10);
	        rootChild0.StyleSetMargin(Edge.Left, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetRight());
        }

        void TestVertical_overridden() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Vertical, 10);
	        rootChild0.StyleSetMargin(Edge.Top, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetBottom());
        }

        void TestHorizontal_overrides_all() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Horizontal, 10);
	        rootChild0.StyleSetMargin(Edge.All, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetRight());
	        assertFloatEqual(20, rootChild0.LayoutGetBottom());
        }

        void TestVertical_overrides_all() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Vertical, 10);
	        rootChild0.StyleSetMargin(Edge.All, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(20, rootChild0.LayoutGetRight());
	        assertFloatEqual(10, rootChild0.LayoutGetBottom());
        }

        void TestAll_overridden() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Left, 10);
	        rootChild0.StyleSetMargin(Edge.Top, 10);
	        rootChild0.StyleSetMargin(Edge.Right, 10);
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        rootChild0.StyleSetMargin(Edge.All, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetRight());
	        assertFloatEqual(10, rootChild0.LayoutGetBottom());
        }

        #endregion


        #region flex_direction_test.go
        void TestFlex_direction_column_no_height() {

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestFlex_direction_row_no_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(20, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestFlex_direction_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestFlex_direction_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(20, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(80, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(70, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestFlex_direction_column_reverse() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.ColumnReverse);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(90, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(70, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(90, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(70, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestFlex_direction_row_reverse() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.RowReverse);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(80, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(70, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(20, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        #endregion


        #region flex_test.go
        void TestFlex_basis_flex_grow_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(75, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(75, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());
        }

        void TestFlex_basis_flex_grow_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(75, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(25, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(25, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(75, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(25, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());
        }

        void TestFlex_basis_flex_shrink_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasis(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexBasis(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());
        }

        void TestFlex_basis_flex_shrink_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasis(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexBasis(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());
        }

        void TestFlex_shrink_to_zero() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(75);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexShrink(1);
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(50);
	        rootChild2.StyleSetHeight(50);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(75, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(75, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());
        }

        void TestFlex_basis_overrides_main_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(20, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(20, rootChild2.LayoutGetHeight());
        }

        void TestFlex_grow_shrink_at_most() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetFlexShrink(1);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetHeight());
        }

        void TestFlex_grow_less_than_factor_one() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(0.2);
	        rootChild0.StyleSetFlexBasis(40);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(0.2);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(0.4);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(132, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(132, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(92, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(224, rootChild2.LayoutGetTop());
	        assertFloatEqual(200, rootChild2.LayoutGetWidth());
	        assertFloatEqual(184, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(132, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(132, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(92, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(224, rootChild2.LayoutGetTop());
	        assertFloatEqual(200, rootChild2.LayoutGetWidth());
	        assertFloatEqual(184, rootChild2.LayoutGetHeight());
        }

        #endregion


        #region flex_wrap_test.go
        void TestWrap_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(30);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(30);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(30);
	        root.InsertChild(rootChild3, 3);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(60, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(30, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(60, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(30, rootChild3.LayoutGetLeft());
	        assertFloatEqual(0, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(60, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(30, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(30, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(30, rootChild2.LayoutGetLeft());
	        assertFloatEqual(60, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(0, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());
        }

        void TestWrap_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(30);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(30);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(30);
	        root.InsertChild(rootChild3, 3);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(30, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(30, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());
        }

        void TestWrap_row_align_items_flex_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.FlexEnd);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(30);
	        root.InsertChild(rootChild3, 3);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());
        }

        void TestWrap_row_align_items_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(30);
	        root.InsertChild(rootChild3, 3);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(5, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(60, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(5, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(30, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(30, rootChild3.LayoutGetHeight());
        }

        void TestFlex_wrap_children_with_min_main_overriding_flex_basis() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetMinWidth(55);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexBasis(50);
	        rootChild1.StyleSetMinWidth(55);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(55, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(55, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(45, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(55, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(45, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(55, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());
        }

        void TestFlex_wrap_wrap_to_child_height() {


	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetAlignItems(Align.FlexStart);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(100);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0Child0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0Child0.StyleSetWidth(100);
	        rootChild0Child0Child0.StyleSetHeight(100);
	        rootChild0Child0.InsertChild(rootChild0Child0Child0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(100);
	        rootChild1.StyleSetHeight(100);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(100, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(100, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());
        }

        void TestFlex_wrap_align_stretch_fits_one_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(150);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(150, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(150, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());
        }

        void TestWrap_reverse_row_align_content_flex_start() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(30, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(40, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrap_reverse_row_align_content_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignContent(Align.Center);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(30, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(40, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrap_reverse_row_single_line_different_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(300);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(300, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(40, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(90, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(120, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(300, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(270, rootChild0.LayoutGetLeft());
	        assertFloatEqual(40, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(240, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(210, rootChild2.LayoutGetLeft());
	        assertFloatEqual(20, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(180, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(150, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrap_reverse_row_align_content_stretch() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignContent(Align.Stretch);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(30, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(40, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrap_reverse_row_align_content_space_around() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignContent(Align.SpaceAround);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(60, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(30, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(80, root.LayoutGetHeight());

	        assertFloatEqual(70, rootChild0.LayoutGetLeft());
	        assertFloatEqual(70, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(60, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(10, rootChild2.LayoutGetLeft());
	        assertFloatEqual(50, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(70, rootChild3.LayoutGetLeft());
	        assertFloatEqual(10, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(40, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrap_reverse_column_fixed_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetFlexWrap(Wrap.WrapReverse);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(30);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(30);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(30);
	        rootChild2.StyleSetHeight(30);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetWidth(30);
	        rootChild3.StyleSetHeight(40);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetWidth(30);
	        rootChild4.StyleSetHeight(50);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(170, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(170, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(170, rootChild2.LayoutGetLeft());
	        assertFloatEqual(30, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(170, rootChild3.LayoutGetLeft());
	        assertFloatEqual(60, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(140, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(30, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(30, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(30, rootChild2.LayoutGetTop());
	        assertFloatEqual(30, rootChild2.LayoutGetWidth());
	        assertFloatEqual(30, rootChild2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild3.LayoutGetLeft());
	        assertFloatEqual(60, rootChild3.LayoutGetTop());
	        assertFloatEqual(30, rootChild3.LayoutGetWidth());
	        assertFloatEqual(40, rootChild3.LayoutGetHeight());

	        assertFloatEqual(30, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(30, rootChild4.LayoutGetWidth());
	        assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestWrapped_row_within_align_items_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(150);
	        rootChild0Child0.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetWidth(80);
	        rootChild0child1.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(120, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());
        }

        void TestWrapped_row_within_align_items_flex_start() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(150);
	        rootChild0Child0.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetWidth(80);
	        rootChild0child1.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(120, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());
        }

        void TestWrapped_row_within_align_items_flex_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexEnd);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(150);
	        rootChild0Child0.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetWidth(80);
	        rootChild0child1.StyleSetHeight(80);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(160, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(120, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(80, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0child1.LayoutGetHeight());
        }

        void TestWrapped_column_max_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetAlignContent(Align.Center);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(700);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(500);
	        rootChild0.StyleSetMaxHeight(200);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetMargin(Edge.Left, 20);
	        rootChild1.StyleSetMargin(Edge.Top, 20);
	        rootChild1.StyleSetMargin(Edge.Right, 20);
	        rootChild1.StyleSetMargin(Edge.Bottom, 20);
	        rootChild1.StyleSetWidth(200);
	        rootChild1.StyleSetHeight(200);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(100);
	        rootChild2.StyleSetHeight(100);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(700, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(250, rootChild0.LayoutGetLeft());
	        assertFloatEqual(30, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(200, rootChild1.LayoutGetLeft());
	        assertFloatEqual(250, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());

	        assertFloatEqual(420, rootChild2.LayoutGetLeft());
	        assertFloatEqual(200, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(700, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(350, rootChild0.LayoutGetLeft());
	        assertFloatEqual(30, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(300, rootChild1.LayoutGetLeft());
	        assertFloatEqual(250, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());

	        assertFloatEqual(180, rootChild2.LayoutGetLeft());
	        assertFloatEqual(200, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestWrapped_column_max_height_flex() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetAlignContent(Align.Center);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetFlexWrap(Wrap.Wrap);
	        root.StyleSetWidth(700);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasisPercent(0);
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(500);
	        rootChild0.StyleSetMaxHeight(200);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetFlexShrink(1);
	        rootChild1.StyleSetFlexBasisPercent(0);
	        rootChild1.StyleSetMargin(Edge.Left, 20);
	        rootChild1.StyleSetMargin(Edge.Top, 20);
	        rootChild1.StyleSetMargin(Edge.Right, 20);
	        rootChild1.StyleSetMargin(Edge.Bottom, 20);
	        rootChild1.StyleSetWidth(200);
	        rootChild1.StyleSetHeight(200);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(100);
	        rootChild2.StyleSetHeight(100);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(700, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(300, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(180, rootChild0.LayoutGetHeight());

	        assertFloatEqual(250, rootChild1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(180, rootChild1.LayoutGetHeight());

	        assertFloatEqual(300, rootChild2.LayoutGetLeft());
	        assertFloatEqual(400, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(700, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(300, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(180, rootChild0.LayoutGetHeight());

	        assertFloatEqual(250, rootChild1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(180, rootChild1.LayoutGetHeight());

	        assertFloatEqual(300, rootChild2.LayoutGetLeft());
	        assertFloatEqual(400, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestWrap_nodes_with_content_sizing_overflowing_margin() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(500);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        rootChild0.StyleSetWidth(85);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0Child0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0Child0.StyleSetWidth(40);
	        rootChild0Child0Child0.StyleSetHeight(40);
	        rootChild0Child0.InsertChild(rootChild0Child0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetMargin(Edge.Right, 10);
	        rootChild0.InsertChild(rootChild0child1, 1);

	        var rootChild0child1Child0 = Node.CreateDefaultNode();
	        rootChild0child1Child0.StyleSetWidth(40);
	        rootChild0child1Child0.StyleSetHeight(40);
	        rootChild0child1.InsertChild(rootChild0child1Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(85, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(40, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(415, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(85, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        assertFloatEqual(45, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(35, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(40, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetHeight());
        }

        void TestWrap_nodes_with_content_sizing_margin_cross() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(500);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetFlexWrap(Wrap.Wrap);
	        rootChild0.StyleSetWidth(70);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0Child0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0Child0.StyleSetWidth(40);
	        rootChild0Child0Child0.StyleSetHeight(40);
	        rootChild0Child0.InsertChild(rootChild0Child0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetMargin(Edge.Top, 10);
	        rootChild0.InsertChild(rootChild0child1, 1);

	        var rootChild0child1Child0 = Node.CreateDefaultNode();
	        rootChild0child1Child0.StyleSetWidth(40);
	        rootChild0child1Child0.StyleSetHeight(40);
	        rootChild0child1.InsertChild(rootChild0child1Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(70, rootChild0.LayoutGetWidth());
	        assertFloatEqual(90, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(430, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(70, rootChild0.LayoutGetWidth());
	        assertFloatEqual(90, rootChild0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(30, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1Child0.LayoutGetTop());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetWidth());
	        assertFloatEqual(40, rootChild0child1Child0.LayoutGetHeight());
        }

        #endregion


        #region had_overflow_test.go
        func newHadOverflowTests() (*Config, *Node) {

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetFlexWrap(Wrap.NoWrap);
	        return config, root
        }

        void TestChildren_overflow_no_wrap_and_no_flex_children() {
	        config, root := newHadOverflowTests();
	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(80);
	        child0.StyleSetHeight(40);
	        child0.StyleSetMargin(Edge.Top, 10);
	        child0.StyleSetMargin(Edge.Bottom, 15);
	        root.InsertChild(child0, 0);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetWidth(80);
	        child1.StyleSetHeight(40);
	        child1.StyleSetMargin(Edge.Bottom, 5);
	        root.InsertChild(child1, 1);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.True(t, root.Layout.HadOverflow);
        }

        void TestSpacing_overflow_no_wrap_and_no_flex_children() {
	        config, root := newHadOverflowTests();
	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(80);
	        child0.StyleSetHeight(40);
	        child0.StyleSetMargin(Edge.Top, 10);
	        child0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(child0, 0);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetWidth(80);
	        child1.StyleSetHeight(40);
	        child1.StyleSetMargin(Edge.Bottom, 5);
	        root.InsertChild(child1, 1);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.True(t, root.Layout.HadOverflow);
        }

        void TestNo_overflow_no_wrap_and_flex_children() {
	        config, root := newHadOverflowTests();
	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(80);
	        child0.StyleSetHeight(40);
	        child0.StyleSetMargin(Edge.Top, 10);
	        child0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(child0, 0);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetWidth(80);
	        child1.StyleSetHeight(40);
	        child1.StyleSetMargin(Edge.Bottom, 5);
	        child1.StyleSetFlexShrink(1);
	        root.InsertChild(child1, 1);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.False(t, root.Layout.HadOverflow);
        }

        void TestHadOverflow_gets_reset_if_not_logger_valid() {
	        config, root := newHadOverflowTests();
	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(80);
	        child0.StyleSetHeight(40);
	        child0.StyleSetMargin(Edge.Top, 10);
	        child0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(child0, 0);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetWidth(80);
	        child1.StyleSetHeight(40);
	        child1.StyleSetMargin(Edge.Bottom, 5);
	        root.InsertChild(child1, 1);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.True(t, root.Layout.HadOverflow);

	        child1.StyleSetFlexShrink(1);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.False(t, root.Layout.HadOverflow);
        }

        void TestSpacing_overflow_in_nested_nodes() {
	        config, root := newHadOverflowTests();
	        var child0 = Node.CreateDefaultNode();
	        child0.StyleSetWidth(80);
	        child0.StyleSetHeight(40);
	        child0.StyleSetMargin(Edge.Top, 10);
	        child0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(child0, 0);
	        var child1 = Node.CreateDefaultNode();
	        child1.StyleSetWidth(80);
	        child1.StyleSetHeight(40);
	        root.InsertChild(child1, 1);
	        var child1_1 = Node.CreateDefaultNode();
	        child1_1.StyleSetWidth(80);
	        child1_1.StyleSetHeight(40);
	        child1_1.StyleSetMargin(Edge.Bottom, 5);
	        child1.InsertChild(child1_1, 0);

	        CalculateLayout(root, 200, 100, DirectionLTR);

	        assert.True(t, root.Layout.HadOverflow);
        }

        #endregion


        #region issue5_test.go
        #endregion


        #region justify_content_test.go
        void TestJustify_content_row_flex_start() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(20, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(92, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(82, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(72, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_row_flex_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetJustifyContent(Justify.FlexEnd);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(72, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(82, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(92, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_row_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(36, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(56, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(56, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(36, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_row_space_between() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetJustifyContent(Justify.SpaceBetween);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(92, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(92, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_row_space_around() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetJustifyContent(Justify.SpaceAround);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(12, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(80, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(102, rootChild0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(102, rootChild1.LayoutGetHeight());

	        assertFloatEqual(12, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(102, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_column_flex_start() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(10, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(10, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(10, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_column_flex_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.FlexEnd);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(72, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(82, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(92, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(72, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(82, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(92, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_column_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(36, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(56, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(36, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(56, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_column_space_between() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.SpaceBetween);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(92, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(92, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestJustify_content_column_space_around() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.SpaceAround);
	        root.StyleSetWidth(102);
	        root.StyleSetHeight(102);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(12, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(102, root.LayoutGetWidth());
	        assertFloatEqual(102, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(12, rootChild0.LayoutGetTop());
	        assertFloatEqual(102, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(46, rootChild1.LayoutGetTop());
	        assertFloatEqual(102, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(102, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        #endregion


        #region margin_test.go
        void TestMargin_start() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Start, 10);
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_top() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Top, 10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

        }

        void TestMargin_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetJustifyContent(Justify.FlexEnd);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.End, 10);
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_bottom() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.FlexEnd);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

        }

        void TestMargin_and_flex_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Start, 10);
	        rootChild0.StyleSetMargin(Edge.End, 10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_and_flex_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Top, 10);
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

        }

        void TestMargin_and_stretch_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Top, 10);
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

        }

        void TestMargin_and_stretch_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Start, 10);
	        rootChild0.StyleSetMargin(Edge.End, 10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_with_sibling_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.End, 10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(45, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(55, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(45, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(55, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(45, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(45, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

        }

        void TestMargin_with_sibling_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMargin(Edge.Bottom, 10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(45, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(55, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(45, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(45, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(55, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(45, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_bottom() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Bottom);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_top() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Top);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(100, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(100, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_bottom_and_top() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Top);
	        rootChild0.StyleSetMarginAuto(Edge.Bottom);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_bottom_and_top_justify_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Top);
	        rootChild0.StyleSetMarginAuto(Edge.Bottom);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_mutiple_children_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Top);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetMarginAuto(Edge.Top);
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(50);
	        rootChild2.StyleSetHeight(50);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(100, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(75, rootChild2.LayoutGetLeft());
	        assertFloatEqual(150, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(100, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(75, rootChild2.LayoutGetLeft());
	        assertFloatEqual(150, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

        }

        void TestMargin_auto_mutiple_children_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetMarginAuto(Edge.Right);
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(50);
	        rootChild2.StyleSetHeight(50);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(150, rootChild2.LayoutGetLeft());
	        assertFloatEqual(75, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(125, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(75, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

        }

        void Testargin_auto_left_and_right_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_left_and_right() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_start_and_end_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Start);
	        rootChild0.StyleSetMarginAuto(Edge.End);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(75, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_start_and_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Start);
	        rootChild0.StyleSetMarginAuto(Edge.End);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_left_and_right_column_and_center() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_left() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_right() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_left_and_right_strech() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_auto_top_and_bottom_strech() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Top);
	        rootChild0.StyleSetMarginAuto(Edge.Bottom);
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(50, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild1.LayoutGetLeft());
	        assertFloatEqual(150, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMargin_should_not_be_part_of_max_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(250);
	        root.StyleSetHeight(250);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Top, 20);
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(100);
	        rootChild0.StyleSetMaxHeight(100);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(250, root.LayoutGetWidth());
	        assertFloatEqual(250, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(250, root.LayoutGetWidth());
	        assertFloatEqual(250, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_should_not_be_part_of_max_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(250);
	        root.StyleSetHeight(250);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Left, 20);
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetMaxWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(250, root.LayoutGetWidth());
	        assertFloatEqual(250, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(250, root.LayoutGetWidth());
	        assertFloatEqual(250, root.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestMargin_auto_left_right_child_bigger_than_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(52);
	        root.StyleSetHeight(52);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(72);
	        rootChild0.StyleSetHeight(72);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(-20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

        }

        void TestMargin_auto_left_child_bigger_than_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(52);
	        root.StyleSetHeight(52);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetWidth(72);
	        rootChild0.StyleSetHeight(72);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(-20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

        }

        void TestMargin_fix_left_auto_right_child_bigger_than_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(52);
	        root.StyleSetHeight(52);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMargin(Edge.Left, 10);
	        rootChild0.StyleSetMarginAuto(Edge.Right);
	        rootChild0.StyleSetWidth(72);
	        rootChild0.StyleSetHeight(72);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(-20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

        }

        void TestMargin_auto_left_fix_right_child_bigger_than_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(52);
	        root.StyleSetHeight(52);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMarginAuto(Edge.Left);
	        rootChild0.StyleSetMargin(Edge.Right, 10);
	        rootChild0.StyleSetWidth(72);
	        rootChild0.StyleSetHeight(72);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(52, root.LayoutGetWidth());
	        assertFloatEqual(52, root.LayoutGetHeight());

	        assertFloatEqual(-30, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-10, rootChild0.LayoutGetTop());
	        assertFloatEqual(72, rootChild0.LayoutGetWidth());
	        assertFloatEqual(72, rootChild0.LayoutGetHeight());

        }

        #endregion


        #region math_test.go
        #endregion


        #region measure_cache_test.go
        func measureMax(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        measureCount := node.Context.(int);
	        measureCount++
	        node.Context = measureCount

	        if widthMode == MeasureModeUndefined {
		        width = 10
	        }
	        if heightMode == MeasureModeUndefined {
		        height = 10
	        }
	        return Size{
		        Width:  width,
		        Height: height,
	        }
        }

        func measureMin(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        measureCount := node.Context.(int);
	        measureCount++
	        node.Context = measureCount

	        if widthMode == MeasureModeUndefined || (widthMode == MeasureModeAtMost && width > 10) {
		        width = 10
	        }
	        if heightMode == MeasureModeUndefined || (heightMode == MeasureModeAtMost && height > 10) {
		        height = 10
	        }
	        return Size{
		        Width:  width,
		        Height: height,
	        }
        }

        func measure8449(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        measureCount, ok := node.Context.(int);
	        if ok {
		        measureCount++
		        node.Context = measureCount
	        }

	        return Size{
		        Width:  84,
		        Height: 49,
	        }
        }

        void TestMeasure_once_single_flexible_child() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMax);
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        measureCount = rootChild0.Context.(int);
	        assert.Equal(t, 1, measureCount);

        }

        void TestRemeasure_with_same_exact_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, 100, DirectionLTR);
	        CalculateLayout(root, 100, 50, DirectionLTR);

	        measureCount = rootChild0.Context.(int);
	        assert.Equal(t, 1, measureCount);

        }

        void TestRemeasure_with_same_atmost_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, 100, DirectionLTR);
	        CalculateLayout(root, 100, 50, DirectionLTR);

	        measureCount = rootChild0.Context.(int);
	        assert.Equal(t, 1, measureCount);

        }

        void TestRemeasure_with_computed_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, 100, DirectionLTR);
	        root.StyleSetAlignItems(Align.Stretch);
	        CalculateLayout(root, 10, 50, DirectionLTR);

	        measureCount = rootChild0.Context.(int);
	        assert.Equal(t, 1, measureCount);

        }

        void TestRemeasure_with_atmost_computed_width_undefined_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, Undefined, DirectionLTR);
	        CalculateLayout(root, 10, Undefined, DirectionLTR);

	        measureCount = rootChild0.Context.(int);
	        assert.Equal(t, 1, measureCount);
        }

        void TestRemeasure_with_already_measured_value_smaller_but_still_float_equal() {
	        measureCount := 0

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(288);
	        root.StyleSetHeight(288);
	        root.StyleSetFlexDirection(FlexDirection.Row);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPadding(Edge.All, 2.88);
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.Context = measureCount
	        rootChild0Child0.SetMeasureFunc(measure8449);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        measureCount = rootChild0Child0.Context.(int);
	        assert.Equal(t, 1, measureCount);
        }

        #endregion


        #region measure_mode_test.go
        type measureConstraint struct {
	        width      float32
	        widthMode  MeasureMode
	        height     float32
	        heightMode MeasureMode
        }

        type measureConstraintList struct {
	        length      int
	        constraints []measureConstraint
        }

        func _measure2(node *Node,
	        width float32,
	        widthMode MeasureMode,
	        height float32,
	        heightMode MeasureMode) Size {
	        constraintList := node.Context.(*measureConstraintList);
	        constraints := constraintList.constraints
	        currentIndex := constraintList.length
	        (&constraints[currentIndex]).width = width
	        (&constraints[currentIndex]).widthMode = widthMode
	        (&constraints[currentIndex]).height = height
	        (&constraints[currentIndex]).heightMode = heightMode
	        constraintList.length = currentIndex + 1

	        if widthMode == MeasureModeUndefined {
		        width = 10
	        }

	        if heightMode == MeasureModeUndefined {
		        height = 10
	        } else {
		        height = width // TODO:: is it a bug in tests ?
	        }
	        return Size{
		        Width:  width,
		        Height: height,
	        }
        }

        void TestExactly_measure_stretched_child_column() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assert.Equal(t, MeasureMode.Exactly, constraintList.constraints[0].widthMode);
        }

        void TestExactly_measure_stretched_child_row() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureMode.Exactly, constraintList.constraints[0].heightMode);
        }

        void TestAt_most_main_axis_column() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].heightMode);
        }

        void TestAt_most_cross_axis_column() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].widthMode);
        }

        void TestAt_most_main_axis_row() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].widthMode);
        }

        void TestAt_most_cross_axis_row() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].heightMode);
        }

        void TestFlex_child() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 2, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].heightMode);

	        assertFloatEqual(100, constraintList.constraints[1].height);
	        assert.Equal(t, MeasureMode.Exactly, constraintList.constraints[1].heightMode);
        }

        void TestFlex_child_with_flex_basis() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(0);
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureMode.Exactly, constraintList.constraints[0].heightMode);
        }

        void TestOverflow_scroll_column() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetOverflow(Overflow.Scroll);
	        root.StyleSetHeight(100);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].widthMode);

	        assert.True(t, FloatIsUndefined(constraintList.constraints[0].height));
	        assert.Equal(t, MeasureModeUndefined, constraintList.constraints[0].heightMode);
        }

        void TestOverflow_scroll_row() {
	        constraintList := measureConstraintList{
		        constraints: make([]measureConstraint, 10),
	        }

	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetOverflow(Overflow.Scroll);
	        root.StyleSetHeight(100);
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &constraintList
	        rootChild0.SetMeasureFunc(_measure2);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, constraintList.length);

	        assert.True(t, FloatIsUndefined(constraintList.constraints[0].width));
	        assert.Equal(t, MeasureModeUndefined, constraintList.constraints[0].widthMode);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assert.Equal(t, MeasureModeAtMost, constraintList.constraints[0].heightMode);
        }

        #endregion


        #region measure_test.go
        func _measure3(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        measureCount, ok := node.Context.(*int);
	        if ok {
		        (*measureCount)++
	        }

	        return Size{Width: 10, Height: 10}
        }

        func _simulate_wrapping_text(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        if widthMode == MeasureModeUndefined || width >= 68 {
		        return Size{Width: 68, Height: 16}
	        }

	        return Size{Width: 50, Height: 32}
        }

        func _measure_assert_negative(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        if width < 0 {
		        panic(fmt.Sprintf("width is %.2f and should be >= 0", width));
	        }
	        if height < 0 {
		        panic(fmt.Sprintf("height is %.2f should be >= 0, height", height));
	        }
	        // EXPECT_GE(width, 0);
	        //EXPECT_GE(height, 0);

	        return Size{
		        Width: 0, Height: 0,
	        }
        }

        void TestDont_measure_single_grow_shrink_child() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure);
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexShrink(1);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 0, measureCount);
        }

        void TestMeasure_absolute_child_with_no_constraints() {
	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild0, 0);

	        measureCount := 0

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetPositionType(PositionType.Absolute);
	        rootChild0Child0.Context = &measureCount
	        rootChild0Child0.SetMeasureFunc(_measure3);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 1, measureCount);
        }

        void TestDont_measure_when_min_equals_max() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure3);
	        rootChild0.StyleSetMinWidth(10);
	        rootChild0.StyleSetMaxWidth(10);
	        rootChild0.StyleSetMinHeight(10);
	        rootChild0.StyleSetMaxHeight(10);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 0, measureCount);
	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestDont_measure_when_min_equals_max_percentages() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure3);
	        rootChild0.StyleSetMinWidthPercent(10);
	        rootChild0.StyleSetMaxWidthPercent(10);
	        rootChild0.StyleSetMinHeightPercent(10);
	        rootChild0.StyleSetMaxHeightPercent(10);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 0, measureCount);
	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestDont_measure_when_min_equals_max_mixed_width_percent() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure3);
	        rootChild0.StyleSetMinWidthPercent(10);
	        rootChild0.StyleSetMaxWidthPercent(10);
	        rootChild0.StyleSetMinHeight(10);
	        rootChild0.StyleSetMaxHeight(10);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 0, measureCount);
	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestDont_measure_when_min_equals_max_mixed_height_percent() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure3);
	        rootChild0.StyleSetMinWidth(10);
	        rootChild0.StyleSetMaxWidth(10);
	        rootChild0.StyleSetMinHeightPercent(10);
	        rootChild0.StyleSetMaxHeightPercent(10);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assert.Equal(t, 0, measureCount);
	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestMeasure_enough_size_should_be_in_single_line() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetAlignSelf(Align.FlexStart);
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);

	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(68, rootChild0.LayoutGetWidth());
	        assertFloatEqual(16, rootChild0.LayoutGetHeight());
        }

        void TestMeasure_not_enough_size_should_wrap() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(55);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetAlignSelf(Align.FlexStart);
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);

	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(32, rootChild0.LayoutGetHeight());
        }

        void TestMeasure_zero_space_should_grow() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(200);
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetFlexGrow(0);

	        measureCount := 0

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Column);
	        rootChild0.StyleSetPadding(Edge.All, 100);
	        rootChild0.Context = &measureCount
	        rootChild0.SetMeasureFunc(_measure3);

	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 282, Undefined, DirectionLTR);

	        assertFloatEqual(282, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
        }

        void TestMeasure_flex_direction_row_and_padding() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetPadding(Edge.Left, 25);
	        root.StyleSetPadding(Edge.Top, 25);
	        root.StyleSetPadding(Edge.Right, 25);
	        root.StyleSetPadding(Edge.Bottom, 25);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(25, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(75, rootChild1.LayoutGetLeft());
	        assertFloatEqual(25, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_flex_direction_column_and_padding() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetPadding(Edge.All, 25);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(25, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(32, rootChild0.LayoutGetHeight());

	        assertFloatEqual(25, rootChild1.LayoutGetLeft());
	        assertFloatEqual(57, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_flex_direction_row_no_padding() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_flex_direction_row_no_padding_align_items_flexstart() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(32, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_with_fixed_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetPadding(Edge.All, 25);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(25, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(25, rootChild1.LayoutGetLeft());
	        assertFloatEqual(35, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_with_flex_shrink() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetPadding(Edge.All, 25);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        rootChild0.StyleSetFlexShrink(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(25, rootChild0.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(25, rootChild1.LayoutGetLeft());
	        assertFloatEqual(25, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        void TestMeasure_no_padding() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMargin(Edge.Top, 20);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_simulate_wrapping_text);
	        rootChild0.StyleSetFlexShrink(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(5);
	        rootChild1.StyleSetHeight(5);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(20, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(32, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(32, rootChild1.LayoutGetTop());
	        assertFloatEqual(5, rootChild1.LayoutGetWidth());
	        assertFloatEqual(5, rootChild1.LayoutGetHeight());
        }

        /*
#if GTEST_HAS_DEATH_TEST
        TEST(YogaDeathTest, cannot_add_child_to_node_with_measure_func) {
          root := YGNodeNew();
          YGroot.SetMeasureFunc(_measure3);

          rootChild0 := YGNodeNew();
          ASSERT_DEATH(YGroot.InsertChild(rootChild0, 0), "Cannot add child.*");
          YGNodeFree(rootChild0);
          ;
        }

        TEST(YogaDeathTest, cannot_add_nonnull_measure_func_to_non_leaf_node) {
          root := YGNodeNew();
          rootChild0 := YGNodeNew();
          YGroot.InsertChild(rootChild0, 0);

          ASSERT_DEATH(YGroot.SetMeasureFunc(_measure3), "Cannot set measure function.*");
          ;
        }
#endif
        */

        void TestCan_nullify_measure_func_on_any_node() {
	        var root = Node.CreateDefaultNode();
	        root.InsertChild(NewNode(), 0);

	        root.SetMeasureFunc(nil);
	        assert.True(t, root.Measure == nil);
        }

        void TestCant_call_negative_measure() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Column);
	        root.StyleSetWidth(50);
	        root.StyleSetHeight(10);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_measure_assert_negative);
	        rootChild0.StyleSetMargin(Edge.Top, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
        }

        void TestCant_call_negative_measure_horizontal() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(10);
	        root.StyleSetHeight(20);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_measure_assert_negative);
	        rootChild0.StyleSetMargin(Edge.Start, 20);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
        }

        #endregion


        #region min_max_dimension_test.go
        void TestMax_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMaxWidth(50);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

        }

        void TestMax_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetMaxHeight(50);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

        }

        void TestMin_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMinHeight(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(80, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

        }

        void TestMin_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMinWidth(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(80, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(20, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(20, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

        }

        void TestJustify_content_min_max() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetWidth(100);
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(60);
	        rootChild0.StyleSetHeight(60);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(40, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

        }

        void TestAlign_items_min_max() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetMinWidth(100);
	        root.StyleSetMaxWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(60);
	        rootChild0.StyleSetHeight(60);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

        }

        void TestJustify_content_overflow_min_max() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(110);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(50);
	        rootChild2.StyleSetHeight(50);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(110, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-20, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(50, root.LayoutGetWidth());
	        assertFloatEqual(110, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-20, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(30, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(80, rootChild2.LayoutGetTop());
	        assertFloatEqual(50, rootChild2.LayoutGetWidth());
	        assertFloatEqual(50, rootChild2.LayoutGetHeight());

        }

        void TestFlex_grow_to_min() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexShrink(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestFlex_grow_in_at_most_container() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetFlexBasis(0);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetHeight());

        }

        void TestFlex_grow_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(0);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_min_max_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestFlex_grow_within_max_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetMaxWidth(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetHeight(20);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_max_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetMaxWidth(300);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetHeight(20);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetHeight());

        }

        void TestFlex_root_ignored() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexGrow(1);
	        root.StyleSetWidth(100);
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(200);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(100);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(300, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(300, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

        }

        void TestFlex_grow_root_minimized() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetMinHeight(100);
	        root.StyleSetMaxHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMinHeight(100);
	        rootChild0.StyleSetMaxHeight(500);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetFlexBasis(200);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetHeight(100);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(300, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(300, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(100, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(300, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(300, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(200, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(100, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

        }

        void TestFlex_grow_height_maximized() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMinHeight(100);
	        rootChild0.StyleSetMaxHeight(500);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetFlexBasis(200);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetHeight(100);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(500, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(400, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(400, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(100, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(500, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(400, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(400, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(100, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_min_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetMinWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_min_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetMinHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(0, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_max_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetMaxWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexShrink(1);
	        rootChild0Child0.StyleSetFlexBasis(100);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetWidth(50);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(50, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(50, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0child1.LayoutGetHeight());

        }

        void TestFlex_grow_within_constrained_max_column() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetMaxHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasis(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetHeight(50);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestChild_min_max_width_flexing() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(120);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(0);
	        rootChild0.StyleSetMinWidth(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetFlexBasisPercent(50);
	        rootChild1.StyleSetMaxWidth(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(120, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(100, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(20, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(120, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(20, rootChild1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild1.LayoutGetHeight());

        }

        void TestMin_width_overrides_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(50);
	        root.StyleSetMinWidth(100);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

        }

        void TestMax_width_overrides_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetMaxWidth(100);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

        }

        void TestMin_height_overrides_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(50);
	        root.StyleSetMinHeight(100);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

        }

        void TestMax_height_overrides_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(200);
	        root.StyleSetMaxHeight(100);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

        }

        void TestMin_max_percent_no_width_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMinWidthPercent(10);
	        rootChild0.StyleSetMaxWidthPercent(10);
	        rootChild0.StyleSetMinHeightPercent(10);
	        rootChild0.StyleSetMaxHeightPercent(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

        }
        #endregion


        #region node_child_test.go
        void TestReset_layout_when_child_removed() {
	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        root.RemoveChild(rootChild0);

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assert.True(t, FloatIsUndefined(rootChild0.LayoutGetWidth()));
	        assert.True(t, FloatIsUndefined(rootChild0.LayoutGetHeight()));
        }

        #endregion


        #region padding_test.go
        void TestPadding_no_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPadding(Edge.Left, 10);
	        root.StyleSetPadding(Edge.Top, 10);
	        root.StyleSetPadding(Edge.Right, 10);
	        root.StyleSetPadding(Edge.Bottom, 10);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(20, root.LayoutGetWidth());
	        assertFloatEqual(20, root.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(20, root.LayoutGetWidth());
	        assertFloatEqual(20, root.LayoutGetHeight());
        }

        void TestPadding_container_match_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPadding(Edge.Left, 10);
	        root.StyleSetPadding(Edge.Top, 10);
	        root.StyleSetPadding(Edge.Right, 10);
	        root.StyleSetPadding(Edge.Bottom, 10);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(30, root.LayoutGetWidth());
	        assertFloatEqual(30, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestPadding_flex_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPadding(Edge.Left, 10);
	        root.StyleSetPadding(Edge.Top, 10);
	        root.StyleSetPadding(Edge.Right, 10);
	        root.StyleSetPadding(Edge.Bottom, 10);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(80, rootChild0.LayoutGetHeight());
        }

        void TestPadding_stretch_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPadding(Edge.Left, 10);
	        root.StyleSetPadding(Edge.Top, 10);
	        root.StyleSetPadding(Edge.Right, 10);
	        root.StyleSetPadding(Edge.Bottom, 10);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(80, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestPadding_center_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetPadding(Edge.Start, 10);
	        root.StyleSetPadding(Edge.End, 20);
	        root.StyleSetPadding(Edge.Bottom, 20);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(40, rootChild0.LayoutGetLeft());
	        assertFloatEqual(35, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(35, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestChild_with_padding_align_end() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.FlexEnd);
	        root.StyleSetAlignItems(Align.FlexEnd);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPadding(Edge.Left, 20);
	        rootChild0.StyleSetPadding(Edge.Top, 20);
	        rootChild0.StyleSetPadding(Edge.Right, 20);
	        rootChild0.StyleSetPadding(Edge.Bottom, 20);
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(100, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(100, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        #endregion


        #region percentage_test.go
        void TestPercentage_width_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidthPercent(30);
	        rootChild0.StyleSetHeightPercent(30);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(140, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());
        }

        void TestPercentage_position_left_top() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(400);
	        root.StyleSetHeight(400);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPositionPercent(Edge.Left, 10);
	        rootChild0.StyleSetPositionPercent(Edge.Top, 20);
	        rootChild0.StyleSetWidthPercent(45);
	        rootChild0.StyleSetHeightPercent(55);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(400, root.LayoutGetWidth());
	        assertFloatEqual(400, root.LayoutGetHeight());

	        assertFloatEqual(40, rootChild0.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0.LayoutGetTop());
	        assertFloatEqual(180, rootChild0.LayoutGetWidth());
	        assertFloatEqual(220, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(400, root.LayoutGetWidth());
	        assertFloatEqual(400, root.LayoutGetHeight());

	        assertFloatEqual(260, rootChild0.LayoutGetLeft());
	        assertFloatEqual(80, rootChild0.LayoutGetTop());
	        assertFloatEqual(180, rootChild0.LayoutGetWidth());
	        assertFloatEqual(220, rootChild0.LayoutGetHeight());
        }

        void TestPercentage_position_bottom_right() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(500);
	        root.StyleSetHeight(500);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPositionPercent(Edge.Right, 20);
	        rootChild0.StyleSetPositionPercent(Edge.Bottom, 10);
	        rootChild0.StyleSetWidthPercent(55);
	        rootChild0.StyleSetHeightPercent(15);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(-100, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-50, rootChild0.LayoutGetTop());
	        assertFloatEqual(275, rootChild0.LayoutGetWidth());
	        assertFloatEqual(75, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(500, root.LayoutGetWidth());
	        assertFloatEqual(500, root.LayoutGetHeight());

	        assertFloatEqual(125, rootChild0.LayoutGetLeft());
	        assertFloatEqual(-50, rootChild0.LayoutGetTop());
	        assertFloatEqual(275, rootChild0.LayoutGetWidth());
	        assertFloatEqual(75, rootChild0.LayoutGetHeight());
        }

        void TestPercentage_flex_basis() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetFlexBasisPercent(25);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(125, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(125, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(75, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(75, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(125, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(75, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_cross() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetFlexBasisPercent(25);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(125, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(125, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(75, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(125, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(125, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(75, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_cross_min_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMinHeightPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(2);
	        rootChild1.StyleSetMinHeightPercent(10);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(140, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(140, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(60, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(140, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(140, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(60, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_main_max_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(10);
	        rootChild0.StyleSetMaxHeightPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(10);
	        rootChild1.StyleSetMaxHeightPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(52, rootChild0.LayoutGetWidth());
	        assertFloatEqual(120, rootChild0.LayoutGetHeight());

	        assertFloatEqual(52, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(148, rootChild1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(148, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(52, rootChild0.LayoutGetWidth());
	        assertFloatEqual(120, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(148, rootChild1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_cross_max_height() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(10);
	        rootChild0.StyleSetMaxHeightPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(10);
	        rootChild1.StyleSetMaxHeightPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(120, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(120, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(120, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(120, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(40, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_main_max_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(15);
	        rootChild0.StyleSetMaxWidthPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(10);
	        rootChild1.StyleSetMaxWidthPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(120, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(40, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(40, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(40, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_cross_max_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(10);
	        rootChild0.StyleSetMaxWidthPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(15);
	        rootChild1.StyleSetMaxWidthPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(40, rootChild1.LayoutGetWidth());
	        assertFloatEqual(150, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(160, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(40, rootChild1.LayoutGetWidth());
	        assertFloatEqual(150, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_main_min_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(15);
	        rootChild0.StyleSetMinWidthPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(10);
	        rootChild1.StyleSetMinWidthPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(120, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(80, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(80, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(120, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(80, rootChild1.LayoutGetWidth());
	        assertFloatEqual(200, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_flex_basis_cross_min_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(10);
	        rootChild0.StyleSetMinWidthPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(15);
	        rootChild1.StyleSetMinWidthPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(150, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(50, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(150, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_multiple_nested_with_padding_margin_and_percentage_values() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasisPercent(10);
	        rootChild0.StyleSetMargin(Edge.Left, 5);
	        rootChild0.StyleSetMargin(Edge.Top, 5);
	        rootChild0.StyleSetMargin(Edge.Right, 5);
	        rootChild0.StyleSetMargin(Edge.Bottom, 5);
	        rootChild0.StyleSetPadding(Edge.Left, 3);
	        rootChild0.StyleSetPadding(Edge.Top, 3);
	        rootChild0.StyleSetPadding(Edge.Right, 3);
	        rootChild0.StyleSetPadding(Edge.Bottom, 3);
	        rootChild0.StyleSetMinWidthPercent(60);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetMargin(Edge.Left, 5);
	        rootChild0Child0.StyleSetMargin(Edge.Top, 5);
	        rootChild0Child0.StyleSetMargin(Edge.Right, 5);
	        rootChild0Child0.StyleSetMargin(Edge.Bottom, 5);
	        rootChild0Child0.StyleSetPaddingPercent(Edge.Left, 3);
	        rootChild0Child0.StyleSetPaddingPercent(Edge.Top, 3);
	        rootChild0Child0.StyleSetPaddingPercent(Edge.Right, 3);
	        rootChild0Child0.StyleSetPaddingPercent(Edge.Bottom, 3);
	        rootChild0Child0.StyleSetWidthPercent(50);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0Child0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0Child0.StyleSetMarginPercent(Edge.Left, 5);
	        rootChild0Child0Child0.StyleSetMarginPercent(Edge.Top, 5);
	        rootChild0Child0Child0.StyleSetMarginPercent(Edge.Right, 5);
	        rootChild0Child0Child0.StyleSetMarginPercent(Edge.Bottom, 5);
	        rootChild0Child0Child0.StyleSetPadding(Edge.Left, 3);
	        rootChild0Child0Child0.StyleSetPadding(Edge.Top, 3);
	        rootChild0Child0Child0.StyleSetPadding(Edge.Right, 3);
	        rootChild0Child0Child0.StyleSetPadding(Edge.Bottom, 3);
	        rootChild0Child0Child0.StyleSetWidthPercent(45);
	        rootChild0Child0.InsertChild(rootChild0Child0Child0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(4);
	        rootChild1.StyleSetFlexBasisPercent(15);
	        rootChild1.StyleSetMinWidthPercent(20);
	        root.InsertChild(rootChild1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(5, rootChild0.LayoutGetLeft());
	        assertFloatEqual(5, rootChild0.LayoutGetTop());
	        assertFloatEqual(190, rootChild0.LayoutGetWidth());
	        assertFloatEqual(48, rootChild0.LayoutGetHeight());

	        assertFloatEqual(8, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(8, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(92, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(25, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(10, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(36, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(6, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(58, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(142, rootChild1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(5, rootChild0.LayoutGetLeft());
	        assertFloatEqual(5, rootChild0.LayoutGetTop());
	        assertFloatEqual(190, rootChild0.LayoutGetWidth());
	        assertFloatEqual(48, rootChild0.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(8, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(92, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(25, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(46, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(36, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(6, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(58, rootChild1.LayoutGetTop());
	        assertFloatEqual(200, rootChild1.LayoutGetWidth());
	        assertFloatEqual(142, rootChild1.LayoutGetHeight());
        }

        void TestPercentage_margin_should_calculate_based_only_on_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetMarginPercent(Edge.Left, 10);
	        rootChild0.StyleSetMarginPercent(Edge.Top, 10);
	        rootChild0.StyleSetMarginPercent(Edge.Right, 10);
	        rootChild0.StyleSetMarginPercent(Edge.Bottom, 10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(10);
	        rootChild0Child0.StyleSetHeight(10);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(160, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0.LayoutGetTop());
	        assertFloatEqual(160, rootChild0.LayoutGetWidth());
	        assertFloatEqual(60, rootChild0.LayoutGetHeight());

	        assertFloatEqual(150, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());
        }

        void TestPercentage_padding_should_calculate_based_only_on_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetPaddingPercent(Edge.Left, 10);
	        rootChild0.StyleSetPaddingPercent(Edge.Top, 10);
	        rootChild0.StyleSetPaddingPercent(Edge.Right, 10);
	        rootChild0.StyleSetPaddingPercent(Edge.Bottom, 10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(10);
	        rootChild0Child0.StyleSetHeight(10);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(20, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(170, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(20, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());
        }

        void TestPercentage_absolute_position() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPositionType(PositionType.Absolute);
	        rootChild0.StyleSetPositionPercent(Edge.Left, 30);
	        rootChild0.StyleSetPositionPercent(Edge.Top, 10);
	        rootChild0.StyleSetWidth(10);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(60, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(60, rootChild0.LayoutGetLeft());
	        assertFloatEqual(10, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestPercentage_width_height_undefined_parent_size() {


	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidthPercent(50);
	        rootChild0.StyleSetHeightPercent(50);
	        root.InsertChild(rootChild0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(0, root.LayoutGetWidth());
	        assertFloatEqual(0, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild0.LayoutGetHeight());
        }

        void TestPercent_within_flex_grow() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(350);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);

	        var rootChild1child0 = Node.CreateDefaultNode();
	        rootChild1child0.StyleSetWidthPercent(100);
	        rootChild1.InsertChild(rootChild1child0, 0);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetWidth(100);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(350, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(100, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(150, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(250, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(350, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(250, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(100, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(150, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(150, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(0, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestPercentage_container_in_wrapping_container() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetJustifyContent(Justify.Center);
	        root.StyleSetAlignItems(Align.Center);
	        root.StyleSetWidth(200);
	        root.StyleSetHeight(200);

	        var rootChild0 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0Child0.StyleSetJustifyContent(Justify.Center);
	        rootChild0Child0.StyleSetWidthPercent(100);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0Child0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0Child0.StyleSetWidth(50);
	        rootChild0Child0Child0.StyleSetHeight(50);
	        rootChild0Child0.InsertChild(rootChild0Child0Child0, 0);

	        var rootChild0Child0_child1 = Node.CreateDefaultNode();
	        rootChild0Child0_child1.StyleSetWidth(50);
	        rootChild0Child0_child1.StyleSetHeight(50);
	        rootChild0Child0.InsertChild(rootChild0Child0_child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0_child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0_child1.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0_child1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0_child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(200, root.LayoutGetWidth());
	        assertFloatEqual(200, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(75, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0Child0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0Child0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0_child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0_child1.LayoutGetTop());
	        assertFloatEqual(50, rootChild0Child0_child1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0_child1.LayoutGetHeight());
        }

        void TestPercent_absolute_position() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(60);
	        root.StyleSetHeight(50);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        rootChild0.StyleSetPositionType(PositionType.Absolute);
	        rootChild0.StyleSetPositionPercent(Edge.Left, 50);
	        rootChild0.StyleSetWidthPercent(100);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidthPercent(100);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetWidthPercent(100);
	        rootChild0.InsertChild(rootChild0child1, 1);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(60, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(30, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(60, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(60, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0child1.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(60, root.LayoutGetWidth());
	        assertFloatEqual(50, root.LayoutGetHeight());

	        assertFloatEqual(30, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(60, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(-60, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(60, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0child1.LayoutGetHeight());
        }

        #endregion


        #region relayout_test.go
        void TestDont_cache_computed_flex_basis_between_layouts() {

	        config.SetExperimentalFeatureEnabled(ExperimentalFeatureWebFlexBasis, true);

	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeightPercent(100);
	        root.StyleSetWidthPercent(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexBasisPercent(100);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, Undefined, DirectionLTR);
	        CalculateLayout(root, 100, 100, DirectionLTR);

	        assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestRecalculate_resolvedDimonsion_onchange() {
	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetMinHeight(10);
	        rootChild0.StyleSetMaxHeight(10);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        rootChild0.StyleSetMinHeight(Undefined);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, rootChild0.LayoutGetHeight());
        }

        #endregion


        #region rounding_function_test.go
        void TestRounding_value() {

	        // Test that whole numbers are rounded to whole despite ceil/floor flags
	        assertFloatEqual(6.0, roundValueToPixelGrid(6.000001, 2.0, false, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(6.000001, 2.0, true, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(6.000001, 2.0, false, true));
	        assertFloatEqual(6.0, roundValueToPixelGrid(5.999999, 2.0, false, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(5.999999, 2.0, true, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(5.999999, 2.0, false, true));

	        // Test that numbers with fraction are rounded correctly accounting for ceil/floor flags
	        assertFloatEqual(6.0, roundValueToPixelGrid(6.01, 2.0, false, false));
	        assertFloatEqual(6.5, roundValueToPixelGrid(6.01, 2.0, true, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(6.01, 2.0, false, true));
	        assertFloatEqual(6.0, roundValueToPixelGrid(5.99, 2.0, false, false));
	        assertFloatEqual(6.0, roundValueToPixelGrid(5.99, 2.0, true, false));
	        assertFloatEqual(5.5, roundValueToPixelGrid(5.99, 2.0, false, true));
        }

        #endregion


        #region rounding_measure_func_test.go
        func _measureFloor(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        return Size{
		        Width: 10.2, Height: 10.2,
	        }
        }

        func _measureCeil(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        return Size{
		        Width: 10.5, Height: 10.5,
	        }
        }

        func _measureFractial(node *Node, width float32, widthMode MeasureMode, height float32, heightMode MeasureMode) Size {
	        return Size{
		        Width: 0.5, Height: 0.5,
	        }
        }

        void TestRounding_feature_with_custom_measure_func_floor() {

	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_measureFloor);
	        root.InsertChild(rootChild0, 0);

	        config.SetPointScaleFactor(0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(10.2, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10.2, rootChild0.LayoutGetHeight());

	        config.SetPointScaleFactor(1);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(11, rootChild0.LayoutGetWidth());
	        assertFloatEqual(11, rootChild0.LayoutGetHeight());

	        config.SetPointScaleFactor(2);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(10.5, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10.5, rootChild0.LayoutGetHeight());

	        config.SetPointScaleFactor(4);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(10.25, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10.25, rootChild0.LayoutGetHeight());

	        config.SetPointScaleFactor(float32(1) / float32(3));

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(12.0, rootChild0.LayoutGetWidth());
	        assertFloatEqual(12.0, rootChild0.LayoutGetHeight());
        }

        void TestRounding_feature_with_custom_measure_func_ceil() {

	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.SetMeasureFunc(_measureCeil);
	        root.InsertChild(rootChild0, 0);

	        config.SetPointScaleFactor(1);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(11, rootChild0.LayoutGetWidth());
	        assertFloatEqual(11, rootChild0.LayoutGetHeight());
        }

        void TestRounding_feature_with_custom_measure_and_fractial_matching_scale() {

	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPosition(Edge.Left, 73.625);
	        rootChild0.SetMeasureFunc(_measureFractial);
	        root.InsertChild(rootChild0, 0);

	        config.SetPointScaleFactor(2);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0.5, rootChild0.LayoutGetWidth());
	        assertFloatEqual(0.5, rootChild0.LayoutGetHeight());
	        assertFloatEqual(73.5, rootChild0.LayoutGetLeft());
        }

        #endregion


        #region rounding_test.go
        void TestRounding_flex_basis_flex_grow_row_width_of_100() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(33, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(33, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(34, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(67, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(33, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(67, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(33, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(33, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(34, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(33, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestRounding_flex_basis_flex_grow_row_prime_number_width() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(113);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild2, 2);

	        var rootChild3 = Node.CreateDefaultNode();
	        rootChild3.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild3, 3);

	        var rootChild4 = Node.CreateDefaultNode();
	        rootChild4.StyleSetFlexGrow(1);
	        root.InsertChild(rootChild4, 4);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(113, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(23, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(23, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(22, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(45, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(23, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        assertFloatEqual(68, rootChild3.LayoutGetLeft());
	        assertFloatEqual(0, rootChild3.LayoutGetTop());
	        assertFloatEqual(22, rootChild3.LayoutGetWidth());
	        assertFloatEqual(100, rootChild3.LayoutGetHeight());

	        assertFloatEqual(90, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(23, rootChild4.LayoutGetWidth());
	        assertFloatEqual(100, rootChild4.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(113, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(90, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(23, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(68, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(22, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(45, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(23, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        assertFloatEqual(23, rootChild3.LayoutGetLeft());
	        assertFloatEqual(0, rootChild3.LayoutGetTop());
	        assertFloatEqual(22, rootChild3.LayoutGetWidth());
	        assertFloatEqual(100, rootChild3.LayoutGetHeight());

	        assertFloatEqual(0, rootChild4.LayoutGetLeft());
	        assertFloatEqual(0, rootChild4.LayoutGetTop());
	        assertFloatEqual(23, rootChild4.LayoutGetWidth());
	        assertFloatEqual(100, rootChild4.LayoutGetHeight());
        }

        void TestRounding_flex_basis_flex_shrink_row() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(101);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexShrink(1);
	        rootChild0.StyleSetFlexBasis(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexBasis(25);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexBasis(25);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(101, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(51, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(51, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(25, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(76, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(25, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(101, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(50, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(51, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(25, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(25, rootChild1.LayoutGetWidth());
	        assertFloatEqual(100, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(25, rootChild2.LayoutGetWidth());
	        assertFloatEqual(100, rootChild2.LayoutGetHeight());
        }

        void TestRounding_flex_basis_overrides_main_size() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(113);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());
        }

        void TestRounding_total_fractial() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(87.4);
	        root.StyleSetHeight(113.4);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(0.7);
	        rootChild0.StyleSetFlexBasis(50.3);
	        rootChild0.StyleSetHeight(20.3);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1.6);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1.1);
	        rootChild2.StyleSetHeight(10.7);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(87, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0.LayoutGetWidth());
	        assertFloatEqual(59, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(59, rootChild1.LayoutGetTop());
	        assertFloatEqual(87, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(87, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(87, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0.LayoutGetWidth());
	        assertFloatEqual(59, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(59, rootChild1.LayoutGetTop());
	        assertFloatEqual(87, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(87, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());
        }

        void TestRounding_total_fractial_nested() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(87.4);
	        root.StyleSetHeight(113.4);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(0.7);
	        rootChild0.StyleSetFlexBasis(50.3);
	        rootChild0.StyleSetHeight(20.3);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetFlexGrow(1);
	        rootChild0Child0.StyleSetFlexBasis(0.3);
	        rootChild0Child0.StyleSetPosition(Edge.Bottom, 13.3);
	        rootChild0Child0.StyleSetHeight(9.9);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        var rootChild0child1 = Node.CreateDefaultNode();
	        rootChild0child1.StyleSetFlexGrow(4);
	        rootChild0child1.StyleSetFlexBasis(0.3);
	        rootChild0child1.StyleSetPosition(Edge.Top, 13.3);
	        rootChild0child1.StyleSetHeight(1.1);
	        rootChild0.InsertChild(rootChild0child1, 1);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1.6);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1.1);
	        rootChild2.StyleSetHeight(10.7);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(87, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0.LayoutGetWidth());
	        assertFloatEqual(59, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(-13, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(12, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(87, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(47, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(59, rootChild1.LayoutGetTop());
	        assertFloatEqual(87, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(87, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(87, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0.LayoutGetWidth());
	        assertFloatEqual(59, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(-13, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(87, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(12, rootChild0Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0child1.LayoutGetLeft());
	        assertFloatEqual(25, rootChild0child1.LayoutGetTop());
	        assertFloatEqual(87, rootChild0child1.LayoutGetWidth());
	        assertFloatEqual(47, rootChild0child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(59, rootChild1.LayoutGetTop());
	        assertFloatEqual(87, rootChild1.LayoutGetWidth());
	        assertFloatEqual(30, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(87, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());
        }

        void TestRounding_fractial_input_1() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(113.4);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());
        }

        void TestRounding_fractial_input_2() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(113.6);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(114, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(65, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(65, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(24, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(25, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(114, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(65, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(65, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(24, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(25, rootChild2.LayoutGetHeight());
        }

        void TestRounding_fractial_input_3() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPosition(Edge.Top, 0.3);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(113.4);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(114, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(65, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(24, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(25, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(114, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(65, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(24, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(25, rootChild2.LayoutGetHeight());
        }

        void TestRounding_fractial_input_4() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetPosition(Edge.Top, 0.7);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(113.4);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetFlexBasis(50);
	        rootChild0.StyleSetHeight(20);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(1, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(1, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(113, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(64, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(64, rootChild1.LayoutGetTop());
	        assertFloatEqual(100, rootChild1.LayoutGetWidth());
	        assertFloatEqual(25, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(89, rootChild2.LayoutGetTop());
	        assertFloatEqual(100, rootChild2.LayoutGetWidth());
	        assertFloatEqual(24, rootChild2.LayoutGetHeight());
        }

        void TestRounding_inner_node_controversy_horizontal() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(320);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetHeight(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeight(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild1child0 = Node.CreateDefaultNode();
	        rootChild1child0.StyleSetFlexGrow(1);
	        rootChild1child0.StyleSetHeight(10);
	        rootChild1.InsertChild(rootChild1child0, 0);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeight(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(320, root.LayoutGetWidth());
	        assertFloatEqual(10, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(107, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(107, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(106, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(106, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(213, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(107, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(320, root.LayoutGetWidth());
	        assertFloatEqual(10, root.LayoutGetHeight());

	        assertFloatEqual(213, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(107, rootChild0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild0.LayoutGetHeight());

	        assertFloatEqual(107, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(106, rootChild1.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(106, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(10, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(107, rootChild2.LayoutGetWidth());
	        assertFloatEqual(10, rootChild2.LayoutGetHeight());
        }

        void TestRounding_inner_node_controversy_vertical() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetHeight(320);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetWidth(10);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetWidth(10);
	        root.InsertChild(rootChild1, 1);

	        var rootChild1child0 = Node.CreateDefaultNode();
	        rootChild1child0.StyleSetFlexGrow(1);
	        rootChild1child0.StyleSetWidth(10);
	        rootChild1.InsertChild(rootChild1child0, 0);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetWidth(10);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(10, root.LayoutGetWidth());
	        assertFloatEqual(320, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(107, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(107, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(213, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(107, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(10, root.LayoutGetWidth());
	        assertFloatEqual(320, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(10, rootChild0.LayoutGetWidth());
	        assertFloatEqual(107, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1.LayoutGetLeft());
	        assertFloatEqual(107, rootChild1.LayoutGetTop());
	        assertFloatEqual(10, rootChild1.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(10, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(213, rootChild2.LayoutGetTop());
	        assertFloatEqual(10, rootChild2.LayoutGetWidth());
	        assertFloatEqual(107, rootChild2.LayoutGetHeight());
        }

        void TestRounding_inner_node_controversy_combined() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetWidth(640);
	        root.StyleSetHeight(320);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetFlexGrow(1);
	        rootChild0.StyleSetHeightPercent(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetFlexGrow(1);
	        rootChild1.StyleSetHeightPercent(100);
	        root.InsertChild(rootChild1, 1);

	        var rootChild1child0 = Node.CreateDefaultNode();
	        rootChild1child0.StyleSetFlexGrow(1);
	        rootChild1child0.StyleSetWidthPercent(100);
	        rootChild1.InsertChild(rootChild1child0, 0);

	        var rootChild1_child1 = Node.CreateDefaultNode();
	        rootChild1_child1.StyleSetFlexGrow(1);
	        rootChild1_child1.StyleSetWidthPercent(100);
	        rootChild1.InsertChild(rootChild1_child1, 1);

	        var rootChild1_child1Child0 = Node.CreateDefaultNode();
	        rootChild1_child1Child0.StyleSetFlexGrow(1);
	        rootChild1_child1Child0.StyleSetWidthPercent(100);
	        rootChild1_child1.InsertChild(rootChild1_child1Child0, 0);

	        var rootChild1_child2 = Node.CreateDefaultNode();
	        rootChild1_child2.StyleSetFlexGrow(1);
	        rootChild1_child2.StyleSetWidthPercent(100);
	        rootChild1.InsertChild(rootChild1_child2, 2);

	        var rootChild2 = Node.CreateDefaultNode();
	        rootChild2.StyleSetFlexGrow(1);
	        rootChild2.StyleSetHeightPercent(100);
	        root.InsertChild(rootChild2, 2);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(640, root.LayoutGetWidth());
	        assertFloatEqual(320, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(213, rootChild0.LayoutGetWidth());
	        assertFloatEqual(320, rootChild0.LayoutGetHeight());

	        assertFloatEqual(213, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(214, rootChild1.LayoutGetWidth());
	        assertFloatEqual(320, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(214, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(107, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child1.LayoutGetLeft());
	        assertFloatEqual(107, rootChild1_child1.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child1.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1_child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1_child1Child0.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child1Child0.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1_child1Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child2.LayoutGetLeft());
	        assertFloatEqual(213, rootChild1_child2.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child2.LayoutGetWidth());
	        assertFloatEqual(107, rootChild1_child2.LayoutGetHeight());

	        assertFloatEqual(427, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(213, rootChild2.LayoutGetWidth());
	        assertFloatEqual(320, rootChild2.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(640, root.LayoutGetWidth());
	        assertFloatEqual(320, root.LayoutGetHeight());

	        assertFloatEqual(427, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(213, rootChild0.LayoutGetWidth());
	        assertFloatEqual(320, rootChild0.LayoutGetHeight());

	        assertFloatEqual(213, rootChild1.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1.LayoutGetTop());
	        assertFloatEqual(214, rootChild1.LayoutGetWidth());
	        assertFloatEqual(320, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(214, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(107, rootChild1child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child1.LayoutGetLeft());
	        assertFloatEqual(107, rootChild1_child1.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child1.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1_child1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child1Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1_child1Child0.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child1Child0.LayoutGetWidth());
	        assertFloatEqual(106, rootChild1_child1Child0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1_child2.LayoutGetLeft());
	        assertFloatEqual(213, rootChild1_child2.LayoutGetTop());
	        assertFloatEqual(214, rootChild1_child2.LayoutGetWidth());
	        assertFloatEqual(107, rootChild1_child2.LayoutGetHeight());

	        assertFloatEqual(0, rootChild2.LayoutGetLeft());
	        assertFloatEqual(0, rootChild2.LayoutGetTop());
	        assertFloatEqual(213, rootChild2.LayoutGetWidth());
	        assertFloatEqual(320, rootChild2.LayoutGetHeight());
        }

        #endregion


        #region size_overflow_test.go
        void TestNested_overflowing_child() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(200);
	        rootChild0Child0.StyleSetHeight(200);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(-100, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());
        }

        void TestNested_overflowing_child_in_constraint_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        rootChild0.StyleSetHeight(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(200);
	        rootChild0Child0.StyleSetHeight(200);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(100, rootChild0.LayoutGetHeight());

	        assertFloatEqual(-100, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());
        }

        void TestParent_wrap_child_size_overflowing_parent() {


	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(100);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.StyleSetWidth(100);
	        rootChild0Child0.StyleSetHeight(200);
	        rootChild0.InsertChild(rootChild0Child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
	        assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
	        assertFloatEqual(200, rootChild0Child0.LayoutGetHeight());
        }

        #endregion


        #region style_test.go
        void TestCopy_style_same() {
	        var node0 = Node.CreateDefaultNode();
	        var node1 = Node.CreateDefaultNode();
	        assert.False(t, node0.IsDirty);

	        NodeCopyStyle(node0, node1);
	        assert.False(t, node0.IsDirty);
        }

        void TestCopy_style_modified() {
	        var node0 = Node.CreateDefaultNode();
	        assert.False(t, node0.IsDirty);
	        assert.Equal(t, FlexDirection.Column, node0.Style.FlexDirection);
	        assert.False(t, node0.StyleGetMaxHeight().Unit != Unit.Undefined);

	        var node1 = Node.CreateDefaultNode();
	        node1.StyleSetFlexDirection(FlexDirection.Row);
	        node1.StyleSetMaxHeight(10);

	        NodeCopyStyle(node0, node1);
	        assert.True(t, node0.IsDirty);
	        assert.Equal(t, FlexDirection.Row, node0.Style.FlexDirection);
	        assertFloatEqual(10, node0.StyleGetMaxHeight().Value);
        }

        void TestCopy_style_modified_same() {
	        var node0 = Node.CreateDefaultNode();
	        node0.StyleSetFlexDirection(FlexDirection.Row);
	        node0.StyleSetMaxHeight(10);
	        CalculateLayout(node0, Undefined, Undefined, DirectionLTR);
	        assert.False(t, node0.IsDirty);

	        var node1 = Node.CreateDefaultNode();
	        node1.StyleSetFlexDirection(FlexDirection.Row);
	        node1.StyleSetMaxHeight(10);

	        NodeCopyStyle(node0, node1);
	        assert.False(t, node0.IsDirty);
        }
        #endregion
#endif

    }
}