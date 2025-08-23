using Microsoft.AspNetCore.Mvc;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelDeliveryController : ControllerBase
{

  public ParcelDeliveryController()
  {
  }

  [HttpPost]
  public ActionResult PostParcel()
  {
    return Ok();
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
