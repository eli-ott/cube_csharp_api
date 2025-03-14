using Microsoft.AspNetCore.Mvc;
using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Repositories;
using MonApi.API.CartLines.Services;
using MonApi.API.OrderLines.DTOs;
using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Services;
using Newtonsoft.Json;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;
using static MonApi.API.Stripe.Controllers.PaymentIntentApiController;

namespace MonApi.API.Stripe.Controllers;

[Route("stripe-webhook")]
public class StripeWebHook : Controller
{

    private readonly IOrdersService _ordersService;
    private readonly ICartLineService _cartLineService;

    public StripeWebHook(IOrdersService ordersService, ICartLineService cartLineService)
    {
        _ordersService = ordersService;
        _cartLineService = cartLineService;
    }

    // If you are testing your webhook locally with the Stripe CLI you
    // can find the endpoint's secret by running `stripe listen`
    // Otherwise, find your endpoint's secret in your webhook
    // settings in the Developer Dashboard
    string endpointSecret = Environment.GetEnvironmentVariable("STRIPE_WEBHOOK_SECRET") ?? throw new InvalidOperationException("Stripe secret 'STRIPE_WEBHOOK_SECRET' not found.");

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent ?? throw new Exception("PaymentIntent is null");
                var metadata = paymentIntent.Metadata;

                var customerId = metadata["customerId"];
                var cartId = metadata["cartId"];

                var stringItems = metadata["items"];
                var items = JsonConvert.DeserializeObject<List<Item>>(stringItems);

                CreateOrderDto createOrder = new()
                {
                    CustomerId = int.Parse(customerId),
                    StatusId = 1,
                    DeliveryDate = DateTime.Now.AddDays(7),
                    OrderLines = items.Select(item => new CreateOrderLineDto()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.DiscountedAmount ?? item.Amount
                    }).ToList()
                };
                await _ordersService.CreateOrderAsync(createOrder);

                List<DeleteCartLineDto> deleteCartLineDtos = items.Select(item => new DeleteCartLineDto()
                {
                    CartId = int.Parse(cartId),
                    ProductId = item.ProductId
                }).ToList();

                foreach (var deleteCartLineDto in deleteCartLineDtos)
                {
                    await _cartLineService.DeleteCartLine(deleteCartLineDto);
                }

            }
            return Ok();
        }
        catch (StripeException)
        {
            return BadRequest();
        }
    }
}
