namespace GoogleConnect
{
    /// <summary>
    /// Object for gmail email properties and contents
    /// </summary>
    public class GoogleEmail
    {
        /// <summary>
        /// Constructor for building email object from visible email properties while on inbox page
        /// </summary>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="date"></param>
        /// <param name="unread"></param>
        public GoogleEmail(string from, string subject, string date, bool unread)
        {
            From = from;
            Subject = subject;
            Date = date;
            Unread = unread;
        }

        /// <summary>
        /// Constructor for building email object from email properties taken from page sources
        /// </summary>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="date"></param>
        /// <param name="unread"></param>
        /// <param name="dateTime"></param>
        /// <param name="email"></param>
        /// <param name="partialBody"></param>
        //extended properties constructor
        public GoogleEmail(string from, string subject, string date, bool unread, string dateTime, string email, string partialBody)
        {
            From = from;
            Subject = subject;
            Date = date;
            Unread = unread;
            Email = email;
            DateTime = dateTime;
            PartialBody = partialBody;
        }

        public string From { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public bool Unread { get; set; }
        public string Email { get; set; }
        public string DateTime { get; set; }
        public string PartialBody { get; set; }

        /// TODO: serialize this for cleaner and simpler way to manipulate email objects
    }
}
