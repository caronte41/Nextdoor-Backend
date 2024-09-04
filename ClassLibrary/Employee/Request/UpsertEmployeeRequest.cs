namespace NextDoorBackend.ClassLibrary.Employee.Request
{
    public class UpsertEmployeeRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
