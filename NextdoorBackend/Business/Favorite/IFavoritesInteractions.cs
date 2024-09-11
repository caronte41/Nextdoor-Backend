using NextDoorBackend.ClassLibrary.Favorite.Request;
using NextDoorBackend.ClassLibrary.Favorite.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.SDK.Entities;

namespace NextDoorBackend.Business.Favorite
{
    public interface IFavoritesInteractions
    {
        Task<AddOrRemoveFavoriteResponse> AddOrRemoveFavorite(AddOrRemoveFavoriteRequest request);
        Task<List<UpsertBusinessProfileRequest>> GetFavoritedBusinessesByIndividual(AddOrRemoveFavoriteRequest request);
        Task<int>GetFavoritesCountForBusiness(AddOrRemoveFavoriteRequest request);
    }
}
