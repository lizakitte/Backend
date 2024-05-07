using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dto;

namespace IntegrationTest
{
	public class QuizApiTest
	{
		[Fact]
		public async void GetShouldReturnQuiz()
		{
			// Arange
			await using var application = new WebApplicationFactory<Program>();
			using var client = application.CreateClient();

			// Act
			var result = await client.GetFromJsonAsync<QuizDto>("/api/v1/quizzes/1");

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
			Assert.IsType<List<QuizItemDto>>(result.Items);
			Assert.NotEmpty(result.Items);
		}

		//[Fact]
		//public async void GetShouldReturnOkStatus()
		//{
		//	//Arrange
		//	await using var application = new WebApplicationFactory<Program>();
		//	using var client = application.CreateClient();

		//	HttpRequestMessage request = new HttpRequestMessage()
		//	{
		//		RequestUri = new Uri("https://localhost:7077/api/books"),
		//		Method = HttpMethod.Post,
		//		Headers = {
		//			{HttpRequestHeader.Authorization.ToString(), "Bearer 3789..."},
		//			{HttpRequestHeader.ContentType.ToString(), "application/json"}
		//		},
		//		Content = JsonContent.Create(body),
		//	};

		//	//Act
		//	var result = await client.GetAsync("/api/v1/quizzes");
		//	var response = await client.SendAsync(request);

		//	//Assert
		//	Assert.Equal(HttpStatusCode.OK, result.StatusCode);
		//	Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
		//}
	}
}
