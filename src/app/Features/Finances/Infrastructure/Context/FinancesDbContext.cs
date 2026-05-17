
using Backend.src.app.Features.Finances.Domain.Entities;
using Backend.src.app.Features.Finances.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Finances.Infrastructure.Context
{
    public class FinancesDbContext : DbContext
    {
        public FinancesDbContext(DbContextOptions<FinancesDbContext> options)
            : base(options) { }

        public DbSet<AccountingMovement> Contabilidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingMovement>(entity =>
            {
                entity.ToTable("Contabilidad");
                entity.HasKey(m => m.IdMovimiento);

                entity.Property(m => m.IdMovimiento)
                      .HasColumnName("id_movimiento")
                      .ValueGeneratedOnAdd();

                entity.Property(m => m.Fecha)
                      .HasColumnName("fecha")
                      .IsRequired();

                entity.Property(m => m.TipoMovimiento)
                      .HasColumnName("tipo_movimiento")
                      .HasConversion<string>()
                      .IsRequired();

                entity.Property(m => m.Descripcion)
                      .HasColumnName("descripcion")
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(m => m.Monto)
                      .HasColumnName("monto")
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}