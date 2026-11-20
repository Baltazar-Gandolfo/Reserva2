using System;

namespace Reserva2.BE.Auxiliares
{
    /// <summary>
    /// Utilidad para manejar los días de la semana en formato sin tildes (compatible con la base de datos).
    /// </summary>
    public static class DiasSemana
    {
        /// <summary>Array con todos los días de la semana en español sin tildes.</summary>
        public static readonly string[] Todos =
            { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };

        /// <summary>
        /// Convierte un <see cref="DayOfWeek"/> de .NET al nombre en español sin tildes.
        /// </summary>
        /// <param name="dia">Día de la semana del enum de .NET.</param>
        /// <returns>Nombre del día en español sin tildes.</returns>
        public static string Desde(DayOfWeek dia)
        {
            return dia switch
            {
                DayOfWeek.Monday => "Lunes",
                DayOfWeek.Tuesday => "Martes",
                DayOfWeek.Wednesday => "Miercoles",
                DayOfWeek.Thursday => "Jueves",
                DayOfWeek.Friday => "Viernes",
                DayOfWeek.Saturday => "Sabado",
                _ => "Domingo"
            };
        }
    }
}
