using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Specifications
{
    public class ClothingItemWithFiltersForCountSpecificication : BaseSpecification<ClothingItem>
    {
        public ClothingItemWithFiltersForCountSpecificication(ClothingSpecParams clothingParams) 
            : base(x => 
                (string.IsNullOrEmpty(clothingParams.Search) || x.Name.ToLower().Contains(clothingParams.Search)) &&
                (!clothingParams.ClothingBrandId.HasValue || x.ClothingBrandId == clothingParams.ClothingBrandId) &&
                (!clothingParams.Gender.HasValue || x.Gender == clothingParams.Gender) &&
                (!clothingParams.Size.HasValue || x.Size == clothingParams.Size) &&
                (!clothingParams.Category.HasValue || x.Category == clothingParams.Category)
            )
        {
        }
    }
}