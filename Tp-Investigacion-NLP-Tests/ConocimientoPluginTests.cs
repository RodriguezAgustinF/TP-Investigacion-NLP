using Tp_Investigacion_NLP_Logica.Plugins;

namespace Tp_Investigacion_NLP_Tests;

public class ConocimientoPluginTests
{
    private readonly ConocimientoPlugin _plugin = new();

    [Fact]
    public void BuscarConocimiento_EncuentraInformacionDeRag()
    {
        string resultado = _plugin.BuscarConocimiento(
            "¿Cómo ayuda RAG a evitar alucinaciones?");

        Assert.Contains("RAG", resultado, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("alucinaciones", resultado, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void BuscarConocimiento_IgnoraAcentosEnLaConsulta()
    {
        string resultado = _plugin.BuscarConocimiento(
            "¿Qué recomienda sobre gestion de secretos?");

        Assert.Contains("variables de entorno", resultado, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void BuscarConocimiento_ConsultaDesconocida_NoInventaInformacion()
    {
        string resultado = _plugin.BuscarConocimiento("ornitología cuántica submarina");

        Assert.Contains("no contiene información", resultado, StringComparison.OrdinalIgnoreCase);
    }
}
