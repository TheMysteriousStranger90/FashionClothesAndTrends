using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Specifications
{
    public class ClothingItemsWithTypesAndBrandsSpecification : BaseSpecification<ClothingItem>
    {
        public ClothingItemsWithTypesAndBrandsSpecification(Guid id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ClothingItemPhotos);
            AddInclude(x => x.ClothingBrand);
        }

        public ClothingItemsWithTypesAndBrandsSpecification(ClothingSpecParams clothingSpecParams)
            : base(x =>
                (string.IsNullOrEmpty(clothingSpecParams.Search) ||
                 x.Name.ToLower().Contains(clothingSpecParams.Search)) &&
                (!clothingSpecParams.ClothingBrandId.HasValue ||
                 x.ClothingBrandId == clothingSpecParams.ClothingBrandId) &&
                (!clothingSpecParams.Gender.HasValue || x.Gender == clothingSpecParams.Gender) &&
                (!clothingSpecParams.Size.HasValue || x.Size == clothingSpecParams.Size) &&
                (!clothingSpecParams.Category.HasValue || x.Category == clothingSpecParams.Category)
            )
        {
            AddInclude(x => x.ClothingItemPhotos);
            AddInclude(x => x.ClothingBrand);
            ApplyPaging(clothingSpecParams.PageSize * (clothingSpecParams.PageIndex - 1), clothingSpecParams.PageSize);

            if (!string.IsNullOrEmpty(clothingSpecParams.Sort))
            {
                switch (clothingSpecParams.Sort)
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
    }
}