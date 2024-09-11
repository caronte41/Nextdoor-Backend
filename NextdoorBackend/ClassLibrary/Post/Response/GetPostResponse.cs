namespace NextDoorBackend.ClassLibrary.Post.Response
{
    public class GetPostResponse
    {
        public Guid? Id { get; set; }
        public Guid? ProfileId { get; set; }
        public string? Summary { get; set; }
        public string? PhotoUrl { get; set; } 
        public string[]? VideoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? NeighborhoodId { get; set; }
    }
}
