using Services.Facade;
using Services.DomainModel.Composite;
using Reserva2.UI.Comercios;
using Reserva2.UI.Usuarios;
using Reserva2.UI.Elementos;
using Reserva2.UI.Disponibilidad;
using Reserva2.UI.Reservas;
using Reserva2.UI.Pagos;
using Reserva2.UI.Auditoria;
using System;
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

            // Actualizar barra de estado
            toolStripStatusLabel1.Text = $"Usuario: {usuario.Nombre}";
            toolStripStatusLabel3.Text = $"Rol: {usuario.Rol}";
            toolStripStatusLabel5.Text = $"Sesión: {SessionManager.GetInstance().FechaLogin:HH:mm}";
            this.Text = $"Reserva2 — {usuario.Nombre} ({usuario.Rol})";

            // Armar sub-menús
            ArmarMenus();

            // Filtrar por rol
            FiltrarMenuPorRol(usuario.Rol);
        }

        private void ArmarMenus()
        {
            archivoToolStripMenuItem.DropDownItems.Clear();
            reservasToolStripMenuItem.DropDownItems.Clear();
            comercioToolStripMenuItem.DropDownItems.Clear();
            pagosToolStripMenuItem.DropDownItems.Clear();
            plataformaToolStripMenuItem.DropDownItems.Clear();
            ayudaToolStripMenuItem.DropDownItems.Clear();

            // --- Archivo ---
            var mnuCerrarSesion = new ToolStripMenuItem("Cerrar sesión");
            mnuCerrarSesion.Click += (s, e) => CerrarSesion();
            var mnuSalir = new ToolStripMenuItem("Salir");
            mnuSalir.Click += (s, e) => Application.Exit();
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuCerrarSesion,
                new ToolStripSeparator(),
                mnuSalir
            });

            // --- Reservas ---
            var mnuNuevaReserva = new ToolStripMenuItem("Nueva reserva");
            mnuNuevaReserva.Click += (s, e) => AbrirForm(new frmNuevaReserva());
            var mnuListaReservas = new ToolStripMenuItem("Mis reservas");
            mnuListaReservas.Click += (s, e) => AbrirForm(new frmReservas());
            reservasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuNuevaReserva,
                mnuListaReservas
            });

            // --- Comercio (Owner) ---
            var mnuElementos = new ToolStripMenuItem("Elementos reservables");
            mnuElementos.Click += (s, e) => AbrirForm(new frmElementos());
            var mnuElementoNuevo = new ToolStripMenuItem("Nuevo elemento");
            mnuElementoNuevo.Click += (s, e) => AbrirForm(new frmElementoABM());
            var mnuDisponibilidad = new ToolStripMenuItem("Disponibilidad semanal");
            mnuDisponibilidad.Click += (s, e) => AbrirForm(new frmDisponibilidad());
            var mnuCalendario = new ToolStripMenuItem("Calendario de slots");
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
            var mnuRegistrarPago = new ToolStripMenuItem("Registrar pago");
            mnuRegistrarPago.Click += (s, e) => AbrirForm(new frmPago());
            pagosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuRegistrarPago
            });

            // --- Plataforma (Admin) ---
            var mnuComercios = new ToolStripMenuItem("Comercios");
            mnuComercios.Click += (s, e) => AbrirForm(new frmComercios());
            var mnuUsuarios = new ToolStripMenuItem("Usuarios");
            mnuUsuarios.Click += (s, e) => AbrirForm(new frmUsuarios());
            var mnuBitacora = new ToolStripMenuItem("Auditoría");
            mnuBitacora.Click += (s, e) => AbrirForm(new frmAuditoria());
            plataformaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuComercios,
                mnuUsuarios,
                new ToolStripSeparator(),
                mnuBitacora
            });

            // --- Ayuda ---
            var mnuAcerca = new ToolStripMenuItem("Acerca de");
            mnuAcerca.Click += (s, e) => MessageBox.Show("Reserva2 v1.0\nDesarrollado por Baltazar Gandolfo\nUAI — Ingeniería de Software 2026", "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ayudaToolStripMenuItem.DropDownItems.Add(mnuAcerca);

            // --- ToolStrip accesos rápidos ---
            toolStripLabel1.Click -= toolStripLabel1_Click_1;
            toolStripLabel1.Click += (s, e) => AbrirForm(new frmNuevaReserva());
            toolStripLabel2.Click -= toolStripLabel2_Click;
            toolStripLabel2.Click += (s, e) => AbrirForm(new frmReservas());
            toolStripLabel3.Click += (s, e) => AbrirForm(new frmPago());
        }

        private void FiltrarMenuPorRol(string rol)
        {
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
            SessionManager.GetInstance().CerrarSesion();
            this.Close();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e) { }
        private void toolStripLabel2_Click(object sender, EventArgs e) { }
    }
}