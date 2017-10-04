using System;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public interface IGpioAdapter
    {
        void InitPin(int number, bool isOutput);
        void DeInitPin(int number);
        IPin GetPin(int number);
        IEnumerable<IPin> GetInitializedPins();
        void SetPin(int number, bool isOn);
    }
}