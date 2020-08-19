using ITMat.UI.WPF.Interfaces;
using ITMat.UI.WPF.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WPF.ServiceLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRestClient client;

        public EmployeeService(IConfiguration configuration)
            => client = new RestClient(configuration["api_url"]);

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var request = new RestRequest("employee", Method.GET);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Content);
            else
                throw new Exception("Could not get employees.");
        }
    }
}