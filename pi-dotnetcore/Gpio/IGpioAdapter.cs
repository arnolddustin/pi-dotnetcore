using System;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public interface IGpioAdapter
    {
        void InitPin(int number);
        void DeInitPin(int number);
        PinValue GetValue(int number);
        PinDirection GetDirection(int number);
        void SetValue(int number, PinValue value);
        void SetDirection(int number, PinDirection direction);
    }
}