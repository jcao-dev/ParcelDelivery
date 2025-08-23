namespace ParcelDelivery.Api.Models;

public class ParcelCreateDto
{

  public string barcode { get; set; } = string.Empty;

  public string sender { get; set; } = string.Empty;

  public string recipient { get; set; } = string.Empty;
  public string deliveryService { get; set; } = string.Empty;

  public string contents { get; set; } = string.Empty;

}