using AudioAdventurer.Library.Client.Local.Manager;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Managers;
using AudioAdventurer.Library.Common.Resolvers;
using AudioAdventurer.Library.Testing.Services;
using Autofac;

namespace AudioAdventurer.Apps.Console.Modules
{
    internal class LocalModule : Module
    {
        private readonly IConfig _config;

        public LocalModule(IConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<InMemoryThingService>()
                .As<IThingService>()
                .SingleInstance();

            builder.RegisterType<CoreBehaviorResolver>()
                .As<IBehaviorResolver>();

            builder.RegisterType<GameManager>()
                .As<IGameManager>();

            builder.RegisterType<ClientManager>()
                .As<IClientManager>();

            builder.RegisterType<CommandManager>()
                .As<ICommandManager>();
        }
    }
}
