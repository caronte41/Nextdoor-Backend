namespace NextDoorBackend.ClassLibrary.FriendshipConnection.Request
{
    public class SendFriendshipConnectionRequest
    {
        public Guid? requestorId { get; set; }
        public Guid? receiverId { get; set; }
    }
}
