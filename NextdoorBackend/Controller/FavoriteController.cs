using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.Favorite;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.Favorite.Request;
using NextDoorBackend.ClassLibrary.Favorite.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFavoritesInteractions _favoritesInteractions;

        public FavoriteController(IFavoritesInteractions favoritesInteractions, AppDbContext context)
        {
            _favoritesInteractions = favoritesInteractions;
            _context = context;
        }

        [HttpPost("AddOrRemoveFavorite")]
        public async Task<BaseResponseDto<AddOrRemoveFavoriteResponse>> AddOrRemoveFavorite(AddOrRemoveFavoriteRequest requestDto)
        {
            try
            {
                var response = await _favoritesInteractions.AddOrRemoveFavorite(requestDto);
                return BaseResponseDto<AddOrRemoveFavoriteResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<AddOrRemoveFavoriteResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetFavoritedBusinessesByIndividual")]
        public async Task<BaseResponseDto<List<UpsertBusinessProfileRequest>>> GetFavoritedBusinessesByIndividual([FromQuery] AddOrRemoveFavoriteRequest requestDto)
        {
            try
            {
                var response = await _favoritesInteractions.GetFavoritedBusinessesByIndividual(requestDto);
                return BaseResponseDto<List<UpsertBusinessProfileRequest>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<UpsertBusinessProfileRequest>>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetFavoritesCountForBusiness")]
        public async Task<BaseResponseDto<int>> GetFavoritesCountForBusiness([FromQuery] AddOrRemoveFavoriteRequest requestDto)
        {
            try
            {
                var response = await _favoritesInteractions.GetFavoritesCountForBusiness(requestDto);
                return BaseResponseDto<int>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<int>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
