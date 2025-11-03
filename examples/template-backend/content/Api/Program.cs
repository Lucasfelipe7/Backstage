using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Configuration;
using Application.Configurations;
using TCE.Base;
using TCE.Base.Dapper;
using Serilog;
using TCE.Base.Logger;

namespace Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Inicializa o logger do Serilog
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();
            Log.Information("Inicializando API");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog(AppLoggerExtensions.ConfigureSerilogLoggers)
                .ConfigureServices((context, services) =>
                {
                    var configuration = ConfigureTce.BuildConfiguration(context.HostingEnvironment);
                    // Configura os serviços
                    services.ConfigureContext(configuration, context.HostingEnvironment);
                    services.ConfigureDapper(configuration);
                    services.ResolveDependencies(configuration);
                    DapperServiceCollection.AddDapper(services, options =>
                        options.ConnectionString = configuration["ConnectionStrings:BdMensagem_uMensagem_Config"]);
                    ConfigureInfra.Configurar(services, configuration);
                })
                .Configure((context, app) =>
                {
                    var env = context.HostingEnvironment;
                    var configuration = ConfigureTce.BuildConfiguration(context.HostingEnvironment);
                    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

                    app.UseResponseCompression();
                    app.UseHttpsRedirection();

                    AppConfiguration.ConfigureApp(app, env, loggerFactory, configuration, "SingleVersion");
                })
                .Build();
        }
    }
}