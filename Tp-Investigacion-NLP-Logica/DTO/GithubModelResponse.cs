using System;
using System.Collections.Generic;
using System.Text;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class GithubModelsResponse
{
    public List<GithubModelsChoice> Choices { get; set; } = new();
}

public class GithubModelsChoice
{
    public GithubModelsMessage Message { get; set; } = new();
}
