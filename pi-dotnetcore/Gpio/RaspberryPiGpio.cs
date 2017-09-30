using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public class RaspberryPiGpio : IGpio
    {
        const int MIN_GPIO_PIN = 0;
        const int MAX_GPIO_PIN = 27;

        public IEnumerable<IPin> GetInitializedPins()
        {
            for (int i = MIN_GPIO_PIN; i <= MAX_GPIO_PIN; i++)
            {
                if (Directory.Exists(GetPinFolder(i)))
                    yield return GetPin(i);
            }
        }

        public void InitPin(int number, bool isOutput)
        {
            if (!Directory.Exists(GetPinFolder(number)))
                File.WriteAllText("/sys/class/gpio/export", number.ToString());

            File.WriteAllText(string.Format("{0}/direction", GetPinFolder(number)), (isOutput) ? "out" : "in");
        }

        public void DeInitPin(int number)
        {
            if (Directory.Exists(GetPinFolder(number)))
                File.WriteAllText("/sys/class/gpio/unexport", number.ToString());
        }

        public IPin GetPin(int number)
        {
            if (!Directory.Exists(GetPinFolder(number)))
                throw new ApplicationException(string.Format("Pin number {0} must be initialized before use.", number));

            return new Pin(
                number,
                File.ReadAllText(string.Format("{0}/direction", GetPinFolder(number))).Trim().Equals("out"),
                File.ReadAllText(string.Format("{0}/value", GetPinFolder(number))).Trim().Equals("1")
            );
        }

        public void SetPin(int number, bool on)
        {
            if (!Directory.Exists(GetPinFolder(number)))
                throw new ApplicationException(string.Format("Pin number {0} must be initialized before use.", number));

            File.WriteAllText(string.Format("{0}/value", GetPinFolder(number)), (on) ? "1" : "0");
        }

        string GetPinFolder(int number)
        {
            if (number < MIN_GPIO_PIN || number > MAX_GPIO_PIN)
                throw new ArgumentOutOfRangeException("number", number, string.Format("number must be between {0} and {1}.", MIN_GPIO_PIN, MAX_GPIO_PIN));

            return string.Format("/sys/class/gpio/gpio{0}", number);
        }
    }
}
