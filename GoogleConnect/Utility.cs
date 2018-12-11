using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace GoogleConnect
{
    /// <summary>
    /// Tests and user scenarios related functionality
    /// </summary>
    [TestFixture]
    public class Utility : TestBase
    {
        /// <summary>
        /// REturns a full path from currently working folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Initialize the browser driver
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Wait untill element is clickable or until a timeout is reached
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
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
