using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage = "required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        //[Required(ErrorMessage = "required")]
        //[Display(Name = "Role")]
        //public string Role { get; set; }
    }
}
