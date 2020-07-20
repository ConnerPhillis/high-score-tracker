using System;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SE4Autism.HighScoreTracker.Database;

namespace SE4Autism.HighScoreTracker.Extensions
{
	public static class StartupExtensions
	{
		public static void UpdateDatabase(this IApplicationBuilder app)
		{
			Console.WriteLine("Updating database");
			using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
			 .CreateScope();
			using var context = serviceScope.ServiceProvider.GetService<LeaderBoardContext>();

			var totalMigrationCount = context.Database.GetMigrations()
			 .Count();
			Console.WriteLine($"found {totalMigrationCount} migrations");
			var pendingMigrationCount = context.Database.GetPendingMigrations()
			 .Count();
			Console.WriteLine($"found {pendingMigrationCount} pending migrations");
			context.Database.Migrate();
		}
	}
}