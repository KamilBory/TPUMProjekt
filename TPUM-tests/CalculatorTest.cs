using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPUM;

namespace TPUM_tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void AdditionTest()
        {
            var calculator = new Calculator();
            int a = 3, b = 4;

            Assert.AreEqual(calculator.Add(a, b), a + b);
        }
    }
}
