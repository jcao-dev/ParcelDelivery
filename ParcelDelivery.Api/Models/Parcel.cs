namespace ParcelDelivery.Api.Models;

public class Parcel
{
  public int ParcelId { get; set; }

  public string Barcode { get; set; } = string.Empty;

  public string Status { get; set; } = string.Empty;

  public DateTime LaunchDate { get; set; }

  public int EtaDays { get; set; }

  public DateTime EstimatedArrivalDate { get; set; }


  public string Origin { get; set; } = string.Empty;

  public string Destination { get; set; } = string.Empty;

  public string Sender { get; set; } = string.Empty;

  public string Recipient { get; set; } = string.Empty;

  public string Contents { get; set; } = string.Empty;

  public ICollection<History> History { get; set; } = [];
}