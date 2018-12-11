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
        public GoogleEmail(string from, string date, string subject)
        {
            From = from;
            Date = date;
            Subject = subject;
        }
        public string From { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
    }
}
