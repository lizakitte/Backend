using BackendLab01;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Dto
{
	public class QuizDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<QuizItemDto> Items { get; set; }

		public static QuizDto Of(Quiz quiz)
		{
			return new QuizDto()
			{
				Id = quiz.Id,
				Title = quiz.Title,
				Items = quiz.Items.Select(QuizItemDto.Of).ToList(),
			};
		}
	}
}
