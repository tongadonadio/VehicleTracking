using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using VehicleTracking.DependencyResolver;
using VehicleTracking.Web.Api.App_Start;

namespace VehicleTracking.Web.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            ComponentResolver.LoadContainer(container, ".\\bin", "VehicleTracking.*.dll");
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}