using Microsoft.EntityFrameworkCore;
using UnlockablePostsAPI.Models;

namespace UnlockablePostsAPI.Data
{
    public class UnlockablePostsContext : DbContext
    {
        public UnlockablePostsContext(DbContextOptions<UnlockablePostsContext> options) : base(options) { }

        public DbSet<Nonce> Nonces { get; set; }
    }
}
