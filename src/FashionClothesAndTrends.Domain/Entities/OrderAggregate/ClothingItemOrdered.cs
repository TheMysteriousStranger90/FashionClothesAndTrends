using System.ComponentModel.DataAnnotations.Schema;

namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class ClothingItemOrdered
{
    public ClothingItemOrdered()
    {
    }

    public ClothingItemOrdered(Guid clothingItemId, string clothingItemName, ICollection<ClothingItemPhoto> clothingItemPhotos)
    {
        ClothingItemId = clothingItemId;
        ClothingItemName = clothingItemName;
        ClothingItemPhotos = clothingItemPhotos;
    }

    public Guid ClothingItemId { get; set; }
    public string ClothingItemName { get; set; } = null!;

    [NotMapped]
    public ICollection<ClothingItemPhoto> ClothingItemPhotos { get; set; } = [];

    [NotMapped]
    public string? MainPictureUrl => ClothingItemPhotos.FirstOrDefault(p => p.IsMain)?.Url
                                     ?? ClothingItemPhotos.FirstOrDefault()?.Url;
}