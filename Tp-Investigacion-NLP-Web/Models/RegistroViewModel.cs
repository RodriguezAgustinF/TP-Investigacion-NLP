using System.ComponentModel.DataAnnotations;
using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Web.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un email válido")]
    [StringLength(50, ErrorMessage = "El email no puede tener más de 50 caracteres")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(50, ErrorMessage = "La contraseña no puede tener más de 50 caracteres")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Es necesario confirmar la contraseña")]
    [StringLength(50, ErrorMessage = "La contraseña de confirmación no puede tener más de 50 caracteres")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmarPassword { get; set; } = string.Empty;

    public static Usuario ToEntity(RegistroViewModel registrarViewModel)
    {
        return new Usuario
        {
            Email = registrarViewModel.Email,
            Nombre = registrarViewModel.Nombre,
            PasswordHash = registrarViewModel.Password,
        };
    }
}