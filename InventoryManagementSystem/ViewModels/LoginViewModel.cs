using System.ComponentModel.DataAnnotations;
namespace InventoryManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me !")]
        public bool RememberMe { get; set; }
    }
}
