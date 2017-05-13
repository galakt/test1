using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ConsoleAppK
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            WebApp.Start<Startup>(url: baseAddress);

            HttpClient client = new HttpClient();
            var response = client.GetAsync(baseAddress + "import.json").Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadLine();
        }
    }
}
