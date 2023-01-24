namespace AirSensor.FunctionApp.Infrastructure.Contracts
{
    public class Error
    {
        public Error(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
