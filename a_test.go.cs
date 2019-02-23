
namespace Rockyfi
{
    class TestCC
    {
        static void assertFloatEqual(float a, float b)
        {
        }
        static void assertEqual(object a, object b)
        {
        }
        static void assertTrue(bool value)
        {
        }
        static void assertFalse(bool value)
        {
        }


#if false
    #region measure_cache_test.go
        static Size measureMax(Node node, float width, MeasureMode widthMode, float height, MeasureMode heightMode) {
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
	        assertEqual(1, measureCount);

        }

        void TestRemeasure_with_same_exact_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, 100, 100, Direction.LTR);
	        CalculateLayout(root, 100, 50, Direction.LTR);

	        measureCount = rootChild0.Context.(int);
	        assertEqual(1, measureCount);

        }

        void TestRemeasure_with_same_atmost_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, 100, 100, Direction.LTR);
	        CalculateLayout(root, 100, 50, Direction.LTR);

	        measureCount = rootChild0.Context.(int);
	        assertEqual(1, measureCount);

        }

        void TestRemeasure_with_computed_width_larger_than_needed_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        Node.CalculateLayout(root, 100, 100, Direction.LTR);
	        root.StyleSetAlignItems(Align.Stretch);
	        CalculateLayout(root, 10, 50, Direction.LTR);

	        measureCount = rootChild0.Context.(int);
	        assertEqual(1, measureCount);

        }

        void TestRemeasure_with_atmost_computed_width_undefined_height() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetAlignItems(Align.FlexStart);

	        var rootChild0 = Node.CreateDefaultNode();
	        measureCount := 0
	        rootChild0.Context = measureCount
	        rootChild0.SetMeasureFunc(measureMin);
	        root.InsertChild(rootChild0, 0);

	        CalculateLayout(root, 100, Undefined, Direction.LTR);
	        CalculateLayout(root, 10, Undefined, Direction.LTR);

	        measureCount = rootChild0.Context.(int);
	        assertEqual(1, measureCount);
        }

        void TestRemeasure_with_already_measured_value_smaller_but_still_float_equal() {
	        measureCount := 0

	        var root = Node.CreateDefaultNode();
	        root.StyleSetWidth(288);
	        root.StyleSetHeight(288);
	        root.StyleSetFlexDirection(FlexDirection.Row);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetPadding(Edge.All, 2.88f);
	        rootChild0.StyleSetFlexDirection(FlexDirection.Row);
	        root.InsertChild(rootChild0, 0);

	        var rootChild0Child0 = Node.CreateDefaultNode();
	        rootChild0Child0.Context = measureCount
	        rootChild0Child0.SetMeasureFunc(measure8449);
	        rootChild0.InsertChild(rootChild0Child0, 0);

	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        measureCount = rootChild0Child0.Context.(int);
	        assertEqual(1, measureCount);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assertEqual(MeasureMode.Exactly, constraintList.constraints[0].widthMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureMode.Exactly, constraintList.constraints[0].heightMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].heightMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].widthMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].widthMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].heightMode);
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

	        assertEqual(2, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].heightMode);

	        assertFloatEqual(100, constraintList.constraints[1].height);
	        assertEqual(MeasureMode.Exactly, constraintList.constraints[1].heightMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureMode.Exactly, constraintList.constraints[0].heightMode);
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

	        assertEqual(1, constraintList.length);

	        assertFloatEqual(100, constraintList.constraints[0].width);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].widthMode);

	        assertTrue(float.IsNaN(constraintList.constraints[0].height));
	        assertEqual(MeasureModeUndefined, constraintList.constraints[0].heightMode);
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

	        assertEqual(1, constraintList.length);

	        assertTrue(float.IsNaN(constraintList.constraints[0].width));
	        assertEqual(MeasureModeUndefined, constraintList.constraints[0].widthMode);

	        assertFloatEqual(100, constraintList.constraints[0].height);
	        assertEqual(MeasureModeAtMost, constraintList.constraints[0].heightMode);
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

	        assertEqual(0, measureCount);
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

	        assertEqual(1, measureCount);
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

	        assertEqual(0, measureCount);
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

	        assertEqual(0, measureCount);
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

	        assertEqual(0, measureCount);
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

	        assertEqual(0, measureCount);
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

	        CalculateLayout(root, 282, Undefined, Direction.LTR);

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
	        assertTrue(root.Measure == nil);
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
#endif

    #region issue5_test.go
    #endregion

    #region math_test.go
    #endregion



#if false
#endif

}
}