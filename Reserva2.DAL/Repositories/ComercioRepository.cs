using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para la entidad Comercio.
    /// </summary>
    public class ComercioRepository
    {
        /// <summary>Obtiene todos los comercios.</summary>
        /// <returns>Lista de comercios.</returns>
        public List<Comercio> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.ToList();
            }
        }

        /// <summary>Obtiene los comercios activos ordenados por nombre.</summary>
        /// <returns>Lista de comercios activos.</returns>
        public List<Comercio> GetActivos()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Where(c => c.Activo).OrderBy(c => c.Nombre).ToList();
            }
        }

        /// <summary>Obtiene los comercios de un usuario propietario.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <returns>Lista de comercios del usuario.</returns>
        public List<Comercio> GetByUsuario(Guid idUsuario)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Where(c => c.IdUsuario == idUsuario).ToList();
            }
        }

        /// <summary>Obtiene un comercio por su identificador.</summary>
        /// <param name="id">Identificador del comercio.</param>
        /// <returns>Comercio encontrado o null.</returns>
        public Comercio GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.FirstOrDefault(c => c.IdComercio == id);
            }
        }

        /// <summary>Obtiene el resumen de actividad de un comercio en un rango de fechas.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="desde">Fecha desde (inclusive).</param>
        /// <param name="hasta">Fecha hasta (exclusive).</param>
        /// <returns>Resumen con recaudación, reservas, cancelaciones y ocupación.</returns>
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

        /// <summary>Verifica si un slug ya está en uso por otro comercio.</summary>
        /// <param name="slug">Slug a verificar.</param>
        /// <param name="idExcluir">Id del comercio a excluir de la búsqueda.</param>
        /// <returns><c>true</c> si el slug ya existe.</returns>
        public bool ExisteSlug(string slug, int? idExcluir = null)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Comercios.Any(c =>
                    c.Slug == slug && (idExcluir == null || c.IdComercio != idExcluir));
            }
        }

        /// <summary>Inserta un nuevo comercio.</summary>
        /// <param name="comercio">Comercio a insertar.</param>
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

        /// <summary>Actualiza los datos de un comercio existente.</summary>
        /// <param name="comercio">Comercio con los datos actualizados.</param>
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

        /// <summary>Activa o desactiva un comercio.</summary>
        /// <param name="id">Identificador del comercio.</param>
        /// <param name="activo">Estado de activación.</param>
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