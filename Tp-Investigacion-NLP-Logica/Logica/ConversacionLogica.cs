using Microsoft.EntityFrameworkCore;
using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class ConversacionLogica : IConversacionLogica
{
    private readonly NLPDbContext _db;

    public ConversacionLogica(NLPDbContext db)
    {
        _db = db;
    }

    public Conversacion CrearConversacion(int usuarioId)
    {
        var conversacion = new Conversacion
        {
            UsuarioId = usuarioId,
            FechaCreacion = DateTime.Now,
            Titulo = $"Conversación {DateTime.Now:dd/MM/yyyy HH:mm}"
        };

        _db.Conversaciones.Add(conversacion);
        _db.SaveChanges();

        return conversacion;
    }

    public List<Conversacion> ObtenerConversaciones(int usuarioId)
    {
        return _db.Conversaciones
            .Where(c => c.UsuarioId == usuarioId)
            .OrderByDescending(c => c.FechaCreacion)
            .ToList();
    }

    public Conversacion? ObtenerConversacion(int conversacionId, int usuarioId)
    {
        var conversacion = _db.Conversaciones
            .Include(c => c.Mensajes)
            .FirstOrDefault(c =>
                c.Id == conversacionId &&
                c.UsuarioId == usuarioId);

        if (conversacion != null)
        {
            conversacion.Mensajes = conversacion.Mensajes
                .OrderBy(m => m.Fecha)
                .ToList();
        }

        return conversacion;
    }

    public Conversacion ObtenerConversacionCompleta(int conversacionId)
    {
        return _db.Conversaciones
                .Include(c => c.Mensajes)
                .First(c => c.Id == conversacionId);
    }
}
