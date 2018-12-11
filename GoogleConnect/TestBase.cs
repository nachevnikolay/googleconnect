using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Configuration;
using OpenQA.Selenium.Support.UI;

namespace GoogleConnect
{
     public class TestBase
    {
        public static IWebDriver driver;
        public static WebDriverWait timeout;

        public static string UserName = "wotcjupiter1";
        public static string Password = "spaceshuttle";
        public static string LOGIN_URL = "http://www.gmail.com";
        public static Utility.Browser DefaultBrowser = Utility.Browser.Chrome;

        public static string USERNAME_TEXTBOX_ID = "identifierId";
        public static string USERNAME_SUBMIT_BUTTON_ID = "identifierNext";
        public static string PASSWORD_TEXTBOX_ID = "password";
        public static string PASSWORD_SUBMIT_BUTTON_ID = "passwordNext";

        public static string COMPOSE_NEW_EMAIL_BUTTON_ID = "//div[contains(text(),'Compose')]";
        public static string CLOSE_NEW_EMAIL_WINDOW_BUTTON_ID = "Ha";
        public static string ACCOUNT_OPTIONS_DROPDOWN_ID = "//*[@class='gb_b gb_hb gb_R']";
        public static string SIGNOUT_BUTTON_ID = "gb_71";
        public static string LOGGED_IN_ID = ":k9";

        public static string EMAIL_APP_CONFIG_ID = "email";
        public static string PASSWORD_APP_CONFIG_ID = "password";
        public static string SELENIUM_DRIVERS_LOCATION_APP_CONFIG_ID = "seleniumDriversLocation";
        public static string DEFAULT_BROWSER_APP_CONFIG_ID = "defaultBrowser";

        public static string SELENIUM_DRIVERS_LOCATION = @"seleniumdrivers\";


        /// <summary>
        /// Read in configuration settings from App.config
        /// </summary>
        public void LoadTestSettings()
        {
            //Read app config values for test infrastructure configuration. If missing use the default values provided here 
            string email = ReadConfigurationSetting(EMAIL_APP_CONFIG_ID);
            if (email != null)
            {
                UserName = email;
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
                DefaultBrowser = (Utility.Browser)Enum.Parse(typeof(Utility.Browser), defaultBrowser, true);
            }
        }

        /// <summary>
        /// Read in a key - value from the test configuration settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadConfigurationSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[key];
        }

        /// <summary>
        /// Init for the full test run
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            LoadTestSettings();
        }

        /// <summary>
        /// Clean up for the full test run
        /// </summary>
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
    }
}
