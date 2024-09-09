using NextDoorBackend.ClassLibrary.GoogleMaps.Request;
using NextDoorBackend.ClassLibrary.GoogleMaps.Response;

namespace NextDoorBackend.Business.GoogleMaps
{
    public interface IGoogleMapsInteractions
    {
        Task<GetLanLngByAddressResponse> GetLatLngByAddress(GetLanLngByAddressRequest request);
        Task<int?> GetNeighborhoodIdByLatLng(double? latitude, double? longitude);
    }
}
