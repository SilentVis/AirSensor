using System;
using System.Threading.Tasks;
using AirSensor.FunctionApp.Infrastructure.Contracts;

namespace AirSensor.FunctionApp.RequestHandlers.SubmitSensorReadings
{
    public class SubmitSensorReadingsRequestHandler : IRequestHandler<SubmitSensorReadingsRequest, SubmitSensorReadingsResult>
    {
        public async Task<SubmitSensorReadingsResult> HandleAsync(SubmitSensorReadingsRequest request)
        {
            var data = request.Payload;

            if (data.DeviceName == "failed")
            {
                return new SubmitSensorReadingsResult(new Error("Incorrect device"));
            }

            var date = DateTime.UtcNow;

            var successMessage = $@"
Incoming message handled : on {date}
Device '{data.DeviceName}' reported params: 
Temperature: {data.Temperature} °C
Humidity: {data.Humidity} %";

            return new SubmitSensorReadingsResult(successMessage);
        }
    }
}
