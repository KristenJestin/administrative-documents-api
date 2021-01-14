using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
			IHost host = CreateHostBuilder(args).Build();
			await host.SeedDataAsync();
			host.Run();
		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
