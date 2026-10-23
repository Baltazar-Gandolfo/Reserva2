using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    public class SlotCalendario
    {
        public Disponibilidad Slot { get; set; }
        public Reserva Reserva { get; set; }

        // Libre, Pendiente de seña, Confirmada o Bloqueada
        public string EstadoVisual { get; set; }
    }

    // CU.Com.012 — Ver Calendario de Disponibilidad (y consulta de slots para el cliente)
    public class DisponibilidadBLL
    {
        private readonly DisponibilidadRepository _repo = new DisponibilidadRepository();
        private readonly GeneradorSlotsBLL _generador = new GeneradorSlotsBLL();

        public List<Disponibilidad> ListarPorElemento(int idElemento, DateTime desde, DateTime hasta)
        {
            return _repo.GetByElemento(idElemento, desde, hasta);
        }

        public List<Disponibilidad> ListarProximosDias(int idElemento)
        {
            return _repo.GetByElemento(idElemento, DateTime.Today,
                DateTime.Today.AddDays(GeneradorSlotsBLL.DiasHorizonte));
        }

        public List<Disponibilidad> ListarLibres(int idElemento)
        {
            _generador.GenerarFaltantes(idElemento);
            var ahora = DateTime.Now;
            return ListarProximosDias(idElemento)
                .Where(d => d.Estado == EstadoSlot.Libre && d.Fecha + d.HoraInicio > ahora)
                .ToList();
        }

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

        // Bloquea un turno libre (cerrado por el comercio) o lo vuelve a liberar.
        public string AlternarBloqueo(int idDisponibilidad)
        {
            string estado = _repo.AlternarBloqueo(idDisponibilidad)
                ?? throw new Exception("El turno está reservado: no se puede bloquear.");

            Services.Facade.BitacoraService.Registrar(
                estado == EstadoSlot.Bloqueada ? "Bloqueo" : "Desbloqueo", "Disponibilidad",
                $"Turno ID {idDisponibilidad} → {estado}", idDisponibilidad);
            return estado;
        }

        public Disponibilidad ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }
    }
}
