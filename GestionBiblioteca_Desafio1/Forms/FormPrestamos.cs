using System;
using System.Drawing;
using System.Windows.Forms;
using GestionBiblioteca_Desafio1.Domain;
using GestionBiblioteca_Desafio1.Services;

namespace GestionBiblioteca_Desafio1.Forms
{
    public class FormPrestamos : UserControl
    {
        private readonly BibliotecaService _servicio;

        private DataGridView dgvPrestamos;
        private ComboBox cmbLibros, cmbUsuarios;
        private Button btnPrestar, btnDevolver, btnVerTodos, btnVerActivos;
        private Panel panelFormulario;
        private Label lblEstado;

        public FormPrestamos(BibliotecaService servicio)
        {
            _servicio = servicio;
            InicializarComponentes();
            CargarPrestamos();
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            var lblTitulo = new Label
            {
                Text = "📋  Gestión de Préstamos",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 15),
                AutoSize = true
            };

            btnVerTodos = new Button
            {
                Text = "Todos",
                Location = new Point(15, 52),
                Size = new Size(90, 28),
                BackColor = Color.FromArgb(30, 58, 95),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnVerTodos.FlatAppearance.BorderSize = 0;
            btnVerTodos.Click += (s, e) => CargarPrestamos(false);

            btnVerActivos = new Button
            {
                Text = "Solo Activos",
                Location = new Point(115, 52),
                Size = new Size(110, 28),
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnVerActivos.FlatAppearance.BorderSize = 0;
            btnVerActivos.Click += (s, e) => CargarPrestamos(true);

            dgvPrestamos = new DataGridView
            {
                Location = new Point(15, 90),
                Size = new Size(620, 425),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9f)
            };
            dgvPrestamos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvPrestamos.ColumnHeadersHeight = 35;
            dgvPrestamos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dgvPrestamos.DefaultCellStyle.SelectionForeColor = Color.Black;

            panelFormulario = new Panel
            {
                Location = new Point(650, 90),
                Size = new Size(280, 425),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 15;

            panelFormulario.Controls.Add(CrearLabel("Libro", y));
            cmbLibros = new ComboBox
            {
                Location = new Point(10, y + 22),
                Width = 255,
                Font = new Font("Segoe UI", 9f),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelFormulario.Controls.Add(cmbLibros);
            y += 65;

            panelFormulario.Controls.Add(CrearLabel("Usuario", y));
            cmbUsuarios = new ComboBox
            {
                Location = new Point(10, y + 22),
                Width = 255,
                Font = new Font("Segoe UI", 9f),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelFormulario.Controls.Add(cmbUsuarios);
            y += 80;

            btnPrestar = CrearBoton("📖 Registrar Préstamo", new Point(10, y), Color.FromArgb(34, 139, 34));
            btnPrestar.Width = 255;
            y += 50;
            btnDevolver = CrearBoton("↩️ Registrar Devolución", new Point(10, y), Color.FromArgb(200, 120, 0));
            btnDevolver.Width = 255;

            btnPrestar.Click += BtnPrestar_Click;
            btnDevolver.Click += BtnDevolver_Click;

            lblEstado = new Label
            {
                Location = new Point(10, y + 50),
                Size = new Size(255, 60),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(30, 58, 95)
            };

            panelFormulario.Controls.AddRange(new Control[]
                { btnPrestar, btnDevolver, lblEstado });

            this.Controls.AddRange(new Control[]
                { lblTitulo, btnVerTodos, btnVerActivos, dgvPrestamos, panelFormulario });

            CargarCombos();
        }

        private Label CrearLabel(string texto, int y) => new Label
        {
            Text = texto,
            Location = new Point(10, y),
            AutoSize = true,
            Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(60, 60, 60)
        };

        private Button CrearBoton(string texto, Point ubicacion, Color color)
        {
            var btn = new Button
            {
                Text = texto,
                Location = ubicacion,
                Size = new Size(125, 36),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void CargarCombos()
        {
            cmbLibros.Items.Clear();
            foreach (var m in _servicio.ObtenerMateriales())
                if (m is Libro l && !l.Prestado)
                    cmbLibros.Items.Add(new ComboItem(l.Id, $"{l.Titulo} (ISBN: {l.ISBN})"));

            cmbUsuarios.Items.Clear();
            foreach (var u in _servicio.ObtenerUsuarios())
                cmbUsuarios.Items.Add(new ComboItem(u.Id, u.Nombre + " - " + u.Carnet));
        }

        private void CargarPrestamos(bool soloActivos = false)
        {
            dgvPrestamos.DataSource = null;
            dgvPrestamos.Columns.Clear();

            var tabla = new System.Data.DataTable();
            tabla.Columns.Add("Id", typeof(int));
            tabla.Columns.Add("Libro");
            tabla.Columns.Add("Usuario");
            tabla.Columns.Add("Fecha Préstamo");
            tabla.Columns.Add("Fecha Devolución");
            tabla.Columns.Add("Estado");

            foreach (var p in _servicio.ObtenerPrestamos(soloActivos))
            {
                string estado = p.Activo ? "Activo" : "Devuelto";
                string fechaDev = p.FechaDevolucion.HasValue
                    ? p.FechaDevolucion.Value.ToString("dd/MM/yyyy HH:mm")
                    : "-";
                tabla.Rows.Add(p.Id, p.TituloMaterial, p.NombreUsuario,
                    p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm"), fechaDev, estado);
            }

            dgvPrestamos.DataSource = tabla;
            if (dgvPrestamos.Columns.Contains("Id"))
                dgvPrestamos.Columns["Id"].Visible = false;

            foreach (DataGridViewRow row in dgvPrestamos.Rows)
            {
                if (row.Cells["Estado"].Value?.ToString() == "Activo")
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void BtnPrestar_Click(object sender, EventArgs e)
        {
            if (cmbLibros.SelectedItem == null) { MostrarError("Selecciona un libro."); return; }
            if (cmbUsuarios.SelectedItem == null) { MostrarError("Selecciona un usuario."); return; }
            try
            {
                var libro = (ComboItem)cmbLibros.SelectedItem;
                var usuario = (ComboItem)cmbUsuarios.SelectedItem;
                var p = _servicio.RegistrarPrestamo(libro.Id, usuario.Id);
                CargarPrestamos();
                CargarCombos();
                lblEstado.Text = $"✅ Préstamo #{p.Id} registrado.";
                lblEstado.ForeColor = Color.FromArgb(34, 139, 34);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0) { MostrarError("Selecciona un préstamo activo."); return; }
            int id = (int)dgvPrestamos.SelectedRows[0].Cells["Id"].Value;
            try
            {
                int materialId = 0;
                foreach (var p in _servicio.ObtenerPrestamos(true))
                    if (p.Id == id) { materialId = p.MaterialId; break; }

                if (materialId == 0) { MostrarError("Este préstamo ya fue devuelto."); return; }

                _servicio.RegistrarDevolucion(materialId);
                CargarPrestamos();
                CargarCombos();
                lblEstado.Text = "↩️ Devolución registrada.";
                lblEstado.ForeColor = Color.FromArgb(200, 120, 0);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public class ComboItem
    {
        public int Id { get; }
        public string Texto { get; }
        public ComboItem(int id, string texto) { Id = id; Texto = texto; }
        public override string ToString() => Texto;
    }
}