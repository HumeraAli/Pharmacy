using System;
using System.Net.Mail;

namespace Services
{
    internal class EmailService
    {
        internal static bool Send(string ownerEmail, string alertMessage)
        {
            //new SmtpClient().Send(ownerEmail, ownerEmail, alertMessage, alertMessage);
            return true;
        }
    }
}