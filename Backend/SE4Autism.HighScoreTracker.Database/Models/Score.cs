using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SE4Autism.HighScoreTracker.Database.Models
{
	public class Score
	{
		[Key]
		public int Id { get; set; }
		
		public int Points { get; set; }
		
		public DateTime EntryDate { get; set; } = DateTime.UtcNow;
		
		public string PlayerName { get; set; }

		public Game Game { get; set; }
		
		public int GameId { get; set; }
	}
}
