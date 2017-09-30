namespace pi_dotnetcore.Gpio
{
    public class Pin : IPin
    {
        public Pin(int pinNumber, bool isOutput, bool isOn)
        {
            this.number = pinNumber;
            this.output = isOutput;
            this.on = isOn;
        }
        public int number { get; private set; }
        public bool output { get; private set; }
        public bool on { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", number, output, on);
        }
    }
}