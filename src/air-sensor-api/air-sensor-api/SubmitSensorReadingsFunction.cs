using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AirSensor.FunctionApp
{
    public class SubmitSensorReadingsFunction
    {
        [FunctionName("SubmitSensorReadings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, ILogger log)
        {
            return new OkObjectResult("Success message");
        }
    }
}
