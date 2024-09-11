using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.ClassLibrary.Favorite.Request;
using NextDoorBackend.ClassLibrary.Favorite.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;

namespace NextDoorBackend.Business.Favorite
{
    public class FavoritesInteractions : IFavoritesInteractions
    {
        private readonly AppDbContext _context;
 
        public FavoritesInteractions(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AddOrRemoveFavoriteResponse> AddOrRemoveFavorite(AddOrRemoveFavoriteRequest request)
        {
            var existingFavorite = await _context.Favorites
             .FirstOrDefaultAsync(f => f.ProfileId == request.IndividualProfileId && f.BusinessProfileId == request.BusinessProfileId);

            if (existingFavorite != null)
            {
                _context.Favorites.Remove(existingFavorite);
            }
            else
            {
                var newFavorite = new FavoritesEntitys
                {
                    Id = Guid.NewGuid(),
                    ProfileId = request.IndividualProfileId,
                    BusinessProfileId = request.BusinessProfileId,
                    FavoritedAt = DateTime.UtcNow
                };

                await _context.Favorites.AddAsync(newFavorite);
            }

            await _context.SaveChangesAsync();
            return new AddOrRemoveFavoriteResponse { };
        }
        public async Task<List<UpsertBusinessProfileRequest>> GetFavoritedBusinessesByIndividual(AddOrRemoveFavoriteRequest request)
        {
            var favoritedBusinesses = await _context.Favorites
        .Where(f => f.ProfileId == request.IndividualProfileId)
        .Select(f => f.BusinessProfile)
        .ToListAsync();

            return favoritedBusinesses.Adapt<List<UpsertBusinessProfileRequest>>();
        }
        public async Task<int> GetFavoritesCountForBusiness(AddOrRemoveFavoriteRequest request)
        {
            var favoritesCount = await _context.Favorites
                .CountAsync(f => f.BusinessProfileId == request.BusinessProfileId);

            return favoritesCount;
        }
    }
}
