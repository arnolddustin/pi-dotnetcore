using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore
{
    public class GpioPin
    {
        public GpioPin(int number)
        {
            Number = number;
            Direction = null;
            Value = null;
        }

        public int Number { get; private set; }

        public GpioDirection? Direction { get; set; }

        public int? Value { get; set; }

        public override string ToString()
        {
            return string.Format("gpio{0}|{1}|{2}", Number, (Direction == null) ? "?" : Direction.ToString(), (Value == null) ? "?" : Value.ToString());
        }
    }
}