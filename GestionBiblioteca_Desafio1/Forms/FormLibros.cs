using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GestionBiblioteca_Desafio1.Domain;
using GestionBiblioteca_Desafio1.Services;

namespace GestionBiblioteca_Desafio1.Forms
{
    public class FormLibros : UserControl
    {
        private readonly BibliotecaService _servicio;

        // Controles
        private DataGridView dgvLibros;
        private TextBox txtBuscar, txtTitulo, txtAutor, txtAnio, txtPaginas, txtISBN, txtCategoria;
        private Button btnAgregar, btnActualizar, btnEliminar, btnLimpiar;
        private Label lblTitulo;
        private Panel panelFormulario;

        public FormLibros(BibliotecaService servicio)
        {
            _servicio = servicio;
            InicializarComponentes();
            CargarLibros();
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ── Título ──────────────────────────────────────────────────
            lblTitulo = new Label
            {
                Text = "📚  Gestión de Libros",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 15),
                AutoSize = true
            };

            // ── Buscar ───────────────────────────────────────────────────
            var lblBuscar = new Label
            {
                Text = "Buscar:",
                Location = new Point(15, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 9f)
            };

            txtBuscar = new TextBox
            {
                Location = new Point(70, 52),
                Width = 300,
                Font = new Font("Segoe UI", 9f)
            };
            txtBuscar.TextChanged += (s, e) => CargarLibros(txtBuscar.Text);

            // ── DataGridView ─────────────────────────────────────────────
            dgvLibros = new DataGridView
            {
                Location = new Point(15, 85),
                Size = new Size(620, 430),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9f)
            };
            dgvLibros.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvLibros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLibros.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvLibros.ColumnHeadersHeight = 35;
            dgvLibros.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dgvLibros.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLibros.SelectionChanged += DgvLibros_SelectionChanged;

            // ── Panel Formulario ─────────────────────────────────────────
            panelFormulario = new Panel
            {
                Location = new Point(650, 85),
                Size = new Size(280, 430),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 15;

            panelFormulario.Controls.Add(CrearLabel("Título *", y));
            txtTitulo = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtTitulo); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Autor *", y));
            txtAutor = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtAutor); y += 60;

            panelFormulario.Controls.Add(CrearLabel("ISBN *", y));
            txtISBN = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtISBN); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Año *", y));
            txtAnio = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtAnio); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Páginas", y));
            txtPaginas = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtPaginas); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Categoría", y));
            txtCategoria = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtCategoria); y += 70;

            // Botones
            btnAgregar = CrearBoton("➕ Agregar", new Point(10, y), Color.FromArgb(34, 139, 34));
            btnActualizar = CrearBoton("✏️ Actualizar", new Point(145, y), Color.FromArgb(30, 58, 95));
            y += 45;
            btnEliminar = CrearBoton("🗑️ Eliminar", new Point(10, y), Color.FromArgb(200, 50, 50));
            btnLimpiar = CrearBoton("🔄 Limpiar", new Point(145, y), Color.FromArgb(120, 120, 120));

            btnAgregar.Click += BtnAgregar_Click;
            btnActualizar.Click += BtnActualizar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnLimpiar.Click += (s, e) => LimpiarFormulario();

            panelFormulario.Controls.AddRange(new Control[]
                { btnAgregar, btnActualizar, btnEliminar, btnLimpiar });

            this.Controls.AddRange(new Control[]
                { lblTitulo, lblBuscar, txtBuscar, dgvLibros, panelFormulario });
        }

        private Label CrearLabel(string texto, int y) => new Label
        {
            Text = texto,
            Location = new Point(10, y),
            AutoSize = true,
            Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(60, 60, 60)
        };

        private TextBox CrearTextBox(int y) => new TextBox
        {
            Location = new Point(10, y),
            Width = 255,
            Font = new Font("Segoe UI", 9f),
            BorderStyle = BorderStyle.FixedSingle
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

        // ── Cargar datos ─────────────────────────────────────────────────
        private void CargarLibros(string busqueda = "")
        {
            dgvLibros.DataSource = null;
            dgvLibros.Columns.Clear();

            var tabla = new System.Data.DataTable();
            tabla.Columns.Add("Id", typeof(int));
            tabla.Columns.Add("Título");
            tabla.Columns.Add("Autor");
            tabla.Columns.Add("ISBN");
            tabla.Columns.Add("Año");
            tabla.Columns.Add("Páginas");
            tabla.Columns.Add("Categoría");
            tabla.Columns.Add("Estado");

            var lista = string.IsNullOrEmpty(busqueda)
                ? _servicio.ObtenerMateriales()
                : _servicio.BuscarMateriales(busqueda);

            foreach (var m in lista)
            {
                if (m is Libro l)
                    tabla.Rows.Add(l.Id, l.Titulo, l.Autor, l.ISBN,
                        l.Anio, l.Paginas, l.Categoria, l.ObtenerEstado());
            }

            dgvLibros.DataSource = tabla;
            if (dgvLibros.Columns.Contains("Id"))
                dgvLibros.Columns["Id"].Visible = false;
        }

        private void DgvLibros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0) return;
            var row = dgvLibros.SelectedRows[0];
            txtTitulo.Text = row.Cells["Título"].Value?.ToString();
            txtAutor.Text = row.Cells["Autor"].Value?.ToString();
            txtISBN.Text = row.Cells["ISBN"].Value?.ToString();
            txtAnio.Text = row.Cells["Año"].Value?.ToString();
            txtPaginas.Text = row.Cells["Páginas"].Value?.ToString();
            txtCategoria.Text = row.Cells["Categoría"].Value?.ToString();
        }

        // ── CRUD ─────────────────────────────────────────────────────────
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var libro = LeerFormulario();
                _servicio.AgregarMaterial(libro);
                CargarLibros();
                LimpiarFormulario();
                MessageBox.Show("Libro agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0) { MostrarError("Selecciona un libro."); return; }
            try
            {
                var libro = LeerFormulario();
                libro.Id = (int)dgvLibros.SelectedRows[0].Cells["Id"].Value;
                _servicio.ActualizarMaterial(libro);
                CargarLibros();
                LimpiarFormulario();
                MessageBox.Show("Libro actualizado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0) { MostrarError("Selecciona un libro."); return; }
            if (MessageBox.Show("¿Eliminar este libro?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                int id = (int)dgvLibros.SelectedRows[0].Cells["Id"].Value;
                _servicio.EliminarMaterial(id);
                CargarLibros();
                LimpiarFormulario();
                MessageBox.Show("Libro eliminado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private Libro LeerFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text)) throw new ArgumentException("El título es obligatorio.");
            if (string.IsNullOrWhiteSpace(txtAutor.Text)) throw new ArgumentException("El autor es obligatorio.");
            if (string.IsNullOrWhiteSpace(txtISBN.Text)) throw new ArgumentException("El ISBN es obligatorio.");
            if (!int.TryParse(txtAnio.Text, out int anio)) throw new ArgumentException("El año debe ser un número.");
            int.TryParse(txtPaginas.Text, out int paginas);

            return new Libro(txtTitulo.Text.Trim(), txtAutor.Text.Trim(),
                anio, paginas, txtISBN.Text.Trim(), txtCategoria.Text.Trim());
        }

        private void LimpiarFormulario()
        {
            txtTitulo.Clear(); txtAutor.Clear(); txtISBN.Clear();
            txtAnio.Clear(); txtPaginas.Clear(); txtCategoria.Clear();
            dgvLibros.ClearSelection();
        }

        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}