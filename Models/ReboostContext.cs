using Microsoft.EntityFrameworkCore;

namespace Reboost {

public class ApplicationDbContext : DbContext {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Battery> Batteries { get; set; } = null!;
        public DbSet<Cabinet> Cabinets { get; set; } = null!;
        public DbSet<CabinetBattery> CabinetBatteries { get; set; } = null!;
        public DbSet<Rent> Rents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CabinetBattery>()
                .HasOne<Cabinet>()
                .WithMany()
                .HasForeignKey(cb => cb.FkCabinetId);
            
            modelBuilder.Entity<CabinetBattery>()
                .HasOne<Battery>()
                .WithMany()
                .HasForeignKey(cb => cb.FkBatteryId);
            
            modelBuilder.Entity<Rent>()
                .HasOne<Battery>()
                .WithMany()
                .HasForeignKey(r => r.FkBatteryId);
            
            modelBuilder.Entity<Rent>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.FkUserId);
            
            modelBuilder.Entity<Rent>()
                .HasOne<Cabinet>()
                .WithMany()
                .HasForeignKey(r => r.FkCabinetFromId);
            
            modelBuilder.Entity<Rent>()
                .HasOne<Cabinet>()
                .WithMany()
                .HasForeignKey(r => r.FkCabinetToId)
                // Evitar a exclusão em cascata
                .OnDelete(DeleteBehavior.Restrict); 
        }

        // Mudar isto para variável de ambiente ou KeyVault
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=REBOOST-PROD;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=REBOOST-QA;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
    }
}