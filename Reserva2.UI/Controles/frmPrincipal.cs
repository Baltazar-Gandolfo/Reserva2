using Services.Facade;
using Services.DomainModel.Composite;
using Reserva2.UI.Comercios;
using Reserva2.UI.Sucursales;
using Reserva2.UI.Usuarios;
using Reserva2.UI.Elementos;
using Reserva2.UI.Disponibilidad;
using Reserva2.UI.Reservas;
using Reserva2.UI.Pagos;
using Reserva2.UI.Auditoria;
using Reserva2.UI.Errores;
using Reserva2.UI.Reportes;
using Reserva2.BLL.Logic.Reservas;
using Reserva2.UI.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Reserva2.UI.Controles
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;

            if (usuario == null)
            {
                MessageBox.Show("No hay sesión activa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            toolStripStatusLabel1.Text = $"Usuario: {usuario.Nombre}";
            toolStripStatusLabel3.Text = $"Rol: {usuario.Rol}";
            toolStripStatusLabel5.Text = $"Sesión: {SessionManager.GetInstance().FechaLogin:HH:mm}";
            this.Text = $"Reserva2 — {usuario.Nombre} ({usuario.Rol})";

            toolStrip1.Visible = false; // sus accesos repetían el menú Reservas
            ArmarMenus();
            FiltrarMenuPorPermisos();
            IniciarControlDeExpiracion();
            AbrirPantallaInicial(usuario);
        }

        // CU.Sis.004: el temporizador solo corre si hay reservas pendientes (también si quedaron de una sesión anterior).
        private void IniciarControlDeExpiracion()
        {
            try
            {
                if (new ReservaBLL().HayPendientes())
                    ExpiracionReservasService.Instancia.Iniciar();
            }
            catch (Exception ex)
            {
                ErrorService.Registrar(ex, "frmPrincipal.IniciarControlDeExpiracion");
            }

            ExpiracionReservasService.Instancia.ReservasExpiradas += OnReservasExpiradas;
            this.FormClosed += (s, e) => ExpiracionReservasService.Instancia.ReservasExpiradas -= OnReservasExpiradas;
        }

        // Llega desde el hilo del temporizador: se pasa al hilo de la interfaz antes de mostrar nada.
        private void OnReservasExpiradas(List<Reserva2.BE.Entities.Reserva> expiradas)
        {
            if (IsDisposed || !IsHandleCreated) return;

            BeginInvoke(new Action(() =>
            {
                var actual = SessionManager.GetInstance().UsuarioActual;
                if (actual == null) return;

                int propias = expiradas.Count(r => r.IdUsuario == actual.IdUsuario);
                if (propias > 0)
                    MessageBox.Show(
                        propias == 1
                            ? "Tu reserva pendiente expiró por falta de pago y el turno fue liberado."
                            : $"{propias} reservas pendientes expiraron por falta de pago.",
                        "Reserva expirada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }

        private string T(string clave)
        {
            try { return IdiomaService.Traducir(clave); }
            catch { return clave; }
        }

        private void ArmarMenus()
        {
            archivoToolStripMenuItem.DropDownItems.Clear();
            reservasToolStripMenuItem.DropDownItems.Clear();
            comercioToolStripMenuItem.DropDownItems.Clear();
            pagosToolStripMenuItem.DropDownItems.Clear();
            plataformaToolStripMenuItem.DropDownItems.Clear();
            ayudaToolStripMenuItem.DropDownItems.Clear();

            archivoToolStripMenuItem.Text = T("archivo");
            reservasToolStripMenuItem.Text = T("reservas");
            comercioToolStripMenuItem.Text = T("comercio");
            pagosToolStripMenuItem.Text = T("pagos");
            plataformaToolStripMenuItem.Text = T("plataforma");
            ayudaToolStripMenuItem.Text = T("ayuda");

            // --- Archivo ---
            var mnuCerrarSesion = new ToolStripMenuItem(T("cerrar sesion"));
            mnuCerrarSesion.Click += (s, e) => CerrarSesion();
            var mnuSalir = new ToolStripMenuItem(T("salir"));
            mnuSalir.Click += (s, e) => Application.Exit();
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuCerrarSesion,
                new ToolStripSeparator(),
                mnuSalir
            });

            // --- Reservas ---
            var mnuNuevaReserva = new ToolStripMenuItem(T("nueva reserva"));
            mnuNuevaReserva.Click += (s, e) => AbrirForm(new frmNuevaReserva());
            var mnuListaReservas = new ToolStripMenuItem(T("mis reservas"));
            mnuListaReservas.Click += (s, e) => AbrirForm(new frmReservas());
            reservasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuNuevaReserva,
                mnuListaReservas
            });

            // --- Comercio ---
            var mnuMisComercios = new ToolStripMenuItem(T("mis comercios"));
            mnuMisComercios.Click += (s, e) => AbrirForm(new frmComercios());
            // El Admin accede desde Plataforma > Comercios (mismo formulario, ve todos)
            mnuMisComercios.Visible = SessionManager.GetInstance().GetRol() != "Admin";

            var mnuElementos = new ToolStripMenuItem(T("elementos"));
            mnuElementos.Click += (s, e) => AbrirForm(new Reserva2.UI.Elementos.frmElementos(0));

            var mnuDisponibilidad = new ToolStripMenuItem(T("disponibilidad"));
            mnuDisponibilidad.Click += (s, e) => AbrirForm(new frmDisponibilidad());
            var mnuCalendario = new ToolStripMenuItem(T("calendario"));
            mnuCalendario.Click += (s, e) => AbrirForm(new frmCalendarioSlots());

            comercioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuMisComercios,
                mnuElementos,
                new ToolStripSeparator(),
                mnuDisponibilidad,
                mnuCalendario
            });

            // --- Pagos ---
            var mnuRegistrarPago = new ToolStripMenuItem(T("registrar pago"));
            mnuRegistrarPago.Click += (s, e) => AbrirForm(new frmReservas(soloPendientes: true));
            pagosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuRegistrarPago
            });

            // --- Plataforma (Admin/Comercio) ---
            var mnuReportes = new ToolStripMenuItem(T("estadisticas"));
            mnuReportes.Click += (s, e) => AbrirForm(new frmReportes());
            var mnuComercios = new ToolStripMenuItem(T("comercios"));
            mnuComercios.Click += (s, e) => AbrirForm(new frmComercios());
            var mnuUsuarios = new ToolStripMenuItem(T("usuarios"));
            mnuUsuarios.Click += (s, e) => AbrirForm(new frmUsuarios());
            var mnuPermisos = new ToolStripMenuItem(T("permisos"));
            mnuPermisos.Click += (s, e) => AbrirForm(new frmPermisos());
            var mnuBackup = new ToolStripMenuItem(T("backup"));
            mnuBackup.Click += (s, e) => AbrirForm(new frmBackup());
            var mnuErrores = new ToolStripMenuItem(T("errores"));
            mnuErrores.Click += (s, e) => AbrirForm(new frmErrores());
            var mnuBitacora = new ToolStripMenuItem(T("auditoria"));
            mnuBitacora.Click += (s, e) => AbrirForm(new frmAuditoria());
            plataformaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuReportes,
                new ToolStripSeparator(),
                mnuComercios,
                mnuUsuarios,
                mnuPermisos,
                new ToolStripSeparator(),
                mnuBackup,
                mnuErrores,
                mnuBitacora
            });

            // --- Ayuda ---
            var mnuIdioma = new ToolStripMenuItem("Idioma");
            try
            {
                var idiomas = IdiomaService.ObtenerIdiomas();
                foreach (var idioma in idiomas)
                {
                    var item = new ToolStripMenuItem(idioma.DisplayName);
                    item.Tag = idioma;
                    item.Click += (s, ev) =>
                    {
                        var cultura = (CultureInfo)((ToolStripMenuItem)s).Tag;
                        Thread.CurrentThread.CurrentCulture = cultura;
                        Thread.CurrentThread.CurrentUICulture = cultura;
                        BitacoraService.Registrar("Cambio idioma", "Sistema", $"Idioma cambiado a: {cultura.DisplayName}");
                        ArmarMenus();
                        FiltrarMenuPorPermisos();
                    };
                    mnuIdioma.DropDownItems.Add(item);
                }
            }
            catch { }

            var mnuAcerca = new ToolStripMenuItem(T("acerca de"));
            mnuAcerca.Click += (s, e) => MessageBox.Show("Reserva2 v1.0\nDesarrollado por Baltazar Gandolfo\nUAI — Ingeniería de Software 2026", T("acerca de"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuIdioma,
                new ToolStripSeparator(),
                mnuAcerca
            });
        }

        private void FiltrarMenuPorPermisos()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual;
            var patentes = usuario.Patentes;

            bool TienePermiso(string clave)
            {
                foreach (var p in patentes)
                {
                    if (p.DataKey == clave) return true;
                }
                return false;
            }

            // Comercio — se muestra si tiene al menos una patente de este grupo
            comercioToolStripMenuItem.Visible =
                TienePermiso("frmComercios") ||
                TienePermiso("frmSucursales") ||
                TienePermiso("frmElementos") ||
                TienePermiso("frmElementoABM") ||
                TienePermiso("frmDisponibilidad") ||
                TienePermiso("frmCalendario");

            // Pagos
            pagosToolStripMenuItem.Visible = TienePermiso("frmPagoManual");

            // Plataforma (Admin + Comercio para reportes)
            plataformaToolStripMenuItem.Visible =
                TienePermiso("frmComercios") ||
                TienePermiso("frmUsuarios") ||
                TienePermiso("frmPermisos") ||
                TienePermiso("frmBitacora") ||
                TienePermiso("frmBackup") ||
                TienePermiso("frmReportes") ||
                TienePermiso("frmErrores");

            // Reservas
            reservasToolStripMenuItem.Visible =
                TienePermiso("frmNuevaReserva") ||
                TienePermiso("frmReservas");
        }

        private void AbrirPantallaInicial(Usuario usuario)
        {
            if (usuario.Rol == "Cliente")
                AbrirForm(new frmReservas());
            else
                AbrirForm(new frmReportes());
        }

        private void AbrirForm(Form form)
        {
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void CerrarSesion()
        {
            BitacoraService.Registrar("Logout", "Usuario", "Cerró sesión");
            SessionManager.GetInstance().CerrarSesion();
            this.Close();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e) { }
        private void toolStripLabel2_Click(object sender, EventArgs e) { }
        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}