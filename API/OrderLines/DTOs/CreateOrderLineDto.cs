namespace MonApi.API.OrderLines.DTOs;

public class CreateOrderLineDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public float UnitPrice { get; set; }
}