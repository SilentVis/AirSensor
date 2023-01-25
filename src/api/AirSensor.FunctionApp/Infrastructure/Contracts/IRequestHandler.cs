using System.Threading.Tasks;

namespace AirSensor.FunctionApp.Infrastructure.Contracts;

public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
{
    Task<TResult> HandleAsync(TRequest request);
}