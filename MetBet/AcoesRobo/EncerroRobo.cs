using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace MetBet.AcoesRobo
{
    public class EncerroRobo : IPipe
    {
        public object Run(dynamic input)
        {
            IWebDriver driver = input.driver;
            Thread.Sleep(4000);
            driver.Quit();
            Environment.Exit(0);

            return input;
        }
    }
}
