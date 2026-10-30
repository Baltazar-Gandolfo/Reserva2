using System;

namespace Reserva2.BLL.Logic
{
    public static class DiasSemana
    {
        // Mismo formato (sin tildes) que los datos de ejemplo del script SQL.
        public static readonly string[] Todos =
            { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };

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
