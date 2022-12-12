using AudioAdventurer.GameExtensions.SampleAdventure.Behaviors;
using AudioAdventurer.GameExtensions.SampleAdventure.Factories;
using AudioAdventurer.Library.Common.Interfaces;
using Autofac;

namespace AudioAdventurer.GameExtensions.SampleAdventure.Modules
{
    public class SampleAdventureModule : Module
    {
        private readonly IConfig _config;

        public SampleAdventureModule(IConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SampleAdventureBehaviorResolver>()
                .As<IBehaviorFactory>();
        }
    }
}
