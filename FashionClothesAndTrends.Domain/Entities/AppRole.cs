using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Domain.Entities;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}