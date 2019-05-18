using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rockyfi;

namespace MyTest
{
    [TestFixture()]
    class MyTest
    {
        [Test]
        public void MyTest_margin_to_left_effect()
        {
            var root = Flex.CreateDefaultNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);
            //root.StyleSetMarginPercent(Edge.Start, 10);
            root.StyleSetMargin(Edge.Left, 10);
            root.StyleSetMargin(Edge.Top, 20);

            Flex.CalculateLayout(root, 100, 100, Direction.LTR);

            Assert.AreEqual(10, root.LayoutGetLeft());
            Assert.AreEqual(20, root.LayoutGetTop());

        }




    }
}
