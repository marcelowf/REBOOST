using Microsoft.EntityFrameworkCore;
using Reboost.Models;

namespace Reboost
{
    public class ReboostDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TokenLogin> TokenLogins { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;
        public DbSet<Battery> Batteries { get; set; } = null!;
        public DbSet<Cabinet> Cabinets { get; set; } = null!;
        public DbSet<CabinetBattery> CabinetBatteries { get; set; } = null!;
        public DbSet<Rent> Rents { get; set; } = null!;

        public ReboostDbContext(DbContextOptions<ReboostDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Rent>()
                .HasOne<Battery>()
                .WithMany()
                .HasForeignKey(r => r.FkBatteryId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Token>()
                .HasOne<Cabinet>()
                .WithMany()
                .HasForeignKey(r => r.FkCabinetId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Token>()
                .HasOne<Battery>()
                .WithMany()
                .HasForeignKey(r => r.FkBatteryId);
            
            modelBuilder.Entity<Token>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.FkUserId);
                
            modelBuilder.Entity<TokenLogin>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.FkUserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=REBOOST-QA;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }
    }
}
