using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SE4Autism.HighScoreTracker.Database
{
	class LeaderBoardContextFactory : IDesignTimeDbContextFactory<LeaderBoardContext>
	{
		public LeaderBoardContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<LeaderBoardContext>();
			optionsBuilder.UseSqlite("Data Source=database.db");

			return new LeaderBoardContext(optionsBuilder.Options);
		}
	}
}
