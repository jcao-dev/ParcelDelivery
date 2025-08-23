
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public class DateValues
{
  public string launchDate = "";
  public int etaDays = 0;
  public string estimatedArrivalDate = "";
}

public class ParcelService : IParcelService
{

  private List<Parcel> parcels { get; set; } = [];

  public ParcelReadDto CreateParcel(ParcelCreateDto parcelCreateDto)
  {

    var dateValues = getDateValues(parcelCreateDto) ?? throw new Exception("Cannot calculate dates");

    var newParcel = new Parcel()
    {
      parcelId = parcels.Count + 1,
      barcode = parcelCreateDto.barcode,
      status = "Open",
      launchDate = dateValues.launchDate,
      etaDays = dateValues.etaDays,
      estimatedArrivalDate = dateValues.estimatedArrivalDate,
      origin = "Earth Distribution Center",
      destination = "Mars Colony Alpha",
      sender = parcelCreateDto.sender,
      recipient = parcelCreateDto.recipient,
      contents = parcelCreateDto.contents,
      history = []
    };

    parcels.Add(newParcel);

    var parcelReadDto = new ParcelReadDto()
    {
      barcode = newParcel.barcode,
      status = newParcel.status,
      launchDate = newParcel.launchDate,
      etaDays = newParcel.etaDays,
      estimatedArrivalDate = newParcel.estimatedArrivalDate,
      origin = newParcel.origin,
      destination = newParcel.destination,
      sender = newParcel.sender,
      recipient = newParcel.recipient,
      contents = newParcel.contents,
    };

    return parcelReadDto;
  }

  private DateValues? getDateValues(ParcelCreateDto parcelCreateDto)
  {

    var dateValues = new DateValues()
    {
      launchDate = "",
      etaDays = 0,
      estimatedArrivalDate = ""
    };

    switch (parcelCreateDto.deliveryService)
    {
      case "Express":

        var expressLaunchDateTime = GetNextWednesday();

        dateValues.launchDate = expressLaunchDateTime.ToString("yyyy-MM-dd");
        dateValues.etaDays = 90;
        dateValues.estimatedArrivalDate = CalculateEAD(expressLaunchDateTime, dateValues.etaDays);

        return dateValues;
      case "Standard":

        dateValues.launchDate = "2025-10-01";

        var standardLaunchDateTime = DateTime.Parse(dateValues.launchDate);

        dateValues.etaDays = 180;
        dateValues.estimatedArrivalDate = CalculateEAD(standardLaunchDateTime, dateValues.etaDays);

        return dateValues;
      default:
        return null;
    }

  }
  private DateTime GetNextWednesday()
  {
    DateTime result = DateTime.Now.AddDays(1);
    while (result.DayOfWeek != DayOfWeek.Wednesday)
      result = result.AddDays(1);
    return result;

  }

  private string CalculateEAD(DateTime launch, int days)
  {
    var ead = launch.AddDays(days);
    return ead.ToString("yyyy-MM-dd");

  }

}