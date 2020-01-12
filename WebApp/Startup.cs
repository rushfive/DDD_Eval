using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campaigns.Domain;
using Campaigns.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using WebApp.Api;

namespace WebApp
{
	public class Startup
	{
		private IConfiguration Configuration { get; }
		private IWebHostEnvironment Environment { get; }

		public Startup(IWebHostEnvironment environment, IConfiguration configuration)
		{
			Environment = environment;
			Configuration = configuration;
		}

		

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(c =>
			{
				c.EnableEndpointRouting = false;
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new Microsoft.OpenApi.Models.OpenApiInfo
					{
						Title = "Campaigns",
						Version = "v1"
					});
			});

			AddMongo(services);

			services.AddScoped<ICampaignsRepository, CampaignsRepository>();
			services.AddSingleton<CampaignsApplicationService>();
		}

		private void AddMongo(IServiceCollection services)
		{
			string mongoConnectionString = Configuration["MongoConnectionString"];
			string databaseName = new ConnectionString(mongoConnectionString).DatabaseName;

			MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
			settings.RetryWrites = true;
			//settings.ReadPreference = ReadPreference.Primary;
			//settings.WriteConcern = WriteConcern.WMajority;

			services
				.AddScoped<IMongoClient>(sp => new MongoClient(settings))
				.AddScoped<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(databaseName));

			BsonSerializer.RegisterSerializer<Guid>(new GuidSerializer(MongoDB.Bson.BsonType.String));

			//var conventions = new ConventionPack();

			//BsonClassMap.RegisterClassMap<Campaign>(cm =>
			//{
			//	cm.MapProperty(c => c.Id,)
			//})


			//conventions.se
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvcWithDefaultRoute();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(
					"/swagger/v1/swagger.json",
					"Campaigns v1");
			});

			//app.UseRouting();

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapGet("/", async context =>
			//	{
			//		await context.Response.WriteAsync("Hello World!");
			//	});
			//});
		}
	}
}
