using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBet.Pipelines
{
    public class CallStartPipelines
    {
        public CallStartPipelines(object input, bool validacaoErro, PipelineGroup pipelines) 
        {
            pipelines["ExecucaoInicial"].Run(input);
            pipelines["ExecucaoRobo"].Run(input);
        }
    }
}
