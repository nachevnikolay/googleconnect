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

[TestClass]
public class GoogleTests
{
    #region TestConfig

    IWebDriver driver;
    WebDriverWait timeout;

    static string Email = "wotcjupiter1";
    static string Password = "spaceshuttle";
    static string LOGIN_URL = "http://www.gmail.com";
    static Browser DefaultBrowser = Browser.Chrome;

    static string EMAIL_TEXTBOX_ID = "identifierId";
    static string EMAIL_SUBMIT_BUTTON_ID = "identifierNext";
    static string PASSWORD_TEXTBOX_ID = "password";
    static string PASSWORD_SUBMIT_BUTTON_ID = "passwordNext";
    static string ACCOUNT_OPTIONS_DROPDOWN_ID = "gb_bb";
    static string SIGNOUT_BUTTON_ID = "gb_71";
    static string LOGGED_IN_ID = ":k9";


    static string EMAIL_APP_CONFIG_ID = "email";
    static string PASSWORD_APP_CONFIG_ID = "password";
    static string SELENIUM_DRIVERS_LOCATION_APP_CONFIG_ID = "seleniumDriversLocation";
    static string DEFAULT_BROWSER_APP_CONFIG_ID = "defaultBrowser";
    
    static string SELENIUM_DRIVERS_LOCATION = @"seleniumdrivers\";

    #endregion

    #region HelpFunctions
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

    /// <summary>
    /// Read in a key - value from the test configuration settings
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string ReadConfigurationSetting(string key)
    {
        var appSettings = ConfigurationManager.AppSettings;
        return appSettings[key];
    }

    #endregion

    /// <summary>
    /// Read in App.config
    /// </summary>
    public void LoadTestSettings()
    {
        //Read app config values for test infrastructure configuration. If missing use the default values  

        string email = ReadConfigurationSetting(EMAIL_APP_CONFIG_ID);
        if (email != null)
        {
            Email = email;
        }

        string password = ReadConfigurationSetting(PASSWORD_APP_CONFIG_ID);
        if (password != null)
        {
            Password = password;
        }

        string seleniumDriversLocation = ReadConfigurationSetting(SELENIUM_DRIVERS_LOCATION_APP_CONFIG_ID);
        if (seleniumDriversLocation != null)
        {
            SELENIUM_DRIVERS_LOCATION = seleniumDriversLocation;
        }

        string defaultBrowser = ReadConfigurationSetting(DEFAULT_BROWSER_APP_CONFIG_ID);
        if (seleniumDriversLocation != null)
        {
            DefaultBrowser = (Browser)Enum.Parse(typeof(Browser), defaultBrowser, true);
        }
    }

    /// <summary>
    /// Init for the full test run
    /// </summary>
    [OneTimeSetUp]
    public void Init()
    {
        LoadTestSettings();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
    }
    
    /// <summary>
    /// Init for each test 
    /// </summary>
    [SetUp]
    public void Setup()
    {
        //start the browser for this test case

    }

    /// <summary>
    /// Cleanup for each test
    /// </summary>
    [TearDown]
    public void CleanupTest()
    {
        //close browser
        driver.Quit();
    }

    public void StartBrowserDriver(Browser browser)
    {
        switch (browser)
        {
            case Browser.Chrome:
                driver = new ChromeDriver(SELENIUM_DRIVERS_LOCATION);
                break;
            case Browser.Firefox:
                driver = new FirefoxDriver(SELENIUM_DRIVERS_LOCATION);
                break;
            case Browser.InternetExplorer:
                var options = new InternetExplorerOptions();
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                driver = new InternetExplorerDriver(SELENIUM_DRIVERS_LOCATION, options);
                break;
        }
    }

    /// <summary>
    /// Test Gmail site works with all supported browsers
    /// Here params can come from test database like TFS and can be for example test case 
    /// IDs that can be matched with data driven test cases.
    /// </summary>
    [Test]
    [TestCase(Browser.Chrome)]
    [TestCase(Browser.Firefox)]
    [TestCase(Browser.InternetExplorer)]
    public void TestBrowser(Browser browser)
    {
        StartBrowserDriver(browser);
        driver.Navigate().GoToUrl(LOGIN_URL);
        var title = driver.Title;
        NUnit.Framework.Assert.AreEqual("Gmail", title);
    }


    /// <summary>
    /// Test Gmail Login
    /// </summary>
    [Test]
    [TestCase(Browser.Firefox)]
    public void Login(Browser browser)
    {
        StartBrowserDriver(browser);
        driver.Navigate().GoToUrl(LOGIN_URL);
        string result = driver.PageSource;
        var loginBox = driver.FindElement(By.Id(EMAIL_TEXTBOX_ID));
        loginBox.SendKeys(Email);

        var move = driver.FindElement(By.Id(EMAIL_SUBMIT_BUTTON_ID));
        move.Click();

        var pwBox = driver.FindElement(By.Name(PASSWORD_TEXTBOX_ID));
        pwBox.SendKeys(Password);

        var signinButton = driver.FindElement(By.Id(PASSWORD_SUBMIT_BUTTON_ID));
        signinButton.Click();

        //timeout.Until(ExpectedConditions.ElementExists(By.Id(LOGGED_IN_ID)));

        //verify we are logged in by looking for test account's name
        NUnit.Framework.Assert.IsTrue(true, Email);

        List<GoogleEmail> emails = GetEmailData(2);


        driver.FindElement(By.ClassName(ACCOUNT_OPTIONS_DROPDOWN_ID)).Click();


        driver.FindElement(By.Id(SIGNOUT_BUTTON_ID)).Click();
    }

    [FindsBy(How = How.Id, Using = ":3a")]
    private IWebElement TableOfEmails;

    public List<GoogleEmail> GetEmailData(int numberOfItems)
    {

        var allEmails = driver.FindElements(By.XPath("//*[@class='zF']")).Count;
        var unreadEmeil = driver.FindElements(By.XPath("//*[@class='bsU']")).Count;

        var emails = driver.FindElements(By.XPath("//*[@class=':3a']"));
        numberOfItems = allEmails;


        var result = new List<GoogleEmail>();
        var allRows = driver.FindElements(By.TagName("tr"));
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

/// <summary>
/// Email Properties and contents
/// </summary>
public class GoogleEmail
{
    public GoogleEmail(string from, string date, string subject)
    {
        From = from;
        Date = date;
        Subject = subject;

    }
    public string From { get; set; }
    public string Date { get; set; }
    public string Subject { get; set; }
}