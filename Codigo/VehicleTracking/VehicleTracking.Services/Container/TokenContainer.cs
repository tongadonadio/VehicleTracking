using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Services.Container
{
    public class TokenContainer
    {

        private static TokenContainer container;

        public Dictionary<Guid, UserDTO> LoggedUsers { get; }

        private TokenContainer()
        {
            this.LoggedUsers = new Dictionary<Guid, UserDTO>();
        }

        public static TokenContainer GetContext()
        {
            if(container == null)
            {
                container = new TokenContainer();
            }
            return container;
        }

    }
}
