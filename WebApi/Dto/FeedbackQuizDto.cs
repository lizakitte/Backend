namespace WebApi.Dto
{
	public class FeedbackQuizDto
	{
		public int QuizId { get; init; }

		public int UserId { get; set; }

		public int TotalQuestions { get; set; }

		public List<FeedbackQuizItemDto> QuizItemsAnswers { get; init; }
	}
}
