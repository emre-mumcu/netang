namespace Backend;

public class ApiResult<T> : ApiResult
{
    public new T? ResultData { get; set; }
}

public class ApiResult
{
    public int ResultCode { get; set; }
    public string ResultMessage { get; set; } = null!;
    public string? ResultData { get; set; } = null;
    public string? TraceId { get; set; } = null;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
}

public enum ApiResultCodes 
{
    Success = 0, 
    Fail = 1
}


public static class ApiResults 
{
    public static ApiResult Success() {
        return new ApiResult() { 
            ResultCode = (int)ApiResultCodes.Success,
            ResultMessage = ApiResultCodes.Success.ToString()            
         };
    }

    public static ApiResult Success<T>(T data)
    {
        return new ApiResult<T>()
        {
            ResultCode = (int)ApiResultCodes.Success,
            ResultMessage = ApiResultCodes.Success.ToString(),
            ResultData = data
        };
    }

}