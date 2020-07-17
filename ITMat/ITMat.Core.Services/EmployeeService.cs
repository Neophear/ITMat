using AutoMapper;
using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class EmployeeService : AbstractService<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository repo;

        public EmployeeService(IEmployeeRepository repo)
            : base(MapperFactory.Create<EmployeeProfile>())
            => this.repo = repo;

        public async Task<EmployeeDTO> FindEmployeeAsync(string manr)
            => Mapper.Map<EmployeeDTO>(await repo.FindEmployeeAsync(manr));

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => Mapper.Map<EmployeeDTO>(await repo.GetEmployeeAsync(id));

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
            => Mapper.Map<IEnumerable<EmployeeDTO>>(await repo.GetEmployeesAsync());

        public async Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync()
            => Mapper.Map<IEnumerable<EmployeeStatusDTO>>(await repo.GetEmployeeStatusesAsync());

        public async Task<int> InsertEmployeeAsync(EmployeeDTO employee)
        {
            Validate(employee);
            return await repo.InsertEmployee(Mapper.Map<Employee>(employee));
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            Validate(employee);
            await repo.UpdateEmployee(id, Mapper.Map<Employee>(employee));
        }

        private void Validate(EmployeeDTO employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (employee.MANR is null)
                throw new ArgumentNullException(nameof(employee.MANR));

            if (!new Regex(@"^[1-9]\d{5}$").IsMatch(employee.MANR))
                throw new ArgumentException($"{nameof(employee.MANR)} must consist of 6 digits, not starting with 0.");

            if (employee.Name is null)
                throw new ArgumentNullException(nameof(employee.Name));

            if (employee.Name == "" || employee.Name.Length > 250)
                throw new ArgumentException($"{nameof(employee.Name)} must be between 1 and 250 characters long.");
        }

        private class EmployeeProfile : Profile
        {
            public EmployeeProfile()
            {
                CreateMap<Employee, EmployeeDTO>();
                CreateMap<EmployeeStatus, EmployeeStatusDTO>();
            }
        }
    }
}