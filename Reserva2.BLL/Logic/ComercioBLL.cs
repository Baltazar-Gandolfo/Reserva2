using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    public class ComercioBLL
    {
        private readonly ComercioRepository _repo = new ComercioRepository();

        public List<Comercio> ListarTodos()
        {
            return _repo.GetAll();
        }

        public List<Comercio> ListarPorOwner()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;
            return _repo.GetByUsuario(usuario.IdUsuario);
        }

        public Comercio ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

        public void Crear(Comercio comercio)
        {
            if (string.IsNullOrWhiteSpace(comercio.Nombre))
                throw new Exception("El nombre del comercio es obligatorio.");

            var usuario = SessionManager.GetInstance().UsuarioActual;
            comercio.IdUsuario = usuario.IdUsuario;
            comercio.Slug = comercio.Nombre.ToLower().Replace(" ", "-");

            _repo.Insertar(comercio);

            BitacoraService.Registrar("Alta", "Comercio", $"Se creó el comercio: {comercio.Nombre}");
        }

        public void Actualizar(Comercio comercio)
        {
            if (string.IsNullOrWhiteSpace(comercio.Nombre))
                throw new Exception("El nombre del comercio es obligatorio.");

            comercio.Slug = comercio.Nombre.ToLower().Replace(" ", "-");
            _repo.Actualizar(comercio);

            BitacoraService.Registrar("Modificación", "Comercio", $"Se modificó el comercio: {comercio.Nombre}");
        }

        public void CambiarEstado(int id, bool activo)
        {
            _repo.CambiarEstado(id, activo);
            string accion = activo ? "Habilitación" : "Baja";
            BitacoraService.Registrar(accion, "Comercio", $"Comercio ID {id} → {(activo ? "Activo" : "Inactivo")}");
        }
    }
}