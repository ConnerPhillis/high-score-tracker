using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Scores;
using SE4Autism.HighScoreTracker.Services.Scores;

namespace SE4Autism.HighScoreTracker.Controllers.Api
{
	[ApiController]
	[Route("api/games/{gameId}/[controller]")]
	public class ScoresController : ControllerBase
	{
		private readonly IScoreService _scoreService;
		private readonly IMapper _mapper;

		public ScoresController(IScoreService scoreService, IMapper mapper)
		{
			_scoreService = scoreService;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(List<ReturnScoreModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetScores(int gameId, int page = 1, int maxResults = 25)
		{
			var scores = _scoreService.GetScores()
			 .Where(v => v.GameId == gameId)
			 .OrderByDescending(v => v.Points)
			 .AsQueryable();

			if (page >= 1 && maxResults > 0)
				scores = scores.Skip((page - 1) * maxResults)
				 .Take(maxResults);

			var models = _mapper.Map<List<ReturnScoreModel>>(await scores.ToListAsync());

			return Ok(models);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetScore(int gameId, int id)
		{
			var score = await _scoreService.GetScoreAsync(id);
			return score == null || score.GameId != gameId
				? (IActionResult) NotFound("could not find this score")
				: Ok(score);
		}

		[HttpPost]
		public async Task<IActionResult> CreateScore(int gameId, NewScoreModel model)
		{ 
			var score = await _scoreService.CreateScoreAsync(gameId, model);

			return score != null
				? (IActionResult) CreatedAtAction(nameof(GetScore),
					new {gameId = score.GameId, id = score.Id},
					score)
				: BadRequest("Failed to generate score");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteScore(int id)
			=> await _scoreService.DeleteScoreAsync(id)
				? (IActionResult) Ok()
				: BadRequest("An error occured deleting the score");
	}
}
