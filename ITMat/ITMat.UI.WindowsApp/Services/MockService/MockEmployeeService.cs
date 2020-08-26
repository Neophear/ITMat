using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.MockService
{
    public class MockEmployeeService : IEmployeeService
    {
        private readonly IEnumerable<EmployeeDTO> employees = new List<EmployeeDTO>
            {
                new EmployeeDTO{ Id = 1, MANR = "370929", Name = "Stiig Gade", Status = 1 },
                new EmployeeDTO{ Id = 2, MANR = "123456", Name = "Peter Petersen", Status = 1 },
                new EmployeeDTO{ Id = 3, MANR = "654321", Name = "Jens Jensen", Status = 2 }
            };

        public Task<int> CreateEmployeeAsync(EmployeeDTO employee)
        {
            throw new System.NotImplementedException();
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
        {
            await Task.Delay(200);
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            await Task.Delay(200);
            return employees;
        }

        public async Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync()
        {
            await Task.Delay(200);
            return new List<EmployeeStatusDTO>
            {
                new EmployeeStatusDTO{Id = 1, Name = "Aktiv", CanLend = true },
                new EmployeeStatusDTO{Id = 2, Name = "Blacklisted", CanLend = false },
                new EmployeeStatusDTO{Id = 3, Name = "Inactive", CanLend = false }
            };
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            await Task.Delay(200);

            var originalEmployee = employees.FirstOrDefault(e => e.Id == id);

            if (originalEmployee == null)
                throw new KeyNotFoundException("Medarbejderen findes ikke.");

            originalEmployee.MANR = employee.MANR;
            originalEmployee.Name = employee.Name;
            originalEmployee.Status = employee.Status;
        }
    }
}