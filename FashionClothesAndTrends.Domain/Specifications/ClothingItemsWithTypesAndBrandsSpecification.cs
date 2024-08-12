using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Specifications
{
    public class ClothingItemsWithTypesAndBrandsSpecification : BaseSpecification<ClothingItem>
    {
        public ClothingItemsWithTypesAndBrandsSpecification(ClothingSpecParams clothingParams) 
            : base(x => 
                (string.IsNullOrEmpty(clothingParams.Search) || x.Name.ToLower().Contains(clothingParams.Search)) &&
                (!clothingParams.ClothingBrandId.HasValue || x.ClothingBrandId == clothingParams.ClothingBrandId) &&
                (!clothingParams.Gender.HasValue || x.Gender == clothingParams.Gender) &&
                (!clothingParams.Size.HasValue || x.Size == clothingParams.Size) &&
                (!clothingParams.Category.HasValue || x.Category == clothingParams.Category)
            )
        {
            AddInclude(x => x.ClothingBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(clothingParams.PageSize * (clothingParams.PageIndex - 1), clothingParams.PageSize);

            if (!string.IsNullOrEmpty(clothingParams.Sort))
            {
                switch (clothingParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ClothingItemsWithTypesAndBrandsSpecification(Guid id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ClothingBrand);
        }
    }
}