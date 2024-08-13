using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using static MetBet.Program;

namespace MetBet.AcoesRobo
{
    public class FrasesTravaSaque : IPipe
    {
        public object Run(dynamic input)
        {
            var wait = input.wait;
            var teclado = new InputSimulator();
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            IWebElement campoBusca = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//div[@id=\'translations_filter\']//input[@type=\'search\']")));
            campoBusca.SendKeys("number_less_than");

            IWebElement campoFraseSaque = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@class=\'form-control translateEditor\']")));
            Thread.Sleep(1000);
            // VERIFICO A HORA ATUAL PARA INCLUIR A FRASE
            if (currentTime >= sixAM && currentTime < tenPM)
            {
                campoFraseSaque.Click();
                campoFraseSaque.Clear();
                campoFraseSaque.SendKeys("O saque máximo por dia deve ser menor ou igual a");
            }
            else if (currentTime >= tenPM)
            {
                campoFraseSaque.Click();
                campoFraseSaque.Clear();
                campoFraseSaque.SendKeys("Por ordem do banco central cumprimos as regras de saque noturno via pix. Entre 22:00 e 07:00. O saque maximo deve ser menor ou igual a");
            }

            IWebElement btnConfirmaFrase = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'btn btn-sm btn-transparent saveTrans\']")));
            btnConfirmaFrase.Click();

            return input;
        }
    }
}
