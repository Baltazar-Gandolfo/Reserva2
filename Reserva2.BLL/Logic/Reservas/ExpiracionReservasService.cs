using Reserva2.BE.Entities;
using Services.Facade;
using System;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace Reserva2.BLL.Logic.Reservas
{
    /// <summary>
    /// Servicio singleton con temporizador de fondo que expira automáticamente las reservas vencidas.
    /// Implementa los patrones Singleton y Observer. Se enciende al crear una reserva pendiente
    /// y se apaga cuando no quedan pendientes.
    /// </summary>
    public sealed class ExpiracionReservasService
    {
        private static readonly Lazy<ExpiracionReservasService> _instancia =
            new Lazy<ExpiracionReservasService>(() => new ExpiracionReservasService());

        /// <summary>
        /// Instancia única del servicio de expiración.
        /// </summary>
        public static ExpiracionReservasService Instancia => _instancia.Value;

        private readonly Timer _timer;
        private readonly ReservaBLL _reservaBll = new ReservaBLL();
        private readonly object _lock = new object();
        private bool _ejecutando;

        /// <summary>
        /// Evento que se dispara desde el hilo del timer cuando se expiran reservas.
        /// La UI debe invocar al hilo de interfaz antes de tocar controles.
        /// </summary>
        public event Action<List<Reserva>> ReservasExpiradas;

        private ExpiracionReservasService()
        {
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds) { AutoReset = true };
            _timer.Elapsed += (s, e) => Tick();
        }

        /// <summary>
        /// Indica si el temporizador de expiración está activo.
        /// </summary>
        public bool EstaActivo => _timer.Enabled;

        /// <summary>
        /// Inicia el temporizador de expiración si no está corriendo.
        /// </summary>
        public void Iniciar()
        {
            lock (_lock)
            {
                if (!_timer.Enabled)
                    _timer.Start();
            }
        }

        /// <summary>
        /// Detiene el temporizador de expiración.
        /// </summary>
        public void Detener()
        {
            lock (_lock)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// Ejecuta un ciclo de expiración: expira las vencidas, notifica observadores y se detiene si no quedan pendientes.
        /// </summary>
        private void Tick()
        {
            if (_ejecutando) return;
            _ejecutando = true;
            try
            {
                var expiradas = _reservaBll.ExpirarVencidas();
                if (expiradas.Count > 0)
                    ReservasExpiradas?.Invoke(expiradas);

                if (!_reservaBll.HayPendientes())
                    Detener();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "ExpiracionReservasService.Tick");
            }
            finally
            {
                _ejecutando = false;
            }
        }
    }
}
