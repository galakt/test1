using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using ConsoleAppK.Data;
using Microsoft.Practices.Unity;
using Owin;

namespace ConsoleAppK
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            var container = new UnityContainer();
            container.RegisterType<IProfileRepository, ProfileRepository>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();
            
            appBuilder.UseWebApi(config);
        }

        //public void ConfigurateServices(IServiceProvider serviceProvider)
        //{

        //}
    }
}
