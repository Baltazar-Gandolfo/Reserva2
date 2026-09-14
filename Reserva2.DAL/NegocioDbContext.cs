using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;

namespace Reserva2.DAL
{
    public class NegocioDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<ElementoReservable> Elementos { get; set; }

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
                e.Property(c => c.IdUsuario).IsRequired();
                e.Ignore("DVH");
            });

            modelBuilder.Entity<ElementoReservable>(e =>
            {
                e.ToTable("ElementoReservable");
                e.HasKey(el => el.IdElemento);
                e.Property(el => el.Nombre).IsRequired().HasMaxLength(150);
                e.Property(el => el.PrecioBase).HasColumnType("decimal(18,2)");
            });
        }
    }
}