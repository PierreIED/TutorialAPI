using Microsoft.EntityFrameworkCore;
using Tutorials.Model;
namespace Tutorials
{
    public class TutorialsContext : DbContext
    {

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Tutorial> Tutorials => Set<Tutorial>();

        public TutorialsContext(DbContextOptions<TutorialsContext> options) : base(options)
        {

        }
    }
}
