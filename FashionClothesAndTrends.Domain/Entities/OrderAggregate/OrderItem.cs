using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem()
    {
    }

    public OrderItem(ClothingItemOrdered itemOrdered, decimal price, int quantity)
    {
        ItemOrdered = itemOrdered;
        Price = price;
        Quantity = quantity;
    }

    public ClothingItemOrdered ItemOrdered { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}