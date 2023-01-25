using AirSensor.FunctionApp.RequestHandlers.Contracts;

namespace AirSensor.FunctionApp.RequestHandlers.SubmitSensorReadings
{
    public class SubmitSensorReadingsRequest : RequestBase<SubmitSensorReadingsPayload, SubmitSensorReadingsResult>
    {
        public SubmitSensorReadingsRequest(SubmitSensorReadingsPayload payload) : base(payload)
        {
        }
    }
}
