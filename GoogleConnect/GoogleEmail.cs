using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleConnect
{
    /// <summary>
    /// Email Properties and contents
    /// </summary>
    public class GoogleEmail
    {
        public GoogleEmail(string from, string subject, string date, bool unread)
        {
            From = from;
            Subject = subject;
            Date = date;
            Unread = unread;
        }
        public string From { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public bool Unread { get; set; }
    }
}
