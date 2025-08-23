using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Services;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelDeliveryController : ControllerBase
{

  private readonly IParcelService _parcelService;
  public ParcelDeliveryController(ParcelService parcelService)
  {
    _parcelService = parcelService;
  }

  [HttpPost]
  public ActionResult<ParcelReadDto> PostParcel(ParcelCreateDto parcelCreateDto)
  {

    try
    {
      var parcelReadDto = _parcelService.CreateParcel(parcelCreateDto);
      return Ok(parcelReadDto);
    }
    catch (Exception ex)
    {
      ModelState.AddModelError("Error", ex.Message);
      return BadRequest(ModelState);
    }

  }

  [HttpPatch]
  public ActionResult PatchParcel()
  {
    return Ok();
  }

  [HttpGet("{barcode}")]
  public ActionResult GetParcel(string barcode)
  {
    return Ok();
  }
}
