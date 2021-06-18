// using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Azure.Functions.Worker.Configuration;

// namespace VANBANDIENTU
// {
//     public class Program
//     {
//         public static void Main()
//         {
//             var host = new HostBuilder()
//                 .ConfigureFunctionsWorkerDefaults()
//                 .Build();

//             host.Run();
//         }
//     }
// }
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace QUANLIVANBAN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                   // webBuilder.UseUrls("http://localhost:5000", "https://localhost:5001");
                });
    }
}