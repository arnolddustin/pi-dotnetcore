namespace pi_dotnetcore.Gpio
{
    public interface IPin
    {
        int number { get; }
        bool output { get; }
        bool on { get; }
    }
}