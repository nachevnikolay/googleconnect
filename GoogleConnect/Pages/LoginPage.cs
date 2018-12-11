using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;

namespace GoogleConnect
{
    public class LoginPage
    {   
        private readonly IWebDriver _driver;

        //constructors
        public LoginPage(IWebDriver driver) => _driver = driver;
        public LoginPage(Utility.Browser browser) => _driver = Utility.StartBrowserDriver(browser);

        //page element ids
        IWebElement txtUserName => _driver.FindElement(By.Id("identifierId"));
        IWebElement btnUserName => _driver.FindElement(By.Id("identifierNext"));
        IWebElement txtPassword => _driver.FindElement(By.Name("password"));
        IWebElement btnLogin => _driver.FindElement(By.Id("passwordNext"));


        //actions

        public void Open()
        {
            _driver.Navigate().GoToUrl(TestBase.LOGIN_URL);
        }

        public void EnterUserName(string userName)
        {
            txtUserName.SendKeys(userName);
        }
        public void ClickNext()
        {
            btnUserName.Click();
        }
        public void EnterPassword(string password)
        {
            txtPassword.SendKeys(password);
        }

        public void ClickLogin()
        {
            btnLogin.Click();
        }
    }
}
