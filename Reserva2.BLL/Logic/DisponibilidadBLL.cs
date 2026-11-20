using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Lógica de negocio para la consulta y gestión de disponibilidad de turnos.
    /// Implementa CU.Com.012 — Ver Calendario de Disponibilidad.
    /// </summary>
    public class DisponibilidadBLL
    {
        private readonly DisponibilidadRepository _repo = new DisponibilidadRepository();
        private readonly GeneradorSlotsBLL _generador = new GeneradorSlotsBLL();

        /// <summary>
        /// Obtiene los slots de disponibilidad de un elemento dentro de un rango de fechas.
        /// </summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <param name="desde">Fecha de inicio del rango.</param>
        /// <param name="hasta">Fecha de fin del rango.</param>
        /// <returns>Lista de slots en el rango indicado.</returns>
        public List<Disponibilidad> ListarPorElemento(int idElemento, DateTime desde, DateTime hasta)
        {
            return _repo.GetByElemento(idElemento, desde, hasta);
        }

        /// <summary>
        /// Obtiene los slots de los próximos días según el horizonte configurado.
        /// </summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <returns>Lista de slots dentro del horizonte de generación.</returns>
        public List<Disponibilidad> ListarProximosDias(int idElemento)
        {
            return _repo.GetByElemento(idElemento, DateTime.Today,
                DateTime.Today.AddDays(GeneradorSlotsBLL.DiasHorizonte));
        }

        /// <summary>
        /// Obtiene los slots libres futuros de un elemento, generando los faltantes previamente.
        /// </summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <returns>Lista de slots libres cuyo horario aún no pasó.</returns>
        public List<Disponibilidad> ListarLibres(int idElemento)
        {
            _generador.GenerarFaltantes(idElemento);
            var ahora = DateTime.Now;
            return ListarProximosDias(idElemento)
                .Where(d => d.Estado == EstadoSlot.Libre && d.Fecha + d.HoraInicio > ahora)
                .ToList();
        }

        /// <summary>
        /// Obtiene el calendario de slots con su estado visual (Libre, Bloqueada, Confirmada, Pendiente de seña).
        /// Genera slots faltantes y expira reservas vencidas antes de armar el calendario.
        /// </summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <param name="desde">Fecha de inicio del rango.</param>
        /// <param name="hasta">Fecha de fin del rango.</param>
        /// <returns>Lista de slots con información visual para el calendario.</returns>
        public List<SlotCalendario> ObtenerCalendario(int idElemento, DateTime desde, DateTime hasta)
        {
            new Reservas.ReservaBLL().ExpirarVencidas();
            _generador.GenerarFaltantes(idElemento);

            var reservas = _repo.GetReservasVigentes(idElemento, desde, hasta);
            var resultado = new List<SlotCalendario>();

            foreach (var slot in _repo.GetByElemento(idElemento, desde, hasta))
            {
                reservas.TryGetValue(slot.IdDisponibilidad, out var reserva);
                string visual = slot.Estado switch
                {
                    EstadoSlot.Libre => "Libre",
                    EstadoSlot.Bloqueada => "Bloqueada",
                    _ => reserva?.Estado == EstadoReserva.Confirmada ? "Confirmada" : "Pendiente de seña"
                };
                resultado.Add(new SlotCalendario { Slot = slot, Reserva = reserva, EstadoVisual = visual });
            }
            return resultado;
        }

        /// <summary>
        /// Alterna el bloqueo de un turno libre (lo bloquea o lo vuelve a liberar).
        /// No se puede bloquear un turno que tiene reserva activa.
        /// </summary>
        /// <param name="idDisponibilidad">Identificador del slot de disponibilidad.</param>
        /// <returns>El nuevo estado del slot.</returns>
        /// <exception cref="Exception">Si el turno está reservado.</exception>
        public string AlternarBloqueo(int idDisponibilidad)
        {
            string estado = _repo.AlternarBloqueo(idDisponibilidad)
                ?? throw new Exception("El turno está reservado: no se puede bloquear.");

            Services.Facade.BitacoraService.Registrar(
                estado == EstadoSlot.Bloqueada ? "Bloqueo" : "Desbloqueo", "Disponibilidad",
                $"Turno ID {idDisponibilidad} → {estado}", idDisponibilidad);
            return estado;
        }

        /// <summary>
        /// Obtiene un slot de disponibilidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador del slot.</param>
        /// <returns>El slot encontrado o null.</returns>
        public Disponibilidad ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }
    }
}
