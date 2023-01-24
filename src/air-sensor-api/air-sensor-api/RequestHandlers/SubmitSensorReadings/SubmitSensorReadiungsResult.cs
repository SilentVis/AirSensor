using AirSensor.FunctionApp.Infrastructure.Contracts;
using AirSensor.FunctionApp.RequestHandlers.Contracts;

namespace AirSensor.FunctionApp.RequestHandlers.SubmitSensorReadings
{
    public class SubmitSensorReadingsResult : ResultBase
    {
        public SubmitSensorReadingsResult()
        {
            SuccessMessage = "Just success";
        }

        public SubmitSensorReadingsResult(string successMessage) : base()
        {
            SuccessMessage = successMessage;
        }

        public SubmitSensorReadingsResult(params Error[] errors) : base(errors)
        {
            SuccessMessage = "";
        }

        public string SuccessMessage { get; }
    }
}
