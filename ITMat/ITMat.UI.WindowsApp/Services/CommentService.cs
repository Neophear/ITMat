using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public class CommentService : AbstractService, ICommentService
    {
        public CommentService(IConfiguration configuration)
            : base(configuration) { }

        public async Task<int> CreateEmployeeCommentAsync(int employeeId, string text)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateLoanCommentAsync(int loanId, string text)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId)
        {
            var request = new RestRequest($"comment/employee/{employeeId}", Method.GET);
            var response = await ExecuteRequestAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(response.Content);
        }

        public async Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId)
        {
            throw new NotImplementedException();
        }
    }
}