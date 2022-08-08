using System.ComponentModel.DataAnnotations;

namespace MlVitrine.ViewModels

{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor informe um usuario")]
        [Display(Name = "Usuario")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Por favor informe uma senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Por favor informe um usuario")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        private string _password;

        [Required(ErrorMessage = "Por favor informe uma senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get { return _password; } set { _password = value; } }

        [Required(ErrorMessage = "Por favor informe um email valido")]
        [Display(Name = "Email")]
        public string UserMail { get; set; }

    }
}

