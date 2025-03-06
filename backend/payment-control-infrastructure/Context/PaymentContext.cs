using Microsoft.EntityFrameworkCore;
using payment_control_infrastructure.Models;

namespace payment_control_infrastructure.Context;

public class PaymentContext : DbContext
{
    public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
    {
    }

    public DbSet<PaymentModel> Payments { get; set; }
    public DbSet<ClientModel> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<PaymentModel>(entity => {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Client)
                    .WithMany(x => x.Payments)
                    .HasForeignKey(x => x.ClientId);
            });

        modelBuilder
            .Entity<ClientModel>(entity => {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Payments)
                    .WithOne(x => x.Client)
                    .HasForeignKey(x => x.ClientId);
            });
        
        base.OnModelCreating(modelBuilder);
    }
}