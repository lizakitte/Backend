using BackendLab01;
using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Mappers;
using Infrastructure.EF.Entities;
using ApplicationCore.Exceptions;

namespace Infrastructure.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;

		public QuizUserServiceEF(QuizDbContext dbContext)
		{
			_context = dbContext;
		}

		public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> FindAllQuizzes()
        {
            return _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .Select(Mapper.FromEntityToQuiz)
                .ToList();
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : Mapper.FromEntityToQuiz(entity);
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            return _context.UserAnswers.Where(a => a.UserId == userId && a.QuizId == quizId)
                .Select(Mapper.FromEntityToQuizItemUserAnswer).ToList();
        }

		public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
		{
			QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
			{
				UserId = userId,
				QuizItemId = quizItemId,
				QuizId = quizId,
				UserAnswer = answer
			};
			try
			{
				var saved = _context.UserAnswers.Add(entity).Entity;
				_context.SaveChanges();
				return new QuizItemUserAnswer(Mapper.FromEntityToQuizItem(saved.QuizItem),
					saved.UserId, saved.QuizId, saved.UserAnswer);
			}
			catch (DbUpdateException e)
			{
				if (e.InnerException.Message.StartsWith("The INSERT"))
				{
					throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
				}
				if (e.InnerException.Message.StartsWith("Violation of"))
				{
					throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
				}
				throw new Exception(e.Message);
			}
		}

		void IQuizUserService.SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
        {
            throw new NotImplementedException();
        }
    }
}
