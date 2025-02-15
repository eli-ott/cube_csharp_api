using MonApi.API.Carts.Models;

namespace MonApi.API.Carts.Extensions;

public static class CartExtensions
{
    public static Cart MapToModel(int customerId)
    {
        return new Cart
        {
            CustomerId = customerId
        };
    }
}