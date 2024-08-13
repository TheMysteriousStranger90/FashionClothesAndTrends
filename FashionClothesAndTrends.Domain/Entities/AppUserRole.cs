using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Domain.Entities;

public class AppUserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public AppRole Role { get; set; }
}