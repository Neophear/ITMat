using ITMat.Core.Interfaces;
using ITMat.Core.Services;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace ITMat.Core.Integrations
{
    public static class ServiceCollectionExtension
    {
        public static void AddITMatCore(this IServiceCollection services)
        {
            
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        }
    }
}