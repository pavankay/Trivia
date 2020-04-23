using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trivia.Data;
using Trivia.Models;

namespace Trivia.Controllers
{
    public class QuizController : Controller
    {
        private readonly TriviaContext _context;

        public QuizController(TriviaContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var questions = _context.Question.Include("Answers").ToArray();
            var randomeNumberGenerator = new Random();
            var randomQuestionIndex = randomeNumberGenerator.Next(0, questions.Length - 1);
            var randomQuestion = questions[randomQuestionIndex];

            var model = new QuizViewModel();
            model.Question = randomQuestion;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            var questionId = Int32.Parse(collection["Question.Id"][0]);
            var selectedAnswerId = Int32.Parse(collection["SelectedAnswerId"][0]);

            var correctAnswer = _context.Answer
                                    .Where(x =>
                                        x.QuestionId == questionId &&
                                        x.Id == selectedAnswerId)
                                    .FirstOrDefault();

            if (correctAnswer.IsRightAnswer)
            {
                return View("Result", "Correct!");
            }
            else
            {
                return View("Result", $"Wrong! The correct answer was: {correctAnswer.Text}");
            }
        }
    }
}