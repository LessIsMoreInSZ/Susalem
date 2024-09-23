using System.IO.Ports;
using susalem.Communication.Args;

namespace susalem.Communication;

public class SerialService : ISerial
{
    private SerialPort? _serial;

    public bool IsConnected { get; protected set; }

    public void Connect()
    {
        if (IsConnected)
        {
            Dispose();
        }

        _serial = new SerialPort();
    }

    public List<string> GetPortNames() => SerialPort.GetPortNames().ToList();

    public void Dispose()
    {
        _serial?.Close();
        _serial?.Dispose();
        _serial = null;
    }

    public event EventHandler<ReceiveArgs>? Receive;

    public void Send(byte[] buffer, int start, int length)
    {
        if (!IsConnected)
        {
            return;
        }

        _serial!.Write(buffer, start, length);
    }

    public Task SendAsync(byte[] buffer, int start, int length)
    {
        return Task.Run(() => { Send(buffer, start, length); });
    }
}