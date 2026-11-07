using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
