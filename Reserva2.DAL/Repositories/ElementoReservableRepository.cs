using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ElementoReservableRepository
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["NegocioDB"].ConnectionString;
        }

        public List<ElementoReservable> GetByComercio(int idComercio)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Elementos
                    .Where(e => e.IdComercio == idComercio)
                    .ToList();
            }
        }

        public ElementoReservable GetById(int id)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Elementos.FirstOrDefault(e => e.IdElemento == id);
            }
        }

        public void Insertar(ElementoReservable elemento)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                elemento.FechaCreacion = DateTime.Now;
                elemento.Estado = "Activo";
                ctx.Elementos.Add(elemento);
                ctx.SaveChanges();
            }
        }

        public void Actualizar(ElementoReservable elemento)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
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
                    ctx.SaveChanges();
                }
            }
        }

        public void CambiarEstado(int id, string estado)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                var elemento = ctx.Elementos.Find(id);
                if (elemento != null)
                {
                    elemento.Estado = estado;
                    ctx.SaveChanges();
                }
            }
        }

        public bool ExisteNombre(string nombre, int idComercio, int? idExcluir = null)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Elementos.Any(e =>
                    e.Nombre.ToLower() == nombre.ToLower()
                    && e.IdComercio == idComercio
                    && (idExcluir == null || e.IdElemento != idExcluir));
            }
        }
        public List<ElementoReservable> GetAll()
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Elementos.ToList();
            }
        }
    }
}