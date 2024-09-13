namespace NextDoorBackend.ClassLibrary.FriendshipConnection.Request
{
    public class RespondToFriendshipConnectionRequest
    {
      public Guid? RequestId { get; set; }
      public bool? IsAccepted { get; set; }
    }
}
