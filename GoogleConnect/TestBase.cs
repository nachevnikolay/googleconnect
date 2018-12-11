﻿using OpenQA.Selenium;
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
     public class TestBase
    {
        #region TestConfig

        public static IWebDriver driver;
        public static WebDriverWait timeout;

        public static string UserName = "wotcjupiter1";
        public static string Password = "spaceshuttle";
        public static string LOGIN_URL = "http://www.gmail.com";
        public static Utility.Browser DefaultBrowser = Utility.Browser.Chrome;

        public static string EMAIL_TEXTBOX_ID = "identifierId";
        public static string EMAIL_SUBMIT_BUTTON_ID = "identifierNext";
        public static string PASSWORD_TEXTBOX_ID = "password";
        public static string PASSWORD_SUBMIT_BUTTON_ID = "passwordNext";
        public static string ACCOUNT_OPTIONS_DROPDOWN_ID = "gb_bb";
        public static string SIGNOUT_BUTTON_ID = "gb_71";
        public static string LOGGED_IN_ID = ":k9";

        public static string EMAIL_APP_CONFIG_ID = "email";
        public static string PASSWORD_APP_CONFIG_ID = "password";
        public static string SELENIUM_DRIVERS_LOCATION_APP_CONFIG_ID = "seleniumDriversLocation";
        public static string DEFAULT_BROWSER_APP_CONFIG_ID = "defaultBrowser";

        public static string SELENIUM_DRIVERS_LOCATION = @"seleniumdrivers\";

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
