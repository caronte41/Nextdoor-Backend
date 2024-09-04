using NextDoorBackend.ClassLibrary.Employee.Request;
using NextDoorBackend.ClassLibrary.Employee.Response;

namespace NextDoorBackend.Business.Employee
{
    public interface IEmployeeInteractions
    {
        Task<UpsertEmployeeResponse> UpsertEmployee(UpsertEmployeeRequest request);
        Task<GetEmployeeByIdResponse> GetEmployeeById(GetEmployeeByIdRequest request);
    }
}
