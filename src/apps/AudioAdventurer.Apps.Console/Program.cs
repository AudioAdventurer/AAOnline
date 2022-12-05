using System.Threading;
using AudioAdventurer.Apps.Console.Modules;
using AudioAdventurer.Library.Adventure.Builders;
using AudioAdventurer.Library.Client.Local.Manager;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Sessions;
using Autofac;


namespace AudioAdventurer.Apps.Console
{
    public class Program
    {
        private static IContainer _container;

        public static void Main(string[] args)
        {
            var config = ConfigHelper.LoadFromEnvironment("AA_StandAlone");

            var builder = new ContainerBuilder();
            builder.RegisterModule(new LocalModule(config));
            _container = builder.Build();

            // Get the IThingService
            var thingService = _container.Resolve<IThingService>();

            // Build the Sample Adventure.
            var startLocation = SampleAdventureBuilder.BuildAdventure(thingService);

            var player = thingService.BuildPlayer(
                "Tester", 
                out PlayerBehavior playerBehavior);

            player.AddParent(startLocation);

            // Get the Game Manager and start it
            var gameManager = _container.Resolve<IGameManager>();
            gameManager.Start();

            ISession session = new Session(player);
            var clientManager = new ClientManager(session);
            clientManager.Start();

            gameManager.AddSession(session);

            do
            {
                Thread.Sleep(250);
            } while (gameManager.Running);
        }
    }
}
