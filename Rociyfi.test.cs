using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

// https://github.com/nunit/nunit-csharp-samples/blob/master/syntax/AssertSyntaxTests.cs
namespace Rockyfi
{
    [TestFixture]
    class RociyfiTest
    {
        static void assertFloatEqual(float expect, float real)
        {
            Assert.That(real, Is.EqualTo(expect).Within(0.00001f));
            if (Math.Abs(real - real) > 0.0001f)
            {
                throw new Exception();
            }
        }

        [Test]
        public void TestAbsoluteLayoutWidthHeightStartTop()
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
            Node.InsertChild(root, rootChild0, 0);
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

        public void TestAll()
        {
            TestAbsoluteLayoutWidthHeightStartTop();
        }
    }
}
