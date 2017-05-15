using System;
using System.ServiceModel;
using ConsoleAppK.Data;
using ConsoleAppK.DI.Wcf;
using ConsoleAppK.WCF;
using Microsoft.Practices.Unity;
using Serilog;

namespace ConsoleAppK
{
    public class WcfStartup
    {
        public static void Start(string address, ILogger logger)
        {
            try
            {
                var host = CreateServiceHost(address, logger);
                host.Open();
                logger?.Information("WCF service host opened");
            }
            catch (AddressAccessDeniedException ae)
            {
                logger?.Error(ae, $"WCF Start AddressAccessDeniedException {Environment.NewLine}" +
                                  $"Try to netsh http add urlacl url= user=" +
                                  $"or run under Administrator");
            }
            catch (Exception exception)
            {
                logger?.Error(exception, "WCF Start Exception");
            }
        }

        internal static ServiceHost CreateServiceHost(string address, ILogger log)
        {
            var logger = log ?? new LoggerConfiguration()
                             .MinimumLevel.Debug()
                             .WriteTo.LiterateConsole()
                             .CreateLogger();

            var host = new UnityServiceHost(typeof(UserInfoProvider), new Uri(address));
            host.Container.RegisterInstance<ILogger>(logger);
            host.Container.RegisterType<IUserInfoRepository, UserInfoRepository>(new ContainerControlledLifetimeManager());

            logger.Information("WcfServiceHostCreated");

            return host;
        }
    }
}
