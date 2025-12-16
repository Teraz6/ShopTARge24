using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.Accounts
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation password doesn't match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
