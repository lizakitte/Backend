﻿using BackendLab01;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dto;

namespace ApplicationCore.Controllers
{
	[ApiController]
	[Route("/api/v1/quizzes")]
	public class QuizController : ControllerBase
	{
		private readonly IQuizUserService _userService;

		public QuizController(IQuizUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult<QuizDto> FindById(int id)
		{
			var quiz = _userService.FindQuizById(id);
			if(quiz == null)
			{
				return NotFound();
			}
			return Ok(QuizDto.Of(quiz));
		}

		[HttpGet]
		public IEnumerable<QuizDto> FindAll()
		{
			return _userService.FindAllQuizzes().Select(QuizDto.Of);
		}

		[HttpPost]
		[Route("{quizId}/items/{itemId}")]
		public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
		{
			_userService.SaveUserAnswerForQuiz(quizId, dto.UserId, itemId, dto.Answer);
		}

		[HttpGet]
		[Route("{quizId}/{userId}")]
		public ActionResult<int> QuizInfo(int quizId, int userId)
		{
			return _userService.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
		}
	}
}