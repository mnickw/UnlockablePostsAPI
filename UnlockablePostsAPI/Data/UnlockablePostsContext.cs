using Microsoft.EntityFrameworkCore;

namespace UnlockablePostsAPI.Data
{
    public class UnlockablePostsContext : DbContext
    {
        public UnlockablePostsContext(DbContextOptions<UnlockablePostsContext> options) : base(options) { }

        //public DbSet<Course> Courses { get; set; }\
    }
}
