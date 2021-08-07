
using System;
using System.Configuration;
using Autofac;
using GreenPipes;
using MassTransit;
using __NAME__.MassTransit.Consumer;
using System.Collections.Generic;
using System.Reflection;
using __NAME__.Shared.Messages;

namespace __NAME__.Modules
{
    public class BusModule : Autofac.Module
    {
        public string HostAddress { get; set; }
        public string VirtualHost { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        private static BusConfiguration _busConfiguration = (BusConfiguration)ConfigurationManager.GetSection("BusConfiguration");

        Func<Action<IReceiveConfigurator>, IBusControl> BusFactory = receiveConfig => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(_busConfiguration.HostAddress), _busConfiguration.VirtualHost, h =>
            {

                h.Username(_busConfiguration.Username);
                h.Password(_busConfiguration.Password);
            });

            receiveConfig(cfg);
        });

        private readonly System.Reflection.Assembly _assemblyToScan = Assembly.Load("__NAME__.MassTransit.Consumer");
        private readonly List<Type> _consumerTypes;

        public BusModule()
        {
            _consumerTypes = new List<Type>();

            foreach (Type type in _assemblyToScan.GetTypes())
            {
                if (typeof(IConsumer).IsAssignableFrom(type))
                {
                    _consumerTypes.Add(type);
                }
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_busConfiguration.IsEnabled)
            {
                builder.AddMassTransit(x =>
                {
                    foreach (var consumerType in _consumerTypes)
                        x.AddConsumer(consumerType);

                    x.AddBus(context => BusFactory(cfg =>
                    {
                        cfg.ReceiveEndpoint(ThisAssembly.GetName().Name, e =>
                        {
                            foreach (var consumerDef in _consumerTypes)
                                e.ConfigureConsumer(context, consumerDef);
                        });

                    }));
                });
            }
        }
    }
}

