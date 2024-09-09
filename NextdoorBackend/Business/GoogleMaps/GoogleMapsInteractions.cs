using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.Algorithm;
using NetTopologySuite.DataStructures;
using NetTopologySuite.Densify;
using NetTopologySuite.Dissolve;
using NetTopologySuite.EdgeGraph;
using NetTopologySuite.GeometriesGraph;
using NetTopologySuite.Index;
using NetTopologySuite.IO;
using NetTopologySuite.LinearReferencing;
using NetTopologySuite.Mathematics;
using NetTopologySuite.Noding;
using NetTopologySuite.Operation;
using NetTopologySuite.Planargraph;
using NetTopologySuite.Precision;
using NetTopologySuite.Shape;
using NetTopologySuite.Simplify;
using NetTopologySuite.Triangulate;
using NetTopologySuite.Utilities;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.GoogleMaps.Request;
using NextDoorBackend.ClassLibrary.GoogleMaps.Response;
using NextDoorBackend.Data;
using Npgsql;
using System.Net;
using System.Xml.Linq;

namespace NextDoorBackend.Business.GoogleMaps
{
    public class GoogleMapsInteractions : IGoogleMapsInteractions
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public GoogleMapsInteractions(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<GetLanLngByAddressResponse> GetLatLngByAddress(GetLanLngByAddressRequest request)
        {
            string apiKey = "AIzaSyBkBErYTxs3Rhosq2Z9C3kD2oPMxQV5oa4"; // Replace with your actual API key
            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/xml?address={Uri.EscapeDataString(request.Address)}&key={apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                // Handle the error appropriately
                throw new Exception("Failed to retrieve location data from Google Maps API");
            }

            XDocument xdoc = XDocument.Load(await response.Content.ReadAsStreamAsync());

            XElement result = xdoc.Element("GeocodeResponse")?.Element("result");
            XElement locationElement = result?.Element("geometry")?.Element("location");
            XElement lat = locationElement?.Element("lat");
            XElement lng = locationElement?.Element("lng");

            if (lat == null || lng == null)
            {
                // Handle missing latitude or longitude appropriately
                throw new Exception("Failed to retrieve latitude or longitude");
            }

            return new GetLanLngByAddressResponse
            {
                Latitude = double.Parse(lat.Value),
                Longitude = double.Parse(lng.Value)
            };
        }
        public async Task<int?> GetNeighborhoodIdByLatLng(double? latitude, double? longitude)
        {
            var query = @"
        SELECT *
        FROM ""Neighborhoods"" n
        WHERE ST_Contains(
            n.""Geometry"",
            ST_SetSRID(ST_MakePoint(@longitude, @latitude), 4326)
        )
        LIMIT 1";

            try
            {
                // Execute the raw SQL query
                var neighborhood = await _context.Neighborhoods
                    .FromSqlRaw(query,
                        new NpgsqlParameter("@longitude", longitude),
                        new NpgsqlParameter("@latitude", latitude))
                    .FirstOrDefaultAsync();

                return neighborhood.Id;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
        }
    }
}
