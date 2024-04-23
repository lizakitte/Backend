﻿using ApplicationCore.Interfaces.Repository;

namespace BackendLab01;

public class QuizAdminService : IQuizAdminService
{
    private IGenericRepository<Quiz, int> quizRepository;
    private IGenericRepository<QuizItem, int> itemRepository;

    public QuizAdminService(IGenericRepository<Quiz, int> quizRepository, IGenericRepository<QuizItem, int> itemRepository)
    {
        this.quizRepository = quizRepository;
        this.itemRepository = itemRepository;
    }

    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        return itemRepository.Add(new QuizItem(question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer, id: 0));
    }

    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        var quizItem = new QuizItem(id: id, question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer);
        itemRepository.Update(id, quizItem);
    }

    public Quiz AddQuiz(string title, List<QuizItem> items)
    {
        return quizRepository.Add(new Quiz( 0, title: title, items: items));
    }

    public Quiz AddQuiz(Quiz quiz)
    {
        return quizRepository.Add(quiz);
    }

    public void AddQuizItemToQuiz(int quizId, QuizItem item)
    {
        var quiz = quizRepository.FindById(quizId);
        quiz?.Items.Add(item);
    }

    public List<QuizItem> FindAllQuizItems()
    {
        return itemRepository.FindAll();
    }

    public List<Quiz> FindAllQuizzes()
    { 
        return quizRepository.FindAll();
    }

	public void DeleteQuiz(int id)
	{
        var quiz = quizRepository.FindById(id);
        foreach (var item in quiz.Items)
        {
            DeleteQuizItem(item.Id);
        }    
        quizRepository.RemoveById(id);
	}

	public void DeleteQuizItem(int id)
	{
        itemRepository.RemoveById(id);
	}
}