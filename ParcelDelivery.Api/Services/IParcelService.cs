

using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public interface IParcelService
{
  public ParcelReadDto? CreateParcel(ParcelCreateDto parcelCreateDto);

}