using Microsoft.AspNetCore.Identity;
using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class UsuarioLogica : IUsuarioLogica
{
    private readonly NLPDbContext _db;
    private readonly PasswordHasher<Usuario> _passwordHasher;

    public UsuarioLogica(NLPDbContext db)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    public Usuario LoginUsuario(string email, string password)
    {
        Usuario usuarioEncontrado = _db.Usuarios.FirstOrDefault(u => u.Email == email);
        if (usuarioEncontrado == null)
        {
            throw new InvalidOperationException("Email o contraseña incorrectos.");
        }
        var resultado = _passwordHasher.VerifyHashedPassword(usuarioEncontrado, usuarioEncontrado.PasswordHash, password);

        if (PasswordVerificationResult.Failed == resultado)
        {
            throw new InvalidOperationException("Email o contraseña incorrectos.");
        }
        return usuarioEncontrado;
    }

    public void RegistrarUsuario(Usuario usuario)
    {
        if (_db.Usuarios.Any(u => u.Email == usuario.Email))
        {
            throw new InvalidOperationException("El email ya está registrado.");
        }
        usuario.PasswordHash = _passwordHasher.HashPassword(usuario, usuario.PasswordHash);
        _db.Usuarios.Add(usuario);
        _db.SaveChanges();
    }
}