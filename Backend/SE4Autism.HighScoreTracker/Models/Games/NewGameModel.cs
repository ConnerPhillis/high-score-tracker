using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace SE4Autism.HighScoreTracker.Models.Games
{
	public class NewGameModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string PointType { get; set; }
		
		public IFormFile Image { get; set; }
	}
}
