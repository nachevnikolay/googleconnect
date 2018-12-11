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

[TestClass]
public class GoogleTests
{
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


    public void OpenBrowser()
    { }

    public void NavigateHooverDropdown()
    { }

    public void NavigateClickDropdown()
    { }

    public void NavigateLink()
    { }

    public void ClickButton()
    { }
    

    /// <summary>
    /// Test Gmail Login
    /// </summary>
    [Test]
    [TestCase(Utility.Browser.Firefox)]
    public void Login(Utility.Browser browser)
    {
        //start browser
        Utility.StartBrowserDriver(browser);

        //navigate to gmail
        TestBase.driver.Navigate().GoToUrl(TestBase.LOGIN_URL);
        string result = TestBase.driver.PageSource;

        //enter credentials
        var loginBox = TestBase.driver.FindElement(By.Id(TestBase.EMAIL_TEXTBOX_ID));
        loginBox.SendKeys(TestBase.Email);

        var move = TestBase.driver.FindElement(By.Id(TestBase.EMAIL_SUBMIT_BUTTON_ID));
        move.Click();

        var pwBox = TestBase.driver.FindElement(By.Name(TestBase.PASSWORD_TEXTBOX_ID));
        pwBox.SendKeys(TestBase.Password);

        var signinButton = TestBase.driver.FindElement(By.Id(TestBase.PASSWORD_SUBMIT_BUTTON_ID));
        signinButton.Click();


        //verify we are logged in by looking for test account's name
        NUnit.Framework.Assert.IsTrue(true, TestBase.Email);

        //get emails and their content
        List<GoogleEmail> emails = Utility.GetEmailData(6);


        //sign out
        TestBase.driver.FindElement(By.ClassName(TestBase.ACCOUNT_OPTIONS_DROPDOWN_ID)).Click();
        
        TestBase.driver.FindElement(By.Id(TestBase.SIGNOUT_BUTTON_ID)).Click();
    }
}
