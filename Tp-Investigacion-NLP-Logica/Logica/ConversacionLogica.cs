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
            Titulo = "Nueva conversación",
            FechaCreacion = DateTime.Now
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

    public string ActualizarTituloSiEsNueva(int conversacionId, string mensaje)
    {
        var conversacion = _db.Conversaciones
            .FirstOrDefault(c => c.Id == conversacionId);

        if (conversacion == null)
        {
            return "Nueva conversación";
        }

        if (conversacion.Titulo != "Nueva conversación")
        {
            return conversacion.Titulo;
        }

        string titulo = mensaje.Trim();

        if (titulo.Length > 35)
        {
            titulo = titulo.Substring(0, 35) + "...";
        }

        conversacion.Titulo = titulo;

        _db.SaveChanges();

        return conversacion.Titulo;
    }

    public void EliminarConversacion(int conversacionId, int usuarioId)
    {
        var conversacion = _db.Conversaciones
            .Include(c => c.Mensajes)
            .FirstOrDefault(c =>
                c.Id == conversacionId &&
                c.UsuarioId == usuarioId);

        if (conversacion == null)
        {
            return;
        }

        _db.Mensajes.RemoveRange(conversacion.Mensajes);

        _db.Conversaciones.Remove(conversacion);

        _db.SaveChanges();
    }
}
