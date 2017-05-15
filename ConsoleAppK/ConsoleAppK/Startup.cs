using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ConsoleAppK.Data;
using Microsoft.Practices.Unity;
using Owin;
using ConsoleAppK.DI.WebApi;
using ConsoleAppK.Logging;
using Serilog;

namespace ConsoleAppK
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = CreateConfig();

            appBuilder.UseWebApi(config);
        }

        internal static HttpConfiguration CreateConfig()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            HttpConfiguration config = new HttpConfiguration();

            var container = new UnityContainer();
            container.RegisterInstance<ILogger>(logger);
            container.RegisterType<IExceptionHandler, ApiExceptionHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProfileRepository, ProfileRepository>(new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new ApiLogHandler(logger));
            
            logger.Information("Api config created");

            return config;
        }
    }
}
