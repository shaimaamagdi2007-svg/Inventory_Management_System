using Microsoft.AspNetCore.Identity;
namespace InventoryManagementSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Address { get; set; }
    }
}
