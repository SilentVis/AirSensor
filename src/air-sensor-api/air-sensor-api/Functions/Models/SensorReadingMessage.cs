using System.Text.Json.Serialization;

namespace AirSensor.FunctionApp.Functions.Models
{
    public class SensorReadingMessage
    {
        [JsonPropertyName("deviceName")]
        public string DeviceName { get; set; }

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }
    }
}
