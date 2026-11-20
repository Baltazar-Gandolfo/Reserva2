using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para sucursales de comercios.
    /// </summary>
    public class SucursalRepository
    {
        /// <summary>Obtiene todas las sucursales.</summary>
        /// <returns>Lista de sucursales.</returns>
        public List<Sucursal> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales.ToList();
            }
        }

        /// <summary>Obtiene las sucursales de un comercio.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <returns>Lista de sucursales del comercio.</returns>
        public List<Sucursal> GetByComercio(int idComercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales
                    .Where(s => s.IdComercio == idComercio)
                    .ToList();
            }
        }

        /// <summary>Obtiene una sucursal por su identificador.</summary>
        /// <param name="id">Identificador de la sucursal.</param>
        /// <returns>Sucursal encontrada o null.</returns>
        public Sucursal GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales.FirstOrDefault(s => s.IdSucursal == id);
            }
        }

        /// <summary>Inserta una nueva sucursal.</summary>
        /// <param name="sucursal">Sucursal a insertar.</param>
        public void Insertar(Sucursal sucursal)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                sucursal.FechaCreacion = DateTime.Now;
                sucursal.Activo = true;
                ctx.Sucursales.Add(sucursal);
                ctx.SaveChanges();
            }
        }

        /// <summary>Actualiza los datos de una sucursal existente.</summary>
        /// <param name="sucursal">Sucursal con los datos actualizados.</param>
        public void Actualizar(Sucursal sucursal)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var existing = ctx.Sucursales.Find(sucursal.IdSucursal);
                if (existing != null)
                {
                    existing.Nombre = sucursal.Nombre;
                    existing.Ubicacion = sucursal.Ubicacion;
                    existing.Activo = sucursal.Activo;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>Activa o desactiva una sucursal.</summary>
        /// <param name="id">Identificador de la sucursal.</param>
        /// <param name="activo">Estado de activación.</param>
        public void CambiarEstado(int id, bool activo)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var sucursal = ctx.Sucursales.Find(id);
                if (sucursal != null)
                {
                    sucursal.Activo = activo;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>Verifica si existe otra sucursal con el mismo nombre en el comercio.</summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <param name="idExcluir">Id de la sucursal a excluir de la búsqueda.</param>
        /// <returns><c>true</c> si el nombre ya existe.</returns>
        public bool ExisteNombre(string nombre, int idComercio, int? idExcluir = null)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales.Any(s =>
                    s.Nombre.ToLower() == nombre.ToLower()
                    && s.IdComercio == idComercio
                    && (idExcluir == null || s.IdSucursal != idExcluir));
            }
        }
    }
}