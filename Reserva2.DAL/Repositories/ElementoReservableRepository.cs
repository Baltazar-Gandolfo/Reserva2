using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ElementoReservableRepository
    {
        public List<ElementoReservable> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .ToList();
            }
        }

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

        public ElementoReservable GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Elementos
                    .Include(e => e.Sucursal)
                    .FirstOrDefault(e => e.IdElemento == id);
            }
        }

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
