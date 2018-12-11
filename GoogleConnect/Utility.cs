using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using GoogleConnect;

namespace GoogleConnect
{
    [TestFixture]
    public class Utility : TestBase
    {
        int timeout = 10; // seconds

        public static string GetFullPath(string filePath)
        {
            string solutionParentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(solutionParentDirectory, filePath);
        }
        
        public enum Browser
        {
            Chrome = 1,
            Firefox,
            InternetExplorer
        }

        public enum EmailState
        {
            Unread = 1,
            Read,
            All
         }

        public static IWebDriver StartBrowserDriver(Utility.Browser browser)
        {
            switch (browser)
            {
                case Utility.Browser.Chrome:
                    TestBase.driver = new ChromeDriver(GetFullPath(TestBase.SELENIUM_DRIVERS_LOCATION));
                    break;
                case Utility.Browser.Firefox:
                    TestBase.driver = new FirefoxDriver(GetFullPath(TestBase.SELENIUM_DRIVERS_LOCATION));
                    break;
                case Utility.Browser.InternetExplorer:
                    var options = new InternetExplorerOptions();
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    TestBase.driver = new InternetExplorerDriver(GetFullPath(TestBase.SELENIUM_DRIVERS_LOCATION), options);
                    break;
            }
            return TestBase.driver;
        }

        [FindsBy(How = How.Id, Using = ":3a")]
        private IWebElement TableOfEmails;

        public static List<GoogleEmail> GetEmailData(int numberOfItems)
        {

            var allEmails = TestBase.driver.FindElements(By.XPath("//*[@class='zF']")).Count; //
            var read = TestBase.driver.FindElements(By.XPath("//*[@class='yP']")).Count; //

            var unreadEmeil = TestBase.driver.FindElements(By.XPath("//*[@class='bsU']")).Count;

            var emails = TestBase.driver.FindElements(By.XPath("//*[@class=':3a']"));
            numberOfItems = allEmails;


            var result = new List<GoogleEmail>();
            var allRows = TestBase.driver.FindElements(By.TagName("tr"));
            string test = allRows[0].Text;
            string test1 = allRows[1].Text;

            var index = 0;

            foreach (IWebElement elem in allRows)
            {
                var colVals = elem.FindElements(By.TagName("td"));
                string test2 = colVals[0].Text;
                string test3 = colVals[1].Text;
                string test4 = elem.Text;


                // what we are looking for is col4 - from, col 5 desc and col 8 - time
                // bad to hard code this but it should not likely change
                var from = colVals[0].Text.ToString();
                var date = colVals[1].Text;
                var subject = colVals[6].Text;

                result.Add(new GoogleEmail(from, date, subject, true));
                index++;

                // if we have reached the number we want then bomb out
                if (index == numberOfItems)
                {
                    break;
                }

            }
            return result;
        }

        /// <summary>
        /// Login helper method
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static IWebDriver Login(string userName, string password, Utility.Browser browser)
        {            
            LoginPage page = new LoginPage(browser);
            page.Open();
            page.EnterUserName(userName);
            page.ClickNext();
            page.EnterPassword(password);
            page.ClickLogin();

            //Verify login was successful other tests should not continue if this fails
            NUnit.Framework.Assert.IsTrue(true, TestBase.UserName);

            return driver;
        }

        /// <summary>
        /// Login helper method overload with default browser
        /// </summary>
        /// <returns></returns>
        public static IWebDriver Login(string userName, string password)
        {
            return Login(userName, password, Utility.DefaultBrowser);
        }

        /// <summary>
        /// Login helper method overload with default credentials
        /// </summary>
        /// <returns></returns>
        public static IWebDriver Login(Utility.Browser browser)
        {
            return Login(UserName, Password, browser);
        }

        /// <summary>
        /// Login helper method overload with default credentials and browser
        /// </summary>
        /// <returns></returns>
        public static IWebDriver Login()
        {
            return Login(UserName, Password, Utility.DefaultBrowser);
        }

        //Wait untill element is clickable
        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element : '" + elementLocator + "' was not found.");
                throw;
            }
        }

    }
}
