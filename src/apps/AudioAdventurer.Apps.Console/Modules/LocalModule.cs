using AudioAdventurer.Library.Client.Local.Manager;
using AudioAdventurer.Library.Common.Actions.Inform;
using AudioAdventurer.Library.Common.Actions.Travel;
using AudioAdventurer.Library.Common.Factories;
using AudioAdventurer.Library.Common.Handlers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Managers;
using AudioAdventurer.Library.Common.Writers;
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

            builder.RegisterType<CoreBehaviorFactory>()
                .As<IBehaviorResolver>();

            builder.RegisterType<GameManager>()
                .As<IGameManager>();

            builder.RegisterType<ClientManager>()
                .As<IClientManager>();

            builder.RegisterType<CommandManager>()
                .As<ICommandManager>();

            builder.RegisterType<ActionHandler>()
                .As<IActionHandler>();

            builder.RegisterType<Move>()
                .As<IGameAction>();

            builder.RegisterType<Look>()
                .As<IGameAction>();

            builder.RegisterType<ServerOutputWriter>()
                .As<IServerOutputWriter>();
        }
    }
}
