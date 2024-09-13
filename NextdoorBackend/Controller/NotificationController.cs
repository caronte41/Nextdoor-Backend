using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.Notification;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Notification.Request;
using NextDoorBackend.ClassLibrary.Notification.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificaitonController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly INotificaitonInteractions _notificaitonInteractions;

        public NotificaitonController(INotificaitonInteractions notificaitonInteractions, AppDbContext context)
        {
            _notificaitonInteractions = notificaitonInteractions;
            _context = context;
        }
        [HttpGet("GetNotificationsByAccountId")]
        public async Task<BaseResponseDto<List<GetNotificationsByAccountIdResponse>>> GetNotificationsByAccountId([FromQuery] GetNotificationsByAccountIdRequest requestDto)
        {
            try
            {
                var response = await _notificaitonInteractions.GetNotificationsByAccountId(requestDto);
                return BaseResponseDto<List<GetNotificationsByAccountIdResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<GetNotificationsByAccountIdResponse>>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
