using System.ComponentModel.DataAnnotations;

namespace MyDebtsApi.ViewModels;

public class RegistroViewModel
{
    
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O e-mail é obrigatório!")]
    [EmailAddress(ErrorMessage = "Esse e-mail não é válido!")]
    public string Email { get; set; }
}