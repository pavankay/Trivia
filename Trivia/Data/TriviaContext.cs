using Microsoft.EntityFrameworkCore;

namespace Trivia.Data
{
    public class TriviaContext : DbContext
    {
        public TriviaContext (DbContextOptions<TriviaContext> options)
            : base(options)
        {
        }

        public DbSet<Trivia.Models.Question> Question { get; set; }

        public DbSet<Trivia.Models.Answer> Answer { get; set; }
    }
}
