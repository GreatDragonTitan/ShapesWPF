namespace HexagonModelTesting
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DrawHexagon;
    using System.Windows.Media;

    [TestClass]
    public class HexagonModelTesting
    {
        [TestMethod]
        public void ConstructorTesting()
        {
            var expectedPoints = new PointCollection();
            var expectedStroke = Brushes.Black;
            var expectedFill = Brushes.AliceBlue;
            var expectedNumber = 0;
            var expectedLeft = 0;
            var expectedTop = 0;
            HexagonModel test = new HexagonModel()
            {
                Points = expectedPoints,
                Stroke = expectedStroke,
                Fill = expectedFill,
                Number = expectedNumber,
                Left = expectedLeft,
                Top = expectedTop
            };
            Assert.AreEqual(expectedPoints.Count, test.Points.Count);
            Assert.AreEqual(expectedStroke, test.Stroke);
            Assert.AreEqual(expectedFill, test.Fill);
            Assert.AreEqual(expectedNumber, test.Number);
            Assert.AreEqual(expectedLeft, test.Left);
            Assert.AreEqual(expectedTop, test.Top);
        }
    }
}
