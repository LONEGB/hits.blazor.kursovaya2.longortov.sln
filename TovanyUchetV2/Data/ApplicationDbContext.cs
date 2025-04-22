using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TovanyUchetV2.Data.Models;

namespace TovanyUchetV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<InventoryOperation> InventoryOperations { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryOperation>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Cascade); //  аскадное удаление

            modelBuilder.Entity<InventoryOperation>()
                .HasOne(op => op.Employee)
                .WithMany()
                .HasForeignKey(op => op.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull); // ”становить NULL при удалении сотрудника

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // 18 всего знаков, из них 2 после зап€той
        }
    }



}