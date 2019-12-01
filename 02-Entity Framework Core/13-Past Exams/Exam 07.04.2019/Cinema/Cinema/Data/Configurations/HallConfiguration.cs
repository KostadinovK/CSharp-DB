namespace Cinema.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> hall)
        {
            hall.HasMany(h => h.Projections)
                .WithOne(p => p.Hall)
                .HasForeignKey(p => p.HallId);

            hall.HasMany(h => h.Seats)
                .WithOne(s => s.Hall)
                .HasForeignKey(s => s.HallId);
        }
    }
}
