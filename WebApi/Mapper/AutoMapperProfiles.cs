using AutoMapper;
using BackendLab01;
using WebApi.Dto;

namespace WebApi.Mapper
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<QuizItem, QuizItemDto>()
				.ForMember(
					q => q.Options,
					op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
			CreateMap<Quiz, QuizDto>()
				.ForMember(
					q => q.Items,
					op => op.MapFrom<List<QuizItem>>(i => i.Items)
				);
			CreateMap<NewQuizDto, Quiz>();

			CreateMap<List<QuizItemUserAnswer>, FeedbackQuizDto>()
				.ForMember(
					f => f.TotalQuestions,
					op => op.MapFrom(l => l.Count)
				)
				.ForMember(
					f => f.QuizId,
					op => op.MapFrom(l => l[0].QuizId)
				)
				.ForMember(
					f => f.UserId,
					op => op.MapFrom(l => l[0].UserId)
				);
		}
	}
}
