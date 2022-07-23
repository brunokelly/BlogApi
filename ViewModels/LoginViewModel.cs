using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail é inválido")]
        public string Password { get; set; }
    }
}
