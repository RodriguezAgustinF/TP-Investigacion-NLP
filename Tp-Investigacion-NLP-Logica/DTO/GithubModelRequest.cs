using System;
using System.Collections.Generic;
using System.Text;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class GithubModelsRequest
{
    public string Model { get; set; } = string.Empty;

    public List<GithubModelsMessage> Messages { get; set; } = new();

    public double Temperature { get; set; } = 0.7;

    public int Max_Tokens { get; set; } = 800;
}
