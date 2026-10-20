using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    public class ElementoReservableBLL
    {
        private readonly ElementoReservableRepository _repo = new ElementoReservableRepository();
        private readonly SucursalRepository _sucursalRepo = new SucursalRepository();

        public List<ElementoReservable> ListarPorComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio);
        }

        public List<ElementoReservable> ListarPorSucursal(int idSucursal)
        {
            return _repo.GetBySucursal(idSucursal);
        }

        // Elementos con los que puede operar el usuario actual: todos (Admin) o los de sus comercios.
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

        public ElementoReservable ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

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

        private void ValidarSucursal(int idSucursal)
        {
            var sucursal = _sucursalRepo.GetById(idSucursal);
            if (sucursal == null)
                throw new Exception("Debe seleccionar una sucursal.");
            if (!sucursal.Activo)
                throw new Exception("La sucursal seleccionada está inactiva.");
        }

        public void CambiarEstado(int id, string nuevoEstado)
        {
            _repo.CambiarEstado(id, nuevoEstado);

            BitacoraService.Registrar(nuevoEstado == "Activo" ? "Habilitación" : "Baja",
                "ElementoReservable",
                $"Elemento ID {id} → {nuevoEstado}");
        }

        public List<ElementoReservable> ListarTodos()
        {
            return _repo.GetAll();
        }
    }
}