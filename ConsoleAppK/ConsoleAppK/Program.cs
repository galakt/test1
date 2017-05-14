using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Runtime.CompilerServices;

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
            
            Console.WriteLine("Started");
            Console.ReadLine();
        }
    }
}
