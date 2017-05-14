using System;
using System.ServiceModel;
using ConsoleAppK.Data;
using ConsoleAppK.DI.Wcf;
using ConsoleAppK.WCF;
using Microsoft.Practices.Unity;

namespace ConsoleAppK
{
    public class WcfStartup
    {
        public static void Start(string address)
        {
            try
            {
                var host = CreateServiceHost(address);
                host.Open();
            }
            catch (AddressAccessDeniedException ae)
            {
                Console.WriteLine(ae);
                Console.WriteLine($"Try to netsh http add urlacl url= user=" +
                                  $"or run under Administrator");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        internal static ServiceHost CreateServiceHost(string address)
        {
            var host = new UnityServiceHost(typeof(UserInfoProvider), new Uri(address));
            host.Container.RegisterType<IUserInfoRepository, UserInfoRepository>(new ContainerControlledLifetimeManager());

            return host;
        }
    }
}
