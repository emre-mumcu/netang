using System.Net;

namespace Backend.App_Lib;

public class ApiResponse<T> : ApiResponse
{
    public new T? ResponseData { get; set; }
}

public class ApiResponse
{
    public int ResponseCode { get; set; }
    public string ResponseType { get; set; } = null!;
    public string ResponseMessage { get; set; } = null!;
    public string? ResponseData { get; set; } = null;
    public string? TraceId { get; set; } = null;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
}

public enum ApiResponseCodes 
{
    Success=0, Fail=1, Exception=2
}


public static class ApiResponses 
{
    public static ApiResponse Success() => new ApiResponse() { 
        ResponseCode = (int)ApiResponseCodes.Success,  
        ResponseType = ApiResponseCodes.Success.ToString().ToUpper(),
        ResponseMessage = "Operation completed"
    };

    public static ApiResponse Success<T>(T? data)
    {
        return new ApiResponse<T>()
        {
            ResponseCode = (int)ApiResponseCodes.Success,
            ResponseType = ApiResponseCodes.Success.ToString().ToUpper(),
            ResponseMessage = "Operation completed",
            ResponseData = data
        };
    }

    public static ApiResponse Fail(string Message)
    {
        return new ApiResponse()
        {
            ResponseCode = (int)ApiResponseCodes.Fail,
            ResponseType = ApiResponseCodes.Fail.ToString().ToUpper(),
            ResponseMessage = Message            
        };
    }

    public static ApiResponse Exception(Exception ex)
    {
        return new ApiResponse()
        {
            ResponseCode = (int)ApiResponseCodes.Exception,
            ResponseType = ApiResponseCodes.Exception.ToString().ToUpper(),
            ResponseMessage = ex.Message
        };
    }
}