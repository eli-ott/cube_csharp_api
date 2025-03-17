using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace MonApi.API.Stripe.Controllers;

[Route("create-payment-intent")]
[ApiController]
public class PaymentIntentApiController : Controller
{
    [HttpPost]
    public ActionResult Create(PaymentIntentCreateRequest request)
    {
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = CalculateOrderAmount(request.Items),
            Currency = "eur",
            Metadata = new Dictionary<string, string>
            {
                { "customerId", request.CustomerId },
                { "cartId", request.CartId },
                { "items", JsonConvert.SerializeObject(request.Items) }
            },
        });

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }

    private long CalculateOrderAmount(Item[] items)
    {
        // Calculate the order total on the server to prevent
        // people from directly manipulating the amount on the client
        long total = 0;
        foreach (Item item in items)
        {
            total += (item.DiscountedAmount ?? item.Amount) * item.Quantity;
        }
        return total;
    }

    public class Item
    {
        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Amount")]
        public long Amount { get; set; }

        [JsonProperty("DiscountedAmount")]
        public long? DiscountedAmount { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }
    }

    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("cartId")]
        public string CartId { get; set; }
    }
}