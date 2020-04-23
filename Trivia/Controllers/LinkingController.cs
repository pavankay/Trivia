using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Trivia.Data;
using Microsoft.EntityFrameworkCore;


namespace Trivia.Controllers
{
    public class LinkingController : Controller
    {
        private readonly TriviaContext _context;
        public LinkingController(TriviaContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var questions = _context.Question.Include("Answers").ToList();

            return View(questions);
        }
    }
}