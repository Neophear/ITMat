using ITMat.Core.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using ITMat.UI.WindowsApp.Models.Exceptions;

namespace ITMat.UI.WindowsApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRestClient client;

        public EmployeeService(IConfiguration configuration)
        {
            client = new RestClient(configuration["api_url"])
            {
                Authenticator = new NtlmAuthenticator()
            };
        }

        public Task<EmployeeDTO> GetEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var request = new RestRequest("employee", Method.GET);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(response.Content);
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException("Din bruger kunne ikke verificeres.");
                    case System.Net.HttpStatusCode.Forbidden:
                        throw new UnauthorizedException();
                    default:
                        throw new Exception("Could not get employees.");
                }
            }
        }

        public Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync()
        {
            throw new NotImplementedException();
        }
    }
}