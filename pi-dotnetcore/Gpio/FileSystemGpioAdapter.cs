using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public class FileSystemGpioAdapter : IGpioAdapter
    {
        Dictionary<int, Stream> _valueStreams;

        public FileSystemGpioAdapter()
        {
            _valueStreams = new Dictionary<int, Stream>();
        }

        public void InitPin(int number)
        {
            File.WriteAllText("/sys/class/gpio/export", number.ToString());

            if (!_valueStreams.ContainsKey(number))
                _valueStreams.Add(number, new FileStream(Path.Combine(GetPinFolder(number), "value"), FileMode.Open));
        }

        public void DeInitPin(int number)
        {
            try
            {
                File.WriteAllText("/sys/class/gpio/unexport", number.ToString());
            }
            catch (IOException ex)
            {
                if (!ex.Message.Equals("Invalid argument"))
                    throw ex;
            }

            if (_valueStreams.ContainsKey(number))
                _valueStreams.Remove(number);
        }

        public PinDirection GetDirection(int number)
        {
            return new FileStream(Path.Combine(GetPinFolder(number), "direction"), FileMode.Open).ReadByte() == (byte)'o' ? PinDirection.Output : PinDirection.Input;
        }

        public PinValue GetValue(int number)
        {
            _valueStreams[number].Seek(0, SeekOrigin.Begin);
            return _valueStreams[number].ReadByte() == (byte)'1' ? PinValue.On : PinValue.Off;
        }

        public void SetDirection(int number, PinDirection direction)
        {
            File.WriteAllText(Path.Combine(GetPinFolder(number), "direction"), direction == PinDirection.Input ? "in" : "out");
        }

        public void SetValue(int number, PinValue value)
        {
            File.WriteAllText(Path.Combine(GetPinFolder(number), "value"), value == PinValue.On ? "1" : "0");
        }

        static string GetPinFolder(int number)
        {
            if (number < 0 || number > 27)
                throw new ArgumentOutOfRangeException("number", number, "pin number must be between 0 and 27");

            return string.Format("/sys/class/gpio/gpio{0}", number);
        }
    }
}
