using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Loggers;
using Autofac;

namespace AudioAdventurer.AdventureBuilderServer.Console.Modules
{
    public class LocalModule : Module
    {
        private readonly IConfig _config;

        public LocalModule(IConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ConsoleLogger>()
                .As<ILogger>();
        }
    }
}
