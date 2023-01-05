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
    }
}
