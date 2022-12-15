using AudioAdventurer.Library.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AudioAdventurer.AdventureBuilderServer.Console.Controllers
{
    [Produces("application/json")]
    public class ThingController : Controller
    {
        private readonly ILogger _logger;

        public ThingController(
            ILogger logger)
        {
            _logger = logger;
        }


    }
}
