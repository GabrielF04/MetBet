using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using static MetBet.Program;

namespace MetBet.AcoesRobo
{
    public class PaginaInicial : IPipe
    {
        public object Run(dynamic input)
        {
            var wait = input.wait;
            var teclado = new InputSimulator();
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            IWebElement btnMudarLinguagem = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//li[@class=\'pr-2 d-none-320 langSelectWrapper\']")));
            btnMudarLinguagem.Click();

            // USAND0 COMNANDOS DE TECLADO, TROCO A LINGUAGEM PARA PT-BR
            Thread.Sleep(1000);
            teclado.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            Thread.Sleep(1000);
            teclado.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            //INTERAGINDO COM PAGAMENTOS
            IWebElement Pagamentos = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Pagamentos')]")));
            Pagamentos.Click();

            IWebElement Cancelamento = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Cancelamento')]")));
            Cancelamento.Click();

            IWebElement configRetirada = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://devadmin.metbet.io/admin-side/withdrawal/setConfig\']")));
            configRetirada.Click();


            // 22:00 == 1001
            //6:00 == 5001
            IWebElement campoRetiradaMaxima = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'InputWdLimit\']")));

            //O ROBO VERIFICA A HORA ATUAL PARA INTERAGIR COM O SAQUE MAXIMO
            if(currentTime >= sixAM)
            {
                campoRetiradaMaxima.Clear();
                campoRetiradaMaxima.SendKeys("5001");
            }
            else if(currentTime >= tenPM)
            {
                campoRetiradaMaxima.Clear();
                campoRetiradaMaxima.SendKeys("1001");
            }

            IWebElement btnSalvar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'btn btn-primary\']")));
            btnSalvar.Click();

            return input;
        }
    }
}
