using Reserva2.BE.Entities;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    public class ComercioDetalle
    {
        public Comercio Comercio { get; set; }
        public string Owner { get; set; }
        public List<ElementoReservable> Elementos { get; set; }
        public List<ReglaNegocio> Reglas { get; set; }
        public ResumenComercio ResumenMes { get; set; }
    }

    public class ComercioBLL
    {
        private readonly ComercioRepository _repo = new ComercioRepository();
        private readonly ReglaNegocioBLL _reglasBll = new ReglaNegocioBLL();

        public List<Comercio> ListarTodos()
        {
            return _repo.GetAll();
        }

        // Catálogo para clientes: solo comercios habilitados.
        public List<Comercio> ListarActivos()
        {
            return _repo.GetActivos();
        }

        public List<Comercio> ListarPorOwner()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;
            return _repo.GetByUsuario(usuario.IdUsuario);
        }

        // CU.Adm.002 — Ver Detalle de Comercio (Admin o dueño del comercio)
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

        public Comercio ObtenerPorId(int id)
        {
            return _repo.GetById(id);
        }

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

        private static string GenerarSlug(string nombre)
        {
            return nombre.Trim().ToLower().Replace(" ", "-");
        }

        public void CambiarEstado(int id, bool activo)
        {
            _repo.CambiarEstado(id, activo);
            string accion = activo ? "Habilitación" : "Baja";
            BitacoraService.Registrar(accion, "Comercio", $"Comercio ID {id} → {(activo ? "Activo" : "Inactivo")}");
        }
    }
}