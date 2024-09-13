using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.FriendshipConnection;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.Notification;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.FriendshipConnection.Request;
using NextDoorBackend.ClassLibrary.FriendshipConnection.Response;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Notification.Request;
using NextDoorBackend.ClassLibrary.Notification.Response;
using NextDoorBackend.ClassLibrary.Post.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipConnectionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFriendshipConnectionInteractions _friendshipConnectionInteractions;

        public FriendshipConnectionController(IFriendshipConnectionInteractions friendshipConnectionInteractions, AppDbContext context)
        {
            _friendshipConnectionInteractions = friendshipConnectionInteractions;
            _context = context;
        }
        [HttpPost("SendFriendshipRequest")]
        public async Task<BaseResponseDto<SendFriendshipConnectionResponse>> SendFriendshipRequest(SendFriendshipConnectionRequest requestDto)
        {
            try
            {
                var response = await _friendshipConnectionInteractions.SendFriendshipRequest(requestDto);
                return BaseResponseDto<SendFriendshipConnectionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<SendFriendshipConnectionResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("RespondToFriendshipRequest")]
        public async Task<BaseResponseDto<SendFriendshipConnectionResponse>> RespondToFriendshipRequest(RespondToFriendshipConnectionRequest requestDto)
        {
            try
            {
                var response = await _friendshipConnectionInteractions.RespondToFriendshipRequest(requestDto);
                return BaseResponseDto<SendFriendshipConnectionResponse>.Success(response);
            }
            catch (Exception ex)
            { 
                return BaseResponseDto<SendFriendshipConnectionResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
