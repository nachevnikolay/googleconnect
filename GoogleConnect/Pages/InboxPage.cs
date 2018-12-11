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
    public class InboxPage : TestBase
    {
        private readonly IWebDriver _driver;

        //Get a successfull login context and pass it to the InboxPage constructor
        public InboxPage(string UserName, string Password)
        {
            _driver = Utility.Login(UserName, Password);
        }


        IWebElement drpdownAccountBubble => _driver.FindElement(By.ClassName(ACCOUNT_OPTIONS_DROPDOWN_ID));
        IWebElement btnSignOut => _driver.FindElement(By.Id(SIGNOUT_BUTTON_ID));
        IWebElement btnCompose => _driver.FindElement(By.XPath(COMPOSE_NEW_EMAIL_BUTTON_ID));

        //Actions
        public IWebDriver Compose()
        {
            Utility.WaitUntilElementClickable(By.XPath(COMPOSE_NEW_EMAIL_BUTTON_ID));
            btnCompose.Click();
            return _driver;
        }
        public void SignOut()
        {
            Utility.WaitUntilElementClickable(By.ClassName(ACCOUNT_OPTIONS_DROPDOWN_ID));
            drpdownAccountBubble.Click();
            btnSignOut.Click();
        }

        //TODO: Write email list related actions
        //Get count of unread emails
        //Get count of total emails


        //Get list of latest unread emails
        public void GetEmails()
        {
            var result = new List<GoogleEmail>();

            //var a = driver.FindElements(By.Id(":3a"));
            var from = driver.FindElements(By.XPath("//*[@class='yW']/span"));
            var subject = driver.FindElements(By.XPath("//*[@class='xY a4W']/div"));
            var time = driver.FindElements(By.XPath("//*[@class='xW xY ']/span"));

            string fromField, emailField, subjectField, timeField;
            bool unreadField = false;
            for (int i = 0; i < subject.Count; i++)
            {
                fromField = from[i].Text;
                emailField = from[i].Text;

                subjectField = subject[i].Text;
                timeField = time[i].Text;
                unreadField = (time[i].Text).Contains("bf3");

                var message = new GoogleEmail(fromField, subjectField, timeField, unreadField);

                result.Add(message);

            }
        }


        //Get list of all emails on front page 


        //TODO: write single email related actions
        //MarkAsRead()
        //MarkAsUnread()
        //Delete()
        //OpenMail()


        //Validation
        //TODO: write Assert/Validate functions
    }
}

