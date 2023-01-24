using System.Threading.Tasks;

namespace AirSensor.FunctionApp.Infrastructure.Contracts
{
    public interface IMediator
    {
        Task<TResult> HandleAsync<TResult>(IRequest<TResult> message);
    }
}
