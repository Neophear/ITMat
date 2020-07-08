using ITMat.Core.Interfaces;
using ITMat.Core.Services;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ITMat.Core.Integrations
{
    public static class ServiceCollectionExtension
    {
        public static void AddITMatCore(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeService, EmployeeService>()
                    .AddSingleton<IEmployeeRepository, EmployeeRepository>();

            services.AddSingleton<IEmployeeStatusService, EmployeeStatusService>()
                    .AddSingleton<IEmployeeStatusRepository, EmployeeStatusRepository>();

            services.AddSingleton<ICommentService, CommentService>()
                    .AddSingleton<ICommentRepository, CommentRepository>();
        }
    }
}