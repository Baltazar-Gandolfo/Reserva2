using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ComercioRepository
    {
        public List<Comercio> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.ToList();
            }
        }

        public List<Comercio> GetActivos()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Where(c => c.Activo).OrderBy(c => c.Nombre).ToList();
            }
        }

        public List<Comercio> GetByUsuario(Guid idUsuario)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Where(c => c.IdUsuario == idUsuario).ToList();
            }
        }

        public Comercio GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.FirstOrDefault(c => c.IdComercio == id);
            }
        }

        // Resumen de actividad entre desde (inclusive) y hasta (exclusive), según la fecha del turno.
        public ResumenComercio GetResumen(int idComercio, DateTime desde, DateTime hasta)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var reservas = ctx.Reservas.Where(r =>
                    r.Disponibilidad.Elemento.Sucursal.IdComercio == idComercio
                    && r.Disponibilidad.Fecha >= desde && r.Disponibilidad.Fecha < hasta);

                int vigentes = reservas.Count(r =>
                    r.Estado == EstadoReserva.Pendiente || r.Estado == EstadoReserva.Confirmada);

                decimal recaudado = ctx.Pagos
                    .Where(p => (p.Estado == EstadoPago.Aprobado || p.Estado == EstadoPago.Retenido)
                        && p.Reserva.Disponibilidad.Elemento.Sucursal.IdComercio == idComercio
                        && p.Reserva.Disponibilidad.Fecha >= desde && p.Reserva.Disponibilidad.Fecha < hasta)
                    .Sum(p => (decimal?)p.Monto) ?? 0m;

                int slots = ctx.Disponibilidades.Count(d =>
                    d.Elemento.Sucursal.IdComercio == idComercio
                    && d.Fecha >= desde && d.Fecha < hasta
                    && d.Estado != EstadoSlot.Bloqueada);

                return new ResumenComercio
                {
                    Recaudado = recaudado,
                    Reservas = reservas.Count(),
                    Canceladas = reservas.Count(r => r.Estado == EstadoReserva.Cancelada),
                    OcupacionPct = slots == 0 ? 0m : Math.Round(vigentes * 100m / slots, 1)
                };
            }
        }

        public bool ExisteSlug(string slug, int? idExcluir = null)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Any(c =>
                    c.Slug == slug && (idExcluir == null || c.IdComercio != idExcluir));
            }
        }

        public void Insertar(Comercio comercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                comercio.FechaCreacion = DateTime.Now;
                comercio.Activo = true;
                ctx.Comercios.Add(comercio);
                ctx.SaveChanges();
            }
        }

        public void Actualizar(Comercio comercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var existing = ctx.Comercios.Find(comercio.IdComercio);
                if (existing != null)
                {
                    existing.Nombre = comercio.Nombre;
                    existing.Slug = comercio.Slug;
                    existing.Ubicacion = comercio.Ubicacion;
                    existing.Activo = comercio.Activo;
                    ctx.SaveChanges();
                }
            }
        }

        public void CambiarEstado(int id, bool activo)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var comercio = ctx.Comercios.Find(id);
                if (comercio != null)
                {
                    comercio.Activo = activo;
                    ctx.SaveChanges();
                }
            }
        }
    }
}