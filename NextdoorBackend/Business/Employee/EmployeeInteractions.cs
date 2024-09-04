using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.ClassLibrary.Employee.Request;
using NextDoorBackend.ClassLibrary.Employee.Response;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;

namespace NextDoorBackend.Business.Employee
{
    public class EmployeeInteractions : IEmployeeInteractions
    {
        private readonly AppDbContext _context;

        public EmployeeInteractions(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeeByIdResponse> GetEmployeeById(GetEmployeeByIdRequest request)
        {
            var data = await _context.Employees
                .Where(f => f.Id == request.Id)
                .FirstOrDefaultAsync();
            var employeeData = data.Adapt<GetEmployeeByIdResponse>();
            return employeeData;
        }
        public async Task<UpsertEmployeeResponse> UpsertEmployee(UpsertEmployeeRequest request)
        {
            var employeeEntity = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id);

            if (employeeEntity == null)
            {
                // Employee doesn't exist, insert new employee
                var newEmployeeEntity = new EmployeeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Title = request.Title
                };

                _context.Employees.Add(newEmployeeEntity);
                await _context.SaveChangesAsync();

                return new UpsertEmployeeResponse
                {
                    Id = newEmployeeEntity.Id
                };
            }
            else
            {
                // Employee exists, update the employee
                employeeEntity.Name = request.Name;
                employeeEntity.Title = request.Title;

                _context.Employees.Update(employeeEntity);
                await _context.SaveChangesAsync();

                return new UpsertEmployeeResponse
                {
                    Id = employeeEntity.Id
                };
            }
        }

    }
}
