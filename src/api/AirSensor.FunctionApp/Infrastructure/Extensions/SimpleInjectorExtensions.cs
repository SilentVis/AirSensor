using AirSensor.FunctionApp.Infrastructure.Contracts;
using SimpleInjector;

namespace AirSensor.FunctionApp.Infrastructure.Extensions
{
    public class SimpleInjectorExtensions
    {
        public static void RegisterServices(Container container)
        {
            var assemblies = new[] { typeof(IRequestHandler<,>).Assembly };
            container.Register(typeof(IRequestHandler<,>), assemblies);
        }
    }
}
