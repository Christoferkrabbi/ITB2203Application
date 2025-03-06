using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Model;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Test>? Tests { get; set; }
    public DbSet<Movie>? MovieList { get; set; }
    public DbSet<Session>? SessionList { get; set; }
}
