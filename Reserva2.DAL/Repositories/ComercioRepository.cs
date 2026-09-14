using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Reserva2.BE.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Reserva2.DAL.Repositories
{
    public class ComercioRepository
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["NegocioDB"].ConnectionString;
        }

        public List<Comercio> GetAll()
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Comercios.ToList();
            }
        }

        public List<Comercio> GetByUsuario(Guid idUsuario)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Comercios.Where(c => c.IdUsuario == idUsuario).ToList();
            }
        }

        public Comercio GetById(int id)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                return ctx.Comercios.FirstOrDefault(c => c.IdComercio == id);
            }
        }

        public void Insertar(Comercio comercio)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                comercio.FechaCreacion = DateTime.Now;
                comercio.Activo = true;
                ctx.Comercios.Add(comercio);
                ctx.SaveChanges();
            }
        }

        public void Actualizar(Comercio comercio)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
            {
                ctx.Comercios.Update(comercio);
                ctx.SaveChanges();
            }
        }

        public void CambiarEstado(int id, bool activo)
        {
            using (var ctx = new NegocioDbContext(GetConnectionString()))
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