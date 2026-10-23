using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Reserva2.BLL.Logic
{
    // CU.Sis.010 — Generar Slots Automáticamente
    public class GeneradorSlotsBLL
    {
        public const int DiasHorizonte = 30;

        private readonly DisponibilidadRepository _dispRepo = new DisponibilidadRepository();
        private readonly ElementoReservableRepository _elementoRepo = new ElementoReservableRepository();
        private readonly ReglaDisponibilidadRepository _reglaRepo = new ReglaDisponibilidadRepository();

        // Completa los slots faltantes de todas las reglas activas del elemento (idempotente).
        // Mantiene el horizonte de DiasHorizonte días siempre cubierto y cubre reglas cargadas fuera de la app.
        public int GenerarFaltantes(int idElemento)
        {
            int creados = 0;
            foreach (var regla in _reglaRepo.GetByElemento(idElemento))
                creados += Generar(regla);
            return creados;
        }

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

                // Intervalos de la duración indicada; el último tiene que entrar completo en la franja
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
