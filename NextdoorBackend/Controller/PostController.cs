using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.Post;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.Post.Request;
using NextDoorBackend.ClassLibrary.Post.Response;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPostsInteractions _postInteractions;

        public PostController(IPostsInteractions postInteractions, AppDbContext context)
        {
            _postInteractions = postInteractions;
            _context = context;
        }
        [HttpPost("UpsertPost")]
        public async Task<BaseResponseDto<UpsertPostResponse>> UpsertPost([FromBody]UpsertPostRequest requestDto)
        {
            try
            {
                var response = await _postInteractions.UpsertPost(requestDto);
                return BaseResponseDto<UpsertPostResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpsertPostResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetPostByPostId")]
        public async Task<BaseResponseDto<GetPostResponse>> GetPostByPostId([FromQuery] GetPostByPostIdRequest requestDto)
        {
            try
            {
                var response = await _postInteractions.GetPostByPostId(requestDto);
                return BaseResponseDto<GetPostResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<GetPostResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetPostsByProfileId")]
        public async Task<BaseResponseDto<List<GetPostResponse>>> GetPostsByProfileId([FromQuery] GetPostsByProfileIdRequest requestDto)
        {
            try
            {
                var response = await _postInteractions.GetPostsByProfileId(requestDto);
                return BaseResponseDto<List<GetPostResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<GetPostResponse>>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllPostsByNeighborhoodId")]
        public async Task<BaseResponseDto<List<GetPostResponse>>> GetAllPostsByNeighborhoodId([FromQuery] GetAllPostsByNeighborhoodIdRequest requestDto)
        {
            try
            {
                var response = await _postInteractions.GetAllPostsByNeighborhoodId(requestDto);
                return BaseResponseDto<List<GetPostResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<GetPostResponse>>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeletePost")]
        public async Task<BaseResponseDto<DeletePostResponse>> DeletePost([FromRoute] GetPostByPostIdRequest requestDto)
        {
            try
            {
                 await _postInteractions.DeletePost(requestDto);
                return BaseResponseDto<DeletePostResponse>.Success();
            }
            catch (Exception ex)
            {
                return BaseResponseDto<DeletePostResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
