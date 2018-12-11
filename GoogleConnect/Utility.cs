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
    public class Utility : TestBase
    {
        public string GetFullPath(string filePath)
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

        public static void StartBrowserDriver(Utility.Browser browser)
        {
            switch (browser)
            {
                case Utility.Browser.Chrome:
                    TestBase.driver = new ChromeDriver(TestBase.SELENIUM_DRIVERS_LOCATION);
                    break;
                case Utility.Browser.Firefox:
                    TestBase.driver = new FirefoxDriver(TestBase.SELENIUM_DRIVERS_LOCATION);
                    break;
                case Utility.Browser.InternetExplorer:
                    var options = new InternetExplorerOptions();
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    TestBase.driver = new InternetExplorerDriver(TestBase.SELENIUM_DRIVERS_LOCATION, options);
                    break;
            }
        }

        [FindsBy(How = How.Id, Using = ":3a")]
        private IWebElement TableOfEmails;

        public static List<GoogleEmail> GetEmailData(int numberOfItems)
        {

            var allEmails = TestBase.driver.FindElements(By.XPath("//*[@class='zF']")).Count;
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

                result.Add(new GoogleEmail(from, date, subject));
                index++;

                // if we have reached the number we want then bomb out
                if (index == numberOfItems)
                {
                    break;
                }

            }
            return result;
        }
    }
}
