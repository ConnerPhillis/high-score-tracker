using System;

using Microsoft.EntityFrameworkCore;

using SE4Autism.HighScoreTracker.Database.Models;

namespace SE4Autism.HighScoreTracker.Database
{
	public class LeaderBoardContext : DbContext
	{
		
		public DbSet<Game> Games { get; set; }
		
		public DbSet<Score> Scores { get; set; }

		public LeaderBoardContext(DbContextOptions options) : base(options)
		{
			
		}
		
	}
}
