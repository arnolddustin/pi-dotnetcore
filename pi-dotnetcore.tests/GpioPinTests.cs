using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pi_dotnetcore.tests
{
    [TestClass]
    public class GpioPinTests
    {
        [TestMethod]
        public void new_GpioPin_has_correct_initial_values()
        {
            var pin = new GpioPin(10);
            Assert.IsNotNull(pin);
            Assert.AreEqual(10, pin.Number);
            Assert.IsNull(pin.Direction);
            Assert.IsNull(pin.Value);
        }

        [TestMethod]
        public void updating_GpioPin_properties_happy_path()
        {
            var pin = new GpioPin(20);

            pin.Direction = GpioDirection.In;
            Assert.AreEqual(GpioDirection.In, pin.Direction);
            
            pin.Direction = GpioDirection.Out;
            Assert.AreEqual(GpioDirection.Out, pin.Direction);

            pin.Value = 1;
            Assert.AreEqual(1, pin.Value);

            pin.Value = 0;
            Assert.AreEqual(0, pin.Value);
        }
    }
}
