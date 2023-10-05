using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(movie => movie.Name).IsRequired().HasMaxLength(150);
        builder.Property(movie => movie.Description).IsRequired().HasMaxLength(500);
        builder.Property(movie => movie.Year).IsRequired();
        builder.Property(movie => movie.Rating).HasColumnType("decimal(18, 2)");
    }
}