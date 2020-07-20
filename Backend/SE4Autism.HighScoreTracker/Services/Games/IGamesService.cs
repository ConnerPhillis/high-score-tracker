using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Games;

namespace SE4Autism.HighScoreTracker.Services.Games
{
	public interface IGamesService
	{
		public IQueryable<Game> GetGames();

		public Task<Game> GetGame(int id);

		public Task<Game> CreateGame(NewGameModel model);

		public Task<bool> DeleteGame(int id);
	}
}
