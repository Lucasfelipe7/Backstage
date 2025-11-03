using Application.AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using TCE.Base.UnitOfWork;

namespace Application.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork<Domain.Contexts.AppContext>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(IMapper), typeof(Mapper));
            services.AddSingleton(provider => ConfigureMap.Configure(provider));

            services.AddTransient<MensagemService>();

            return services;
        }
    }
}
