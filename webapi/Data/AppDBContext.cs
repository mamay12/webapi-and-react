using Microsoft.EntityFrameworkCore;

namespace webapi.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlServer("Server=localhost;Database=postdb;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Post[] postsToSeed = new Post[6];

            for (int i = 1; i <= 6; i++)
            {
                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    Content = "Post number " + i,
                    Title = $"This post number {i} and this is good"
                };
            }

            modelBuilder.Entity<Post>().HasData(postsToSeed);
        }
    }
}