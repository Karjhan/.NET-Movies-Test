namespace API.Errors;

public class APIResponse
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public APIResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message is null ? GetDefaultMessageForStatusCode(statusCode) : message;
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        switch (statusCode)
        {
            case 300:
                return "Message for default redirect!";
            case 301:
                return "Message for resource has been moved to!";
            case 400:
                return "Message for bad request!";
            case 401:
                return "Message for bad authorization!";
            case 404:
                return "Message for resource not found";
            case 500:
                return "Message for server side error!";
            default:
                return "Default error message!";
        }
    }
}