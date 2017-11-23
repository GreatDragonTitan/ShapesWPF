namespace HexagonModelTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DrawHexagon;
    using System.Windows.Shapes;

    [TestClass]
    public class ViewModelTesing
    {
        [TestMethod]
        public void ConstructorTesting()
        {
            ViewModel test = new ViewModel();
            Assert.AreNotEqual(null, test.Polygones);
            Assert.AreEqual(0, test.Polygones.Count);
            Assert.AreNotEqual("", test.Mode);

        }

        [TestMethod]
        public void CountTesting()
        {
            ViewModel test = new ViewModel();
            Assert.AreEqual(0, test.Count);
        }

        [TestMethod]
        public void AddTesting()
        {
            ViewModel test = new ViewModel();
            int expected = 3;
            for(int i=0;i<expected;++i)
            {
                test.Add(new Polygon());
            }
            Assert.AreEqual(expected, test.Count);
        }

        [TestMethod]
        public void LastTesting()
        {
            ViewModel test = new ViewModel();
            int expected = 3;
            int expectedPoint = 4;
            for (int i = 0; i < expected; ++i)
            {
                test.Add(new Polygon());
            }
            for (int i = 0; i < expected; ++i)
            {
                test.Add_Last(new System.Windows.Point());
            }
            Assert.AreEqual(expectedPoint - 1, test.Count_Last);
        }
    }
}
