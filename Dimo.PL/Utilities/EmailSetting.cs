using System.Net;
using System.Net.Mail;

namespace Dimo.PL.Utilities
{
    public  static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("routemaha@gmail.com","ilpmmfybmqtcdvxd");
            client.Send("routemaha@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
