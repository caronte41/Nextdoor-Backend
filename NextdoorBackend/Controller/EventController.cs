using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.Event;
using NextDoorBackend.Business.Favorite;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.Event.Request;
using NextDoorBackend.ClassLibrary.Event.Response;
using NextDoorBackend.ClassLibrary.Favorite.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEventInteractions _eventInteractions;

        public EventController(IEventInteractions eventInteractions, AppDbContext context)
        {
            _eventInteractions = eventInteractions;
            _context = context;
        }
        [HttpPost("CreateEventAsync")]
        public async Task<BaseResponseDto<CreateEventResponse>> CreateEventAsync([FromBody] CreateEventRequest requestDto)
        {
            try
            {
                var response = await _eventInteractions.CreateEventAsync(requestDto);
                return BaseResponseDto<CreateEventResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<CreateEventResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("ChangeEventStatus")]
        public async Task<BaseResponseDto<ChangeEventStatusResponse>> ChangeEventStatus([FromBody] ChangeEventStatusRequest requestDto)
        {
            try
            {
                var response = await _eventInteractions.ChangeEventStatus(requestDto);
                return BaseResponseDto<ChangeEventStatusResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<ChangeEventStatusResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
