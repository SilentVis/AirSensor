using System;
using SimpleInjector;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AirSensor.FunctionApp.Infrastructure.Contracts;
using AirSensor.FunctionApp.Infrastructure.Extensions;
using AirSensor.FunctionApp.Infrastructure.SimpleInjector;

[assembly: FunctionsStartup(typeof(AirSensor.FunctionApp.Startup))]
namespace AirSensor.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        private readonly Container _container = new ();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this);
            services.AddSingleton<InitCompletion>();
            services.AddScoped(typeof(IMediator), typeof(AzureToSimpleInjectorMediator));


            services.AddSimpleInjector(_container, options =>
            {
                // Prevent the use of hosted services (not supported by Azure Functions).
                options.EnableHostedServiceResolution = false;

                // Allow injecting ILogger into application components
                options.AddLogging();
            });

            SimpleInjectorExtensions.RegisterServices(_container);
        }

        public void Configure(IServiceProvider app)
        {
            app.UseSimpleInjector(_container);

            _container.Verify();
        }

        public override void Configure(IFunctionsHostBuilder builder) => this.ConfigureServices(builder.Services);

        public sealed class InitCompletion
        {
            public InitCompletion(Startup startup, IServiceProvider app) => startup.Configure(app);
        }
    }
}
