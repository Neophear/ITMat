using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public class LoanService : AbstractService, ILoanService
    {
        public LoanService(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<LoanListedDTO>> GetEmployeeLoansAsync(int employeeId)
            => await ExecuteAsync<IEnumerable<LoanListedDTO>>(new RestRequest($"loan/employee/{employeeId}", Method.GET));

        public async Task<LoanDTO> GetLoanAsync(int id)
            => await ExecuteAsync<LoanDTO>(new RestRequest($"loan/{id}", Method.GET));

        public async Task<IEnumerable<LoanEmployeeListedDTO>> GetLoansAsync()
            => await ExecuteAsync<IEnumerable<LoanEmployeeListedDTO>>(new RestRequest("loan", Method.GET));
    }
}