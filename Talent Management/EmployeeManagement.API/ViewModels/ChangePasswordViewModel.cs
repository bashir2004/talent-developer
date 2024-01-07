using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagement.API.ViewModels
{
    public class ChangePasswordViewModel
    {
        [JsonPropertyName("currentPassword")]
        [DataType(DataType.Password)]
        [Required]
        public string CurrentPassword { get; set; }

        [JsonPropertyName("newPassword")]
        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }

        [JsonPropertyName("confirmPassword")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
