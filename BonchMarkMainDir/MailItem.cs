using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonchMark
{
    public class MailItem
    {
        public string Date { get; }
        public string Subject { get; }
        public string Files { get; }
        public string Sender { get; }
        public string? Message { get; }

        public MailItem(string date, string subject, string files, string destination, string? message = null)
        {
            Date = date;
            Subject = subject;
            Files = files;
            Sender = destination;
            Message = message;
        }
    }
}
