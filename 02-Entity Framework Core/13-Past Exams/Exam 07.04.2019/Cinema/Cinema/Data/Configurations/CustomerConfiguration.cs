namespace Cinema.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> customer)
        {
            customer.HasMany(c => c.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId);
        }
    }
}
