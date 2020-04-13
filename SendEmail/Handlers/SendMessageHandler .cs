using System;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSender.Messages;
using Rebus.Handlers;

namespace EmailSender.Handlers
{
    class SendEmailHandler : IHandleMessages<SendEmail>
    {
        readonly SmtpClient _smtpClient;

        public SendEmailHandler(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        //обрабатывает сообщения
        public async Task Handle(SendEmail message)
        {

            Console.WriteLine("Message Recipient {0}, subject {1}, body {2}", message.Recipient, message.Subject, message.Body);
        }
    }
}