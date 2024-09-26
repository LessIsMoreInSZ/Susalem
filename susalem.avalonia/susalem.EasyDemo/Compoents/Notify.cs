using Avalonia.Controls.Notifications;
using susalem.Communication.Interface;

namespace susalem.EasyDemo.Compoents;

public class Notify : INotify
{
    private readonly WindowNotificationManager _notificationManager;

    public Notify(WindowNotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
    }


    public void Info(string message)
    {
        _notificationManager.Show(new Notification("提示", message, NotificationType.Information));
    }

    public void Success(string message)
    {
        _notificationManager?.Show(new Notification("提示", message, NotificationType.Success));
    }

    public void Warn(string message)
    {
        _notificationManager?.Show(new Notification("提示", message, NotificationType.Warning));
    }

    public void Error(string message)
    {
        _notificationManager?.Show(new Notification("提示", message, NotificationType.Error));
    }
}