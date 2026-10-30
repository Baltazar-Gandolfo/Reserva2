using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class SucursalRepository
    {
        public List<Sucursal> GetAll()
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales.ToList();
            }
        }

        public List<Sucursal> GetByComercio(int idComercio)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales
                    .Where(s => s.IdComercio == idComercio)
                    .ToList();
            }
        }

        public Sucursal GetById(int id)
        {
            using (var ctx = NegocioContextFactory.Crear())
            {
                return ctx.Sucursales.FirstOrDefault(s => s.IdSucursal == id);
            }
        }

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