using System.ComponentModel.DataAnnotations;

namespace Tp_Investigacion_NLP_Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un email válido")]
    [StringLength(50, ErrorMessage = "El email no puede tener más de 50 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = string.Empty;
}