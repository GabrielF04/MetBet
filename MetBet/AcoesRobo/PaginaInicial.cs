using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WindowsInput.Native;

using static MetBet.Program;

namespace MetBet.AcoesRobo
{
    public class PaginaInicial : IPipe
    {
        public object Run(dynamic input)
        {
            IWebDriver driver = input.driver;
            string userDocumentationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var wait = input.wait;
            var teclado = new InputSimulator();
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            string diretorioPrint = $@"{userDocumentationPath}\TravaSaquePrints";
            string nomeArquivoPrint = "RetiradaMaxima.png";

            string caminhoCompleto = Path.Combine(diretorioPrint, nomeArquivoPrint);

            if (Directory.Exists(diretorioPrint))
            {
                // Deleta todos os arquivos do diretório
                foreach (var file in Directory.GetFiles(diretorioPrint))
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Caso o diretório não exista, crie-o
                Directory.CreateDirectory(diretorioPrint);
            }

            IWebElement btnMudarLinguagem = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//li[@class='pr-2 d-none-320 langSelectWrapper']")));
            btnMudarLinguagem.Click();

            // Aguarda o menu dropdown carregar e localiza a opção de idioma "Português (Brasil)"
            IWebElement opcaoPortugues = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//li[contains(text(), 'Pt-br')]")));
            opcaoPortugues.Click();

            //INTERAGINDO COM PAGAMENTOS
            IWebElement Pagamentos = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Pagamentos')]")));
            Pagamentos.Click();

            IWebElement Cancelamento = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Cancelamento')]")));
            Cancelamento.Click();

            IWebElement configRetirada = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://admin.metbet.io/admin-side/withdrawal/setConfig\']")));
            configRetirada.Click();


            // 22:00 == 1001
            //6:00 == 5001
            IWebElement campoRetiradaMaxima = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'InputWdLimit\']")));

            //O ROBO VERIFICA A HORA ATUAL PARA INTERAGIR COM O SAQUE MAXIMO

            if (currentTime >= sixAM && currentTime < tenPM)
            {
                campoRetiradaMaxima.Clear();
                campoRetiradaMaxima.SendKeys("10001");
            }
            else if (currentTime >= tenPM)
            {
                campoRetiradaMaxima.Clear();
                campoRetiradaMaxima.SendKeys("1001");
            }
            Thread.Sleep(1000);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile($"{caminhoCompleto}.Png");


            IWebElement btnSalvar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@class=\'btn btn-primary\']")));
            btnSalvar.Click();

            return input;
        }
    }
}
