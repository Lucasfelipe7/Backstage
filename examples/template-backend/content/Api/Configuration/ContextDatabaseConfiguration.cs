using Domain.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Application.Configuration
{
    /// <summary>
    /// setup geral para os contextos -> bancos utilizados na aplicação
    /// </summary>
    public static class ContextDatabaseConfiguration
    {

        /// <summary>
        /// resolve as dependências de banco na aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureContext(
            this IServiceCollection services, 
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddDbContextPool<AppContext>(options =>
                       options.UseSqlServer(configuration["ConnectionStrings:BdTemplate_uTemplate_Config"],
                        sqlServerOptions => sqlServerOptions
                                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors()
                                .LogTo(Log.Logger.Information, LogLevel.Information, null));
            }
            else
            {
                services.AddDbContextPool<AppContext>(options =>
                    options.UseSqlServer(configuration["ConnectionStrings:BdTemplate_uTemplate_Config"],
                    sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            }


            return services;
        }
    }
}
