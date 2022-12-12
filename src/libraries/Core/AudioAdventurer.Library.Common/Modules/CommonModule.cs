using AudioAdventurer.Library.Common.Actions.Communicate;
using AudioAdventurer.Library.Common.Actions.Inform;
using AudioAdventurer.Library.Common.Actions.Travel;
using AudioAdventurer.Library.Common.Interfaces;
using Autofac;

namespace AudioAdventurer.Library.Common.Modules
{
    public class CommonModule : Module
    {
        private readonly IConfig _config;

        public CommonModule(IConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Help>()
                .As<IGameAction>();

            builder.RegisterType<Look>()
                .As<IGameAction>();

            builder.RegisterType<Move>()
                .As<IGameAction>();

            builder.RegisterType<Say>()
                .As<IGameAction>();
        }
    }
}
