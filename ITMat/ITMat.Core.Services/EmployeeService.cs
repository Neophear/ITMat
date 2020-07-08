using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public async Task<int> InsertEmployeeAsync(EmployeeDTO employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.MANR is null)
                throw new ArgumentNullException(nameof(employee.MANR));

            if (!new Regex(@"^[1-9]\d{5}$").IsMatch(employee.MANR))
                throw new ArgumentException($"{nameof(employee.MANR)} must consist of 6 digits, not starting with 0.", nameof(employee.MANR));

            if (employee.Name is null)
                throw new ArgumentNullException(nameof(employee.Name));

            if (employee.Name == "" || employee.Name.Length > 250)
                throw new ArgumentException($"{nameof(employee.Name)} must be between 1 and 250 characters long.");

            return await repo.InsertEmployee(employee);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            if (id <= 0)
                throw new ArgumentException($"{nameof(id)} must be greater than 0.", nameof(id));

            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.MANR is null)
                throw new ArgumentNullException(nameof(employee.MANR));

            if (!new Regex(@"^[1-9]\d{5}$").IsMatch(employee.MANR))
                throw new ArgumentException($"{nameof(employee.MANR)} must consist of 6 digits, not starting with 0.", nameof(employee.MANR));

            if (employee.Name is null)
                throw new ArgumentNullException(nameof(employee.Name));

            if (employee.Name == "" || employee.Name.Length > 250)
                throw new ArgumentException($"{nameof(employee.Name)} must be between 1 and 250 characters long.", nameof(employee.Name));
            
            await repo.UpdateEmployee(id, employee);
        }
    }
}