using System;
using System.IO;
using System.Reflection;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using SE4Autism.HighScoreTracker.AutoMapper;
using SE4Autism.HighScoreTracker.Database;
using SE4Autism.HighScoreTracker.Extensions;
using SE4Autism.HighScoreTracker.Middleware;
using SE4Autism.HighScoreTracker.Services.GameImages;
using SE4Autism.HighScoreTracker.Services.Games;
using SE4Autism.HighScoreTracker.Services.Scores;

namespace SE4Autism.HighScoreTracker
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
			 .SetCompatibilityVersion(CompatibilityVersion.Latest)
			 .AddDataAnnotations()
			 .AddApiExplorer();

			services.AddAutoMapper(typeof(DefaultProfile).Assembly);

			services.AddDbContext<LeaderBoardContext>(
				c => c.UseSqlite(_configuration["Database:ConnectionString"]));

			services.AddScoped<IGamesService, GamesService>();
			services.AddScoped<IGameImageService, GameImageService>();
			services.AddScoped<IScoreService, ScoreService>();

			services.AddSwaggerGen(
				o =>
				{
					o.SwaggerDoc(
						"v1",
						new OpenApiInfo
						{
							Title = "HighScore LeaderBoard API",
							Description = "API for high score leader board",
							Version = "1.0"
						});

					var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
					o.IncludeXmlComments(xmlPath);
				});

			services.AddSpaStaticFiles(c => c.RootPath = "../../frontend/se4autism-leaderboard/build");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UpdateDatabase();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(
				endpoints =>
				{
					endpoints.MapControllers();
				});

			app.UseSwagger();
			app.UseSwaggerUI(
				o =>
				{
					o.DocumentTitle = "HighScore leaderboard API";
					o.SwaggerEndpoint("/swagger/v1/swagger.json", "HighScore leaderboard API");
					o.RoutePrefix = "api/documentation";
				});

			app.UseSpaCatchMiddleware();

			app.UseSpa(
				c =>
				{
					c.Options.SourcePath = "../../frontend/se4autism-leaderboard";
					c.Options.DefaultPageStaticFileOptions = new StaticFileOptions
					{
						RequestPath = "/leaderboard"
					};


					if (env.IsDevelopment())
						c.UseReactDevelopmentServer("start");
				});
		}
	}
}