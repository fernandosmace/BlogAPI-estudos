using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    public string Email { get; set; }
    public string Password { get; set; }
}