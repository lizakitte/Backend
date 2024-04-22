using BackendLab01;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("/api/v1/quizzes/admin")]
	public class QuizAdminController : Controller
	{
		private readonly IQuizAdminService _adminService;

		public QuizAdminController(IQuizAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpPost]
		[Route("{title}")]
		public void AddQuiz(string title, [FromBody] List<QuizItem> items)
		{
			_adminService.AddQuiz(title, items);
		}

		[HttpPost]
		[Route("{question}/{points}")]
		public ActionResult<QuizItem> AddQuizItem(string question, 
			[FromBody] List<string> incorrectAnswers, [FromQuery] string correctAnswer, int points)
		{
			return _adminService.AddQuizItem(question, incorrectAnswers, correctAnswer, points);
		}

		[HttpDelete]
		[Route("quiz/{id}")]
		public void DeleteQuiz(int id)
		{
			_adminService.DeleteQuiz(id);
		}

		[HttpDelete]
		[Route("quizitem/{id}")]
		public void DeleteQuizItem(int id)
		{
			_adminService.DeleteQuizItem(id);
		}

		[HttpGet]
		public ActionResult<List<Quiz>> FindAll()
		{
			return _adminService.FindAllQuizzes();
		}
		
		[HttpGet]
		public ActionResult<List<QuizItem>> FindAllItems()
		{
			return _adminService.FindAllQuizItems();
		}
	}
}
