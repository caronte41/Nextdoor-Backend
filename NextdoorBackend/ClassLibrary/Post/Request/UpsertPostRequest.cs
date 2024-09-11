using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.SDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Post.Request
{
    public class UpsertPostRequest 
    {
        public Guid? Id { get; set; }
        public Guid? ProfileId { get; set; }
        public string? Summary { get; set; }
        public byte[]? PhotoData { get; set; }  // Base64 for image
        public byte[]? VideoData { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? NeighborhoodId { get; set; }
    }
}
