using Autofac;
using __NAME__.CommandProcessor.Command;
using __NAME__.CommandProcessor.Dispatcher;
using __NAME__.Domain.Query;
using __NAME__.MassTransit.Client;
using __NAME__.Mongo.DatabaseFactory;
using __NAME__.ServiceBus;
using System.Reflection;

namespace __NAME__
{
    public class DefaultModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerLifetimeScope();
            builder.RegisterType<DbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<MassTransitBus>().As<ITransitBus>().InstancePerLifetimeScope();
            var serviceDomainCommand = Assembly.Load("__NAME__.Domain.Handler");
            builder.RegisterAssemblyTypes(serviceDomainCommand)
            .AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(serviceDomainCommand)
            .AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            var serviceDomainQuery = Assembly.Load("__NAME__.Domain.Query");
            builder.RegisterAssemblyTypes(serviceDomainQuery).AssignableTo<IQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();

            var massTransitClient = Assembly.Load("__NAME__.MassTransit.Client");
            builder.RegisterAssemblyTypes(massTransitClient)
              .AsClosedTypesOf(typeof(IRequestClient<,>)).InstancePerLifetimeScope();

        }
    }
}
