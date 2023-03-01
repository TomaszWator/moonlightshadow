using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Mime;
using System.Threading;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MoonlightShadow.Models;

namespace WebApi.Services
{
    public interface IMailSenderService
    {
        void Send(string from, string to, string subject, string html);
    }

    public class MailSenderService : IMailSenderService
    {
        private readonly MailSettings _mailSettings;

        public MailSenderService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void Send(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            System.Console.WriteLine(email.Body);

            using (var client = new SmtpClient())
            {
                client.Connect(_mailSettings.Host_Address, _mailSettings.Host_Port, SecureSocketOptions.StartTls);
                client.Authenticate(_mailSettings.Host_UserName, _mailSettings.Host_Password);
                client.Send(email);
                client.Disconnect(true);
            }
        }

        public void SendRegistrationMail(string toEmail, string login, string token)
        {
            var link = "https://localhost:5001/SignUp/VerifyEmail?email=" + toEmail + "&" + "token=" + token;
            Send("MoonlightShadow@MoonlightShadow.com",
                toEmail,
                "Rejestracja w serwisie MoonlightShadow",
                "Witaj " + login + ", <br>dziękujemy za rejestrację. Użyj poniższego linku do zweryfikowania adresu email: <a href=" + link + ">" + link + "</a><br><br>Pozdrawiamy, MoonlighShadow");
        }

        public void SendNewPassword(string toEmail, string login, string password, string token)
        {
            var link = "https://localhost:5001/Login/AcceptRemindMePassword/?email=" + toEmail + "&" + "token=" + token;
            
            Send("MoonlightShadow@MoonlightShadow.com",
                toEmail,
                "Resetowanie hasła w serwisie MoonlightShadow",
                "Witaj " + login + ", <br>Nowe hasło: " + password + "<br>Po użyciu linku <a href=" + link + ">" + link + "</a> możesz zalogować się na swoje konto<br>Jeżeli nie wysyłałeś zgłoszenia o przypomnienie hasła, nie klikaj w link!!! <br><br> Pozdrawiamy, MoonlighShadow");
        }


        public void SendWaitingForAcceptPaymentMail(string toEmail)
        {
            Send("MoonlightShadow@MoonlightShadow.com",
                toEmail,
                "Zakup oczekuje na weryfikacje płatności",
                "Witaj drogi kliencie, <br>dziękujemy za potwierdzenie zamówienia. Po zaksięgowaniu środków na naszym koncie poinformujemy Cię o dalszym etapie wysyłki");
        }

        public void SendPaymentAcceptedMail(string toEmail, string TitleTransaction)
        {
            Send("MoonlightShadow@MoonlightShadow.com",
                toEmail,
                "Płatność zaksięgowana dla transakcji: " + TitleTransaction,
                "Witaj drogi kliencie, <br>dziękujemy za dokonanie płatności. Realizacja zamówienia nastąpi w ciągu najbliższych 24 godzin");
        }
    }
}