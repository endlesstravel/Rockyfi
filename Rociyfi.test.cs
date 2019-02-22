using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://github.com/nunit/nunit-csharp-samples/blob/master/syntax/AssertSyntaxTests.cs
namespace Rockyfi
{
    class RociyfiTest
    {
        static void assertFloatEqual(float expect, float real)
        {
            if (Math.Abs(expect - real) > 0.0001f)
            {
                throw new Exception();
            }
        }
        #if true
        #region absolute
        void TestAbsoluteLayoutWidthHeightStartTop()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Start, 10);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutStartTopEndBottom()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Start, 10);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetPosition(Edge.End, 10);
            rootChild0.StyleSetPosition(Edge.Bottom, 10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(80, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(80, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutWidthHeightStartTopEndBottom()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Start, 10);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetPosition(Edge.End, 10);
            rootChild0.StyleSetPosition(Edge.Bottom, 10);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestDoNotClampHeightOfAbsoluteNodeToHeightOfItsOverflowHiddenParent()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetOverflow(Overflow.Hidden);
            root.StyleSetWidth(50);
            root.StyleSetHeight(50);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Start, 0);
            rootChild0.StyleSetPosition(Edge.Top, 0);
            root.InsertChild(rootChild0, 0);

            var rootChild0Child0 = Node.CreateDefaultNode();
            rootChild0Child0.StyleSetWidth(100);
            rootChild0Child0.StyleSetHeight(100);
            rootChild0.InsertChild(rootChild0Child0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(50, root.LayoutGetWidth());
            assertFloatEqual(50, root.LayoutGetHeight());

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
            assertFloatEqual(50, root.LayoutGetWidth());
            assertFloatEqual(50, root.LayoutGetHeight());

            assertFloatEqual(-50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(100, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0Child0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutWithinBorder()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetMargin(Edge.Left, 10);
            root.StyleSetMargin(Edge.Top, 10);
            root.StyleSetMargin(Edge.Right, 10);
            root.StyleSetMargin(Edge.Bottom, 10);
            root.StyleSetPadding(Edge.Left, 10);
            root.StyleSetPadding(Edge.Top, 10);
            root.StyleSetPadding(Edge.Right, 10);
            root.StyleSetPadding(Edge.Bottom, 10);
            root.StyleSetBorder(Edge.Left, 10);
            root.StyleSetBorder(Edge.Top, 10);
            root.StyleSetBorder(Edge.Right, 10);
            root.StyleSetBorder(Edge.Bottom, 10);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 0);
            rootChild0.StyleSetPosition(Edge.Top, 0);
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetPositionType(PositionType.Absolute);
            rootChild1.StyleSetPosition(Edge.Right, 0);
            rootChild1.StyleSetPosition(Edge.Bottom, 0);
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetPositionType(PositionType.Absolute);
            rootChild2.StyleSetPosition(Edge.Left, 0);
            rootChild2.StyleSetPosition(Edge.Top, 0);
            rootChild2.StyleSetMargin(Edge.Left, 10);
            rootChild2.StyleSetMargin(Edge.Top, 10);
            rootChild2.StyleSetMargin(Edge.Right, 10);
            rootChild2.StyleSetMargin(Edge.Bottom, 10);
            rootChild2.StyleSetWidth(50);
            rootChild2.StyleSetHeight(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetPositionType(PositionType.Absolute);
            rootChild3.StyleSetPosition(Edge.Right, 0);
            rootChild3.StyleSetPosition(Edge.Bottom, 0);
            rootChild3.StyleSetMargin(Edge.Left, 10);
            rootChild3.StyleSetMargin(Edge.Top, 10);
            rootChild3.StyleSetMargin(Edge.Right, 10);
            rootChild3.StyleSetMargin(Edge.Bottom, 10);
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(50);
            root.InsertChild(rootChild3, 3);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(10, root.LayoutGetLeft());
            assertFloatEqual(10, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(40, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(20, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(30, rootChild3.LayoutGetLeft());
            assertFloatEqual(30, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(10, root.LayoutGetLeft());
            assertFloatEqual(10, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(40, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(20, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(30, rootChild3.LayoutGetLeft());
            assertFloatEqual(30, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsAndJustifyContentCenter()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsAndJustifyContentFlexEnd()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.FlexEnd);
            root.StyleSetAlignItems(Align.FlexEnd);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(60, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(60, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutJustifyContentCenter()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsCenter()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsCenterOnChildOnly()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.Center);
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsAndJustifyContentCenterAndTopPosition()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsAndJustifyContentCenterAndBottomPosition()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Bottom, 10);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(50, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(25, rootChild0.LayoutGetLeft());
            assertFloatEqual(50, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsoluteLayoutAlignItemsAndJustifyContentCenterAndLeftPosition()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 5);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(5, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(5, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAbsolute_layout_align_items_and_justify_content_center_and_right_position()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetFlexGrow(1);
            root.StyleSetWidth(110);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Right, 5);
            rootChild0.StyleSetWidth(60);
            rootChild0.StyleSetHeight(40);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(45, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(110, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(45, rootChild0.LayoutGetLeft());
            assertFloatEqual(30, rootChild0.LayoutGetTop());
            assertFloatEqual(60, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestPosition_root_with_rtl_should_position_withoutdirection()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetPosition(Edge.Left, 72);
            root.StyleSetWidth(52);
            root.StyleSetHeight(52);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(72, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(52, root.LayoutGetWidth());
            assertFloatEqual(52, root.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(72, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(52, root.LayoutGetWidth());
            assertFloatEqual(52, root.LayoutGetHeight());
        }

        void TestAbsolute_layout_percentage_bottom_based_on_parent_height()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(200);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPositionPercent(Edge.Top, 50);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetPositionType(PositionType.Absolute);
            rootChild1.StyleSetPositionPercent(Edge.Bottom, 50);
            rootChild1.StyleSetWidth(10);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetPositionType(PositionType.Absolute);
            rootChild2.StyleSetPositionPercent(Edge.Top, 10);
            rootChild2.StyleSetPositionPercent(Edge.Bottom, 10);
            rootChild2.StyleSetWidth(10);
            root.InsertChild(rootChild2, 2);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(200, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(100, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(90, rootChild1.LayoutGetTop());
            assertFloatEqual(10, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(10, rootChild2.LayoutGetWidth());
            assertFloatEqual(160, rootChild2.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(200, root.LayoutGetHeight());

            assertFloatEqual(90, rootChild0.LayoutGetLeft());
            assertFloatEqual(100, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(90, rootChild1.LayoutGetLeft());
            assertFloatEqual(90, rootChild1.LayoutGetTop());
            assertFloatEqual(10, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(90, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(10, rootChild2.LayoutGetWidth());
            assertFloatEqual(160, rootChild2.LayoutGetHeight());
        }

        void TestAbsolute_layout_in_wrap_reverse_column_container()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexWrap(Wrap.WrapReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(20);
            rootChild0.StyleSetHeight(20);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());
        }

        void TestAbsolute_layout_in_wrap_reverse_row_container()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetFlexWrap(Wrap.WrapReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(20);
            rootChild0.StyleSetHeight(20);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(80, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(80, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());
        }

        void TestAbsolute_layout_in_wrap_reverse_column_container_flex_end()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexWrap(Wrap.WrapReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.FlexEnd);
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(20);
            rootChild0.StyleSetHeight(20);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());
        }

        void TestAbsolute_layout_in_wrap_reverse_row_container_flex_end()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetFlexWrap(Wrap.WrapReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.FlexEnd);
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetWidth(20);
            rootChild0.StyleSetHeight(20);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(20, rootChild0.LayoutGetWidth());
            assertFloatEqual(20, rootChild0.LayoutGetHeight());
        }
        #endregion
        #endif
        
        #if true
        #region align_content_test
        void TestAlignContentFlexStart()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(130);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            rootChild2.StyleSetHeight(10);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(10);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            rootChild4.StyleSetHeight(10);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(130, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(10, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(10, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(20, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(130, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(30, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(80, rootChild2.LayoutGetLeft());
            assertFloatEqual(10, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(30, rootChild3.LayoutGetLeft());
            assertFloatEqual(10, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(80, rootChild4.LayoutGetLeft());
            assertFloatEqual(20, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_flex_start_without_height_on_children()
        {



            var root = Node.CreateDefaultNode();
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(10);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(0, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(10, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(10, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(20, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(0, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(10, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(10, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(20, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_flex_start_with_flex()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(100);
            root.StyleSetHeight(120);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetFlexBasisPercent(0);
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetFlexBasisPercent(0);
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetFlexGrow(1);
            rootChild3.StyleSetFlexShrink(1);
            rootChild3.StyleSetFlexBasisPercent(0);
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(120, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(40, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(80, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(80, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(40, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(120, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(120, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(40, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(80, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(80, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(40, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(120, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_flex_end()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignContent(Align.FlexEnd);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            rootChild2.StyleSetHeight(10);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(10);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            rootChild4.StyleSetHeight(10);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(10, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(30, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(40, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(10, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(20, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(30, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(40, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(0, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(0, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(0, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(0, rootChild0.LayoutGetHeight());

            assertFloatEqual(100, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(0, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(0, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(0, rootChild3.LayoutGetHeight());

            assertFloatEqual(100, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(0, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_spacebetween()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.SpaceBetween);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(130);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            rootChild2.StyleSetHeight(10);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(10);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            rootChild4.StyleSetHeight(10);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(130, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(45, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(45, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(90, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(130, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(80, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(30, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(80, rootChild2.LayoutGetLeft());
            assertFloatEqual(45, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(30, rootChild3.LayoutGetLeft());
            assertFloatEqual(45, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(80, rootChild4.LayoutGetLeft());
            assertFloatEqual(90, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

        }

        void TestAlign_content_spacearound()
        {



            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.SpaceAround);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(140);
            root.StyleSetHeight(120);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(10);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            rootChild2.StyleSetHeight(10);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            rootChild3.StyleSetHeight(10);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            rootChild4.StyleSetHeight(10);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(140, root.LayoutGetWidth());
            assertFloatEqual(120, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(15, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(15, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(55, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(55, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(95, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(140, root.LayoutGetWidth());
            assertFloatEqual(120, root.LayoutGetHeight());

            assertFloatEqual(90, rootChild0.LayoutGetLeft());
            assertFloatEqual(15, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            assertFloatEqual(40, rootChild1.LayoutGetLeft());
            assertFloatEqual(15, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(10, rootChild1.LayoutGetHeight());

            assertFloatEqual(90, rootChild2.LayoutGetLeft());
            assertFloatEqual(55, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(10, rootChild2.LayoutGetHeight());

            assertFloatEqual(40, rootChild3.LayoutGetLeft());
            assertFloatEqual(55, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(90, rootChild4.LayoutGetLeft());
            assertFloatEqual(95, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_children()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild0Child0 = Node.CreateDefaultNode();
            rootChild0Child0.StyleSetFlexGrow(1);
            rootChild0Child0.StyleSetFlexShrink(1);
            rootChild0Child0.StyleSetFlexBasisPercent(0);
            rootChild0.InsertChild(rootChild0Child0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_flex()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetFlexShrink(1);
            rootChild1.StyleSetFlexBasisPercent(0);
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetFlexGrow(1);
            rootChild3.StyleSetFlexShrink(1);
            rootChild3.StyleSetFlexBasisPercent(0);
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
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
            assertFloatEqual(0, rootChild1.LayoutGetWidth());
            assertFloatEqual(100, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(100, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(0, rootChild3.LayoutGetWidth());
            assertFloatEqual(100, rootChild3.LayoutGetHeight());

            assertFloatEqual(100, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(100, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());

            assertFloatEqual(100, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(0, rootChild1.LayoutGetWidth());
            assertFloatEqual(100, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(100, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(0, rootChild3.LayoutGetWidth());
            assertFloatEqual(100, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(100, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_flex_no_shrink()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetFlexShrink(1);
            rootChild1.StyleSetFlexBasisPercent(0);
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetFlexGrow(1);
            rootChild3.StyleSetFlexBasisPercent(0);
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
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
            assertFloatEqual(0, rootChild1.LayoutGetWidth());
            assertFloatEqual(100, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(100, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(0, rootChild3.LayoutGetWidth());
            assertFloatEqual(100, rootChild3.LayoutGetHeight());

            assertFloatEqual(100, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(100, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());

            assertFloatEqual(100, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(0, rootChild1.LayoutGetWidth());
            assertFloatEqual(100, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(100, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(0, rootChild3.LayoutGetTop());
            assertFloatEqual(0, rootChild3.LayoutGetWidth());
            assertFloatEqual(100, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(100, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_margin()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetMargin(Edge.Left, 10);
            rootChild1.StyleSetMargin(Edge.Top, 10);
            rootChild1.StyleSetMargin(Edge.Right, 10);
            rootChild1.StyleSetMargin(Edge.Bottom, 10);
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetMargin(Edge.Left, 10);
            rootChild3.StyleSetMargin(Edge.Top, 10);
            rootChild3.StyleSetMargin(Edge.Right, 10);
            rootChild3.StyleSetMargin(Edge.Bottom, 10);
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            assertFloatEqual(60, rootChild1.LayoutGetLeft());
            assertFloatEqual(10, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(40, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(40, rootChild2.LayoutGetHeight());

            assertFloatEqual(60, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(20, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(80, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(20, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());

            assertFloatEqual(40, rootChild1.LayoutGetLeft());
            assertFloatEqual(10, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(40, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(40, rootChild2.LayoutGetHeight());

            assertFloatEqual(40, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(20, rootChild3.LayoutGetHeight());

            assertFloatEqual(100, rootChild4.LayoutGetLeft());
            assertFloatEqual(80, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(20, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_padding()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetPadding(Edge.Left, 10);
            rootChild1.StyleSetPadding(Edge.Top, 10);
            rootChild1.StyleSetPadding(Edge.Right, 10);
            rootChild1.StyleSetPadding(Edge.Bottom, 10);
            rootChild1.StyleSetWidth(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetPadding(Edge.Left, 10);
            rootChild3.StyleSetPadding(Edge.Top, 10);
            rootChild3.StyleSetPadding(Edge.Right, 10);
            rootChild3.StyleSetPadding(Edge.Bottom, 10);
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(50, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_single_row()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
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

        void TestAlign_content_stretch_row_with_fixed_height()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(60);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(60, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(80, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(80, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(20, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(80, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(20, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(60, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(80, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(80, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(20, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(80, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(20, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_max_height()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetMaxHeight(20);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(50, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(50, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_row_with_min_height()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(150);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetMinHeight(80);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetWidth(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetWidth(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetWidth(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(90, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(90, rootChild1.LayoutGetHeight());

            assertFloatEqual(100, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(90, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(90, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(90, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(150, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(100, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(90, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(90, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(0, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(90, rootChild2.LayoutGetHeight());

            assertFloatEqual(100, rootChild3.LayoutGetLeft());
            assertFloatEqual(90, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(10, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(90, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(10, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_column()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignContent(Align.Stretch);
            root.StyleSetFlexWrap(Wrap.Wrap);
            root.StyleSetWidth(100);
            root.StyleSetHeight(150);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            root.InsertChild(rootChild0, 0);

            var rootChild0Child0 = Node.CreateDefaultNode();
            rootChild0Child0.StyleSetFlexGrow(1);
            rootChild0Child0.StyleSetFlexShrink(1);
            rootChild0Child0.StyleSetFlexBasisPercent(0);
            rootChild0.InsertChild(rootChild0Child0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetFlexShrink(1);
            rootChild1.StyleSetFlexBasisPercent(0);
            rootChild1.StyleSetHeight(50);
            root.InsertChild(rootChild1, 1);

            var rootChild2 = Node.CreateDefaultNode();
            rootChild2.StyleSetHeight(50);
            root.InsertChild(rootChild2, 2);

            var rootChild3 = Node.CreateDefaultNode();
            rootChild3.StyleSetHeight(50);
            root.InsertChild(rootChild3, 3);

            var rootChild4 = Node.CreateDefaultNode();
            rootChild4.StyleSetHeight(50);
            root.InsertChild(rootChild4, 4);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(150, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(50, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(0, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild2.LayoutGetLeft());
            assertFloatEqual(50, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(0, rootChild3.LayoutGetLeft());
            assertFloatEqual(100, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(50, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(150, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(50, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0Child0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(50, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(0, rootChild1.LayoutGetHeight());

            assertFloatEqual(50, rootChild2.LayoutGetLeft());
            assertFloatEqual(50, rootChild2.LayoutGetTop());
            assertFloatEqual(50, rootChild2.LayoutGetWidth());
            assertFloatEqual(50, rootChild2.LayoutGetHeight());

            assertFloatEqual(50, rootChild3.LayoutGetLeft());
            assertFloatEqual(100, rootChild3.LayoutGetTop());
            assertFloatEqual(50, rootChild3.LayoutGetWidth());
            assertFloatEqual(50, rootChild3.LayoutGetHeight());

            assertFloatEqual(0, rootChild4.LayoutGetLeft());
            assertFloatEqual(0, rootChild4.LayoutGetTop());
            assertFloatEqual(50, rootChild4.LayoutGetWidth());
            assertFloatEqual(50, rootChild4.LayoutGetHeight());
        }

        void TestAlign_content_stretch_is_not_overriding_align_items()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignContent(Align.Stretch);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetFlexDirection(FlexDirection.Row);
            rootChild0.StyleSetAlignContent(Align.Stretch);
            rootChild0.StyleSetAlignItems(Align.Center);
            rootChild0.StyleSetWidth(100);
            rootChild0.StyleSetHeight(100);
            root.InsertChild(rootChild0, 0);

            var rootChild0Child0 = Node.CreateDefaultNode();
            rootChild0Child0.StyleSetAlignContent(Align.Stretch);
            rootChild0Child0.StyleSetWidth(10);
            rootChild0Child0.StyleSetHeight(10);
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
            assertFloatEqual(45, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());

            assertFloatEqual(90, rootChild0Child0.LayoutGetLeft());
            assertFloatEqual(45, rootChild0Child0.LayoutGetTop());
            assertFloatEqual(10, rootChild0Child0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0Child0.LayoutGetHeight());
        }
        #endregion
        #endif
        
        #if true
        #region align_self_test.go
        void TestAlign_self_center()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.Center);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(45, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(45, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestAlign_self_flex_end()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.FlexEnd);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(90, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestAlign_self_flex_start()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.FlexStart);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
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

        void TestAlign_self_flex_end_override_flex_start()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.FlexEnd);
            rootChild0.StyleSetWidth(10);
            rootChild0.StyleSetHeight(10);
            root.InsertChild(rootChild0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(90, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(10, rootChild0.LayoutGetWidth());
            assertFloatEqual(10, rootChild0.LayoutGetHeight());
        }

        void TestAlign_self_baseline()
        {


            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAlignSelf(Align.Baseline);
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(50);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetAlignSelf(Align.Baseline);
            rootChild1.StyleSetWidth(50);
            rootChild1.StyleSetHeight(20);
            root.InsertChild(rootChild1, 1);

            var rootChild1child0 = Node.CreateDefaultNode();
            rootChild1child0.StyleSetWidth(50);
            rootChild1child0.StyleSetHeight(10);
            rootChild1.InsertChild(rootChild1child0, 0);
            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(50, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild1child0.LayoutGetTop());
            assertFloatEqual(50, rootChild1child0.LayoutGetWidth());
            assertFloatEqual(10, rootChild1child0.LayoutGetHeight());

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.RTL);

            assertFloatEqual(0, root.LayoutGetLeft());
            assertFloatEqual(0, root.LayoutGetTop());
            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(40, rootChild1.LayoutGetTop());
            assertFloatEqual(50, rootChild1.LayoutGetWidth());
            assertFloatEqual(20, rootChild1.LayoutGetHeight());

            assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
            assertFloatEqual(0, rootChild1child0.LayoutGetTop());
            assertFloatEqual(50, rootChild1child0.LayoutGetWidth());
            assertFloatEqual(10, rootChild1child0.LayoutGetHeight());
        }
        #endregion
        #endif

        #if true
        #region aspect_ration_test.go
        static Size _measure(Node node, float width, MeasureMode widthMode,
            float height, MeasureMode heightMode)
        {

            if (widthMode != MeasureMode.Exactly)
            {
                width = 50;
            }
            if (heightMode != MeasureMode.Exactly)
            {
                height = 50;
            }
            return new Size(width, height);
        }

        void TestAspect_ratio_cross_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_main_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_both_dimensions_defined_row()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(100);
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_both_dimensions_defined_column()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(100);
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_align_stretch()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_flex_grow()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_flex_shrink()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(150);
            rootChild0.StyleSetFlexShrink(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_basis()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetFlexBasis(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_absolute_layout_width_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 0);
            rootChild0.StyleSetPosition(Edge.Top, 0);
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_absolute_layout_height_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 0);
            rootChild0.StyleSetPosition(Edge.Top, 0);
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_with_max_cross_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetMaxWidth(40);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(40, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_with_max_main_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetMaxHeight(40);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(40, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_with_min_cross_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(30);
            rootChild0.StyleSetMinWidth(40);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(40, rootChild0.LayoutGetWidth());
            assertFloatEqual(30, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_with_min_main_defined()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(30);
            rootChild0.StyleSetMinHeight(40);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(40, rootChild0.LayoutGetWidth());
            assertFloatEqual(40, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_double_cross()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(2);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_half_cross()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(100);
            rootChild0.StyleSetAspectRatio(0.5f);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_double_main()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetAspectRatio(0.5f);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_half_main()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(100);
            rootChild0.StyleSetAspectRatio(2);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_with_measure_func()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.SetMeasureFunc(_measure);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_width_height_flex_grow_row()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(200);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_width_height_flex_grow_column()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(200);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_height_as_flex_basis()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetWidth(200);
            root.StyleSetHeight(200);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetHeight(100);
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetAspectRatio(1);
            root.InsertChild(rootChild1, 1);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(75, rootChild0.LayoutGetWidth());
            assertFloatEqual(75, rootChild0.LayoutGetHeight());

            assertFloatEqual(75, rootChild1.LayoutGetLeft());
            assertFloatEqual(0, rootChild1.LayoutGetTop());
            assertFloatEqual(125, rootChild1.LayoutGetWidth());
            assertFloatEqual(125, rootChild1.LayoutGetHeight());
        }

        void TestAspect_ratio_width_as_flex_basis()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(200);
            root.StyleSetHeight(200);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            var rootChild1 = Node.CreateDefaultNode();
            rootChild1.StyleSetWidth(100);
            rootChild1.StyleSetFlexGrow(1);
            rootChild1.StyleSetAspectRatio(1);
            root.InsertChild(rootChild1, 1);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(75, rootChild0.LayoutGetWidth());
            assertFloatEqual(75, rootChild0.LayoutGetHeight());

            assertFloatEqual(0, rootChild1.LayoutGetLeft());
            assertFloatEqual(75, rootChild1.LayoutGetTop());
            assertFloatEqual(125, rootChild1.LayoutGetWidth());
            assertFloatEqual(125, rootChild1.LayoutGetHeight());
        }

        void TestAspect_ratio_overrides_flex_grow_row()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(0.5f);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(100, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_overrides_flex_grow_column()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetFlexGrow(1);
            rootChild0.StyleSetAspectRatio(2);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(100, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_left_right_absolute()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 10);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetPosition(Edge.Right, 10);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(80, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_top_bottom_absolute()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetPositionType(PositionType.Absolute);
            rootChild0.StyleSetPosition(Edge.Left, 10);
            rootChild0.StyleSetPosition(Edge.Top, 10);
            rootChild0.StyleSetPosition(Edge.Bottom, 10);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(10, rootChild0.LayoutGetLeft());
            assertFloatEqual(10, rootChild0.LayoutGetTop());
            assertFloatEqual(80, rootChild0.LayoutGetWidth());
            assertFloatEqual(80, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_width_overrides_align_stretch_row()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetFlexDirection(FlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_height_overrides_align_stretch_column()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(0, rootChild0.LayoutGetLeft());
            assertFloatEqual(0, rootChild0.LayoutGetTop());
            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_allow_child_overflow_parent_size()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.FlexStart);
            root.StyleSetWidth(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(4);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(50, root.LayoutGetHeight());

            assertFloatEqual(200, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_defined_main_with_margin()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetHeight(50);
            rootChild0.StyleSetAspectRatio(1);
            rootChild0.StyleSetMargin(Edge.Left, 10);
            rootChild0.StyleSetMargin(Edge.Right, 10);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        void TestAspect_ratio_defined_cross_with_margin()
        {
            var root = Node.CreateDefaultNode();
            root.StyleSetAlignItems(Align.Center);
            root.StyleSetJustifyContent(Justify.Center);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            var rootChild0 = Node.CreateDefaultNode();
            rootChild0.StyleSetWidth(50);
            rootChild0.StyleSetAspectRatio(1);
            rootChild0.StyleSetMargin(Edge.Left, 10);
            rootChild0.StyleSetMargin(Edge.Right, 10);
            root.InsertChild(rootChild0, 0);

            Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

            assertFloatEqual(100, root.LayoutGetWidth());
            assertFloatEqual(100, root.LayoutGetHeight());

            assertFloatEqual(50, rootChild0.LayoutGetWidth());
            assertFloatEqual(50, rootChild0.LayoutGetHeight());
        }

        #endregion
        #endif

        #if true
        #region baseline_func_test.go
        static float baselineFunc(Node node, float width, float height) {
	        return (float)node.Context;
        }

        void TestAlign_baseline_customer_func() {
	        var root = Node.CreateDefaultNode();
	        root.StyleSetFlexDirection(FlexDirection.Row);
	        root.StyleSetAlignItems(Align.Baseline);
	        root.StyleSetWidth(100);
	        root.StyleSetHeight(100);

	        var rootChild0 = Node.CreateDefaultNode();
	        rootChild0.StyleSetWidth(50);
	        rootChild0.StyleSetHeight(50);
	        root.InsertChild(rootChild0, 0);

	        var rootChild1 = Node.CreateDefaultNode();
	        rootChild1.StyleSetWidth(50);
	        rootChild1.StyleSetHeight(20);
	        root.InsertChild(rootChild1, 1);

	        float baselineValue = 10;
	        var rootChild1child0 = Node.CreateDefaultNode();
            rootChild1child0.Context = baselineValue;
	        rootChild1child0.StyleSetWidth(50);
            rootChild1child0.SetBaselineFunc(baselineFunc);
	        rootChild1child0.StyleSetHeight(20);
	        rootChild1.InsertChild(rootChild1child0, 0);
	        Node.CalculateLayout(root, float.NaN, float.NaN, Direction.LTR);

	        assertFloatEqual(0, root.LayoutGetLeft());
	        assertFloatEqual(0, root.LayoutGetTop());
	        assertFloatEqual(100, root.LayoutGetWidth());
	        assertFloatEqual(100, root.LayoutGetHeight());

	        assertFloatEqual(0, rootChild0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild0.LayoutGetTop());
	        assertFloatEqual(50, rootChild0.LayoutGetWidth());
	        assertFloatEqual(50, rootChild0.LayoutGetHeight());

	        assertFloatEqual(50, rootChild1.LayoutGetLeft());
	        assertFloatEqual(40, rootChild1.LayoutGetTop());
	        assertFloatEqual(50, rootChild1.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1.LayoutGetHeight());

	        assertFloatEqual(0, rootChild1child0.LayoutGetLeft());
	        assertFloatEqual(0, rootChild1child0.LayoutGetTop());
	        assertFloatEqual(50, rootChild1child0.LayoutGetWidth());
	        assertFloatEqual(20, rootChild1child0.LayoutGetHeight());
        }
        #endregion
        #endif


        public void XTestAll()
        {
            var m = typeof(RociyfiTest).GetMethods(
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
                );
            foreach (var method in m)
            {
                if (method.Name.StartsWith("Test"))
                {
                    try
                    {
                        Console.Write($"Test: {method.Name} -> ");
                        method.Invoke(this, null);
                        Console.WriteLine($"passed");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.StackTrace}");
                    }
                }
                else
                {
                    Console.WriteLine($"ignore: {method.Name}");
                }
            }
        }
    }
}
