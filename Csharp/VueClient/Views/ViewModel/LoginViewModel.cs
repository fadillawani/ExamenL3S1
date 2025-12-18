using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email ou téléphone requis")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Mot de passe requis")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
