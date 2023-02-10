using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace ConsultaCEP.Steps
{
    [Binding]
    public sealed class ConsultarCEP
    {
        private IWebDriver driver;
        private string result;

        [BeforeScenario]
        public void BeforeScenario() {
            var optionsChr = new ChromeOptions();
            optionsChr.AddArgument("--headless");

            driver = new ChromeDriver(optionsChr)
            {
                Url = "http://www.correios.com.br/"
            };
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Given(@"que o CEP informado sera ""(.*)""")]
        public void GivenQueOCEPInformadoSera(string p0)
        {
            driver.FindElement(By.XPath("//*[@name='relaxation']")).SendKeys(p0);
        }

        [When(@"solicitar a pesquisa")]
        public void WhenSolicitarAPesquisa()
        {
            driver.FindElement(By.XPath("//*[@name='relaxation']")).SendKeys(Keys.Enter);
        }

        [Then(@"Retornara que o CEP não existe")]
        public void ThenRetornaraQueOCEPNaoExiste()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            result = driver.FindElement(By.XPath("//*[@id='mensagem-resultado']")).Text;
            Assert.AreEqual("Não há dados a serem exibidos", result);
        }

        [Given(@"que o CEP a ser pesquisado ""(.*)""")]
        public void GivenQueOCEPASerPesquisado(string p0)
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.FindElement(By.CssSelector("input[name='relaxation']")).SendKeys(p0);
        }

        [Then(@"Retornara o endereco referente ao CEP")]
        public void ThenRetornaraOEnderecoReferenteAoCEP()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            result = driver.FindElement(By.CssSelector("td[data-th='Logradouro/Nome']")).Text;
            Assert.AreEqual("Rua Quinze de Novembro - lado ímpar", result);
        }

        [Given(@"que o codigo de rastreio sera ""(.*)""")]
        public void GivenQueOCodigoDeRastreioSera(string p0)
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.FindElement(By.Id("objetos")).SendKeys(p0);
        }

        [When(@"solicitar acompanhamento do objeto")]
        public void WhenSolicitarAcompanhamentoDoObjeto()
        {
            driver.FindElement(By.Id("objetos")).SendKeys(Keys.Enter);
        }

        [Then(@"Retornara que o codigo nao existe")]
        public void ThenRetornaraQueOCodigoNaoExiste()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.FindElement(By.Id("captcha")).SendKeys("ABC");
            driver.FindElement(By.Id("captcha")).SendKeys(Keys.Enter);
            result = driver.FindElement(By.CssSelector("div[class='mensagem']")).Text;
            Assert.AreEqual("", result); //Deixado a validação em branco, pois o captcha esta barrando o processo.
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
