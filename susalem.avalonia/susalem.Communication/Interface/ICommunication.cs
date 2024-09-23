using susalem.Communication.Args;

namespace susalem.Communication;

public interface ICommunication : IDisposable
{
    /// <summary>
    /// 接收事件
    /// </summary>
    public event EventHandler<ReceiveArgs> Receive;

    public bool IsConnected { get; }

    public void Connect();

    public void Send(byte[] buffer, int start, int length);

    public Task SendAsync(byte[] buffer, int start, int length);

    public void Send(byte[] buffer)
    {
        Send(buffer, 0, buffer.Length);
    }

    public Task SendAsync(byte[] buffer)
    {
        return SendAsync(buffer, 0, buffer.Length);
    }
}