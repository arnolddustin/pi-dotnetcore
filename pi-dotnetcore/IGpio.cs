using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore
{
    public interface IGpio
    {
        GpioPin Get(int number);
        GpioPin Set(int number, GpioDirection direction, bool on);
    }
}