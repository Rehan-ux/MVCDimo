using System.ComponentModel.DataAnnotations;

namespace Dimo.PL.Views.Account
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
       // [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
