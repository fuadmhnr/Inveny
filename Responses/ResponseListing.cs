namespace Inveny.Responses;

public class ResponseListing<T>
{
  public ResponseListing()
  {
    
  }

  public ResponseListing(Dictionary<string, List<T>> data, bool success, int status, string message, int total, int limit, int currentPage, int totalPage)
  {
    Data = data;
    Success = success;
    Status = status;
    Message = message;
    Total = total;
    Limit = limit;
    CurrentPage = currentPage;
    TotalPage = totalPage;
  }
  public Dictionary<string, List<T>> Data { get; set; }
  public bool Success { get; set; }
  public int Status { get; set; }
  public string Message { get; set; } = string.Empty;
  public int Total { get; set; }
  public int Limit { get; set; }
  public int CurrentPage { get; set; }
  public int TotalPage { get; set; }
}