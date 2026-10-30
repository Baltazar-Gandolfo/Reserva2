using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    public class BloqueHorario
    {
        public string DiaSemana { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fin { get; set; }
    }

    // CU.Com.011 — Crear Regla de Disponibilidad
    public class ReglaDisponibilidadBLL
    {
        private readonly ReglaDisponibilidadRepository _repo = new ReglaDisponibilidadRepository();
        private readonly ElementoReservableRepository _elementoRepo = new ElementoReservableRepository();
        private readonly GeneradorSlotsBLL _generador = new GeneradorSlotsBLL();

        public List<ReglaDisponibilidad> ListarPorElemento(int idElemento)
        {
            return _repo.GetByElemento(idElemento);
        }

        // Devuelve la cantidad de slots generados.
        public int Crear(ReglaDisponibilidad regla)
        {
            var elemento = _elementoRepo.GetById(regla.IdElemento);
            if (elemento == null)
                throw new Exception("Debe seleccionar un elemento.");
            if (elemento.Estado != "Activo")
                throw new Exception("El elemento seleccionado está inactivo.");

            if (!DiasSemana.Todos.Contains(regla.DiaSemana))
                throw new Exception("Debe seleccionar un día de la semana.");

            if (regla.HoraFin <= regla.HoraInicio)
                throw new Exception("La hora de fin debe ser posterior a la hora de inicio.");

            if (regla.DuracionSlot <= 0)
                throw new Exception("La duración del slot debe ser mayor a cero.");

            if (TimeSpan.FromMinutes(regla.DuracionSlot) > regla.HoraFin - regla.HoraInicio)
                throw new Exception("La duración del slot es mayor a la franja horaria.");

            if (regla.PrecioOverride.HasValue && regla.PrecioOverride.Value <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            // FA 6.2
            if (_repo.ExisteSuperposicion(regla.IdElemento, regla.DiaSemana, regla.HoraInicio, regla.HoraFin))
                throw new Exception("La regla se superpone con otra regla del mismo día.");

            regla.Activo = true;
            _repo.Insertar(regla);

            BitacoraService.Registrar("Alta", "ReglaDisponibilidad",
                $"Se creó regla {regla.DiaSemana} {regla.HoraInicio:hh\\:mm}-{regla.HoraFin:hh\\:mm} para el elemento: {elemento.Nombre}",
                regla.IdReglaDisponibilidad);

            int creados = _generador.Generar(regla);

            BitacoraService.Registrar("Alta", "Disponibilidad",
                $"Se generaron {creados} slots para el elemento: {elemento.Nombre} ({regla.DiaSemana})",
                regla.IdElemento);

            return creados;
        }

        // Reemplaza el horario semanal completo del elemento (grilla de la pantalla de disponibilidad).
        // Cada bloque es una franja continua de un día; la duración del slot es la del elemento.
        public int GuardarHorarioSemanal(int idElemento, List<BloqueHorario> bloques)
        {
            var elemento = _elementoRepo.GetById(idElemento);
            if (elemento == null)
                throw new Exception("Debe seleccionar un elemento.");
            if (elemento.Estado != "Activo")
                throw new Exception("El elemento seleccionado está inactivo.");

            var reglas = new List<ReglaDisponibilidad>();
            foreach (var b in bloques)
            {
                if (!DiasSemana.Todos.Contains(b.DiaSemana))
                    throw new Exception("Día de la semana inválido.");
                if (b.Fin <= b.Inicio)
                    throw new Exception("La hora de fin debe ser posterior a la hora de inicio.");
                if (TimeSpan.FromMinutes(elemento.DuracionSlot) > b.Fin - b.Inicio)
                    throw new Exception("Hay una franja más corta que la duración del turno.");

                reglas.Add(new ReglaDisponibilidad
                {
                    DiaSemana = b.DiaSemana,
                    HoraInicio = b.Inicio,
                    HoraFin = b.Fin,
                    DuracionSlot = elemento.DuracionSlot,
                    Activo = true,
                    IdElemento = idElemento
                });
            }

            // Se descartan los slots libres futuros que quedaron fuera del nuevo horario
            _repo.ReemplazarReglas(idElemento, reglas, (dia, ini, fin) =>
                reglas.Any(r => r.DiaSemana == dia && ini >= r.HoraInicio && fin <= r.HoraFin));

            int creados = 0;
            foreach (var regla in reglas)
                creados += _generador.Generar(regla);

            BitacoraService.Registrar("Modificación", "Disponibilidad",
                $"Se actualizó el horario semanal de {elemento.Nombre}: {reglas.Count} franjas, {creados} slots nuevos",
                idElemento);

            return creados;
        }
    }
}
