using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Notification.Request;
using NextDoorBackend.ClassLibrary.Notification.Response;
using NextDoorBackend.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextDoorBackend.Business.Notification
{
    public class NotificaitonInteractions : INotificaitonInteractions
    {
        private readonly AppDbContext _context;

        public NotificaitonInteractions(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetNotificationsByAccountIdResponse>> GetNotificationsByAccountId(GetNotificationsByAccountIdRequest request)
        {
            var notifications = await _context.Notifications
              .Where(n => n.AccountId == request.AccountId)
              .OrderByDescending(n => n.CreatedAt) // Assuming you have a CreatedAt or similar field for sorting
              .ToListAsync();

            var response = notifications.Adapt<List<GetNotificationsByAccountIdResponse>>();
            return response;
        }
    }
}
