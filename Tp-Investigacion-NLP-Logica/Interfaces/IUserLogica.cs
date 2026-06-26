using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IUsuarioLogica
{
    Usuario LoginUsuario(string email, string password);
    void RegistrarUsuario(Usuario usuario);
}
