using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Time.Testing;
using ParcelDelivery.Api.Controllers;
using ParcelDelivery.Api.Dto;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Services;

namespace ParcelDelivery.Api.Testing;

public class ParcelDeliveryTests
{

  private readonly ParcelDeliveryController _controller;
  private readonly ParcelService _service;

  // private readonly FakeTimeProvider _timeProvider;

  public ParcelDeliveryTests()
  {
    // _timeProvider = new FakeTimeProvider(startDateTime: DateTime.Now);
    _service = new ParcelService();
    _controller = new ParcelDeliveryController(_service);
  }

  [Fact]
  public void Post_Success()
  {
    var testData = new ParcelCreateDto()
    {
      barcode = "RMARS1234567890123456789M",
      sender = "Anders Hejlsberg",
      recipient = "Elon Musk",
      deliveryService = "Express",
      contents = "Signed C# language specification and a birthday card"
    };

    var response = _controller.PostParcel(testData);
    var result = response.Result as OkObjectResult;
    var parcel = result?.Value as ParcelReadDto;

    Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
    Assert.Equal("RMARS1234567890123456789M", parcel?.barcode);
    Assert.Equal("Created", parcel?.status);


  }

  [Fact]
  public void Patch_Status_Success()
  {
    var testData = new ParcelCreateDto()
    {
      barcode = "RMARS1211111111122222222M",
      sender = "John Smith",
      recipient = "John Smith the Second",
      deliveryService = "Standard",
      contents = "Another one"
    };

    _controller.PostParcel(testData);

    var patchData = new ParcelUpdateStatusDto()
    {
      newStatus = "OnRocketToMars"
    };

    var response = _controller.PatchParcelStatus(testData.barcode, patchData);
    var result = response.Result as OkObjectResult;
    var message = result?.Value as string;

    Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
    Assert.Equal($"The status of {testData.barcode} has been changed to {patchData.newStatus}", message);
  }

  [Fact]
  public void Get_Success()
  {
    DateTime.UtcNow.ToString("yyyy-MM-dd");

    var testData = new ParcelCreateDto()
    {
      barcode = "RMARS1411151116722222222M",
      sender = "Jason Fleetwood",
      recipient = "Bill Gates",
      deliveryService = "Express",
      contents = "Vaccines in a crate"
    };

    _controller.PostParcel(testData);

    var response = _controller.GetParcel(testData.barcode);
    var result = response.Result as OkObjectResult;
    var parcel = result?.Value as ParcelReadWithHistoryDto;

    Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
    Assert.Equal("RMARS1411151116722222222M", parcel?.barcode);

    var history = parcel?.history.ToList();
    Assert.NotNull(history);
    Assert.Equal(DateTime.UtcNow.ToString("yyyy-MM-dd"), history[0].timeStamp);
  }


  [Fact]
  public void Check_Invalid_Barcode()
  {
    var barcode2 = "435431234567890123456789M";
    var ex = Assert.Throws<Exception>(() => _service.CheckValidBarcode(barcode2));
    Assert.Equal("Barcode has to start with RMAS", ex.Message);

    var barcode3 = "RMARS12345678901234567890";
    ex = Assert.Throws<Exception>(() => _service.CheckValidBarcode(barcode3));
    Assert.Equal("Barcode must end with a capital letter", ex.Message);

    var barcode4 = "RMARS12345678901234567M";
    ex = Assert.Throws<Exception>(() => _service.CheckValidBarcode(barcode4));
    Assert.Equal("Barcode must have 19 digits", ex.Message);

  }


}
