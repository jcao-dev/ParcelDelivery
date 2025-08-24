

using ParcelDelivery.Api.Dto;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public interface IParcelService
{
  public ParcelReadDto? PostParcel(ParcelCreateDto parcelCreateDto);

  public string PatchParcelStatus(string barcode, ParcelUpdateStatusDto parcelUpdateStatusDto);

  public ParcelReadWithHistoryDto GetParcel(string barcode);

}