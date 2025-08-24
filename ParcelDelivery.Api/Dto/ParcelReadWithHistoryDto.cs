
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Dto;

public class ParcelReadWithHistoryDto : ParcelReadDto
{
  public ICollection<HistoryReadDto> history { get; set; } = [];

}