namespace NextDoorBackend.ClassLibrary.MasterData.Request
{
    public class UpsertGenderRequest
    {
        public Guid? Id { get; set; }
        public string? GenderName { get; set; }
    }
}
