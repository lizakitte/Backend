using AutoMapper;
using BackendLab01;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("/api/v1/quizzes/admin")]
	public class QuizAdminController : Controller
	{
		private readonly IQuizAdminService _adminService;
		private readonly IMapper _mapper;
		private readonly IValidator<QuizItem> _validator;

		public QuizAdminController(IQuizAdminService adminService, IMapper mapper, IValidator<QuizItem> validator)
		{
			_adminService = adminService;
			_mapper = mapper;
			_validator = validator;
		}

		[HttpPost]
		public ActionResult<object> AddQuiz(LinkGenerator link, NewQuizDto dto)
		{
			var quiz = _adminService.AddQuiz(_mapper.Map<Quiz>(dto));
			quiz.Items = new List<QuizItem>();
			return Created(
				link.GetPathByAction(HttpContext, nameof(GetQuiz), null, new { quizId = quiz.Id }),
				quiz
			);
		}

		[HttpGet]
		[Route("{quizId}")]
		public ActionResult<Quiz> GetQuiz(int quizId)
		{
			var quiz = _adminService.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
			return quiz is null ? NotFound() : quiz;
		}

		//[HttpPost]
		//[Route("{title}")]
		//public void AddQuiz(string title, [FromBody] List<QuizItem> items)
		//{
		//	_adminService.AddQuiz(title, items);
		//}

		//[HttpPost]
		//[Route("{question}/{points}")]
		//public ActionResult<QuizItem> AddQuizItem(string question, 
		//	[FromBody] List<string> incorrectAnswers, [FromQuery] string correctAnswer, int points)
		//{
		//	return _adminService.AddQuizItem(question, incorrectAnswers, correctAnswer, points);
		//}

		[HttpPatch]
		[Route("{quizId}")]
		[Consumes("application/json-patch+json")]
		public ActionResult<Quiz> AddQuizItem(int quizId, JsonPatchDocument<Quiz>? patchDoc)
		{
			var quiz = _adminService.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
			if (quiz is null || patchDoc is null)
			{
				return NotFound(new
				{
					error = $"Quiz width id {quizId} not found"
				});
			}

			var disablesOperation = patchDoc
				.Operations
				.FirstOrDefault(
				p => p.OperationType == OperationType.Replace && p.path == "id"
				);

			if (disablesOperation is not null)
			{
				return BadRequest(new { error = "Can not replace id!" });
			}

			int previousCount = quiz.Items.Count;
			patchDoc.ApplyTo(quiz, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (previousCount < quiz.Items.Count)
			{
				QuizItem item = quiz.Items[^1];

				var validationResult = _validator.Validate(item);
				if (!validationResult.IsValid)
				{
					return BadRequest(validationResult.Errors);
				}

				quiz.Items.RemoveAt(quiz.Items.Count - 1);
				_service.AddQuizItemToQuiz(quizId, item);
			}
			return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
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
		[Route("allquizzes")]
		public ActionResult<List<Quiz>> FindAll()
		{
			return _adminService.FindAllQuizzes();
		}
		
		[HttpGet]
		[Route("allquizitems")]
		public ActionResult<List<QuizItem>> FindAllItems()
		{
			return _adminService.FindAllQuizItems();
		}
	}
}
