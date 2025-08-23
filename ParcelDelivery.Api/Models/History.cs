namespace ParcelDelivery.Api.Models;

public class History
{
  public int historyId { get; set; }
  public string status { get; set; } = string.Empty;

  public string timeStamp { get; set; } = string.Empty;

}