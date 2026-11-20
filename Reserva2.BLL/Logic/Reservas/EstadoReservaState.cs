using Reserva2.BE.Auxiliares;
using System;

namespace Reserva2.BLL.Logic.Reservas
{
    /// <summary>
    /// Clase base abstracta del patrón State para los estados de una reserva.
    /// Define las transiciones permitidas; las subclases habilitan las que corresponden.
    /// </summary>
    public abstract class EstadoReservaState
    {
        /// <summary>
        /// Nombre del estado.
        /// </summary>
        public abstract string Nombre { get; }

        /// <summary>
        /// Valida si la reserva puede pasar a Confirmada. Lanza excepción si no es válido.
        /// </summary>
        /// <exception cref="Exception">Si la transición no está permitida desde este estado.</exception>
        public virtual void ValidarConfirmar() =>
            throw new Exception($"No se puede confirmar una reserva {Nombre.ToLower()}.");

        /// <summary>
        /// Valida si la reserva puede pasar a Cancelada. Lanza excepción si no es válido.
        /// </summary>
        /// <exception cref="Exception">Si la transición no está permitida desde este estado.</exception>
        public virtual void ValidarCancelar() =>
            throw new Exception($"No se puede cancelar una reserva {Nombre.ToLower()}.");

        /// <summary>
        /// Valida si la reserva puede pasar a Expirada. Lanza excepción si no es válido.
        /// </summary>
        /// <exception cref="Exception">Si la transición no está permitida desde este estado.</exception>
        public virtual void ValidarExpirar() =>
            throw new Exception($"No se puede expirar una reserva {Nombre.ToLower()}.");

        /// <summary>
        /// Obtiene la instancia de State correspondiente al estado indicado.
        /// </summary>
        /// <param name="estado">Nombre del estado de la reserva.</param>
        /// <returns>Instancia del State correspondiente.</returns>
        /// <exception cref="Exception">Si el estado es desconocido.</exception>
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

    /// <summary>
    /// Estado Pendiente: permite confirmar, cancelar y expirar.
    /// </summary>
    public class EstadoPendiente : EstadoReservaState
    {
        /// <inheritdoc/>
        public override string Nombre => EstadoReserva.Pendiente;
        /// <inheritdoc/>
        public override void ValidarConfirmar() { }
        /// <inheritdoc/>
        public override void ValidarCancelar() { }
        /// <inheritdoc/>
        public override void ValidarExpirar() { }
    }

    /// <summary>
    /// Estado Confirmada: solo permite cancelar.
    /// </summary>
    public class EstadoConfirmada : EstadoReservaState
    {
        /// <inheritdoc/>
        public override string Nombre => EstadoReserva.Confirmada;
        /// <inheritdoc/>
        public override void ValidarCancelar() { }
    }

    /// <summary>
    /// Estado Cancelada: estado final, no permite transiciones.
    /// </summary>
    public class EstadoCancelada : EstadoReservaState
    {
        /// <inheritdoc/>
        public override string Nombre => EstadoReserva.Cancelada;
    }

    /// <summary>
    /// Estado Expirada: estado final, no permite transiciones.
    /// </summary>
    public class EstadoExpirada : EstadoReservaState
    {
        /// <inheritdoc/>
        public override string Nombre => EstadoReserva.Expirada;
    }
}
