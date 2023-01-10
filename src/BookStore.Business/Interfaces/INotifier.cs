using BookStore.Business.Notifications;

namespace BookStore.Business.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}