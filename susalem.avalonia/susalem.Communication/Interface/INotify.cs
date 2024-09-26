namespace susalem.Communication.Interface;

public interface INotify
{
    public void Info(string message);
    public void Success(string message);
    public void Warn(string message);
    public void Error(string message);
}