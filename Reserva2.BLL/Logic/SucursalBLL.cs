using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de sucursales de un comercio.
    /// </summary>
    public class SucursalBLL
    {
        private readonly SucursalRepository _repo = new SucursalRepository();

        /// <summary>
        /// Obtiene todas las sucursales del sistema.
        /// </summary>
        /// <returns>Lista completa de sucursales.</returns>
        public List<Sucursal> ListarTodas()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Obtiene las sucursales de un comercio específico.
        /// </summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <returns>Lista de sucursales del comercio.</returns>
        public List<Sucursal> ListarPorComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio);
        }

        /// <summary>
        /// Obtiene una sucursal por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la sucursal.</param>
        /// <returns>La sucursal encontrada o null.</returns>
        public Sucursal ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Crea una nueva sucursal validando nombre y unicidad dentro del comercio.
        /// </summary>
        /// <param name="sucursal">Datos de la sucursal a crear.</param>
        /// <exception cref="Exception">Si el nombre está vacío o ya existe en el comercio.</exception>
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

        /// <summary>
        /// Actualiza los datos de una sucursal existente.
        /// </summary>
        /// <param name="sucursal">Sucursal con los datos actualizados.</param>
        /// <exception cref="Exception">Si el nombre está vacío o ya existe en el comercio.</exception>
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

        /// <summary>
        /// Cambia el estado de una sucursal entre activa e inactiva.
        /// </summary>
        /// <param name="id">Identificador de la sucursal.</param>
        /// <param name="activo">True para habilitar, false para dar de baja.</param>
        public void CambiarEstado(int id, bool activo)
        {
            _repo.CambiarEstado(id, activo);
            string accion = activo ? "Habilitación" : "Baja";
            BitacoraService.Registrar(accion, "Sucursal",
                $"Sucursal ID {id} → {(activo ? "Activa" : "Inactiva")}");
        }
    }
}
