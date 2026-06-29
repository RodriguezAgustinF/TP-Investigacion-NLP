using System;
using System.Collections.Generic;
using System.Text;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface ILLMServiceFactory
{
    ILLMService GetService(LLMProvider provider);
}
