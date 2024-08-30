using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace FashionClothesAndTrends.WebAPI.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly string _whSecret;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger,
        IConfiguration config)
    {
        _logger = logger;
        _paymentService = paymentService;
        _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        try
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return Ok(basket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating or updating payment intent for basket {BasketId}",
                basketId);
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        try
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded: {PaymentId}", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: {OrderId}", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed: {PaymentId}", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order updated to payment failed: {OrderId}", order.Id);
                    break;
            }

            return new EmptyResult();
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook error: {Message}", ex.Message);
            return BadRequest(new ApiResponse(400, "Stripe webhook error"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing Stripe webhook");
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }
}