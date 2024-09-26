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
        _serial.PortName = PortName;
        _serial.BaudRate = BaudRate;
        _serial.Parity = Parity;
        _serial.DataBits = DataBits;
        _serial.StopBits = StopBits;
        _serial.Open();
        _serial.DataReceived += OnDataReceived;
        IsConnected = true;
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (sender is not SerialPort serialPort)
        {
            return;
        }

        int length = serialPort.BytesToRead;
        byte[] buffer = new byte[length];
        serialPort.Read(buffer, 0, length);
        Task.Factory.StartNew(() =>
        {
            Receive?.Invoke(this, new ReceiveArgs()
            {
                Data = buffer
            });
        });
    }


    public List<string> GetPortNames() => SerialPort.GetPortNames().ToList();

    public void Dispose()
    {
        if (_serial != null)
        {
            _serial.DataReceived -= OnDataReceived;
            _serial.Close();
            _serial.Dispose();
            _serial = null;
        }
        IsConnected = false;
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
        return Task.Factory.StartNew(() => { Send(buffer, start, length); });
    }

    public string? PortName { get; set; }
    public int BaudRate { get; set; }
    public int DataBits { get; set; }
    public StopBits StopBits { get; set; }
    public Parity Parity { get; set; }
}