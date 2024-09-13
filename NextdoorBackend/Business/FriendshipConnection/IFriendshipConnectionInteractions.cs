using NextDoorBackend.ClassLibrary.FriendshipConnection.Request;
using NextDoorBackend.ClassLibrary.FriendshipConnection.Response;

namespace NextDoorBackend.Business.FriendshipConnection
{
    public interface IFriendshipConnectionInteractions
    {
        Task<SendFriendshipConnectionResponse> SendFriendshipRequest(SendFriendshipConnectionRequest request);
        Task<SendFriendshipConnectionResponse> RespondToFriendshipRequest(RespondToFriendshipConnectionRequest request);
    }
}
