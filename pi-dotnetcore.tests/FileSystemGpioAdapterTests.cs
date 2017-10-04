using Microsoft.VisualStudio.TestTools.UnitTesting;
using pi_dotnetcore.Gpio;

namespace pi_dotnetcore.tests
{
    [TestClass]
    public class FileSystemGpioAdapterTests
    {
        [TestMethod]
        public void constructor_test()
        {
            var gpio = new FileSystemGpioAdapter();
            Assert.IsNotNull(gpio);
        }
    }
}
