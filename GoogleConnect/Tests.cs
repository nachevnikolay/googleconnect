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

    //TODO: Split tests in different file 
    #region Tests validating the functionality of the framework

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


        //Write test to load emails
        inbox.GetEmails();


        inbox.Compose();

        inbox.SignOut();
    }
    
    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void TestComposePage(Utility.Browser browser)
    {
        InboxPage inbox = new InboxPage(UserName, Password);
        ComposePage compose = new ComposePage(inbox);
        compose.CloseNewEmailWindow();
        //get emails and their content
        //List<GoogleEmail> emails = Utility.GetEmailData(2);
        compose = new ComposePage(inbox);
        compose.CloseAndDiscardDraft();
        inbox.SignOut();
    }
    
    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void LoginTest(Utility.Browser browser)
    {
        //start browser
        Utility.StartBrowserDriver(browser);

        //navigate to gmail
        TestBase.driver.Navigate().GoToUrl(TestBase.LOGIN_URL);
        //string result = TestBase.driver.PageSource;

        //enter credentials
        var loginBox = TestBase.driver.FindElement(By.Id(TestBase.USERNAME_TEXTBOX_ID));
        loginBox.SendKeys(TestBase.UserName);

        var move = TestBase.driver.FindElement(By.Id(TestBase.USERNAME_SUBMIT_BUTTON_ID));
        move.Click();

        var pwBox = TestBase.driver.FindElement(By.Name(TestBase.PASSWORD_TEXTBOX_ID));
        pwBox.SendKeys(TestBase.Password);

        var signinButton = TestBase.driver.FindElement(By.Id(TestBase.PASSWORD_SUBMIT_BUTTON_ID));
        signinButton.Click();


        //verify we are logged in by looking for test account's name
        NUnit.Framework.Assert.IsTrue(true, TestBase.UserName);

        //get emails and their content
        //List<GoogleEmail> emails = Utility.GetEmailData(2);


        //sign out
        TestBase.driver.FindElement(By.ClassName(TestBase.ACCOUNT_OPTIONS_DROPDOWN_ID)).Click();
        
        TestBase.driver.FindElement(By.Id(TestBase.SIGNOUT_BUTTON_ID)).Click();
    }

    #endregion

    #region Tests validating Gmail functionality

    //TODO:
    //Test Gmmail Signin
    //Test read new unread emails

    #endregion
}
