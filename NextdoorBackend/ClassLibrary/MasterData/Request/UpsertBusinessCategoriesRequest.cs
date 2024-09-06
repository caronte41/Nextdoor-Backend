namespace NextDoorBackend.ClassLibrary.MasterData.Request
{
    public class UpsertBusinessCategoriesRequest
    {
        public Guid? Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? CategorySubType { get; set; }
    }
}
