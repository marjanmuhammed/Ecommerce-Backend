using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly RazorpayService _razorpayService;

    public PaymentController(RazorpayService razorpayService)
    {
        _razorpayService = razorpayService;
    }

    [HttpPost("create-order")]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            if (request.Amount <= 0)
            {
                return BadRequest(new { error = "Amount must be greater than 0" });
            }

            var orderId = _razorpayService.CreateOrder(request.Amount);
            return Ok(new { orderId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public class CreateOrderRequest
{
    public decimal Amount { get; set; }
}