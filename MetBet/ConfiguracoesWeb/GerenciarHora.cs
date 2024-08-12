using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBet.ConfiguracoesWeb
{
    public class GerenciarHora : IPipe
    {
        public object Run(dynamic input)
        {

            DateTime now = DateTime.Now;

            // Definir os horários de 6:00 AM e 22:00 PM
            TimeSpan sixAM = new TimeSpan(6, 0, 0);
            TimeSpan tenPM = new TimeSpan(22, 0, 0);

            // Obter a hora atual como TimeSpan
            TimeSpan currentTime = now.TimeOfDay;

            // Verificar se a hora atual é 6:00 AM ou maior
            if (currentTime >= sixAM && currentTime < tenPM)
            {
                Console.WriteLine("É 6:00 AM ou mais, mas ainda não são 22:00.");
            }
            else if (currentTime >= tenPM)
            {
                Console.WriteLine("É 22:00 ou mais.");
            }
            else
            {
                Console.WriteLine("Ainda não são 6:00 AM.");
            }

            input.sixAM = sixAM;
            input.tenPM = tenPM;
            input.currentTime = currentTime;

            return input;
        }
    }
}
