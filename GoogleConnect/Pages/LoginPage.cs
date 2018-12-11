using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace GoogleConnect
{
    public class LoginPage : TestBase
    {   
        private readonly IWebDriver _driver;

        //Constructors
        public LoginPage(IWebDriver driver) => _driver = driver;
        public LoginPage(Utility.Browser browser) => _driver = Utility.StartBrowserDriver(browser);

        //page element ids
        IWebElement txtUserName => _driver.FindElement(By.Id(USERNAME_TEXTBOX_ID));
        IWebElement btnUserName => _driver.FindElement(By.Id(USERNAME_SUBMIT_BUTTON_ID));
        IWebElement txtPassword => _driver.FindElement(By.Name(PASSWORD_TEXTBOX_ID));
        IWebElement btnLogin => _driver.FindElement(By.Id(PASSWORD_SUBMIT_BUTTON_ID));

        //Actions
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
            Utility.WaitUntilElementClickable(By.Id(USERNAME_SUBMIT_BUTTON_ID));
            btnUserName.Click();
        }
        public void EnterPassword(string password)
        {
            Utility.WaitUntilElementClickable(By.Id(PASSWORD_TEXTBOX_ID));
            txtPassword.SendKeys(password);
        }
        public void ClickLogin()
        {
            btnLogin.Click();
        }

        //Validation
        //TODO: write Assert/Validate functions
    }
}
