using Services.Facade;
using Services.DomainModel.Composite;
using Reserva2.UI.Comercios;
using Reserva2.UI.Usuarios;
using Reserva2.UI.Elementos;
using Reserva2.UI.Disponibilidad;
using Reserva2.UI.Reservas;
using Reserva2.UI.Pagos;
using Reserva2.UI.Auditoria;
using Reserva2.UI.Config;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Reserva2.UI.Controles
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            this.Load += frmPrincipal_Load;
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

            ArmarMenus();
            FiltrarMenuPorRol(usuario.Rol);
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

            // --- Comercio (Owner) ---
            var mnuElementos = new ToolStripMenuItem(T("elementos"));
            mnuElementos.Click += (s, e) => AbrirForm(new frmElementos());
            var mnuElementoNuevo = new ToolStripMenuItem(T("nuevo elemento"));
            mnuElementoNuevo.Click += (s, e) => AbrirForm(new frmElementoABM());
            var mnuDisponibilidad = new ToolStripMenuItem(T("disponibilidad"));
            mnuDisponibilidad.Click += (s, e) => AbrirForm(new frmDisponibilidad());
            var mnuCalendario = new ToolStripMenuItem(T("calendario"));
            mnuCalendario.Click += (s, e) => AbrirForm(new frmCalendarioSlots());
            comercioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuElementos,
                mnuElementoNuevo,
                new ToolStripSeparator(),
                mnuDisponibilidad,
                mnuCalendario
            });

            // --- Pagos ---
            var mnuRegistrarPago = new ToolStripMenuItem(T("registrar pago"));
            mnuRegistrarPago.Click += (s, e) => AbrirForm(new frmPago());
            pagosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuRegistrarPago
            });

            // --- Plataforma (Admin) ---
            var mnuComercios = new ToolStripMenuItem(T("comercios"));
            mnuComercios.Click += (s, e) => AbrirForm(new frmComercios());
            var mnuUsuarios = new ToolStripMenuItem(T("usuarios"));
            mnuUsuarios.Click += (s, e) => AbrirForm(new frmUsuarios());
            var mnuPermisos = new ToolStripMenuItem(T("permisos"));
            mnuPermisos.Click += (s, e) => AbrirForm(new frmPermisos());
            var mnuBackup = new ToolStripMenuItem(T("backup"));
            mnuBackup.Click += (s, e) => AbrirForm(new frmBackup());
            var mnuBitacora = new ToolStripMenuItem(T("auditoria"));
            mnuBitacora.Click += (s, e) => AbrirForm(new frmAuditoria());
            plataformaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuComercios,
                mnuUsuarios,
                mnuPermisos,
                new ToolStripSeparator(),
                mnuBackup,
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
                        FiltrarMenuPorRol(SessionManager.GetInstance().UsuarioActual.Rol);
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

            // --- ToolStrip accesos rápidos ---
            toolStripLabel1.Click -= toolStripLabel1_Click_1;
            toolStripLabel1.Click += (s, e) => AbrirForm(new frmNuevaReserva());
            toolStripLabel2.Click -= toolStripLabel2_Click;
            toolStripLabel2.Click += (s, e) => AbrirForm(new frmReservas());
            toolStripLabel3.Click += (s, e) => AbrirForm(new frmPago());
        }

        private void FiltrarMenuPorRol(string rol)
        {
            comercioToolStripMenuItem.Visible = true;
            pagosToolStripMenuItem.Visible = true;
            plataformaToolStripMenuItem.Visible = true;

            switch (rol)
            {
                case "Admin":
                    comercioToolStripMenuItem.Visible = false;
                    pagosToolStripMenuItem.Visible = false;
                    break;
                case "Comercio":
                    plataformaToolStripMenuItem.Visible = false;
                    break;
                case "Cliente":
                    comercioToolStripMenuItem.Visible = false;
                    plataformaToolStripMenuItem.Visible = false;
                    break;
                default:
                    plataformaToolStripMenuItem.Visible = false;
                    comercioToolStripMenuItem.Visible = false;
                    break;
            }
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
    }
}