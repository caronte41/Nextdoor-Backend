using NetTopologySuite.Geometries;

namespace NextDoorBackend.SDK.Entities
{
    public class NeighborhoodEntity
    {
        public int Id { get; set; }
        public long PlaceId { get; set; }
        public string OSMType { get; set; }
        public long OSMId { get; set; }
        public string DisplayName { get; set; }
        public float Importance { get; set; }
        public string Address { get; set; }
        public Polygon Bbox { get; set; } // PostGIS geometry type
        public Polygon Geometry { get; set; } // PostGIS geometry type
      
    }
}
