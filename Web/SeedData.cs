using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();

            QuizItem item1 = new QuizItem(
                0, 
                "1 + 2 = ?", 
                new List<string>() { "1", "2" },
                "3"
                );
            QuizItem item2 = new QuizItem(
                1,
                "5 + 6 = ?",
                new List<string>() { "9", "6" },
                "11"
                );
            QuizItem item3 = new QuizItem(
                2,
                "10 + 7 = ?",
                new List<string>() { "12", "19" },
                "17"
                );
            QuizItem item4 = new QuizItem(
                3,
                "1 * 2 = ?",
                new List<string>() { "1", "3" },
                "2"
                );
            QuizItem item5 = new QuizItem(
                4,
                "5 * 6 = ?",
                new List<string>() { "11", "6" },
                "30"
                );
            QuizItem item6 = new QuizItem(
                5,
                "10 * 7 = ?",
                new List<string>() { "17", "3" },
                "70"
                );

            Quiz quiz1 = new Quiz(
                0, 
                new List<QuizItem>() { item1, item2, item3 },
                "Addition"
                );
            Quiz quiz2 = new Quiz(
                1,
                new List<QuizItem>() { item4, item5, item6 },
                "Multiplying"
                );

            quizItemRepo.Add(item1);
            quizItemRepo.Add(item2);
            quizItemRepo.Add(item3);
            quizItemRepo.Add(item4);
            quizItemRepo.Add(item5);
            quizItemRepo.Add(item6);

            quizRepo.Add(quiz1);
            quizRepo.Add(quiz2);
        }
    }
}