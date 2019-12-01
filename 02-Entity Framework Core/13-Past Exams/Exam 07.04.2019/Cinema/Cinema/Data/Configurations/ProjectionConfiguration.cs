namespace Cinema.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
    {
        public void Configure(EntityTypeBuilder<Projection> projection)
        {
            projection.HasMany(p => p.Tickets)
                .WithOne(t => t.Projection)
                .HasForeignKey(t => t.ProjectionId);
        }
    }
}
