using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
