using Microsoft.Extensions.DependencyInjection;
using System;

namespace AirSensor.FunctionApp.Infrastructure.SimpleInjector
{
    internal sealed class ServiceScope : IServiceScope
    {
        public ServiceScope(IServiceProvider serviceProvider) =>
            ServiceProvider = serviceProvider;

        public IServiceProvider ServiceProvider { get; }

        public void Dispose() { }
    }
}
