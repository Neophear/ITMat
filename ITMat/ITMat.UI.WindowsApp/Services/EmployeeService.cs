using ITMat.Core.DTO;
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
        {
            var request = new RestRequest($"employee/{id}", Method.GET);
            var response = await ExecuteRequestAsync(request);

            return JsonConvert.DeserializeObject<EmployeeDTO>(response.Content);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var request = new RestRequest("employee", Method.GET);
            var response = await ExecuteRequestAsync(request);

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(response.Content);
        }

        public async Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync()
        {
            var request = new RestRequest("employee/status", Method.GET);
            var response = await ExecuteRequestAsync(request);

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeStatusDTO>>(response.Content);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            var request = new RestRequest($"employee/{id}", Method.PUT);
            request.AddJsonBody(employee);

            await ExecuteRequestAsync(request);
        }
    }
}