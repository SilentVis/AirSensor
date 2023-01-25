namespace AirSensor.FunctionApp.RequestHandlers.SubmitSensorReadings
{
    public class SubmitSensorReadingsPayload
    {
        public SubmitSensorReadingsPayload(string deviceName, float temperature, float humidity)
        {
            DeviceName = deviceName;
            Temperature = temperature;
            Humidity = humidity;
        }

        public string DeviceName { get; }

        public float Temperature { get; }

        public float Humidity { get; }
    }
}
