using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SE4Autism.HighScoreTracker.Services.GameImages
{
	public class GameImageService : IGameImageService
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<GameImageService> _logger;

		public GameImageService(IConfiguration configuration,
			ILogger<GameImageService> logger)
		{
			_configuration = configuration;
			_logger = logger;
		}

		public async Task<bool> SetImageAsync(int gameId, Stream stream)
		{
			var filePath = GenerateGameImageUri(gameId);

			try
			{
				var file = new FileInfo(filePath);

				if (file.Directory == null)
					return false;

				file.Directory.Create();

				await using var fileStream = file.Create();
				await stream.CopyToAsync(fileStream);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"failed to update image for game {gameId}");
				return false;
			}
		}

		public Task<Stream> ReadImageAsync(int gameId)
		{
			var filePath = GenerateGameImageUri(gameId);

			return File.Exists(filePath)
				? Task.FromResult(File.OpenRead(filePath) as Stream)
				: Task.FromResult(null as Stream);
		}

		private string GenerateGameImageUri(int gameId)
			=> $"{_configuration["ContentRoot"]}/gameImages/{gameId}";
	}
}
