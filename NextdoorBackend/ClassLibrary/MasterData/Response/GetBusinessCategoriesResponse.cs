namespace NextDoorBackend.ClassLibrary.MasterData.Response
{
    public class GetBusinessCategoriesResponse
    {
        public Guid? Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? CategorySubType { get; set; }
    }
}
