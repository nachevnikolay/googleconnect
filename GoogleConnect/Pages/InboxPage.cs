using System.Collections.Generic;
using OpenQA.Selenium;

namespace GoogleConnect
{
    /// <summary>
    /// Inbox page control class encapsulating functionality and content manipulation on the inbox page
    /// </summary>
    public class InboxPage : TestBase
    {
        // XPath IDs eventually extract all static content to separate file
        private static string XPATH_UNREAD_EMAILS = "//*[@class='zA yO']";
        private static string XPATH_READ_EMAILS = "//*[@class='zA zE']";

        private static string XPATH_FROM = "//*[@class='bA4']/span";
        private static string XPATH_SUBJECT = "//*[@class='y6']/span/span";
        private static string XPATH_BODY_PARTIAL = "//*[@class='y2']";
        private static string XPATH_DATE = "//*[@class='xW xY ']/span";

        private static string XPATH_ATTRIBUTE_TITLE = "title";
        private static string XPATH_ATTRIBUTE_EMAIL = "email";
        private static string XPATH_ATTRIBUTE_NAME = "name";
        private readonly IWebDriver _driver;

        #region Constructors

        /// <summary>
        /// Get a successfull login context and pass it to the InboxPage constructor
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        public InboxPage(string UserName, string Password)
        {
            _driver = Utility.Login(UserName, Password);
        }

        #endregion

        #region Actions

        IWebElement drpdownAccountBubble => _driver.FindElement(By.XPath(ACCOUNT_OPTIONS_DROPDOWN_ID));
        IWebElement btnSignOut => _driver.FindElement(By.Id(SIGNOUT_BUTTON_ID));
        IWebElement btnCompose => _driver.FindElement(By.XPath(COMPOSE_NEW_EMAIL_BUTTON_ID));

        /// <summary>
        /// Open new email popup window
        /// </summary>
        /// <returns></returns>
        public IWebDriver Compose()
        {
            Utility.WaitUntilElementClickable(By.XPath(COMPOSE_NEW_EMAIL_BUTTON_ID));
            btnCompose.Click();
            return _driver;
        }

        /// <summary>
        /// Sign out of Gmail
        /// </summary>
        public void SignOut()
        {
            drpdownAccountBubble.Click();

            btnSignOut.Click();
        }

        /// <summary>
        /// Get list of latest emails
        /// Can specify number of emails and type (Read, Unread, All)
        /// </summary>
        /// <param name="numberOfEmails">How many of the latest emails to fetch. 
        /// Will return all available emails from the front page if this is larger</param>
        /// <param name="emailState">Fetch Read, Unread or All type of emails</param>
        /// <returns></returns>
        public List<GoogleEmail> GetEmails(int numberOfEmails, Utility.EmailState emailState)
        {
            var result = new List<GoogleEmail>();

            List<IWebElement> emailsToProcess = new List<IWebElement>();

            //Waiting on content being fuly loaded and visible
            //TODO: This needs to be abstracted away as now I peppered it across the code as needed with the limited amount of time 
            // test results history to know what elements are randomly failing 
            Utility.WaitUntilElementClickable(By.XPath(XPATH_UNREAD_EMAILS));
            Utility.WaitUntilElementClickable(By.XPath(XPATH_READ_EMAILS));

            switch (emailState)
            {
                case Utility.EmailState.Read:
                    emailsToProcess.AddRange(driver.FindElements(By.XPath(XPATH_READ_EMAILS)));
                    break;
                case Utility.EmailState.Unread:
                    emailsToProcess.AddRange(driver.FindElements(By.XPath(XPATH_UNREAD_EMAILS)));
                    break;
                case Utility.EmailState.All:
                    emailsToProcess.AddRange(driver.FindElements(By.XPath(XPATH_UNREAD_EMAILS)));
                    emailsToProcess.AddRange(driver.FindElements(By.XPath(XPATH_READ_EMAILS)));
                    break;
            }

            string from, subject, date, email, dateFull, bodyPartial;

            //Process either the requested number of emails or all available if less than what requested
            for (int i = 0; i < (emailsToProcess.Count>numberOfEmails ? numberOfEmails : emailsToProcess.Count); i++)
            {
                //TODO: 
                //That parsing work can be done through serialization by making the GoogleEmail class serializable. 
                // Then all we need to do is passing in the web element for each email to the GoogleEmail constructor.
                
                var fromElement = emailsToProcess[i].FindElement(By.XPath(XPATH_FROM));
                email = fromElement.GetAttribute(XPATH_ATTRIBUTE_EMAIL);
                from = fromElement.GetAttribute(XPATH_ATTRIBUTE_NAME);

                var subjectElement = emailsToProcess[i].FindElement(By.XPath(XPATH_SUBJECT));
                subject = subjectElement.Text;

                var bodyElement = emailsToProcess[i].FindElement(By.XPath(XPATH_BODY_PARTIAL));
                bodyPartial = bodyElement.Text;

                var dateElement = emailsToProcess[i].FindElement(By.XPath(XPATH_DATE));
                date = dateElement.Text;
                dateFull = dateElement.GetAttribute(XPATH_ATTRIBUTE_TITLE);
              
                // Build email with user visible information from list
                var message = new GoogleEmail(from, subject, date, true);

                // Build email from full email content available from inbox page if we need extended verification
                var messageExpandedProperties = new GoogleEmail(from, subject, date, true, dateFull, email, bodyPartial);

                result.Add(message);
            }
            return result;
        }

        //TODO: Write email list related actions
        //Get count of unread emails
        //Get count of total emails

        //TODO: write single email related actions
        //MarkAsRead()
        //MarkAsUnread()
        //Delete()
        //OpenMail()

        #endregion
        
        //Validation
        //TODO: write helper Assert/Validate functions to be used in Tests
    }
}

