namespace HexagonModelTesting
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DrawHexagon;
    using System.Collections.ObjectModel;
    using System.Windows.Media;

    [TestClass]
    public class HexagonesModelTesting
    {
        [TestMethod]
        public void ConstructorTesting()
        {
            ObservableCollection<HexagonModel> expected = new ObservableCollection<HexagonModel>();
            var test = new HexagonesModel() { Shapes = expected};
            Assert.AreEqual(expected.Count, test.Shapes.Count);
        }

        [TestMethod]
        public void SerializingTesting()
        {
            var expectedPoints = new PointCollection();
            var expectedStroke = Brushes.Black;
            var expectedFill = Brushes.AliceBlue;
            var expectedNumber = 3;
            var expectedLeft = 76;
            var expectedTop = 12;
            var expectedPath = "test.xml";

            ObservableCollection<HexagonModel> expected = new ObservableCollection<HexagonModel>() {
                new HexagonModel()
                {
                    Points = expectedPoints,
                    Stroke = expectedStroke,
                    Fill = expectedFill,
                    Number = expectedNumber,
                    Left = expectedLeft,
                    Top = expectedTop
                }
            };

            HexagonesModel test_1 = new HexagonesModel() { Shapes = expected };

            test_1.Serialize(expectedPath);

            HexagonesModel test_2 = new HexagonesModel();

            test_2.Deserialize(expectedPath);

            Assert.AreEqual(expected.Count, test_2.Shapes.Count);
            Assert.AreEqual(expectedNumber, test_2.Shapes[0].Number);
            Assert.AreEqual(expectedLeft, test_2.Shapes[0].Left);
            Assert.AreEqual(expectedTop, test_2.Shapes[0].Top);
        }
    }
}
