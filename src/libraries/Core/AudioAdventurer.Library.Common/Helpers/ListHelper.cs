using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ListHelper
    {
        public static ExitDestination GetDestinationFrom(
            this List<ExitDestination> destinations, 
            Guid originId)
        {
            return (from d 
                    in destinations 
                    where originId != d.TargetId
                    select d)
                .FirstOrDefault();
        }
    }
}
