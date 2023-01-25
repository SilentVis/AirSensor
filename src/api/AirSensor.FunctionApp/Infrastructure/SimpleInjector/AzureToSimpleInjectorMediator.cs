using System;
using System.Threading.Tasks;
using AirSensor.FunctionApp.Infrastructure.Contracts;
using SimpleInjector;
using SimpleInjector.Integration.ServiceCollection;
using SimpleInjector.Lifestyles;

namespace AirSensor.FunctionApp.Infrastructure.SimpleInjector
{
    public class AzureToSimpleInjectorMediator : IMediator
    {
        private readonly Container _container;
        private readonly IServiceProvider _serviceProvider;

        public AzureToSimpleInjectorMediator(
            Startup.InitCompletion completor, //This required to SimpleInjector integration work
            IServiceProvider serviceProvider,
            Container container)
        {
            _serviceProvider = serviceProvider;
            _container = container;
        }

        public async Task<TResult> HandleAsync<TResult>(IRequest<TResult> message)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _container.GetInstance<ServiceScopeProvider>().ServiceScope =
                    new ServiceScope(_serviceProvider);

                return await HandleCoreAsync(message);
            }
        }

        private async Task<TResult> HandleCoreAsync<TResult>(IRequest<TResult> message) =>
            await GetHandler(message).HandleAsync(message);

        private IRequestHandler<TResult> GetHandler<TResult>(IRequest<TResult> message)
        {
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(message.GetType(), typeof(TResult));
            var wrapperType = typeof(RequestHandlerWrapper<,>)
                .MakeGenericType(message.GetType(), typeof(TResult));

            return (IRequestHandler<TResult>)Activator.CreateInstance(
                wrapperType, _container.GetInstance(handlerType));
        }

        private interface IRequestHandler<TResult>
        {
            Task<TResult> HandleAsync(IRequest<TResult> message);
        }

        private class RequestHandlerWrapper<TRequest, TResult> : IRequestHandler<TResult> where TRequest : IRequest<TResult>
        {
            public RequestHandlerWrapper(IRequestHandler<TRequest, TResult> handler) =>
                Handler = handler;

            private IRequestHandler<TRequest, TResult> Handler { get; }

            public Task<TResult> HandleAsync(IRequest<TResult> message) =>
                Handler.HandleAsync((TRequest)message);
        }
    }
}
