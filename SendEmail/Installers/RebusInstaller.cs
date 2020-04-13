﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EmailSender.Handlers;
using Rebus.CastleWindsor;
using Rebus.Config;

namespace EmailSender.Installers
{
    public class RebusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //регистрирует обработчик для полученного сообщения
            container.RegisterHandler<SendEmailHandler>();

            //устанавливает тип контейнера с которым будем работать
            //и тип сообщений которые следует получать получаемых сообщений
            Configure.With(new CastleWindsorContainerAdapter(container))
                .Transport(t => t.UseMsmq("emailsender"))
                .Start();
        }
    }
}