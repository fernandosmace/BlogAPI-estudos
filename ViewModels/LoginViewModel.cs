using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;
public class LoginViewModel
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; }
}