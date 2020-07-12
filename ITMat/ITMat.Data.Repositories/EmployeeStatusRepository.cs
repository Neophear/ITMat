using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Repositories
{
    internal class EmployeeStatusRepository : AbstractDapperRepository<EmployeeStatus, EmployeeStatusDTO>, IEmployeeStatusRepository
    {
        #region Queries
        private const string SqlGetEmployeeStatuses = "select * from employeestatus";
        #endregion

        public EmployeeStatusRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync()
            => await QueryMultipleAsync(SqlGetEmployeeStatuses);
    }
}