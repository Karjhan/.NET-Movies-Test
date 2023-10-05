using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataContexts;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public DbSet<Actor> Actors { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}