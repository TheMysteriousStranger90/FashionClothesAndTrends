using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Entities;

public class ClothingItem : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Gender Gender { get; set; }
    public Size Size { get; set; }
    public Category Category { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<FavoriteItem> FavoriteItems { get; set; }
    public decimal? Discount { get; set; }
    public bool IsInStock { get; set; }
    public Guid ClothingBrandId { get; set; }
    public ClothingBrand ClothingBrand { get; set; }
    public ICollection<ClothingItemPhoto?> ClothingItemPhotos { get; set; } = new List<ClothingItemPhoto?>();
}