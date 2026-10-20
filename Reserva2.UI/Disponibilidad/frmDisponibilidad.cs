using Reserva2.BE.Entities;
using Reserva2.BLL.Logic;
using Reserva2.UI.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Reserva2.UI.Disponibilidad
{
    // CU.Com.011 — Crear Regla de Disponibilidad (horario semanal por elemento)
    public partial class frmDisponibilidad : Form
    {
        private readonly ElementoReservableBLL _elementoBll = new ElementoReservableBLL();
        private readonly ReglaDisponibilidadBLL _reglaBll = new ReglaDisponibilidadBLL();

        // Celdas marcadas: (índice de día 0=Lunes, minutos desde las 00:00 del inicio del turno)
        private readonly HashSet<(int dia, int minutos)> _marcadas = new HashSet<(int, int)>();
        private ElementoReservable _elemento;
        private bool _cargando;

        private readonly int _idElementoInicial;

        public frmDisponibilidad(int idElementoInicial = 0)
        {
            InitializeComponent();
            _idElementoInicial = idElementoInicial;
            this.Load += frmDisponibilidad_Load;
            cmbElemento.SelectedIndexChanged += cmbElemento_SelectedIndexChanged;
            numDesde.ValueChanged += rangoHorario_Changed;
            numHasta.ValueChanged += rangoHorario_Changed;
            btnCopiarLunes.Click += btnCopiarLunes_Click;
            btnMarcarTodo.Click += btnMarcarTodo_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += (s, e) => this.Close();
        }

        private void frmDisponibilidad_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].ReadOnly = true;

            numDesde.Minimum = 0; numDesde.Maximum = 23; numDesde.Value = 8;
            numHasta.Minimum = 1; numHasta.Maximum = 24; numHasta.Value = 23;

            try
            {
                var elementos = _elementoBll.ListarDelUsuarioActual()
                    .Where(x => x.Estado == "Activo").ToList();

                cmbElemento.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbElemento.DisplayMember = nameof(ItemCombo<ElementoReservable>.Texto);
                cmbElemento.DataSource = elementos
                    .Select(x => new ItemCombo<ElementoReservable>(x, $"{x.Sucursal.Nombre} — {x.Nombre}"))
                    .ToList();

                if (_idElementoInicial > 0)
                {
                    var items = (List<ItemCombo<ElementoReservable>>)cmbElemento.DataSource;
                    int idx = items.FindIndex(i => i.Valor.IdElemento == _idElementoInicial);
                    if (idx >= 0) cmbElemento.SelectedIndex = idx;
                }

                if (elementos.Count == 0)
                {
                    MessageBox.Show("No hay elementos activos. Creá un elemento antes de configurar su horario.",
                        "Sin elementos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGuardar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbElemento.SelectedItem is ItemCombo<ElementoReservable> item)
                CargarElemento(item.Valor);
        }

        private void CargarElemento(ElementoReservable elemento)
        {
            _elemento = elemento;
            this.Text = $"Disponibilidad — {elemento.Nombre} (turnos de {elemento.DuracionSlot} min)";
            _marcadas.Clear();

            var reglas = _reglaBll.ListarPorElemento(elemento.IdElemento);
            int dur = elemento.DuracionSlot;

            foreach (var r in reglas)
            {
                int dia = Array.IndexOf(DiasSemana.Todos, r.DiaSemana);
                if (dia < 0) continue;
                for (int m = (int)r.HoraInicio.TotalMinutes; m + dur <= (int)r.HoraFin.TotalMinutes; m += dur)
                    _marcadas.Add((dia, m));
            }

            _cargando = true;
            if (reglas.Count > 0)
            {
                numDesde.Value = Math.Min(23, (int)reglas.Min(r => r.HoraInicio.TotalMinutes) / 60);
                numHasta.Value = Math.Max(1, Math.Min(24, (int)Math.Ceiling(reglas.Max(r => r.HoraFin.TotalMinutes) / 60)));
            }
            _cargando = false;

            Reconstruir(capturar: false);
        }

        private void rangoHorario_Changed(object sender, EventArgs e)
        {
            if (_cargando || _elemento == null) return;
            if (numHasta.Value <= numDesde.Value)
                return; // rango inválido mientras se edita: se ignora
            Reconstruir(capturar: true);
        }

        // Pasa el estado de las celdas visibles al conjunto de marcadas.
        private void CapturarMarcadas()
        {
            dataGridView1.EndEdit();
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                int minutos = (int)fila.Tag;
                for (int dia = 0; dia < 7; dia++)
                {
                    bool marcada = fila.Cells[dia + 1].Value is bool b && b;
                    if (marcada) _marcadas.Add((dia, minutos));
                    else _marcadas.Remove((dia, minutos));
                }
            }
        }

        private void Reconstruir(bool capturar)
        {
            if (capturar) CapturarMarcadas();

            dataGridView1.Rows.Clear();
            int dur = _elemento.DuracionSlot;

            for (int m = (int)numDesde.Value * 60; m + dur <= (int)numHasta.Value * 60; m += dur)
            {
                int idx = dataGridView1.Rows.Add($"{m / 60:00}:{m % 60:00}");
                var fila = dataGridView1.Rows[idx];
                fila.Tag = m;
                for (int dia = 0; dia < 7; dia++)
                    fila.Cells[dia + 1].Value = _marcadas.Contains((dia, m));
            }
        }

        private void btnCopiarLunes_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                bool lunes = fila.Cells[1].Value is bool b && b;
                for (int c = 2; c <= 7; c++)
                    fila.Cells[c].Value = lunes;
            }
        }

        private void btnMarcarTodo_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            bool todasMarcadas = dataGridView1.Rows.Cast<DataGridViewRow>()
                .All(f => Enumerable.Range(1, 7).All(c => f.Cells[c].Value is bool b && b));

            foreach (DataGridViewRow fila in dataGridView1.Rows)
                for (int c = 1; c <= 7; c++)
                    fila.Cells[c].Value = !todasMarcadas;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_elemento == null)
            {
                MessageBox.Show("Seleccioná un elemento.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CapturarMarcadas();
                var bloques = ArmarBloques();

                int creados = _reglaBll.GuardarHorarioSemanal(_elemento.IdElemento, bloques);

                MessageBox.Show(
                    $"Horario guardado. Se generaron {creados} turnos nuevos para los próximos {GeneradorSlotsBLL.DiasHorizonte} días.",
                    "Disponibilidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Une las filas consecutivas marcadas de cada día en franjas continuas.
        private List<BloqueHorario> ArmarBloques()
        {
            var bloques = new List<BloqueHorario>();
            int dur = _elemento.DuracionSlot;

            for (int dia = 0; dia < 7; dia++)
            {
                int? inicio = null;
                int ultimoFin = 0;

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    int m = (int)fila.Tag;
                    bool marcada = fila.Cells[dia + 1].Value is bool b && b;

                    if (marcada && inicio == null)
                        inicio = m;

                    if (marcada)
                        ultimoFin = m + dur;

                    if (!marcada && inicio != null)
                    {
                        bloques.Add(NuevoBloque(dia, inicio.Value, ultimoFin));
                        inicio = null;
                    }
                }

                if (inicio != null)
                    bloques.Add(NuevoBloque(dia, inicio.Value, ultimoFin));
            }
            return bloques;
        }

        private static BloqueHorario NuevoBloque(int dia, int inicio, int fin)
        {
            return new BloqueHorario
            {
                DiaSemana = DiasSemana.Todos[dia],
                Inicio = TimeSpan.FromMinutes(inicio),
                Fin = TimeSpan.FromMinutes(fin)
            };
        }
    }
}
