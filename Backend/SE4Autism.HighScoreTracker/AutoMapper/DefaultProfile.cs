using AutoMapper;

using SE4Autism.HighScoreTracker.Database.Models;
using SE4Autism.HighScoreTracker.Models.Games;
using SE4Autism.HighScoreTracker.Models.Scores;

namespace SE4Autism.HighScoreTracker.AutoMapper
{
	public class DefaultProfile : Profile
	{

		public DefaultProfile()
		{
			CreateMap<NewGameModel, Game>();

			CreateMap<NewScoreModel, Score>();
			CreateMap<Score, ReturnScoreModel>();
		}
		
		
	}
}
