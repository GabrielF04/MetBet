using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBet.ConfiguracoesWeb
{
    public class WebConfig : IPipe
    {
        public object Run(dynamic input)
        {
            IWebDriver driver = new ChromeDriver();
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("start-maximized");

            string urlPainelMetBet = "https://admin.metbet.io/";

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(urlPainelMetBet);

            string userDocumentationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var builder = new ConfigurationBuilder()
            .AddJsonFile(@$"{userDocumentationPath}\appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            string login = config["Credentials:Login"];
            string password = config["Credentials:Password"];

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            input.login = login; 
            input.password = password;
            input.wait = wait;
            input.driver = driver;

            return input;
        }
    }
}
