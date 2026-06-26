using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class MensajeLogica : IMensajeLogica
{
    private readonly NLPDbContext _db;

    public MensajeLogica(NLPDbContext db)
    {
        _db = db;
    }

    public void AgregarMensajeUsuario(int conversacionId, string contenido)
    {
        var mensaje = new Mensaje
        {
            ConversacionId = conversacionId,
            Contenido = contenido,
            Fecha = DateTime.Now,
            Rol = "user"
        };

        _db.Mensajes.Add(mensaje);
        _db.SaveChanges();
    }

    public void AgregarMensajeAsistente(int conversacionId, string contenido)
    {
        var mensaje = new Mensaje
        {
            ConversacionId = conversacionId,
            Contenido = contenido,
            Fecha = DateTime.Now,
            Rol = "assistant"
        };

        _db.Mensajes.Add(mensaje);
        _db.SaveChanges();
    }

    public List<Mensaje> ObtenerMensajes(int conversacionId)
    {
        return _db.Mensajes
            .Where(m => m.ConversacionId == conversacionId)
            .OrderBy(m => m.Fecha)
            .ToList();
    }
}
