using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pi_dotnetcore.tests
{
    [TestClass]
    public class GpioTests
    {
        [TestMethod]
        public void constructor_test()
        {
            var gpio = new Gpio();
            Assert.IsNotNull(gpio);
        }
    }
}
