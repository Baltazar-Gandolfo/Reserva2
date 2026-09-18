using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    public class ElementoReservableBLL
    {
        private readonly ElementoReservableRepository _repo = new ElementoReservableRepository();

        public List<ElementoReservable> ListarPorComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio);
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

            if (_repo.ExisteNombre(elemento.Nombre, elemento.IdComercio))
                throw new Exception("Ya existe un elemento con ese nombre en este comercio.");

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

            if (_repo.ExisteNombre(elemento.Nombre, elemento.IdComercio, elemento.IdElemento))
                throw new Exception("Ya existe un elemento con ese nombre en este comercio.");

            _repo.Actualizar(elemento);

            BitacoraService.Registrar("Modificación", "ElementoReservable",
                $"Se modificó el elemento: {elemento.Nombre}");
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