using System;

namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageForStatusCode(statusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "A bad request was made.",
                401 => "You are not Authorized.",
                404 => "Resource not found.",
                500 => "Error",
                _ => null
            };
        }
        
    }
}