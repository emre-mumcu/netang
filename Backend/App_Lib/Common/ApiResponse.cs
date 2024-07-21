using System.Net;

namespace Backend.App_Lib.Common;

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
    public string? Exception { get; set; } = null;
    public string? InnerException { get; set; } = null;
    public string? TraceId { get; set; } = null;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public long ExecutionTime { get; set; }
}

public enum ApiResponseCodes 
{
    // Operation completed.
    Success=0, 
    // Operation is NOT completed due to user request.
    Fail=1, 
    // Operation is NOT completed due to application error.
    Exception=2
}


public static class ApiResponses 
{
    public static ApiResponse Success() => new ApiResponse() { 
        ResponseCode = (int)ApiResponseCodes.Success,  
        ResponseType = ApiResponseCodes.Success.ToString().ToUpper(),
        ResponseMessage = "Operation completed"
    };

    public static ApiResponse<T> Success<T>(T? data, long executionTime = 0)
    {
        return new ApiResponse<T>()
        {
            ResponseCode = (int)ApiResponseCodes.Success,
            ResponseType = ApiResponseCodes.Success.ToString().ToUpper(),
            ResponseMessage = "Operation completed",
            ResponseData = data,
            ExecutionTime = executionTime
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
            ResponseMessage = "An error occured",
            Exception = ex?.Message,
            InnerException = ex?.InnerException?.Message
        };
    }

    public static ApiResponse<T> Exception<T>(Exception ex)
    {
        return new ApiResponse<T>()
        {
            ResponseCode = (int)ApiResponseCodes.Exception,
            ResponseType = ApiResponseCodes.Exception.ToString().ToUpper(),
            ResponseMessage = "An error occured",
            ResponseData = default(T),
            Exception = ex?.Message,
            InnerException = ex?.InnerException?.Message
        };
    }
}