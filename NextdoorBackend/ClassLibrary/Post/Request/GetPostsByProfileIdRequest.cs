namespace NextDoorBackend.ClassLibrary.Post.Request
{
    public class GetPostsByProfileIdRequest
    {
        public Guid? ProfileId { get; set; }
        public string? FlowType { get; set; }
        public double? UserLatitude { get; set; }
        public double? UserLongitude { get; set; }
    }

}
