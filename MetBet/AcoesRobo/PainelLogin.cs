using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetBet.Program;

namespace MetBet.AcoesRobo
{
    public class PainelLogin : IPipe
    {
        public object Run(dynamic input)
        {
            var wait = input.wait;
            string login = input.login;
            string password = input.password;

            IWebElement campoLogin = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@name=\'email\']")));
            campoLogin.SendKeys(login);

            IWebElement campoSenha = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@name=\'password\']")));
            campoSenha.SendKeys(password);

            IWebElement btnLogin = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'mainBtn\']")));
            btnLogin.Click();

            return input;
        }
    }
}
