using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de elementos reservables (canchas, salas, mesas, etc.).
    /// </summary>
    public class ElementoReservableBLL
    {
        private readonly ElementoReservableRepository _repo = new ElementoReservableRepository();
        private readonly SucursalRepository _sucursalRepo = new SucursalRepository();

        /// <summary>
        /// Obtiene los elementos reservables de un comercio específico.
        /// </summary>
        /// <param name="idComercio">Identificador del comercio.</param>
        /// <returns>Lista de elementos del comercio.</returns>
        public List<ElementoReservable> ListarPorComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio);
        }

        /// <summary>
        /// Obtiene los elementos reservables de una sucursal específica.
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal.</param>
        /// <returns>Lista de elementos de la sucursal.</returns>
        public List<ElementoReservable> ListarPorSucursal(int idSucursal)
        {
            return _repo.GetBySucursal(idSucursal);
        }

        /// <summary>
        /// Obtiene los elementos que el usuario actual puede gestionar: todos para Admin, solo los propios para Comercio.
        /// </summary>
        /// <returns>Lista de elementos visibles para el usuario actual.</returns>
        /// <exception cref="Exception">Si no hay sesión iniciada.</exception>
        public List<ElementoReservable> ListarDelUsuarioActual()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");

            if (usuario.Rol == "Admin")
                return _repo.GetAll();

            var propios = new ComercioRepository().GetByUsuario(usuario.IdUsuario)
                .Select(c => c.IdComercio).ToHashSet();
            return _repo.GetAll().Where(e => propios.Contains(e.Sucursal.IdComercio)).ToList();
        }

        /// <summary>
        /// Obtiene un elemento reservable por su identificador.
        /// </summary>
        /// <param name="id">Identificador del elemento.</param>
        /// <returns>El elemento encontrado o null.</returns>
        public ElementoReservable ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Crea un nuevo elemento reservable validando nombre, precio, seña, capacidad y sucursal.
        /// </summary>
        /// <param name="elemento">Datos del elemento a crear.</param>
        /// <exception cref="Exception">Si alguna validación falla o ya existe un elemento con el mismo nombre en la sucursal.</exception>
        public void Crear(ElementoReservable elemento)
        {
            if (string.IsNullOrWhiteSpace(elemento.Nombre))
                throw new Exception("El nombre del elemento es obligatorio.");

            if (elemento.PrecioBase <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (elemento.PorcentajeSeña < 0 || elemento.PorcentajeSeña > 100)
                throw new Exception("El porcentaje de seña debe estar entre 0 y 100.");

            if (elemento.Capacidad < 1)
                throw new Exception("La capacidad debe ser al menos 1.");

            ValidarSucursal(elemento.IdSucursal);

            if (_repo.ExisteNombre(elemento.Nombre, elemento.IdSucursal))
                throw new Exception("Ya existe un elemento con ese nombre en esta sucursal.");

            _repo.Insertar(elemento);

            BitacoraService.Registrar("Alta", "ElementoReservable",
                $"Se creó el elemento: {elemento.Nombre}");
        }

        /// <summary>
        /// Actualiza un elemento reservable existente con las mismas validaciones que la creación.
        /// </summary>
        /// <param name="elemento">Elemento con los datos actualizados.</param>
        /// <exception cref="Exception">Si alguna validación falla.</exception>
        public void Actualizar(ElementoReservable elemento)
        {
            if (string.IsNullOrWhiteSpace(elemento.Nombre))
                throw new Exception("El nombre del elemento es obligatorio.");

            if (elemento.PrecioBase <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (elemento.PorcentajeSeña < 0 || elemento.PorcentajeSeña > 100)
                throw new Exception("El porcentaje de seña debe estar entre 0 y 100.");

            ValidarSucursal(elemento.IdSucursal);

            if (_repo.ExisteNombre(elemento.Nombre, elemento.IdSucursal, elemento.IdElemento))
                throw new Exception("Ya existe un elemento con ese nombre en esta sucursal.");

            _repo.Actualizar(elemento);

            BitacoraService.Registrar("Modificación", "ElementoReservable",
                $"Se modificó el elemento: {elemento.Nombre}");
        }

        /// <summary>
        /// Valida que la sucursal exista y esté activa.
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal a validar.</param>
        /// <exception cref="Exception">Si la sucursal no existe o está inactiva.</exception>
        private void ValidarSucursal(int idSucursal)
        {
            var sucursal = _sucursalRepo.GetById(idSucursal);
            if (sucursal == null)
                throw new Exception("Debe seleccionar una sucursal.");
            if (!sucursal.Activo)
                throw new Exception("La sucursal seleccionada está inactiva.");
        }

        /// <summary>
        /// Cambia el estado de un elemento reservable.
        /// </summary>
        /// <param name="id">Identificador del elemento.</param>
        /// <param name="nuevoEstado">Nuevo estado a asignar (Activo/Inactivo).</param>
        public void CambiarEstado(int id, string nuevoEstado)
        {
            _repo.CambiarEstado(id, nuevoEstado);

            BitacoraService.Registrar(nuevoEstado == "Activo" ? "Habilitación" : "Baja",
                "ElementoReservable",
                $"Elemento ID {id} → {nuevoEstado}");
        }

        /// <summary>
        /// Obtiene todos los elementos reservables del sistema.
        /// </summary>
        /// <returns>Lista completa de elementos.</returns>
        public List<ElementoReservable> ListarTodos()
        {
            return _repo.GetAll();
        }
    }
}
