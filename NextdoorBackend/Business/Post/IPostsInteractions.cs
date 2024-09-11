using NextDoorBackend.ClassLibrary.Post.Request;
using NextDoorBackend.ClassLibrary.Post.Response;

namespace NextDoorBackend.Business.Post
{
    public interface IPostsInteractions
    {
        Task<UpsertPostResponse>UpsertPost(UpsertPostRequest request);
        Task<GetPostResponse> GetPostByPostId(GetPostByPostIdRequest request);
        Task<List<GetPostResponse>>GetPostsByProfileId(GetPostsByProfileIdRequest request);
        Task<List<GetPostResponse>> GetAllPostsByNeighborhoodId(GetAllPostsByNeighborhoodIdRequest request);
        Task DeletePost(GetPostByPostIdRequest request);
    }
}
