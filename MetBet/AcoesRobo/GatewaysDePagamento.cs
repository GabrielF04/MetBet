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
            IWebDriver driver = input.driver;
            var wait = input.wait;
            var teclado = new InputSimulator();
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            IWebElement gatewayPagamento = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/payment_gateways/deposits\']")));
            gatewayPagamento.Click();

            IWebElement retiradas = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/payment_gateways/withdrawals\']")));
            retiradas.Click();

            IWebElement betterBro = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class=\'  details-control\']")));
            betterBro.Click();

            IWebElement betterBro3Pontos = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/payment_gateways/withdrawal/799\']")));
            betterBro3Pontos.Click();

            // PAGEDOWN
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            Thread.Sleep(1000);
            IWebElement campoLimiteRetiradaMinimo = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class='align-middle']//div[@class='relative betLimits_input_left border']/input")));
            IWebElement campoLimiteRetiradaMaximo = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//td[@class='align-middle']//div[@class='relative betLimits_input_right border']/input")));

            // Ajuste o valor dos campos com base na hora atual
            if (currentTime >= sixAM && currentTime < tenPM)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = '50'; arguments[0].dispatchEvent(new Event('change'));", campoLimiteRetiradaMinimo);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = '5000'; arguments[0].dispatchEvent(new Event('change'));", campoLimiteRetiradaMaximo);
            }
            else if (currentTime >= tenPM)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = '50'; arguments[0].dispatchEvent(new Event('change'));", campoLimiteRetiradaMinimo);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = '1000'; arguments[0].dispatchEvent(new Event('change'));", campoLimiteRetiradaMaximo);
            }

            IWebElement btnSalvar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'btn btn-primary\']")));
            btnSalvar.Click();
            return input;
        }
    }
}
