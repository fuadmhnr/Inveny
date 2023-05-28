namespace Inveny.Responses;

public class ResponseNotFound
{

  public ResponseNotFound(bool success, string message, int errorCode, object data)
  {
    Success = success;
    Message = message;
    ErrorCode = errorCode;
    Data = data;
  }
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public int ErrorCode { get; set; }
  public object? Data { get; set; }
}
