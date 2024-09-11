using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.Post.Request;
using NextDoorBackend.ClassLibrary.Post.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;

namespace NextDoorBackend.Business.Post
{
    public class PostsInteractions : IPostsInteractions
    {
        private readonly AppDbContext _context;
        private readonly IMasterDataInteractions _masterDataInteractions;
        public PostsInteractions(AppDbContext context, IMasterDataInteractions masterDataInteractions)
        {
            _context = context;
            _masterDataInteractions = masterDataInteractions;
        }
        public async Task<UpsertPostResponse> UpsertPost(UpsertPostRequest request)
        {
            if (request.ProfileId == null)
                throw new ArgumentNullException(nameof(request.ProfileId), "ProfileId cannot be null.");

            int? neighborhoodId;

            // First, try to find the NeighborhoodId in the IndividualProfiles table
            var individualProfile = await _context.IndividualProfiles
                .FirstOrDefaultAsync(ip => ip.Id == request.ProfileId.Value);

            if (individualProfile != null)
            {
                neighborhoodId = individualProfile.NeighborhoodId;
            }
            else
            {
                // If not found in IndividualProfiles, try the BusinessProfiles table
                var businessProfile = await _context.BusinessProfiles
                    .FirstOrDefaultAsync(bp => bp.Id == request.ProfileId.Value);

                if (businessProfile != null)
                {
                    neighborhoodId = businessProfile.NeighborhoodId;
                }
                else
                {
                    throw new Exception("Profile not found in either IndividualProfiles or BusinessProfiles.");
                }
            }

            PostsEntity post;

            // If Id is provided, try to find the existing post
            if (request.Id.HasValue)
            {
                post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.Id == request.Id.Value && p.ProfileId == request.ProfileId.Value);

                if (post != null)
                {
                    // Update the existing post
                    post.Summary = request.Summary;
                    post.PhotoUrl = request.PhotoData != null ? await _masterDataInteractions.SaveFile(request.PhotoData, "photos") : post.PhotoUrl;
                    post.VideoUrl = request.VideoData != null ? await _masterDataInteractions.SaveFile(request.VideoData, "videos") : post.VideoUrl;
                    post.CreatedAt = request.CreatedAt ?? DateTime.UtcNow;
                    post.NeighborhoodId = neighborhoodId;

                    _context.Posts.Update(post);
                }
                else
                {
                    // If no matching post was found, create a new one
                    post = new PostsEntity
                    {
                        Id = Guid.NewGuid(),
                        ProfileId = request.ProfileId.Value,
                        Summary = request.Summary,
                        PhotoUrl = request.PhotoData != null ? await _masterDataInteractions.SaveFile(request.PhotoData, "photos") : null,
                        VideoUrl = request.VideoData != null ? await _masterDataInteractions.SaveFile(request.VideoData, "videos") : null,
                        CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
                        NeighborhoodId = neighborhoodId
                    };

                    await _context.Posts.AddAsync(post);
                }
            }
            else
            {
                // If no Id is provided, create a new post
                post = new PostsEntity
                {
                    Id = Guid.NewGuid(),
                    ProfileId = request.ProfileId.Value,
                    Summary = request.Summary,
                    PhotoUrl = request.PhotoData != null ? await _masterDataInteractions.SaveFile(request.PhotoData, "photos") : null,
                    VideoUrl = request.VideoData != null ? await _masterDataInteractions.SaveFile(request.VideoData, "videos") : null,
                    CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
                    NeighborhoodId = neighborhoodId
                };

                await _context.Posts.AddAsync(post);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return new UpsertPostResponse
            {
                Id=post.Id
            };
        }
        public async Task<GetPostResponse> GetPostByPostId(GetPostByPostIdRequest request) 
        {
            var post = await _context.Posts
           .Where(f => f.Id == request.PostId)
           .FirstOrDefaultAsync(); 

            return post.Adapt<GetPostResponse>();
        }
        public async Task<List<GetPostResponse>> GetPostsByProfileId(GetPostsByProfileIdRequest request) 
        {
            var post = await _context.Posts
              .Where(f => f.ProfileId == request.ProfileId)
              .ToListAsync();

            return post.Adapt<List<GetPostResponse>>();
        }
        public async Task<List<GetPostResponse>> GetAllPostsByNeighborhoodId(GetAllPostsByNeighborhoodIdRequest request) 
        {
            var post = await _context.Posts
                  .Where(f => f.NeighborhoodId == request.NeighborhoodId)
                  .ToListAsync();

            return post.Adapt<List<GetPostResponse>>();
        }

    }
}
