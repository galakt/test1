﻿using System;
using Microsoft.Owin.Hosting;
using System.Runtime.CompilerServices;
using Serilog;

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
            WcfStartup.Start(WcfBaseAddress, new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .CreateLogger());

            Console.ReadLine();
        }
    }
}
