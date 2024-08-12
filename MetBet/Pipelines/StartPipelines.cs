using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBet.Pipelines
{
    public class StartPipelines
    {
        public StartPipelines()
        {
            var input = new ExpandoObject();
            Pipelines pipelines = new Pipelines();

            bool validacaoErro = false;
            int contador = 0;

            do
            {
                try
                {
                    CallStartPipelines callStartPipelines = new CallStartPipelines(input, validacaoErro, pipelines);

                }
                catch (Exception ex)
                {
                    Program.Erro = ex.ToString();
                    Console.WriteLine(Program.Erro);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("Antes default");
                    Console.ReadLine();
                    switch (Program.Erro)
                    {
                        case var erroElemento when erroElemento.Contains("Erro"):
                            validacaoErro = true;
                            contador++;
                            break;
                        default:

                            Console.WriteLine("default::::::::::: pressione enter");
                            Console.WriteLine(Program.Erro);
                            Console.ReadLine();
                            break;
                    }
                }
            } while (validacaoErro && contador <= 10);

        }
    }
}
