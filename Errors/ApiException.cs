namespace API.Errors
{
    // A class called ApiException that does not inherit, has a constructor and the three following properties: StatusCode, Message and Details
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string details = null)
        {
            // The constructor sets the values of the properties
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        // The properties are readonly
        public int StatusCode { get; }
        public string Message { get; }
        public string Details { get; }
    }


}