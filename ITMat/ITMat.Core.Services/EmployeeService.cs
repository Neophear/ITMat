using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repo;

        public EmployeeService(IEmployeeRepository repo)
            => this.repo = repo;

        public async Task<EmployeeDTO> FindEmployeeAsync(string manr)
            => await repo.FindEmployeeAsync(manr);

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await repo.GetEmployeeAsync(id);

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
            => await repo.GetEmployeesAsync();
    }
}