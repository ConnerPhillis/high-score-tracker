using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using SE4Autism.HighScoreTracker.Database;
using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Games;
using SE4Autism.HighScoreTracker.Services.GameImages;

namespace SE4Autism.HighScoreTracker.Services.Games
{
	public class GamesService : IGamesService
	{
		private readonly LeaderBoardContext _context;
		private readonly IGameImageService _gameImageService;
		private readonly IMapper _mapper;

		public GamesService(
			LeaderBoardContext context,
			IGameImageService gameImageService,
			IMapper mapper)
		{
			_context = context;
			_gameImageService = gameImageService;
			_mapper = mapper;
		}

		public IQueryable<Game> GetGames()
			=> _context.Games;

		public async Task<Game> GetGame(int id)
			=> await _context.Games.FindAsync(id);

		public async Task<Game> CreateGame(NewGameModel model)
		{
			try
			{
				var dbModel = _mapper.Map<Game>(model);
				await _context.Games.AddAsync(dbModel);
				await _context.SaveChangesAsync();

				if (model.Image != null)
					await _gameImageService.SetImageAsync(dbModel.Id, model.Image.OpenReadStream());

				return dbModel;
			}
			catch
			{
				return null;
			}
		}

		public async Task<bool> DeleteGame(int id)
		{
			try
			{
				var model = await GetGame(id);
				_context.Games.Remove(model);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}