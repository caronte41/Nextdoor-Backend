using NextDoorBackend.ClassLibrary.Common;

namespace NextDoorBackend.ClassLibrary.Post.Request
{
    public class GetAllPostsByNeighborhoodIdRequest 
    {
        public int? NeighborhoodId { get; set; }
        public Guid? ProfileId { get; set; }
    }
}
