using System.ComponentModel.DataAnnotations;

namespace Dimo.PL.Views.Account
{
    public class ForgetPassWordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Nessecary")]
        public string Email { get; set; }
    }
}
