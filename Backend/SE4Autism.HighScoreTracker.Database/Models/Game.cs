using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SE4Autism.HighScoreTracker.Database.Models
{
	public class Game
	{
		
		[Key]
		public int Id { get; set; }
		
		public string Name { get; set; }

		public string Description { get; set; }

		public string PointType { get; set; }
		
	}
}
