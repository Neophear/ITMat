using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public class EmployeeService : AbstractService, IEmployeeService
    {
        public EmployeeService(IConfiguration configuration)
            : base(configuration) { }

        public async Task<int> CreateEmployee(EmployeeDTO employee)
        {
            var request = new RestRequest("employee", Method.POST);
            request.AddJsonBody(employee);
            var response = await ExecuteRequestAsync(request);

            return JsonConvert.DeserializeObject<dynamic>(response.Content).Id;
        }

        public Task<int> CreateEmployeeAsync(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await ExecuteAsync<EmployeeDTO>(new RestRequest($"employee/{id}", Method.GET));

        public async Task<IEnumerable<EmployeeListedDTO>> GetEmployeesAsync()
            => await ExecuteAsync<IEnumerable<EmployeeListedDTO>>(new RestRequest("employee", Method.GET));

        public async Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync()
            => await ExecuteAsync<IEnumerable<EmployeeStatusDTO>>(new RestRequest("employee/status", Method.GET));

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            var request = new RestRequest($"employee/{id}", Method.PUT);
            request.AddJsonBody(employee);
            await ExecuteRequestAsync(request);
        }
    }
}