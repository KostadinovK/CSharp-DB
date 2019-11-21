namespace CarDealer.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext(DbContextOptions options)
            : base(options)
        {
        }

        public CarDealerContext()
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CarDealerDb;Trusted_Connection=True;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartCar>(e =>
            {
                e.HasKey(k => new { k.CarId, k.PartId });

                e.HasOne(pc => pc.Car)
                    .WithMany(c => c.PartCars)
                    .HasForeignKey(pc => pc.CarId);

                e.HasOne(pc => pc.Part)
                    .WithMany(p => p.PartCars)
                    .HasForeignKey(pc => pc.PartId);
            });

            modelBuilder.Entity<Part>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<Sale>(e => { 
                e.HasOne(s => s.Car)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CarId);

                e.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);
            });

        }
    }
}
