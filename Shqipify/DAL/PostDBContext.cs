using Microsoft.EntityFrameworkCore;
using Shqipify.Models.DBEnteties;

namespace Shqipify.DAL
{
    public class PostDBContext : DbContext
    {
        public PostDBContext(DbContextOptions<PostDBContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<University> Universities { get; set; }
    }
}
