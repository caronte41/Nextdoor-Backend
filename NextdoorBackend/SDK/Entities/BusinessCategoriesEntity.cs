namespace NextDoorBackend.SDK.Entities
{
    public class BusinessCategoriesEntity
    {
        public Guid? Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? CategorySubType { get; set; }
    }
}
