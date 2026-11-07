using Microsoft.Build.Framework;

namespace InventoryManagementSystem.ViewModels
{
    public class SignOutViewModel
    {
        [Required]
        public bool IsSignedOut { get; set; }
    }
}
