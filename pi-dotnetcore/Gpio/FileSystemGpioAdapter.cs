using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pi_dotnetcore.Gpio
{
    /// <summary>
    /// Implementation of IGpioAdapter that uses the RaspberryPi file system
    /// </summary>
    public class FileSystemGpioAdapter : IGpioAdapter
    {
        Dictionary<int, FileStream> _valuestreams;

        public FileSystemGpioAdapter()
        {
            _valuestreams = new Dictionary<int, FileStream>();
        }

        #region IGpioAdapter Interface 

        public void InitPin(int number)
        {
            if (!File.Exists(GetPinFolder(number)))
            {
                File.WriteAllText("/sys/class/gpio/export", number.ToString());
                if (!_valuestreams.ContainsKey(number))
                    _valuestreams.Add(number, new FileStream(Path.Combine(GetPinFolder(number), "value"), FileMode.Open));
            }
        }

        public void DeInitPin(int number)
        {
            try
            {
                File.WriteAllText("/sys/class/gpio/unexport", number.ToString());
            }
            catch (IOException) { }

            if (_valuestreams.ContainsKey(number))
                _valuestreams.Remove(number);

        }

        public PinDirection GetDirection(int number)
        {
            using (var fs = new FileStream(Path.Combine(GetPinFolder(number), "direction"), FileMode.Open))
            {
                return fs.ReadByte() == (byte)'o' ? PinDirection.Output : PinDirection.Input;
            }
        }

        public PinValue GetValue(int number)
        {
            _valuestreams[number].Position = 0;
            return _valuestreams[number].ReadByte() == (byte)'1' ? PinValue.On : PinValue.Off;
        }

        public void SetDirection(int number, PinDirection direction)
        {
            File.WriteAllText(Path.Combine(GetPinFolder(number), "direction"), direction == PinDirection.Input ? "in" : "out");
        }

        public void SetValue(int number, PinValue value)
        {
            File.WriteAllText(Path.Combine(GetPinFolder(number), "value"), value == PinValue.On ? "1" : "0");
        }

        #endregion

        static string GetPinFolder(int number)
        {
            if (number < 0 || number > 27)
                throw new ArgumentOutOfRangeException("number", number, "pin number must be between 0 and 27");

            return string.Format("/sys/class/gpio/gpio{0}", number);
        }
    }
}
