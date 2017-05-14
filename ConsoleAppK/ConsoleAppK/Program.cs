using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using ConsoleAppK.Data;
using ConsoleAppK.DI.Wcf;
using ConsoleAppK.WCF;
using Microsoft.Practices.Unity;

[assembly: InternalsVisibleTo("ConsoleAppK.Tests")]

namespace ConsoleAppK
{
    internal class Program
    {
        internal static string BaseAddress = "http://localhost:9000/";

        static void Main(string[] args)
        {
            // Start OWIN host 
            WebApp.Start<Startup>(url: BaseAddress);

            // Start WCF
            try
            {
                var host = CreateServiceHost();
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

            Console.WriteLine("Started");
            Console.ReadLine();
        }

        internal static ServiceHost CreateServiceHost()
        {
            var host = new UnityServiceHost(typeof(UserInfoProvider), new Uri(BaseAddress));
            host.Container.RegisterType<IUserInfoRepository, UserInfoRepository>(new ContainerControlledLifetimeManager());

            return host;
        }
    }
}
