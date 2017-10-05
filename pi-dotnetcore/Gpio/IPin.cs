namespace pi_dotnetcore.Gpio
{
    public enum PinDirection { Input, Output }

    public enum PinValue { On, Off }

    public interface IPin
    {
        int Number { get; }
        PinDirection Direction { get; }
        PinValue Value { get; }
    }
}