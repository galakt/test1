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
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;

[assembly: InternalsVisibleTo("ConsoleAppK.Tests")]

namespace ConsoleAppK
{
    internal class Program
    {
        internal static string WebApiBaseAddress = "http://localhost:9100/";

        internal static string WcfBaseAddress = "http://localhost:9101/";

        static void Main(string[] args)
        {
            // Start OWIN host 
            WebApp.Start<Startup>(url: WebApiBaseAddress);

            // Start WCF
            WcfStartup.Start(WcfBaseAddress);

            Console.WriteLine("Started");
            Console.ReadLine();
        }
    }
}
