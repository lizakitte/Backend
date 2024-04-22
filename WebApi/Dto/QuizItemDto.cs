using BackendLab01;

namespace WebApi.Dto
{
	public class QuizItemDto
	{
		public int Id { get; set; }
		public string Question { get; set; }
		public List<string> Options { get; set; }

		public static QuizItemDto Of(QuizItem quiz)
		{
			var options = new List<string>(quiz.IncorrectAnswers)
			{
				quiz.CorrectAnswer
			};
			return new QuizItemDto()
			{
				Id = quiz.Id,
				Question = quiz.Question,
				Options = options
			};
		}
	}
}
