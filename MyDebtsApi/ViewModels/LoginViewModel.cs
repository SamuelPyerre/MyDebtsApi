using System.ComponentModel.DataAnnotations;

namespace MyDebtsApi.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória!")]
    public string Password { get; set; }
}