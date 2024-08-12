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
    public class GatewaysDePagamento : IPipe
    {
        public object Run(dynamic input)
        {
            // TELA DE GATEWAYS DE PAGAMENTOS
            var wait = input.wait;
            var teclado = new InputSimulator();
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            IWebElement gatewayPagamento = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://devadmin.metbet.io/admin-side/payment_gateways/deposits\']")));
            gatewayPagamento.Click();

            IWebElement retiradas = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\"https://devadmin.metbet.io/admin-side/payment_gateways/withdrawals\"]")));
            retiradas.Click();

            IWebElement betterBro = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class=\'  details-control\']")));
            betterBro.Click();

            IWebElement betterBro3Pontos = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://devadmin.metbet.io/admin-side/payment_gateways/withdrawal/913\']")));
            betterBro3Pontos.Click();

            // PAGEDOWN
            teclado.Keyboard.KeyPress((VirtualKeyCode)0x22);
            Thread.Sleep(1000);
            IWebElement campoLimiteRetiradaMinimo = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class=\'align-middle\']//div[@class=\'relative betLimits_input_left border\']/input")));

            // CLICO NO CAMPO PRIMEIRO PARA PODER LIMPA-LO
            //OBS: DEVO CLICAR PRIMEIRO E USAR COMANDO DE TECLADO PARA LIMPAR
            campoLimiteRetiradaMinimo.Click();
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            campoLimiteRetiradaMinimo.SendKeys("50");

            IWebElement campoLimiteRetiradaMaximo = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class=\'align-middle\']//div[@class=\'relative betLimits_input_right border\']/input")));

            campoLimiteRetiradaMaximo.Click();
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);
            teclado.Keyboard.KeyPress(VirtualKeyCode.BACK);

            // VERIFICO A HORA ATUAL PARA AJUSTAR A RETIRADA MAXIMA
            if (currentTime >= sixAM)
            {
                campoLimiteRetiradaMaximo.Clear();
                campoLimiteRetiradaMaximo.SendKeys("5000");
            }
            else if (currentTime >= tenPM)
            {
                campoLimiteRetiradaMaximo.Clear();
                campoLimiteRetiradaMaximo.SendKeys("1000");
            }

            IWebElement btnSalvar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'btn btn-primary btn-success\']")));
            btnSalvar.Click();

            return input;
        }
    }
}
