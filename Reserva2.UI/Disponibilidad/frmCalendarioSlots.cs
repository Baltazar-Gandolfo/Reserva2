using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Reserva2.UI.Controles;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Disponibilidad
{
    // CU.Com.012 — Ver Calendario de Disponibilidad
    public partial class frmCalendarioSlots : Form
    {
        private static readonly Color ColorLibre = Color.PaleGreen;
        private static readonly Color ColorPendiente = Color.Gold;
        private static readonly Color ColorConfirmada = Color.LightSkyBlue;
        private static readonly Color ColorBloqueada = Color.IndianRed;

        private static readonly string[] Abreviaturas = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };

        private readonly ElementoReservableBLL _elementoBll = new ElementoReservableBLL();
        private readonly DisponibilidadBLL _dispBll = new DisponibilidadBLL();
        private ElementoReservable _elemento;

        public frmCalendarioSlots()
        {
            InitializeComponent();
            this.Load += frmCalendarioSlots_Load;
            cboElemento.SelectedIndexChanged += (s, e) => Refrescar();
            mcSemana.DateChanged += (s, e) => Refrescar();
            dvgSlots.CellMouseDown += dvgSlots_CellMouseDown;
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            verReservaToolStripMenuItem.Click += verReserva_Click;
            bloquearTurnoToolStripMenuItem.Click += bloquearTurno_Click;
        }

        private void frmCalendarioSlots_Load(object sender, EventArgs e)
        {
            mcSemana.MaxSelectionCount = 1;
            dvgSlots.AllowUserToAddRows = false;
            dvgSlots.AllowUserToDeleteRows = false;
            dvgSlots.ReadOnly = true;
            dvgSlots.RowHeadersVisible = false;
            dvgSlots.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dvgSlots.MultiSelect = false;
            dvgSlots.ContextMenuStrip = contextMenuStrip1;

            // El comercio no reserva desde el calendario: esa acción es del cliente (CU.Cli.001).
            reservarEsteTurnoToolStripMenuItem.Visible = false;

            PintarReferencias();

            try
            {
                var elementos = _elementoBll.ListarDelUsuarioActual()
                    .Where(x => x.Estado == "Activo").ToList();

                cboElemento.DropDownStyle = ComboBoxStyle.DropDownList;
                cboElemento.DisplayMember = nameof(ItemCombo<ElementoReservable>.Texto);
                cboElemento.DataSource = elementos
                    .Select(x => new ItemCombo<ElementoReservable>(x, $"{x.Sucursal.Nombre} — {x.Nombre}"))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Refrescar();
        }

        private void PintarReferencias()
        {
            foreach (var (lbl, color) in new[]
            {
                (label2, ColorLibre), (label3, ColorPendiente),
                (label4, ColorConfirmada), (label5, ColorBloqueada)
            })
            {
                lbl.BackColor = color;
                lbl.ForeColor = Color.Black;
            }
        }

        private void Refrescar()
        {
            _elemento = (cboElemento.SelectedItem as ItemCombo<ElementoReservable>)?.Valor;
            dvgSlots.Rows.Clear();

            DateTime fecha = mcSemana.SelectionStart.Date;
            DateTime lunes = fecha.AddDays(-(((int)fecha.DayOfWeek + 6) % 7));
            DateTime domingo = lunes.AddDays(6);

            for (int i = 0; i < 7; i++)
                dvgSlots.Columns[i + 1].HeaderText = $"{Abreviaturas[i]} {lunes.AddDays(i):dd/MM}";

            if (_elemento == null)
            {
                label6.Text = "Seleccioná un elemento";
                return;
            }

            try
            {
                var slots = _dispBll.ObtenerCalendario(_elemento.IdElemento, lunes, domingo);

                var horas = slots.Select(s => s.Slot.HoraInicio).Distinct().OrderBy(h => h).ToList();
                foreach (var hora in horas)
                {
                    int idx = dvgSlots.Rows.Add(hora.ToString(@"hh\:mm"));
                    var fila = dvgSlots.Rows[idx];

                    for (int i = 0; i < 7; i++)
                    {
                        var celda = fila.Cells[i + 1];
                        var item = slots.FirstOrDefault(s =>
                            s.Slot.Fecha == lunes.AddDays(i) && s.Slot.HoraInicio == hora);
                        if (item != null)
                            Pintar(celda, item);
                    }
                }

                int ocupados = slots.Count(s => s.EstadoVisual == "Pendiente de seña" || s.EstadoVisual == "Confirmada");
                label6.Text = $"Semana {lunes:dd/MM} — {domingo:dd/MM} · {ocupados} turnos ocupados de {slots.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Pintar(DataGridViewCell celda, SlotCalendario item)
        {
            Color color;
            string texto;
            switch (item.EstadoVisual)
            {
                case "Libre": color = ColorLibre; texto = "Libre"; break;
                case "Pendiente de seña": color = ColorPendiente; texto = "Pend. seña"; break;
                case "Confirmada": color = ColorConfirmada; texto = "Confirmada"; break;
                default: color = ColorBloqueada; texto = "Bloqueada"; break;
            }

            celda.Value = texto;
            celda.Tag = item;
            celda.Style.BackColor = color;
            celda.Style.ForeColor = Color.Black;
            celda.Style.SelectionBackColor = ControlPaint.Dark(color, 0.1f);
            celda.Style.SelectionForeColor = Color.Black;
        }

        private SlotCalendario SlotSeleccionado()
        {
            return dvgSlots.CurrentCell?.Tag as SlotCalendario;
        }

        private void dvgSlots_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex > 0)
                dvgSlots.CurrentCell = dvgSlots.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var item = SlotSeleccionado();
            if (item == null)
            {
                e.Cancel = true;
                return;
            }

            verReservaToolStripMenuItem.Enabled = item.Reserva != null;
            bloquearTurnoToolStripMenuItem.Enabled =
                item.EstadoVisual == "Libre" || item.EstadoVisual == "Bloqueada";
            bloquearTurnoToolStripMenuItem.Text =
                item.EstadoVisual == "Bloqueada" ? "Desbloquear turno" : "Bloquear turno";
        }

        private void bloquearTurno_Click(object sender, EventArgs e)
        {
            var item = SlotSeleccionado();
            if (item == null) return;

            try
            {
                _dispBll.AlternarBloqueo(item.Slot.IdDisponibilidad);
                Refrescar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verReserva_Click(object sender, EventArgs e)
        {
            var item = SlotSeleccionado();
            if (item?.Reserva == null) return;

            var r = item.Reserva;
            string cliente = UsuarioService.GetAll()
                .FirstOrDefault(u => u.IdUsuario == r.IdUsuario)?.Nombre ?? r.IdUsuario.ToString();

            string detalle =
                $"Reserva N° {r.IdReserva}\n" +
                $"Cliente: {cliente}\n" +
                $"Turno: {item.Slot.Fecha:dd/MM/yyyy} {item.Slot.HoraInicio:hh\\:mm}-{item.Slot.HoraFin:hh\\:mm}\n" +
                $"Estado: {r.Estado}\n" +
                $"Reservada: {r.FechaReserva:dd/MM/yyyy HH:mm}";

            if (r.Estado == EstadoReserva.Pendiente && r.ExpiraEn.HasValue)
                detalle += $"\nExpira: {r.ExpiraEn:HH:mm}";

            MessageBox.Show(detalle, "Detalle de la reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
