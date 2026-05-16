// Infrastructure/Context/AppointmentsDbContext.cs
using Backend.src.app.Features.Appointments.Domain.Entities;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Appointments.Infrastructure.Context
{
    public class AppointmentsDbContext : DbContext
    {
        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options)
            : base(options) { }

        public DbSet<Appointment> Pedidos { get; set; }
        public DbSet<AppointmentDetails> DetallePedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(a => a.IdPedido);

                entity.Property(a => a.IdPedido)
                      .HasColumnName("idPedido")
                      .ValueGeneratedOnAdd(); 

                entity.Property(a => a.IdUsuario)
                      .HasColumnName("idUsuario")
                      .IsRequired();

                entity.Property(a => a.IdEmpleado)
                      .HasColumnName("idEmpleado")
                      .IsRequired(false); // ← nullable

                entity.Property(a => a.IdMoto)
                      .HasColumnName("idMoto")
                      .IsRequired();

                entity.Property(a => a.FechaCreacionCita)
                      .HasColumnName("fechaCreacionCita")
                      .IsRequired();

                entity.Property(a => a.FechaCita)
                      .HasColumnName("fechaCita")
                      .IsRequired();

                entity.Property(a => a.HoraCita)
                      .HasColumnName("horaCita")
                      .IsRequired();

                entity.Property(a => a.Total)
                      .HasColumnName("total")
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(a => a.EstadoCita)
                      .HasColumnName("EstadoCita")
                      .HasConversion<string>() // ← guarda el enum como string en la BD
                      .IsRequired();

                // Relación padre → hijo
                entity.HasMany(a => a.Detalles)
                      .WithOne()
                      .HasForeignKey(d => d.IdPedido)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AppointmentDetails>(entity =>
            {
                entity.ToTable("DetallePedidos");
                entity.HasKey(d => d.IdDetalle);

                entity.Property(d => d.IdDetalle)
                      .HasColumnName("idDetalle")
                      .ValueGeneratedOnAdd();

                entity.Property(d => d.IdPedido)
                      .HasColumnName("idPedido")
                      .IsRequired();

                entity.Property(d => d.IdServicio)
                      .HasColumnName("idServicio")
                      .IsRequired();

                entity.Property(d => d.PrecioUnitario)
                      .HasColumnName("precioUnitario")
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(d => d.SubTotal)
                      .HasColumnName("subtotal")
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}