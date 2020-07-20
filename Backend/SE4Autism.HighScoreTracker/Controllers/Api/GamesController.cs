	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	using SE4Autism.HighScoreTracker.Models.Games;
	using SE4Autism.HighScoreTracker.Services.GameImages;
	using SE4Autism.HighScoreTracker.Services.Games;

namespace SE4Autism.HighScoreTracker.Controllers.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class GamesController : ControllerBase
	{
		private readonly IGamesService _gamesService;
		private readonly IGameImageService _gameImageService;

		public GamesController(IGamesService gamesService, 
			IGameImageService gameImageService)
		{
			_gamesService = gamesService;
			_gameImageService = gameImageService;
		}

		[HttpGet]
		public IActionResult AllGames()
			=> Ok(_gamesService.GetGames());

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGame(int id)
		{
			var game = await _gamesService.GetGame(id);

			if(game != null)
				return Ok(await _gamesService.GetGame(id));
			return NotFound($"Could not find game with id {id}");
		}

		[HttpGet("{id}/image")]
		public async Task<IActionResult> GetGameImage(int id)
		{
			var image = await _gameImageService.ReadImageAsync(id);

			return image != null 
				? (IActionResult) File(image, "image/png") 
				: NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> CreateGame([FromForm] NewGameModel gameModel)
		{
			if (!IsValidImage(gameModel.Image))
				return BadRequest(
					$"invalid content type for image, got {gameModel.Image.ContentType} but expected image/png");

			var item = await _gamesService.CreateGame(gameModel);
			return item != null
				? (IActionResult) CreatedAtAction(nameof(GetGame), new {id = item.Id}, item)
				: BadRequest("Failed to create game");
		}

		[HttpPut("{id}/image")]
		public async Task<IActionResult> SetGameImage(int id, IFormFile image)
		{
			if(!IsValidImage(image))
				return BadRequest(
					$"invalid content type for image, got {image.ContentType} but expected image/png");

			var result = await _gameImageService.SetImageAsync(id, image.OpenReadStream());

			return result
				? (IActionResult) AcceptedAtRoute(nameof(GetGameImage), new {id})
				: BadRequest("Failed to set image");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGame(int id)
			=> await _gamesService.DeleteGame(id)
				? (IActionResult) Ok()
				: BadRequest("an error occured deleting the game");

		private bool IsValidImage(IFormFile formFile)
			=> formFile != null && formFile.ContentType == "image/png";
	}
}