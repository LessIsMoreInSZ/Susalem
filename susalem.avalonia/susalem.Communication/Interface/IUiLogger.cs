namespace susalem.Communication.Interface;

public interface IUiLogger
{
    public void Message(string message, string color);

    public void Info(string message);

    public void Success(string message);

    public void Warning(string message);

    public void Error(string message);
}