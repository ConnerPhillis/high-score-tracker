using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE4Autism.HighScoreTracker.Models.Scores
{
	public class ReturnScoreModel
	{
		public int Id { get; set; }
		public int Points { get; set; }
		public DateTime EntryDate { get; set; }
		public string PlayerName { get; set; }
		public int GameId { get; set; }
	}
}
