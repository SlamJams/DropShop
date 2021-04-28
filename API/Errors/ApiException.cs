namespace API.Errors
{
    public class ApiException : ApiErrorResponse
    {
        public string StackDetails { get; set; }
        public ApiException(int statusCode, string message = null, string stackDetails = null) : base(statusCode, message)
        {
            StackDetails = stackDetails;
        }
    }
}