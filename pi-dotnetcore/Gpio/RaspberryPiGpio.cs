using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public class RaspberryPiGpio : IGpio
    {
        int[] _pins = new int[] { 2, 3, 4, 17, 27, 22, 10, 9, 11, 0, 5, 6, 13, 19, 26, 14, 15, 18, 23, 24, 25, 8, 7, 1, 12, 16, 20, 21 };

        public IPin GetPin(int number)
        {
            if (!_pins.Contains(number))
                throw new ArgumentOutOfRangeException("number");

            bool isoutput = false;
            bool ison = false;

            var folder = string.Format("/sys/class/gpio/gpio{0}", number);
            if (Directory.Exists(folder))
            {
                var directionfile = string.Format("{0}/direction", folder);
                if (File.Exists(directionfile))
                    isoutput = File.ReadAllText(directionfile).Trim().Equals("out");

                var valuefile = string.Format("{0}/value", folder);
                if (File.Exists(valuefile))
                    ison = File.ReadAllText(valuefile).Trim().Equals("1");
            }

            return new Pin(number, isoutput, ison);
        }

        public IEnumerable<int> ListPinNumbers()
        {
            return _pins;
        }

        public bool SetPin(int number, bool output, bool on)
        {
            if (!_pins.Contains(number))
                throw new ArgumentOutOfRangeException("number");

            var pinFolder = string.Format("/sys/class/gpio/gpio{0}", number);

            if (!Directory.Exists(pinFolder))
                File.WriteAllText("/sys/class/gpio/export", number.ToString());

            File.WriteAllText(string.Format("{0}/direction", pinFolder), (output) ? "out" : "in");
            File.WriteAllText(string.Format("{0}/value", pinFolder), (on) ? "1" : "0");

            return true;
        }
    }
}
