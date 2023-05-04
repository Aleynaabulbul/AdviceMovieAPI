using AdviceMovieAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AdviceMovieAPI.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

    }
}
