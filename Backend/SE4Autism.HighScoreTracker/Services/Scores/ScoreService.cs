using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using SE4Autism.HighScoreTracker.Database;
using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Scores;

namespace SE4Autism.HighScoreTracker.Services.Scores
{
	public class ScoreService : IScoreService
	{
		private readonly LeaderBoardContext _context;
		private readonly IMapper _mapper;

		public ScoreService(LeaderBoardContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		public IQueryable<Score> GetScores()
			=> _context.Scores;

		public async Task<Score> GetScoreAsync(int id)
			=> await _context.Scores.FindAsync(id);

		public async Task<Score> CreateScoreAsync(int gameId, NewScoreModel model)
		{
			try
			{
				var dbItem = _mapper.Map<Score>(model);
				dbItem.GameId = gameId;
				await _context.Scores.AddAsync(dbItem);
				await _context.SaveChangesAsync();
				return dbItem;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		} 

		public async Task<bool> DeleteScoreAsync(int id)
		{
			try
			{
				var dbItem = await GetScoreAsync(id);
				_context.Scores.Remove(dbItem);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}
	}
}
