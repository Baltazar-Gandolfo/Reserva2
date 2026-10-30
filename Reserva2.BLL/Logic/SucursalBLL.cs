using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    public class SucursalBLL
    {
        private readonly SucursalRepository _repo = new SucursalRepository();

        public List<Sucursal> ListarTodas()
        {
            return _repo.GetAll();
        }

        public List<Sucursal> ListarPorComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio);
        }

        public Sucursal ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

        public void Crear(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.Nombre))
                throw new Exception("El nombre de la sucursal es obligatorio.");

            if (_repo.ExisteNombre(sucursal.Nombre, sucursal.IdComercio))
                throw new Exception("Ya existe una sucursal con ese nombre en este comercio.");

            _repo.Insertar(sucursal);

            BitacoraService.Registrar("Alta", "Sucursal",
                $"Se creó la sucursal: {sucursal.Nombre}");
        }

        public void Actualizar(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.Nombre))
                throw new Exception("El nombre de la sucursal es obligatorio.");

            if (_repo.ExisteNombre(sucursal.Nombre, sucursal.IdComercio, sucursal.IdSucursal))
                throw new Exception("Ya existe una sucursal con ese nombre en este comercio.");

            _repo.Actualizar(sucursal);

            BitacoraService.Registrar("Modificación", "Sucursal",
                $"Se modificó la sucursal: {sucursal.Nombre}");
        }

        public void CambiarEstado(int id, bool activo)
        {
            _repo.CambiarEstado(id, activo);
            string accion = activo ? "Habilitación" : "Baja";
            BitacoraService.Registrar(accion, "Sucursal",
                $"Sucursal ID {id} → {(activo ? "Activa" : "Inactiva")}");
        }
    }
}