using BackendLab01;
using Infrastructure.EF.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public static class Mapper
    {
        public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
        {
            return new QuizItem(
                entity.Id,
                entity.Question,
                entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
                entity.CorrectAnswer);
        }

        public static Quiz FromEntityToQuiz(QuizEntity entity)
        {
            return new Quiz(
                entity.Id,
                entity.Items.Select(e => FromEntityToQuizItem(e)).ToList(),
                entity.Title
                );
        }

        public static QuizItemUserAnswer FromEntityToQuizItemUserAnswer(QuizItemUserAnswerEntity entity)
        {
            return new QuizItemUserAnswer(
                FromEntityToQuizItem(entity.QuizItem),
                entity.UserId,
                entity.QuizId,
                entity.UserAnswer
                );
        }
	}
}
