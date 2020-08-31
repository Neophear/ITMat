using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.MockService
{
    public class MockEmployeeService : AbstractMockService, IEmployeeService
    {
        private readonly IEnumerable<EmployeeDTO> employees = new List<EmployeeDTO>
            {
                new EmployeeDTO{ Id = 1, MANR = "370929", Name = "Stiig Gade", Status = 1 },
                new EmployeeDTO{ Id = 2, MANR = "123456", Name = "Peter Petersen", Status = 1 },
                new EmployeeDTO{ Id = 3, MANR = "654321", Name = "Jens Jensen", Status = 2 }
            };

        private readonly IEnumerable<EmployeeStatusDTO> statuses = new List<EmployeeStatusDTO>
            {
                new EmployeeStatusDTO{Id = 1, Name = "Aktiv", CanLend = true },
                new EmployeeStatusDTO{Id = 2, Name = "Blacklisted", CanLend = false },
                new EmployeeStatusDTO { Id = 3, Name = "Inactive", CanLend = false }
            };

        public Task<int> CreateEmployeeAsync(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await ExecuteWithDelay(() =>
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                return new EmployeeDTO
                {
                    Id = employee.Id,
                    MANR = employee.MANR,
                    Name = employee.Name,
                    Status = employee.Status
                };
            });

        public async Task<IEnumerable<EmployeeListedDTO>> GetEmployeesAsync()
            => await ExecuteWithDelay(() => employees.Select(e =>
                new EmployeeListedDTO
                {
                    Id = e.Id,
                    MANR = e.MANR,
                    Name = e.Name,
                    Status = statuses.First(s => s.Id == e.Status).Name
                }));

        public async Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync()
            => await ExecuteWithDelay(() => statuses.Select(s => new EmployeeStatusDTO
            {
                Id = s.Id,
                Name = s.Name,
                CanLend = s.CanLend
            }));

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
            => await ExecuteWithDelay(() =>
            {
                var originalEmployee = employees.FirstOrDefault(e => e.Id == id);

                if (originalEmployee == null)
                    throw new KeyNotFoundException("Medarbejderen findes ikke.");

                originalEmployee.MANR = employee.MANR;
                originalEmployee.Name = employee.Name;
                originalEmployee.Status = employee.Status;
            });
    }
}