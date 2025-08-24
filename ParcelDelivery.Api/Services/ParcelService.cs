using ParcelDelivery.Api.Dto;
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

  private Dictionary<string, List<string>> statuses = new Dictionary<string, List<string>>
  {
    ["Created"] = ["OnRocketToMars"],
    ["OnRocketToMars"] = ["LandedOnMars", "Lost"],
    ["LandedOnMars"] = ["OutForMartianDelivery"],
    ["OutForMartianDelivery"] = ["Delivered", "Lost"],
    ["Delivered"] = [],
    ["Lost"] = []
  };

  public ParcelReadDto PostParcel(ParcelCreateDto parcelCreateDto)
  {

    CheckValidBarcode(parcelCreateDto.barcode);

    var parcel = parcels.FirstOrDefault(parcel => parcel.barcode == parcelCreateDto.barcode);
    if (parcel != null)
    {
      throw new Exception("Parcel with this barcode already exists");
    }


    var dateValues = GetDateValues(parcelCreateDto) ?? throw new Exception("Cannot calculate dates");

    var newParcel = new Parcel()
    {
      parcelId = parcels.Count + 1,
      barcode = parcelCreateDto.barcode,
      status = statuses.First().Key,
      launchDate = dateValues.launchDate,
      etaDays = dateValues.etaDays,
      estimatedArrivalDate = dateValues.estimatedArrivalDate,
      origin = "Earth Distribution Center",
      destination = "Mars Colony Alpha",
      sender = parcelCreateDto.sender,
      recipient = parcelCreateDto.recipient,
      contents = parcelCreateDto.contents,
      history = [
        new History {
          historyId = 1,
          status = statuses.First().Key,
          timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd")
        }
      ]
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


  public string PatchParcelStatus(string barcode, ParcelUpdateStatusDto parcelUpdateStatusDto)
  {
    var parcel = FindExistingParcel(barcode);

    var currentStatus = statuses.FirstOrDefault(status => status.Key == parcel.status);

    var validStatuses = currentStatus.Value;

    if (validStatuses == null || validStatuses.Count == 0)
    {
      throw new Exception($"Parcel has a status of {parcel.status} and cannot be changed");
    }

    var hasValidTransition = validStatuses.Any(status => status == parcelUpdateStatusDto.newStatus);

    if (!hasValidTransition)
    {
      throw new Exception($"{parcelUpdateStatusDto.newStatus} is not a valid status for a parcel {parcel.status} can only transition to the following - {string.Join("/", validStatuses)}");
    }

    parcel.status = parcelUpdateStatusDto.newStatus;
    var newHistory = new History()
    {
      historyId = parcel.history.Count + 1,
      status = parcel.status,
      timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd")
    };
    parcel.history.Add(newHistory);

    return $"The status of {barcode} has been changed to {parcel.status}";


  }

  public ParcelReadWithHistoryDto GetParcel(string barcode)
  {
    var parcel = FindExistingParcel(barcode);

    var parcelRead = new ParcelReadWithHistoryDto()
    {
      barcode = parcel.barcode,
      status = parcel.status,
      launchDate = parcel.launchDate,
      etaDays = parcel.etaDays,
      estimatedArrivalDate = parcel.estimatedArrivalDate,
      origin = parcel.origin,
      destination = parcel.destination,
      sender = parcel.sender,
      recipient = parcel.recipient,
      contents = parcel.contents,
      history = parcel.history.Select(history =>
      {
        return new HistoryReadDto
        {
          status = history.status,
          timeStamp = history.timeStamp
        };
      }).ToList()
    };

    return parcelRead;
  }

  private DateValues? GetDateValues(ParcelCreateDto parcelCreateDto)
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

  private void CheckValidBarcode(string barcode)
  {
    if (barcode.Length != 25)
    {
      throw new Exception("Barcode needs to be 25 characters long");
    }

    if (!barcode.StartsWith("RMARS"))
    {
      throw new Exception("Barcode has to start with RMAS");
    }

    var numbers = barcode.Substring(5, 19);

    if (!numbers.All(char.IsNumber))
    {
      throw new Exception("Barcode must have 19 digits");
    }


    var lastLetter = barcode[barcode.Length - 1];
    if (!(lastLetter >= 'A' && lastLetter <= 'Z'))
    {
      throw new Exception("Barcode must end with a capital letter");
    }


  }

  private Parcel FindExistingParcel(string barcode)
  {
    CheckValidBarcode(barcode);

    var parcel = parcels.FirstOrDefault(parcel => parcel.barcode == barcode);
    if (parcel == null)
    {
      throw new Exception("Parcel with this barcode does not exists");
    }

    return parcel;
  }

}