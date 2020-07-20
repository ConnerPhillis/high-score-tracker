using System.Linq;
using System.Threading.Tasks;

using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Scores;

namespace SE4Autism.HighScoreTracker.Services.Scores
{
	public interface IScoreService
	{
		public IQueryable<Score> GetScores();

		public Task<Score> GetScoreAsync(int id);

		public Task<Score> CreateScoreAsync(int gameId, NewScoreModel model);

		public Task<bool> DeleteScoreAsync(int id);
	}
}
