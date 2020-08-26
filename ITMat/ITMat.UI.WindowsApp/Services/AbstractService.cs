using ITMat.UI.WindowsApp.Models.Exceptions;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public class AbstractService
    {
        private readonly IRestClient client;

        public AbstractService(IConfiguration configuration)
            => client = new RestClient(configuration["api_url"]) { Authenticator = new NtlmAuthenticator() };

        public async Task<IRestResponse> ExecuteRequestAsync(IRestRequest request)
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return response;
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException("Din bruger kunne ikke verificeres.");
                    case System.Net.HttpStatusCode.Forbidden:
                        throw new UnauthorizedException("Ingen adgang.");
                    default:
                        throw new Exception("Kunne ikke hente data.");
                }
            }
        }
    }
}