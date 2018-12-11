using OpenQA.Selenium;

namespace GoogleConnect
{
    /// <summary>
    /// Login page control class encapsulating functionality of loging in to Gmail 
    /// </summary>
    public class LoginPage : TestBase
    {   
        private readonly IWebDriver _driver;

        #region Constructors

        public LoginPage(IWebDriver driver) => _driver = driver;
        public LoginPage(Utility.Browser browser) => _driver = Utility.StartBrowserDriver(browser);

        #endregion

        #region Actions

        //page element ids
        IWebElement txtUserName => _driver.FindElement(By.Id(USERNAME_TEXTBOX_ID));
        IWebElement btnUserName => _driver.FindElement(By.Id(USERNAME_SUBMIT_BUTTON_ID));
        IWebElement txtPassword => _driver.FindElement(By.Name(PASSWORD_TEXTBOX_ID));
        IWebElement btnLogin => _driver.FindElement(By.Id(PASSWORD_SUBMIT_BUTTON_ID));

        /// <summary>
        /// Open Gmail.com
        /// </summary>
        public void Open()
        {
            _driver.Navigate().GoToUrl(TestBase.LOGIN_URL);
        }

        /// <summary>
        /// Type username
        /// </summary>
        /// <param name="userName"></param>
        public void EnterUserName(string userName)
        {
            txtUserName.SendKeys(userName);
        }

        /// <summary>
        /// Click on Next after typing username
        /// </summary>
        public void ClickNext()
        {
            Utility.WaitUntilElementClickable(By.Id(USERNAME_SUBMIT_BUTTON_ID));
            btnUserName.Click();
        }

        /// <summary>
        /// Type in password
        /// </summary>
        /// <param name="password">password</param>
        public void EnterPassword(string password)
        {
            Utility.WaitUntilElementClickable(By.Id(PASSWORD_TEXTBOX_ID));
            txtPassword.SendKeys(password);
        }

        /// <summary>
        /// Click on Login
        /// </summary>
        public void ClickLogin()
        {
            btnLogin.Click();
        }

        #endregion

        //Validation
        //TODO: write Assert/Validate functions to be used in tests
    }
}
