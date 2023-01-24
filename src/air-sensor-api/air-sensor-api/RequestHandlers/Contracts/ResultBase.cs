using System.Collections.Generic;
using System.Linq;
using AirSensor.FunctionApp.Infrastructure.Contracts;

namespace AirSensor.FunctionApp.RequestHandlers.Contracts
{
    public abstract class ResultBase
    {
        protected ResultBase()
        {
            Errors = new List<Error>();
        }

        protected ResultBase(params Error[] errors)
        {
            Errors = errors;
        }

        public bool IsSuccessful => !Errors.Any();

        public IEnumerable<Error> Errors { get; }
    }
}
