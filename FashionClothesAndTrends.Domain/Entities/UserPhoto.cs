using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class UserPhoto : BaseEntity
{
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
}
