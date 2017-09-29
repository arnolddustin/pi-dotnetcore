using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore
{
    public enum PinDirection { In, Out }

    public class GpioPin
    {
        public GpioPin(int number)
        {
            Number = number;
            Direction = null;
            Value = null;
        }

        public int Number { get; private set; }

        public PinDirection? Direction { get; set; }

        public int? Value { get; set; }

        public override string ToString()
        {
            return string.Format("gpio{0}|{1}|{2}", Number, (Direction == null) ? "?" : Direction.ToString(), (Value == null) ? "?" : Value.ToString());
        }
    }

    public class Gpio
    {
        int[] _numbers;

        public Gpio()
        {
            _numbers = new int[] { 2, 3, 4, 17, 27, 22, 10, 9, 11, 0, 5, 6, 13, 19, 26, 14, 15, 18, 23, 24, 25, 8, 7, 1, 12, 16, 20, 21 };
        }

        public GpioPin Get(int number)
        {
            if (!_numbers.Contains(number))
            {
                throw new ArgumentOutOfRangeException("number", number, "valid pin numbers are: " + string.Join(",", _numbers));
            }

            var pin = new GpioPin(number);

            var folder = string.Format("/sys/class/gpio/gpio{0}", number);
            if (Directory.Exists(folder))
            {
                if (File.Exists(string.Format("{0}/value", folder)))
                {
                    int value;
                    if (int.TryParse(File.ReadAllText(string.Format("{0}/value", folder)), out value))
                        pin.Value = value;
                }

                if (File.Exists(string.Format("{0}/direction", folder)))
                {
                    switch (File.ReadAllText(string.Format("{0}/direction", folder)))
                    {
                        case "in":
                            pin.Direction = PinDirection.In;
                            break;

                        case "out":
                            pin.Direction = PinDirection.Out;
                            break;
                    }
                }
            }

            return pin;
        }
        public void Set(int pin, PinDirection direction, bool on)
        {
            if (!(_numbers).Contains(pin))
            {
                throw new ArgumentOutOfRangeException("Invalid pin.");
            }

            var pinFolder = string.Format("gpio{0}", pin);

            // make sure pin is open
            if (!Directory.Exists("/sys/class/gpio/" + pinFolder))
            {
                File.WriteAllText("/sys/class/gpio/export", pin.ToString());
            }

            // configure pin for output
            File.WriteAllText("/sys/class/gpio/" + pinFolder + "/direction", "out");

            File.WriteAllText("/sys/class/gpio/" + pinFolder + "/value", (on ? 1 : 0).ToString());
        }
    }
}
