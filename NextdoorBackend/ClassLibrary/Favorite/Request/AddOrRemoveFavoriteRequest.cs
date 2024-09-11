namespace NextDoorBackend.ClassLibrary.Favorite.Request
{
    public class AddOrRemoveFavoriteRequest
    {
        public Guid? IndividualProfileId { get; set; }
        public Guid? BusinessProfileId { get; set; }
    }
}
