using OpenQA.Selenium;

namespace GoogleConnect
{
    /// <summary>
    /// Compose page control class encapsulating functionality of the popup compose mail window 
    /// </summary>
    class ComposePage : TestBase
    {
        private readonly IWebDriver _driver;

        #region Constructors

        /// <summary>
        /// Get a Compose page context from inbox page
        /// </summary>
        /// <param name="inboxPage"></param>
        public ComposePage(InboxPage inboxPage) => _driver = inboxPage.Compose();

        /// <summary>
        /// Log in, get a successfull inbox context and pass it to the Compose page constructor
        /// </summary>
        /// <param name="UserName">username</param>
        /// <param name="Password">password</param>
        public ComposePage(string UserName, string Password)
        {
            InboxPage inbox = new InboxPage(UserName, Password);
            _driver = inbox.Compose();
        }

        #endregion

        #region Actions

        IWebElement btnCloseNewEmailWindow => _driver.FindElement(By.ClassName("Ha"));
        IWebElement btnCloseAndDiscardDraft => _driver.FindElement(By.ClassName("oh"));


        /// <summary>
        /// Close the compose new email popup window. Draft is saved if there are any changes made to the form
        /// </summary>
        public void CloseNewEmailWindow()
        {
            Utility.WaitUntilElementClickable(By.ClassName("Ha"));

            btnCloseNewEmailWindow.Click();
        }

        /// <summary>
        /// Close the compose new email popup window. Draft is discarded
        /// </summary>
        public void CloseAndDiscardDraft()
        {
            Utility.WaitUntilElementClickable(By.ClassName("oh"));

            btnCloseAndDiscardDraft.Click();
        }

        //TODO: write actions
        //SendMail()

        #endregion

        //TODO: write any verification functions to be used by tests 
    }
}
