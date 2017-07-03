using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LibraryTest.SmtpServer
{
    // working with smtp server and sends email
    public class EmailService
    {
        public void SendEmail(string bookTitle, string mailToSend)
        {
            SmtpClient smtpMail = new SmtpClient("smtp.gmail.com", 587);

            smtpMail.EnableSsl = true;
            smtpMail.Credentials = new NetworkCredential("librarytakenbook@gmail.com", "Password2017");

            // our mail address
            MailAddress from = new MailAddress("librarytakenbook@gmail.com", "Admin");
            // mail address we want to send
            MailAddress to = new MailAddress(mailToSend);
            // create object of mail
            MailMessage mailMassage = new MailMessage(from, to);
            // Subject of mail
            mailMassage.Subject = "Taken book";
            // text
            mailMassage.Body = "You take the " + bookTitle + " book.";
            smtpMail.Send(mailMassage);
        }
    }
}