using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para elementos reservables.
    /// </summary>
    public class ElementoReservableRepository
    {
        /// <summary>Obtiene todos los elementos reservables con su sucursal.</summary>
        /// <returns>Lista de elementos.</returns>
        public List<ElementoReservable> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .ToList();
            }
        }

        /// <summary>Obtiene los elementos de un comercio.</summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <returns>Lista de elementos del comercio.</returns>
        public List<ElementoReservable> GetByComercio(int idComercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .Where(e => e.Sucursal.IdComercio == idComercio)
                    .ToList();
            }
        }

        /// <summary>Obtiene los elementos de una sucursal.</summary>
        /// <param name="idSucursal">Identificador de la sucursal.</param>
        /// <returns>Lista de elementos de la sucursal.</returns>
        public List<ElementoReservable> GetBySucursal(int idSucursal)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .Where(e => e.IdSucursal == idSucursal)
                    .ToList();
            }
        }

        /// <summary>Obtiene un elemento por su identificador con su sucursal.</summary>
        /// <param name="id">Identificador del elemento.</param>
        /// <returns>Elemento encontrado o null.</returns>
        public ElementoReservable GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .FirstOrDefault(e => e.IdElemento == id);
            }
        }

        /// <summary>Inserta un nuevo elemento reservable.</summary>
        /// <param name="elemento">Elemento a insertar.</param>
        public void Insertar(ElementoReservable elemento)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                elemento.Sucursal = null;
                elemento.FechaCreacion = DateTime.Now;
                elemento.Estado = "Activo";
                ctx.Elementos.Add(elemento);
                ctx.SaveChanges();
            }
        }

        /// <summary>Actualiza los datos de un elemento existente.</summary>
        /// <param name="elemento">Elemento con los datos actualizados.</param>
        public void Actualizar(ElementoReservable elemento)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var existing = ctx.Elementos.Find(elemento.IdElemento);
                if (existing != null)
                {
                    existing.Nombre = elemento.Nombre;
                    existing.Descripcion = elemento.Descripcion;
                    existing.Tipo = elemento.Tipo;
                    existing.PrecioBase = elemento.PrecioBase;
                    existing.Capacidad = elemento.Capacidad;
                    existing.DuracionSlot = elemento.DuracionSlot;
                    existing.PorcentajeSeña = elemento.PorcentajeSeña;
                    existing.Estado = elemento.Estado;
                    existing.IdSucursal = elemento.IdSucursal;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>Cambia el estado de un elemento reservable.</summary>
        /// <param name="id">Identificador del elemento.</param>
        /// <param name="estado">Nuevo estado.</param>
        public void CambiarEstado(int id, string estado)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                var elemento = ctx.Elementos.Find(id);
                if (elemento != null)
                {
                    elemento.Estado = estado;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>Verifica si existe otro elemento con el mismo nombre en la sucursal.</summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <param name="idSucursal">Identificador de la sucursal.</param>
        /// <param name="idExcluir">Id del elemento a excluir de la búsqueda.</param>
        /// <returns><c>true</c> si el nombre ya existe.</returns>
        public bool ExisteNombre(string nombre, int idSucursal, int? idExcluir = null)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos.Any(e =>
                    e.Nombre.ToLower() == nombre.ToLower()
                    && e.IdSucursal == idSucursal
                    && (idExcluir == null || e.IdElemento != idExcluir));
            }
        }
    }
}
