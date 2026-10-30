using Reserva2.BE.Entities;
using Services.Facade;
using System;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace Reserva2.BLL.Logic.Reservas
{
    // Singleton + Observer: un único temporizador de fondo que expira las reservas vencidas.
    // Se enciende cuando alguien crea una reserva pendiente y se apaga solo cuando no quedan pendientes.
    public sealed class ExpiracionReservasService
    {
        private static readonly Lazy<ExpiracionReservasService> _instancia =
            new Lazy<ExpiracionReservasService>(() => new ExpiracionReservasService());

        public static ExpiracionReservasService Instancia => _instancia.Value;

        private readonly Timer _timer;
        private readonly ReservaBLL _reservaBll = new ReservaBLL();
        private readonly object _lock = new object();
        private bool _ejecutando;

        // Se dispara desde un hilo del timer: la UI debe pasar al hilo de interfaz (Invoke) antes de tocar controles.
        public event Action<List<Reserva>> ReservasExpiradas;

        private ExpiracionReservasService()
        {
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds) { AutoReset = true };
            _timer.Elapsed += (s, e) => Tick();
        }

        public bool EstaActivo => _timer.Enabled;

        public void Iniciar()
        {
            lock (_lock)
            {
                if (!_timer.Enabled)
                    _timer.Start();
            }
        }

        public void Detener()
        {
            lock (_lock)
            {
                _timer.Stop();
            }
        }

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
