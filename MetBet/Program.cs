using OpenQA.Selenium;
using System.Diagnostics;
using System.Dynamic;

namespace MetBet
{
    public class Program
    {
        public static void Contador()
        {
            for (var i = 0; i <= 1200; i++)
            {
                Thread.Sleep(1000);
            }
            Environment.Exit(0);
        }
        public static string Erro { get; set; }
        public static bool ExecucaoFinalizou { get; set; }
        static void OnApplicationExit(object sender, EventArgs e)
        {
            if (!Program.ExecucaoFinalizou)
            {
                //CadastroInterrompido.ItemKeyInterrompido();
            }
        }
        static void Main(string[] args)
        {
            try
            {
                ExpandoObject input = new ExpandoObject();
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnApplicationExit);
                var sw = new Stopwatch();//tempo de execucao do processo
                sw.Start();

                Pipelines.StartPipelines startPipelines = new Pipelines.StartPipelines();

                sw.Stop();
                Console.WriteLine(@"Tempo gasto : " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
                Console.WriteLine(@"Tempo decorrido: {0:hh\:mm\:ss}", sw.Elapsed);
                Console.ReadLine();
                ExecucaoFinalizou = true;
                Environment.Exit(0);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }
        public static class CustomExpectedConditions
        {
            public static Func<IWebDriver, IWebElement> ElementIsVisible(By locator)
            {
                return driver =>
                {
                    try
                    {
                        var element = driver.FindElement(locator);
                        return element.Displayed ? element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                };
            }
        }
    }
}
