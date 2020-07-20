using System.IO;
using System.Threading.Tasks;

namespace SE4Autism.HighScoreTracker.Services.GameImages
{
	public interface IGameImageService
	{
		public Task<bool> SetImageAsync(int gameId, Stream stream);

		public Task<Stream> ReadImageAsync(int gameId);
	}
}
