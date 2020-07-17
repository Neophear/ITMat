using ITMat.Core.Data.Interfaces;
using ITMat.Core.Data.Repositories;
using ITMat.Core.Interfaces;
using ITMat.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ITMat.Core.Integrations
{
    public static class ServiceCollectionExtension
    {
        public static void AddITMatCore(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>()
                    .AddSingleton<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<ICommentService, CommentService>()
                    .AddSingleton<ICommentRepository, CommentRepository>();

            services.AddTransient<ILoanService, LoanService>()
                    .AddSingleton<ILoanRepository, LoanRepository>();

            services.AddTransient<IItemService, ItemService>()
                    .AddSingleton<IItemRepository, ItemRepository>();
        }
    }
}