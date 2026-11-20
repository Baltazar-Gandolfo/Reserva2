using Reserva2.BE.Auxiliares;
using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    /// <summary>
    /// Lógica de negocio para la gestión de comercios.
    /// </summary>
    public class ComercioBLL
    {
        private readonly ComercioRepository _repo = new ComercioRepository();
        private readonly ReglaNegocioBLL _reglasBll = new ReglaNegocioBLL();

        /// <summary>
        /// Obtiene todos los comercios registrados en el sistema.
        /// </summary>
        /// <returns>Lista completa de comercios.</returns>
        public List<Comercio> ListarTodos()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Obtiene los comercios habilitados para el catálogo de clientes.
        /// </summary>
        /// <returns>Lista de comercios activos.</returns>
        public List<Comercio> ListarActivos()
        {
            return _repo.GetActivos();
        }

        /// <summary>
        /// Obtiene los comercios cuyo dueño es el usuario actualmente logueado.
        /// </summary>
        /// <returns>Lista de comercios del usuario actual.</returns>
        public List<Comercio> ListarPorOwner()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;
            return _repo.GetByUsuario(usuario.IdUsuario);
        }

        /// <summary>
        /// Obtiene el detalle completo de un comercio, incluyendo owner, elementos, reglas y resumen mensual.
        /// Solo accesible por el Admin o el dueño del comercio.
        /// </summary>
        /// <param name="id">Identificador del comercio.</param>
        /// <returns>Detalle del comercio con información agregada.</returns>
        /// <exception cref="Exception">Si no hay sesión, el comercio no existe o no tiene permisos.</exception>
        public ComercioDetalle ObtenerDetalle(int id)
        {
            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
            var comercio = _repo.GetById(id)
                ?? throw new Exception("El comercio no existe.");

            if (usuario.Rol != "Admin" && comercio.IdUsuario != usuario.IdUsuario)
                throw new Exception("No tiene permisos sobre este comercio.");

            var owner = UsuarioService.GetAll().FirstOrDefault(u => u.IdUsuario == comercio.IdUsuario);
            var primerDia = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            return new ComercioDetalle
            {
                Comercio = comercio,
                Owner = owner?.Nombre ?? comercio.IdUsuario.ToString(),
                Elementos = new ElementoReservableRepository().GetByComercio(id),
                Reglas = new ReglaNegocioRepository().GetByComercio(id),
                ResumenMes = _repo.GetResumen(id, primerDia, primerDia.AddMonths(1))
            };
        }

        /// <summary>
        /// Obtiene un comercio por su identificador.
        /// </summary>
        /// <param name="id">Identificador del comercio.</param>
        /// <returns>El comercio encontrado o null.</returns>
        public Comercio ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Crea un nuevo comercio asignándolo al usuario logueado y genera las reglas de negocio por defecto.
        /// </summary>
        /// <param name="comercio">Datos del comercio a crear.</param>
        /// <exception cref="Exception">Si el nombre está vacío o ya existe un comercio con el mismo slug.</exception>
        public void Crear(Comercio comercio)
        {
            if (string.IsNullOrWhiteSpace(comercio.Nombre))
                throw new Exception("El nombre del comercio es obligatorio.");

            var usuario = SessionManager.GetInstance().UsuarioActual;
            comercio.IdUsuario = usuario.IdUsuario;
            comercio.Slug = GenerarSlug(comercio.Nombre);

            if (_repo.ExisteSlug(comercio.Slug))
                throw new Exception("Ya existe un comercio con ese nombre.");

            _repo.Insertar(comercio);
            _reglasBll.CrearReglasPorDefecto(comercio.IdComercio);

            BitacoraService.Registrar("Alta", "Comercio", $"Se creó el comercio: {comercio.Nombre}");
        }

        /// <summary>
        /// Actualiza los datos de un comercio existente.
        /// </summary>
        /// <param name="comercio">Comercio con los datos actualizados.</param>
        /// <exception cref="Exception">Si el nombre está vacío o el slug ya está en uso por otro comercio.</exception>
        public void Actualizar(Comercio comercio)
        {
            if (string.IsNullOrWhiteSpace(comercio.Nombre))
                throw new Exception("El nombre del comercio es obligatorio.");

            comercio.Slug = GenerarSlug(comercio.Nombre);

            if (_repo.ExisteSlug(comercio.Slug, comercio.IdComercio))
                throw new Exception("Ya existe un comercio con ese nombre.");

            _repo.Actualizar(comercio);

            BitacoraService.Registrar("Modificación", "Comercio", $"Se modificó el comercio: {comercio.Nombre}");
        }

        /// <summary>
        /// Genera un slug URL-friendly a partir del nombre del comercio.
        /// </summary>
        /// <param name="nombre">Nombre del comercio.</param>
        /// <returns>Slug en minúsculas con espacios reemplazados por guiones.</returns>
        private static string GenerarSlug(string nombre)
        {
            return nombre.Trim().ToLower().Replace(" ", "-");
        }

        /// <summary>
        /// Cambia el estado de un comercio entre activo e inactivo.
        /// </summary>
        /// <param name="id">Identificador del comercio.</param>
        /// <param name="activo">True para habilitar, false para dar de baja.</param>
        public void CambiarEstado(int id, bool activo)
        {
            _repo.CambiarEstado(id, activo);
            string accion = activo ? "Habilitación" : "Baja";
            BitacoraService.Registrar(accion, "Comercio", $"Comercio ID {id} → {(activo ? "Activo" : "Inactivo")}");
        }
    }
}
