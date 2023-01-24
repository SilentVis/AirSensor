using AirSensor.FunctionApp.Infrastructure.Contracts;

namespace AirSensor.FunctionApp.RequestHandlers.Contracts
{
    public abstract class RequestBase<TPayload, TResult> : IRequest<TResult>
    {
        protected RequestBase(TPayload payload)
        {
            Payload = payload;
        }

        public TPayload Payload { get; }
    }
}
