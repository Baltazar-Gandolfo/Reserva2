using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Generador automático de slots de disponibilidad.
    /// Implementa CU.Sis.010 — Generar Slots Automáticamente.
    /// </summary>
    public class GeneradorSlotsBLL
    {
        /// <summary>
        /// Cantidad de días hacia adelante que se mantienen cubiertos con slots.
        /// </summary>
        public const int DiasHorizonte = 30;

        private readonly DisponibilidadRepository _dispRepo = new DisponibilidadRepository();
        private readonly ElementoReservableRepository _elementoRepo = new ElementoReservableRepository();
        private readonly ReglaDisponibilidadRepository _reglaRepo = new ReglaDisponibilidadRepository();

        /// <summary>
        /// Completa los slots faltantes de todas las reglas activas del elemento (idempotente).
        /// Mantiene el horizonte de <see cref="DiasHorizonte"/> días siempre cubierto.
        /// </summary>
        /// <param name="idElemento">Identificador del elemento reservable.</param>
        /// <returns>Cantidad total de slots creados.</returns>
        public int GenerarFaltantes(int idElemento)
        {
            int creados = 0;
            foreach (var regla in _reglaRepo.GetByElemento(idElemento))
                creados += Generar(regla);
            return creados;
        }

        /// <summary>
        /// Genera los slots para una regla de disponibilidad específica dentro del horizonte.
        /// Usa el precio override de la regla o el precio base del elemento si no tiene.
        /// </summary>
        /// <param name="regla">Regla de disponibilidad a procesar.</param>
        /// <returns>Cantidad de slots nuevos creados.</returns>
        /// <exception cref="Exception">Si el elemento de la regla no existe.</exception>
        public int Generar(ReglaDisponibilidad regla)
        {
            var elemento = _elementoRepo.GetById(regla.IdElemento)
                ?? throw new Exception("El elemento de la regla no existe.");

            // FA 2.2.1 / FA 2.2.2: precio override o, si no tiene, el precio base del elemento
            decimal precio = regla.PrecioOverride ?? elemento.PrecioBase;
            var duracion = TimeSpan.FromMinutes(regla.DuracionSlot);
            var ahora = DateTime.Now;
            var slots = new List<Disponibilidad>();

            for (int i = 0; i < DiasHorizonte; i++)
            {
                DateTime fecha = DateTime.Today.AddDays(i);
                if (DiasSemana.Desde(fecha.DayOfWeek) != regla.DiaSemana)
                    continue;

                for (var inicio = regla.HoraInicio; inicio + duracion <= regla.HoraFin; inicio += duracion)
                {
                    if (fecha + inicio <= ahora)
                        continue;

                    slots.Add(new Disponibilidad
                    {
                        Fecha = fecha,
                        HoraInicio = inicio,
                        HoraFin = inicio + duracion,
                        Precio = precio,
                        Estado = EstadoSlot.Libre,
                        IdElemento = regla.IdElemento
                    });
                }
            }

            return _dispRepo.InsertarNuevos(slots);
        }
    }
}
