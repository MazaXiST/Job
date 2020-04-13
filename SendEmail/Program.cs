using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using EmailSender.Messages;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new WindsorContainer())
            {
                container.Install(FromAssembly.This());

                var bus = Configure.With(new BuiltinHandlerActivator())
       .Transport(t => t.UseMsmqAsOneWayClient())
       .Routing(r => r.TypeBased().MapAssemblyOf<SendEmail>("emailsender"))
       .Start();

                using (bus)
                {
                    while (true)
                    {
                        var recipient = ReadLine("recipient");

                        if (string.IsNullOrEmpty(recipient))
                        {
                            break;
                        }

                        var subject = ReadLine("subject");
                        var body = ReadLine("body");

                        bus.Send(new SendEmail(recipient, subject, body)).Wait();
                    }
                }

                Console.ReadKey();
            }
        }

        static string ReadLine(string what)
        {
            Console.Write($"Please enter {what} > ");
            var text = Console.ReadLine();
            return text;
        }

    }
}
