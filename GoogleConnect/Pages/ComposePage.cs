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
    class ComposePage : TestBase
    {
        private readonly IWebDriver _driver;

        //Constructors
        //Get a Compose page context from inbox page
        public ComposePage(InboxPage inboxPage) => _driver = inboxPage.Compose();

        //Get a successfull login context and pass it to the InboxPage constructor
        public ComposePage(string UserName, string Password)
        {
            InboxPage inbox = new InboxPage(UserName, Password);
            _driver = inbox.Compose();
        }

        IWebElement btnCloseNewEmailWindow => _driver.FindElement(By.ClassName("Ha"));
        IWebElement btnCloseAndDiscardDraft => _driver.FindElement(By.ClassName("oh"));


        //Actions
        public void CloseNewEmailWindow()
        {
            Utility.WaitUntilElementClickable(By.ClassName("Ha"));

            btnCloseNewEmailWindow.Click();
        }
        public void CloseAndDiscardDraft()
        {
            Utility.WaitUntilElementClickable(By.ClassName("oh"));

            btnCloseAndDiscardDraft.Click();
        }

        //TODO: write actions
        //SendMail()



        //Validation
        //TODO: write Assert/Validate functions
    }
}
