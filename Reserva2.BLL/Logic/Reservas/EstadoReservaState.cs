using Reserva2.BE.Entities;
using System;

namespace Reserva2.BLL.Logic.Reservas
{
    // Patrón State: cada estado define qué transiciones permite.
    public abstract class EstadoReservaState
    {
        public abstract string Nombre { get; }

        public virtual void ValidarConfirmar() =>
            throw new Exception($"No se puede confirmar una reserva {Nombre.ToLower()}.");

        public virtual void ValidarCancelar() =>
            throw new Exception($"No se puede cancelar una reserva {Nombre.ToLower()}.");

        public virtual void ValidarExpirar() =>
            throw new Exception($"No se puede expirar una reserva {Nombre.ToLower()}.");

        public static EstadoReservaState De(string estado)
        {
            return estado switch
            {
                EstadoReserva.Pendiente => new EstadoPendiente(),
                EstadoReserva.Confirmada => new EstadoConfirmada(),
                EstadoReserva.Cancelada => new EstadoCancelada(),
                EstadoReserva.Expirada => new EstadoExpirada(),
                _ => throw new Exception($"Estado de reserva desconocido: {estado}.")
            };
        }
    }

    public class EstadoPendiente : EstadoReservaState
    {
        public override string Nombre => EstadoReserva.Pendiente;
        public override void ValidarConfirmar() { }
        public override void ValidarExpirar() { }
    }

    public class EstadoConfirmada : EstadoReservaState
    {
        public override string Nombre => EstadoReserva.Confirmada;
        public override void ValidarCancelar() { }
    }

    public class EstadoCancelada : EstadoReservaState
    {
        public override string Nombre => EstadoReserva.Cancelada;
    }

    public class EstadoExpirada : EstadoReservaState
    {
        public override string Nombre => EstadoReserva.Expirada;
    }
}
