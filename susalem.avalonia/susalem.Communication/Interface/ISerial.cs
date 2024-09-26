using System.IO.Ports;

namespace susalem.Communication;

public interface ISerial : ICommunication
{
    public string? PortName { get; set; }

    public int BaudRate { get; set; }
    public int DataBits { get; set; }
    public StopBits StopBits { get; set; }
    public Parity Parity { get; set; }
}