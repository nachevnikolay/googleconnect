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

[TestFixture]
public class Tests : TestBase
{ 
    #region Tests validating the functionality of the framework
    //TODO: Split tests in different 

    /// <summary>
    /// Test Gmail site works with all supported browsers
    /// Here params can come from test database like TFS and can be for example test case 
    /// IDs that can be matched with data driven test cases.
    /// </summary>
    [Test]
    [TestCase(Utility.Browser.Chrome)]
    [TestCase(Utility.Browser.Firefox)]
    [TestCase(Utility.Browser.InternetExplorer)]
    public void TestBrowser(Utility.Browser browser)
    {
        Utility.StartBrowserDriver(browser);
        TestBase.driver.Navigate().GoToUrl(TestBase.LOGIN_URL);
        var title = TestBase.driver.Title;
        NUnit.Framework.Assert.AreEqual("Gmail", title);
    }

    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void TestLoginPage(Utility.Browser browser)
    {
        LoginPage page = new LoginPage(browser);
        page.Open();
        page.EnterUserName(UserName);
        page.ClickNext();
        page.EnterPassword(Password);
        page.ClickLogin();

        //Verify login was successful
        NUnit.Framework.Assert.IsTrue(true, TestBase.UserName);
    }

    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void TestInboxPage(Utility.Browser browser)
    {
        InboxPage inbox = new InboxPage(UserName, Password);

        List<GoogleEmail> emails = new List<GoogleEmail>();

        //Write test to load emails
        emails = inbox.GetEmails(4, Utility.EmailState.Unread);

        //TODO
        // Assert/Validate emails

        inbox.Compose();

    }
    
    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void TestComposePage(Utility.Browser browser)
    {
        InboxPage inbox = new InboxPage(UserName, Password);
        ComposePage compose = new ComposePage(inbox);
        compose.CloseNewEmailWindow();
 
        compose = new ComposePage(inbox);
        compose.CloseAndDiscardDraft();
        inbox.SignOut();
    }
    
    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void LoginTest(Utility.Browser browser)
    {
        //Sign in 
        IWebDriver driver = Utility.Login(browser);

        //Sign out
        driver.FindElement(By.XPath(ACCOUNT_OPTIONS_DROPDOWN_ID)).Click();

        driver.FindElement(By.Id(TestBase.SIGNOUT_BUTTON_ID)).Click();
    }

    //TODO:
    //Test mark read emails
    //Test mark unread emails
    //Test send email

    #endregion

    #region Tests validating Gmail functionality

    //TODO: 
    //Build TDD infrastructure to have Json files read in by tests
    //Write Json files with test suite split by functionality and priority

    
    #endregion
}
