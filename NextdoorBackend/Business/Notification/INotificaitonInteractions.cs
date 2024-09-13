using NextDoorBackend.ClassLibrary.Notification.Request;
using NextDoorBackend.ClassLibrary.Notification.Response;

namespace NextDoorBackend.Business.Notification
{
    public interface INotificaitonInteractions
    {
        Task<List<GetNotificationsByAccountIdResponse>> GetNotificationsByAccountId(GetNotificationsByAccountIdRequest request);
    }
}
