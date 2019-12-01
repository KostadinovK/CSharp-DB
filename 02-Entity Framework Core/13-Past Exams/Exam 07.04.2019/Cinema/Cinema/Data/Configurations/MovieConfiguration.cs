

namespace Cinema.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> movie)
        {
            movie.HasMany(m => m.Projections)
                .WithOne(p => p.Movie)
                .HasForeignKey(p => p.MovieId);
        }
    }
}
