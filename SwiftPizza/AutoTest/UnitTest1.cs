using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Autotest
{
    [TestClass]
    public class automatedUI
    {
        private readonly IWebDriver driver;

        public automatedUI()
        {
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void launchbrowser()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Auth/Login");
        }

        [TestMethod]
        public void ShouldEnterCorrectAccountDetails()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Auth/Login");
            IWebElement Email = driver.FindElement(By.Name("Email"));
            Email.SendKeys("Johndo@gmail.com");


            IWebElement Password = driver.FindElement(By.Name("Password"));
            Password.SendKeys("Johnny");

            driver.FindElement(By.ClassName("btn-primary")).Click();
            Thread.Sleep(5000);
        }

        [TestMethod]
        public void ShouldEnterInCorrectAccountDetails()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Auth/Login");
            IWebElement Email = driver.FindElement(By.Name("Email"));
            Email.SendKeys("Johndo1997@gmail.com");


            IWebElement Password = driver.FindElement(By.Name("Password"));
            Password.SendKeys("Johnny1234");

            driver.FindElement(By.ClassName("btn-primary")).Click();
            Thread.Sleep(5000);
        }

        [TestMethod]

        public void ShouldSortByAccending()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");
            IWebElement SortOrder = driver.FindElement(By.Name("SortOrder"));
            SelectElement element = new SelectElement(SortOrder);

            element.SelectByValue("asc");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);



        }

        [TestMethod]
        public void ShouldSortByDecending()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");
            IWebElement SortOrder = driver.FindElement(By.Name("SortOrder"));
            SelectElement element = new SelectElement(SortOrder);

            element.SelectByValue("desc");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);




        }
        [TestMethod]
        public void ShouldEnterValidKeyWords()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");
            IWebElement SearchTerm = driver.FindElement(By.Name("SearchTerm"));

            SearchTerm.SendKeys("Cheesy");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);



        }

        [TestMethod]
        public void ShouldEnterInValidKeyWords()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");
            IWebElement SearchTerm = driver.FindElement(By.Name("SearchTerm"));

            SearchTerm.SendKeys("Mushroom");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
        }


        [TestMethod]
        public void ShouldEnterSpecialCharaters()
        {
            driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");
            IWebElement SearchTerm = driver.FindElement(By.Name("SearchTerm"));

            SearchTerm.SendKeys("!@#$");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Thread.Sleep(5000);




        }

        [TestMethod]
        public void ShouldAddItemsToCart()
        {

            using (var driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");

                var button = driver.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']"));

                button.Click();
                Thread.Sleep(5000);


            }

        }

        [TestMethod]
        public void ShloudRemoveItemsFromCart()
        {
            using (var driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");


                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver1 => driver1.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']")) != null);

                var addButton = driver.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']"));
                addButton.Click();

                Thread.Sleep(5000);

                wait.Until(driver1 => driver1.FindElement(By.XPath("//button[@onclick=\"removePizzaFromCart('1')\"]")) != null);

                var removeButton = driver.FindElement(By.XPath("//button[@onclick=\"removePizzaFromCart('1')\"]"));
                removeButton.Click();

                Thread.Sleep(5000);
            }




        }

        [TestMethod]

        public void ShouldAcceptPayment()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");

                var button = driver.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']"));

                button.Click();

                driver.FindElement(By.Id("proceedToPaymentBtn")).Click();
                IWebElement cardNumber = driver.FindElement(By.Id("cardNumber"));

                cardNumber.SendKeys("7530876448327543");

                IWebElement expiry = driver.FindElement(By.Id("expiry"));

                expiry.SendKeys("08/29");

                IWebElement cvv = driver.FindElement(By.Id("cvv"));

                cvv.SendKeys("455");

                driver.FindElement(By.XPath("//button[@onclick='makePayment()']")).Click();
                Thread.Sleep(5000);
            }

        }

        [TestMethod]
        public void ShouldDeclinePaymentIccorrectInfo()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");

                var button = driver.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']"));

                button.Click();

                driver.FindElement(By.Id("proceedToPaymentBtn")).Click();
                IWebElement cardNumber = driver.FindElement(By.Id("cardNumber"));

                cardNumber.SendKeys("27543");

                IWebElement expiry = driver.FindElement(By.Id("expiry"));

                expiry.SendKeys("08/29");

                IWebElement cvv = driver.FindElement(By.Id("cvv"));

                cvv.SendKeys("455");

                driver.FindElement(By.XPath("//button[@onclick='makePayment()']")).Click();

                Thread.Sleep(5000);
            }

        }
        [TestMethod]

        public void ShouldEnterBlankDetails()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:7020/Home/Cart");

                var button = driver.FindElement(By.CssSelector("button[onclick*='addPizzaToCart']"));

                button.Click();

                driver.FindElement(By.Id("proceedToPaymentBtn")).Click();
                IWebElement cardNumber = driver.FindElement(By.Id("cardNumber"));

                cardNumber.SendKeys("");

                IWebElement expiry = driver.FindElement(By.Id("expiry"));

                expiry.SendKeys("");

                IWebElement cvv = driver.FindElement(By.Id("cvv"));

                cvv.SendKeys("");

                driver.FindElement(By.XPath("//button[@onclick='makePayment()']")).Click();
                Thread.Sleep(5000);
            }

        }
    }
}
