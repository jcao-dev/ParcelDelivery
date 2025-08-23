namespace ParcelDelivery.Api.Models;

public class History
{
  public int HistoryId { get; set; }
  public string Status { get; set; } = string.Empty;

  public string TimeStamp { get; set; } = string.Empty;

}