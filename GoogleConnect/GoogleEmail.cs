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
        //visible properties constructor
        public GoogleEmail(string from, string subject, string date, bool unread)
        {
            From = from;
            Subject = subject;
            Date = date;
            Unread = unread;
        }

        //extended properties constructor
        public GoogleEmail(string from, string subject, string date, bool unread, string dateTime, string email, string partialBody)
        {
            From = from;
            Subject = subject;
            Date = date;
            Unread = unread;
            Email = email;
            DateTime = dateTime;
            partialBody = partialBody;
        }

        public string From { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public bool Unread { get; set; }
        public string Email { get; set; }
        public string DateTime { get; set; }
        public string PartialBody { get; set; }
    }
}
