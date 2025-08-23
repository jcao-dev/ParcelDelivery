
namespace ParcelDelivery.Api.Models;

public class ParcelReadDto
{

  public string barcode { get; set; } = string.Empty;

  public string status { get; set; } = string.Empty;

  public string launchDate { get; set; } = string.Empty;

  public int etaDays { get; set; }

  public string estimatedArrivalDate { get; set; } = string.Empty;


  public string origin { get; set; } = string.Empty;

  public string destination { get; set; } = string.Empty;

  public string sender { get; set; } = string.Empty;

  public string recipient { get; set; } = string.Empty;

  public string contents { get; set; } = string.Empty;


}