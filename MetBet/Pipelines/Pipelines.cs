using MetBet.AcoesRobo;
using MetBet.ConfiguracoesWeb;
using MetBet.Email;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBet.Pipelines
{
    public class Pipelines : PipelineGroup 
    {
        public Pipelines() : base()
        {
            Pipeline("ExecucaoInicial")
                .Pipe<WebConfig>()
                .Pipe<GerenciarHora>()
                ;

            Pipeline("ExecucaoRobo")
                .Pipe<PainelLogin>()
                .Pipe<PaginaInicial>()
                //.Pipe<GatewaysDePagamento>()
                .Pipe<CmsTela>()
                .Pipe<FrasesTravaSaque>()
                .Pipe<EnviaEmail>()
                .Pipe<EncerroRobo>()
                ;
        }
    }
}
