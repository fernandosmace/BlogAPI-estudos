using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "O senha deve ter no mínimo 8 caracteres com letras maiúsculas e minúsculas, números e caracteres especiais.")]
    public string Password { get; set; }
}