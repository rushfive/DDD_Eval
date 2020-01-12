using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Title = "[DDD] WebApp";

			Log.Logger = CreateSerilogLogger();

			var configuration = BuildConfiguration(args);

			ConfigureWebHost(configuration).Build().Run();
		}

		private static IConfiguration BuildConfiguration(string[] args)
		   => new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.AddEnvironmentVariables()
				.Build();

		private static IWebHostBuilder ConfigureWebHost(
			IConfiguration configuration)
			=> new WebHostBuilder()
				.UseSerilog()
				.UseStartup<Startup>()
				.UseConfiguration(configuration)
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseKestrel();

		private static Serilog.ILogger CreateSerilogLogger() =>
			new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateLogger();
	}
}
