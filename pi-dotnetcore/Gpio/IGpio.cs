using System;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    public interface IGpio
    {
        IEnumerable<int> ListPinNumbers();
        IPin GetPin(int number);
        bool SetPin(int number, bool output, bool on);
    }
}