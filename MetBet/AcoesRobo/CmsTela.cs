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
    public class CmsTela : IPipe
    {
        public object Run(dynamic input)
        {
            var wait = input.wait;
            var teclado = new InputSimulator();

            IWebElement cms = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'CMS')]")));
            cms.Click();

            IWebElement traducao = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Tradução')]")));
            traducao.Click();

            IWebElement traducaoPlataforma = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/translate_static/list\']")));
            traducaoPlataforma.Click();

            // PAGEDOWN
            teclado.Keyboard.KeyPress((VirtualKeyCode)0x22);
            Thread.Sleep(1000);

            IWebElement btnPtBr = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/translate_static/lang/pt-br\']")));
            btnPtBr.Click();

            return input;
        }
    }
}
