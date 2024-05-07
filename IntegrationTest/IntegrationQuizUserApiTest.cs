using Infrastructure;
using Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Dto;

namespace IntegrationTest
{
	public class IntegrationQuizUserApiTest : IClassFixture<QuizAppTestFactory<Program>>
	{
		private readonly HttpClient _client;
		private readonly QuizAppTestFactory<Program> _app;
		private readonly QuizDbContext _context;

		public IntegrationQuizUserApiTest(QuizAppTestFactory<Program> app)
		{
			_app = app;
			_client = _app.CreateClient();
			using (var scope = app.Services.CreateScope())
			{
				_context = scope.ServiceProvider.GetService<QuizDbContext>();
				var items = new HashSet<QuizItemEntity> {
				new()
				{
					Id = 1, CorrectAnswer = "7", Question = "2 + 5", IncorrectAnswers =
						new HashSet<QuizItemAnswerEntity>
						{
							new() {Id = 11, Answer = "5"},
							new() {Id = 12, Answer = "6"},
							new() {Id = 13, Answer = "8"},
						}
				},
				new()
				{
					Id = 2, CorrectAnswer = "10", Question = "3 + 7", IncorrectAnswers =
						new HashSet<QuizItemAnswerEntity>
						{
							new() {Id = 14, Answer = "8"},
							new() {Id = 15, Answer = "9"},
							new() {Id = 16, Answer = "11"},
						}
				},
				};
				if (_context.Quizzes.Count() == 0)
				{
					_context.Quizzes.Add(
						new QuizEntity
						{
							Id = 1,
							Items = items,
							Title = "Matematyka"
						}
					);
					_context.SaveChanges();
				}
			}
		}
		[Fact]
		public async void GetShouldReturnTwoQuizzes()
		{
			//Arrange

			//Act
			var result = await _client.GetFromJsonAsync<QuizDto>("/api/v1/quizzes/1");

			////Assert
			//if (result != null)
			//{
			//	Assert.Single(result);
			//	Assert.Equal("Matematyka", result[0].Title);
			//}

			Assert.NotNull(result);
			Assert.IsType<QuizDto>(result);
			Assert.Equal(1, result.Id);
			Assert.Equal(2, result.Items.Count());
		}

		[Fact]
		public async void GetShouldReturnOkStatus()
		{
			//Arrange

			//Act
			var result = await _client.GetAsync("/api/v1/quizzes");

			//Assert
			Assert.Equal(HttpStatusCode.OK, result.StatusCode);
			Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
		}

		[Fact]
		public async void GetShouldReturnNotFound()
		{
			var result = await _client.GetAsync("/api/v1/quizzes/100");
			Assert.NotNull(result);
			Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
		}
	}
}
