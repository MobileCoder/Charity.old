using System;
using System.Net;
using System.Net.Mail;

namespace Utility
{
    public class Email
    {
        private Email()
        {
        }

        private static string host;
        private static int port;
        private static MailAddress fromAddress;
        private static string fromPassword;

        public static void Initialize(string _host, int _port, string _email, string _displayName, string _fromPassword)
        {
            host = _host;
            port = _port;
            fromAddress = new MailAddress(_email, _displayName);
            fromPassword = _fromPassword;
        }

        public static void Send(string email, string displayName, string subject, string body)
        {
            MailAddress toAddress = new MailAddress(email, displayName);
            SmtpClient smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}