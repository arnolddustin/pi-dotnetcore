using System;
using System.Collections;
using System.Collections.Generic;

namespace pi_dotnetcore.Gpio
{
    /// <summary>
    /// Interface for interacting with the Raspberry Pi GPIO pins
    /// </summary>
    public interface IGpioAdapter
    {
        /// <summary>
        /// Initialize a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        void InitPin(int number);

        /// <summary>
        /// Deinitialize a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        void DeInitPin(int number);

        /// <summary>
        /// Read the value of a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        /// <returns>The value of the pin (on|off)</returns>
        PinValue GetValue(int number);

        /// <summary>
        /// Read the direction of a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        /// <returns>The direction of the pin (input|output)</returns>
        PinDirection GetDirection(int number);

        /// <summary>
        /// Sets the value of a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        /// <param name="value">The value to set (on|off)</param>
        void SetValue(int number, PinValue value);

        /// <summary>
        /// Sets the direction of a pin
        /// </summary>
        /// <param name="number">The pin number</param>
        /// <param name="direction">The pin diretion (input|output)</param>
        void SetDirection(int number, PinDirection direction);
    }
}