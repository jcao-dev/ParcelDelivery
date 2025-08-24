using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.Dto;
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
      var parcelReadDto = _parcelService.PostParcel(parcelCreateDto);
      return Ok(parcelReadDto);
    }
    catch (Exception ex)
    {
      ModelState.AddModelError("Error", ex.Message);
      return BadRequest(ModelState);
    }

  }

  [HttpPatch("{barcode}")]
  public ActionResult<string> PatchParcelStatus(string barcode, ParcelUpdateStatusDto parcelUpdateStatusDto)
  {
    try
    {
      var message = _parcelService.PatchParcelStatus(barcode, parcelUpdateStatusDto);
      return Ok(message);
    }
    catch (Exception ex)
    {
      ModelState.AddModelError("Error", ex.Message);
      return BadRequest(ModelState);
    }
  }

  [HttpGet("{barcode}")]
  public ActionResult GetParcel(string barcode)
  {

    return Ok();

  }
}
