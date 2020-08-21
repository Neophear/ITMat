using ITMat.UI.WPF.Interfaces;
using ITMat.UI.WPF.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
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
            await Task.Delay(2000);
            
            return new List<Employee>
            {
                new Employee{ Id = 1, MANR = "370929", Name = "Stiig Gade", Status = new EmployeeStatus{ Name = "Aktiv" } },
                new Employee{ Id = 2, MANR = "123456", Name = "Peter Petersen", Status = new EmployeeStatus{ Name = "Aktiv" } },
                new Employee{ Id = 3, MANR = "654321", Name = "Jens Jensen", Status = new EmployeeStatus{ Name = "Aktiv" } }
            };
        }

        //public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        //{
        //    var request = new RestRequest("employee", Method.GET);
        //    var response = await client.ExecuteAsync(request);

        //    if (response.IsSuccessful)
        //        return JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Content);
        //    else
        //        throw new Exception("Could not get employees.");
        //}
    }
}