using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.Favorite.Response;
using NextDoorBackend.ClassLibrary.Post.Request;
using NextDoorBackend.ClassLibrary.Post.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using Npgsql;

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

            if (request.Id.HasValue)
            {
                post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.Id == request.Id.Value && p.ProfileId == request.ProfileId.Value);

                if (post != null)
                {
                    post.Summary = request.Summary;
                    post.PhotoUrl = request.PhotoData != null ? await _masterDataInteractions.SaveFile(request.PhotoData, "photos") : post.PhotoUrl;
                    post.VideoUrl = request.VideoData != null ? await _masterDataInteractions.SaveFile(request.VideoData, "videos") : post.VideoUrl;
                    post.CreatedAt = request.CreatedAt ?? DateTime.UtcNow;
                    post.NeighborhoodId = neighborhoodId;

                    _context.Posts.Update(post);
                }
                else
                {
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

            await _context.SaveChangesAsync();

            return new UpsertPostResponse
            {
                Id=post.Id
            };
        }
        public async Task<GetPostResponse> GetPostByPostId(GetPostByPostIdRequest request) 
        {
            var post = await _context.Posts
           .Where(f => f.Id == request.PostId).Include(p => p.Comments)
           .FirstOrDefaultAsync(); 

            return post.Adapt<GetPostResponse>();
        }
        public async Task<List<GetPostResponse>> GetPostsByProfileId(GetPostsByProfileIdRequest request) 
        {
            var query = _context.Posts
         .Include(p => p.Comments)
         .Include(p => p.Likes)
         .AsQueryable();

            // Apply filters based on the flow type
            switch (request.FlowType)
            {
                case "Recent":
                    // Order by CreatedAt, newest first
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;

                case "Nearby":
                    // Make sure ProfileId is provided to fetch the user's neighborhood
                    if (request.ProfileId.HasValue)
                    {
                        // Get the user's neighborhood
                        var userNeighborhoodId = await _context.IndividualProfiles
                            .FirstOrDefaultAsync(ip => ip.Id == request.ProfileId.Value);

                        if (userNeighborhoodId != null)
                        {
                            // Get the user's neighborhood geometry
                            var userNeighborhood = await _context.Neighborhoods
                                .Where(n => n.Id == userNeighborhoodId.NeighborhoodId.Value)
                                .Select(n => n.Geometry)
                                .FirstOrDefaultAsync();

                            if (userNeighborhood != null)
                            {
                                // Order by distance to the user's neighborhood
                                var sqlQuery = @"
                                           SELECT p.*
                                           FROM ""Posts"" p
                                           JOIN ""Neighborhoods"" pn ON p.""NeighborhoodId"" = pn.""Id""
                                           JOIN ""Neighborhoods"" un ON un.""Id"" = (
                                           SELECT ""NeighborhoodId""
                                           FROM ""IndividualProfiles"" ip
                                           WHERE ip.""Id"" = @userProfileId
                                                  )
                                                  WHERE ST_Distance(pn.""Geometry"", un.""Geometry"") < @distanceThreshold
                                                  ORDER BY ST_Distance(pn.""Geometry"",un.""Geometry"")
                                           ";

                                var userProfileIdParam = new NpgsqlParameter("@userProfileId", NpgsqlTypes.NpgsqlDbType.Uuid) { Value = request.ProfileId.Value };
                                var distanceThresholdParam = new NpgsqlParameter("@distanceThreshold", NpgsqlTypes.NpgsqlDbType.Integer) { Value = 10000 }; // Adjust as needed

                                query = _context.Posts
                                    .FromSqlRaw(sqlQuery, userProfileIdParam, distanceThresholdParam).Include(p => p.Comments).Include(p => p.Likes).AsQueryable();
                            }
                        }
                    }
                    break;

                case "Trending":
                    // Order by combined likes and comments count
                    query = query.OrderByDescending(p => (p.Likes.Count + p.Comments.Count));
                    break;

                default:
                    throw new ArgumentException("Invalid flow type.");
            }

            // Execute the query and adapt the results
            var posts = await query.ToListAsync();

            var postResponses = posts.Select(post => {
                var response = post.Adapt<GetPostResponse>();
                var likesList = post.Likes ?? new List<PostLikesEntity>();
                response.LikesCount = likesList.Count;
                response.UserLiked = likesList.Any(like => like.ProfileId == request.ProfileId);
                return response;
            }).ToList();

            return postResponses;
        }
        public async Task<List<GetPostResponse>> GetAllPostsByNeighborhoodId(GetAllPostsByNeighborhoodIdRequest request) 
        {
            var posts = await _context.Posts
                  .Where(f => f.NeighborhoodId == request.NeighborhoodId).Include(p => p.Comments).Include(p => p.Likes)
                  .ToListAsync();

            var postResponses = posts.Select(post => {
                var response = post.Adapt<GetPostResponse>();
                response.LikesCount = post.Likes.Count;
                response.UserLiked = post.Likes.Any(like => like.ProfileId == request.ProfileId);
                return response;
            }).ToList();

            return postResponses;
        }
        public async Task DeletePost(GetPostByPostIdRequest request)
        {
            var post = await _context.Posts
        .FirstOrDefaultAsync(p => p.Id == request.PostId);

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
        }
        public async Task<AddCommentResponse> AddComment(AddCommentRequest request)
        {
            PostCommentsEntity comment;

            comment = new PostCommentsEntity
            {
                Id = Guid.NewGuid(),
                ProfileId = request.ProfileId.Value,
                PostId = request.PostId.Value,
                Comment = request.Comment,
                CommentedAt = request.CommentedAt ?? DateTime.UtcNow,
            };

            await _context.PostComments.AddAsync(comment);

            await _context.SaveChangesAsync();

            return new AddCommentResponse 
            {
                Id = comment.Id,
            };
        }
        public async Task<AddPostLikeResponse> AddOrRemovePostLike(AddPostLikeRequest request)
        {
            var existingPostLike = await _context.PostLikes
             .FirstOrDefaultAsync(f => f.ProfileId == request.ProfileId && f.Id == request.Id);

            if (existingPostLike != null)
            {
                _context.PostLikes.Remove(existingPostLike);
            }
            else
            {
                var newPostLike = new PostLikesEntity
                {
                    Id = Guid.NewGuid(),
                    ProfileId = request.ProfileId.Value,
                    PostId = request.PostId.Value,
                    LikedAt = request.LikedAt ?? DateTime.UtcNow,
                };

                await _context.PostLikes.AddAsync(newPostLike);
            }

            await _context.SaveChangesAsync();
            return new AddPostLikeResponse
            {
               
            };
        }

    }
}
