using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using AirSensor.FunctionApp.Functions.Models;
using AirSensor.FunctionApp.Infrastructure.Contracts;
using AirSensor.FunctionApp.RequestHandlers.SubmitSensorReadings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AirSensor.FunctionApp.Functions
{
    public class SubmitSensorReadingsFunction
    {
        private readonly IMediator _mediator;

        public SubmitSensorReadingsFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("SubmitSensorReadings")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, ILogger log)
        {
            var requestMessage = await request.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<SensorReadingMessage>(requestMessage);

            var requestPayload = new SubmitSensorReadingsPayload(data.DeviceName, data.Temperature, data.Humidity);
            var result = await _mediator.HandleAsync(new SubmitSensorReadingsRequest(requestPayload));

            if (!result.IsSuccessful)
            {
                return new BadRequestErrorMessageResult(result.Errors.First().ToString());
            }

            return new OkObjectResult(result.SuccessMessage);
        }
    }
}
