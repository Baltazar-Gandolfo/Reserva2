using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;

namespace Reserva2.DAL
{
    public class NegocioDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<ElementoReservable> Elementos { get; set; }
        public DbSet<ReglaDisponibilidad> ReglasDisponibilidad { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<ReglaNegocio> ReglasNegocio { get; set; }
        public DbSet<ListaEspera> ListasEspera { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        public NegocioDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comercio>(e =>
            {
                e.ToTable("Comercio");
                e.HasKey(c => c.IdComercio);
                e.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
                e.Property(c => c.Slug).HasMaxLength(200);
                e.Property(c => c.Ubicacion).HasMaxLength(200);
                e.Property(c => c.LogoUrl).HasMaxLength(500);
                e.Property(c => c.IdUsuario).IsRequired();
            });

            modelBuilder.Entity<Sucursal>(e =>
            {
                e.ToTable("Sucursal");
                e.HasKey(s => s.IdSucursal);
                e.Property(s => s.Nombre).IsRequired().HasMaxLength(100);
                e.Property(s => s.Ubicacion).HasMaxLength(200);
                e.HasOne(s => s.Comercio).WithMany().HasForeignKey(s => s.IdComercio);
            });

            modelBuilder.Entity<ElementoReservable>(e =>
            {
                e.ToTable("ElementoReservable");
                e.HasKey(x => x.IdElemento);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                e.Property(x => x.Descripcion).HasMaxLength(500);
                e.Property(x => x.Tipo).IsRequired().HasMaxLength(50);
                e.Property(x => x.Estado).IsRequired().HasMaxLength(20);
                e.Property(x => x.PrecioBase).HasColumnType("decimal(18,2)");
                e.Property(x => x.PorcentajeSeña).HasColumnName("PorcentajeSena").HasColumnType("decimal(5,2)");
                e.HasOne(x => x.Sucursal).WithMany().HasForeignKey(x => x.IdSucursal);
            });

            modelBuilder.Entity<ReglaDisponibilidad>(e =>
            {
                e.ToTable("ReglaDisponibilidad");
                e.HasKey(r => r.IdReglaDisponibilidad);
                e.Property(r => r.DiaSemana).IsRequired().HasMaxLength(20);
                e.Property(r => r.PrecioOverride).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Disponibilidad>(e =>
            {
                e.ToTable("Disponibilidad");
                e.HasKey(d => d.IdDisponibilidad);
                e.Property(d => d.Fecha).HasColumnType("date");
                e.Property(d => d.Precio).HasColumnType("decimal(18,2)");
                e.Property(d => d.Estado).IsRequired().HasMaxLength(20);
                e.HasOne(d => d.Elemento).WithMany().HasForeignKey(d => d.IdElemento);
            });

            modelBuilder.Entity<Reserva>(e =>
            {
                e.ToTable("Reserva");
                e.HasKey(r => r.IdReserva);
                e.Property(r => r.Estado).IsRequired().HasMaxLength(20);
                e.Property(r => r.MotivoCancelacion).HasMaxLength(500);
                e.HasOne(r => r.Disponibilidad).WithMany().HasForeignKey(r => r.IdDisponibilidad);
            });

            modelBuilder.Entity<Pago>(e =>
            {
                e.ToTable("Pago");
                e.HasKey(p => p.IdPago);
                e.Property(p => p.Estado).IsRequired().HasMaxLength(30);
                e.Property(p => p.MetodoPago).IsRequired().HasMaxLength(30);
                e.Property(p => p.Monto).HasColumnType("decimal(18,2)");
                e.HasOne(p => p.Reserva).WithMany().HasForeignKey(p => p.IdReserva);
            });

            modelBuilder.Entity<ReglaNegocio>(e =>
            {
                e.ToTable("ReglaNegocio");
                e.HasKey(r => r.IdReglaNegocio);
                e.Property(r => r.Clave).IsRequired().HasMaxLength(100);
                e.Property(r => r.Descripcion).HasMaxLength(500);
                e.Property(r => r.Valor).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<ListaEspera>(e =>
            {
                e.ToTable("ListaEspera");
                e.HasKey(l => l.IdListaEspera);
                e.Property(l => l.Estado).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Notificacion>(e =>
            {
                e.ToTable("Notificacion");
                e.HasKey(n => n.IdNotificacion);
                e.Property(n => n.Canal).IsRequired().HasMaxLength(50);
                e.Property(n => n.Estado).IsRequired().HasMaxLength(30);
                e.Property(n => n.Tipo).HasMaxLength(50);
            });
        }
    }
}
