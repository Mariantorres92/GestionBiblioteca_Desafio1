using System;
using System.Drawing;
using System.Windows.Forms;
using GestionBiblioteca_Desafio1.Domain;
using GestionBiblioteca_Desafio1.Services;

namespace GestionBiblioteca_Desafio1.Forms
{
    public class FormUsuarios : UserControl
    {
        private readonly BibliotecaService _servicio;

        private DataGridView dgvUsuarios;
        private TextBox txtBuscar, txtNombre, txtCarnet, txtCorreo;
        private Button btnAgregar, btnActualizar, btnEliminar, btnLimpiar;
        private Panel panelFormulario;

        public FormUsuarios(BibliotecaService servicio)
        {
            _servicio = servicio;
            InicializarComponentes();
            CargarUsuarios();
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            var lblTitulo = new Label
            {
                Text = "👤  Gestión de Usuarios",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 15),
                AutoSize = true
            };

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
            txtBuscar.TextChanged += (s, e) => CargarUsuarios(txtBuscar.Text);

            dgvUsuarios = new DataGridView
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
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeight = 35;
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;

            panelFormulario = new Panel
            {
                Location = new Point(650, 85),
                Size = new Size(280, 430),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 15;

            panelFormulario.Controls.Add(CrearLabel("Nombre *", y));
            txtNombre = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtNombre); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Carnet *", y));
            txtCarnet = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtCarnet); y += 60;

            panelFormulario.Controls.Add(CrearLabel("Correo *", y));
            txtCorreo = CrearTextBox(y + 22); panelFormulario.Controls.Add(txtCorreo); y += 80;

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
                { lblTitulo, lblBuscar, txtBuscar, dgvUsuarios, panelFormulario });
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

        private void CargarUsuarios(string busqueda = "")
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Columns.Clear();

            var tabla = new System.Data.DataTable();
            tabla.Columns.Add("Id", typeof(int));
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Carnet");
            tabla.Columns.Add("Correo");
            tabla.Columns.Add("Préstamos activos");

            foreach (var u in _servicio.ObtenerUsuarios())
            {
                if (string.IsNullOrEmpty(busqueda) ||
                    u.Nombre.ToLower().Contains(busqueda.ToLower()) ||
                    u.Carnet.ToLower().Contains(busqueda.ToLower()))
                {
                    int prestamos = 0;
                    foreach (var p in _servicio.ObtenerPrestamos(true))
                        if (p.UsuarioId == u.Id) prestamos++;

                    tabla.Rows.Add(u.Id, u.Nombre, u.Carnet, u.Correo, prestamos);
                }
            }

            dgvUsuarios.DataSource = tabla;
            if (dgvUsuarios.Columns.Contains("Id"))
                dgvUsuarios.Columns["Id"].Visible = false;
        }

        private void DgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0) return;
            var row = dgvUsuarios.SelectedRows[0];
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
            txtCarnet.Text = row.Cells["Carnet"].Value?.ToString();
            txtCorreo.Text = row.Cells["Correo"].Value?.ToString();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = LeerFormulario();
                _servicio.AgregarUsuario(usuario);
                CargarUsuarios();
                LimpiarFormulario();
                MessageBox.Show("Usuario agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0) { MostrarError("Selecciona un usuario."); return; }
            try
            {
                var usuario = LeerFormulario();
                usuario.Id = (int)dgvUsuarios.SelectedRows[0].Cells["Id"].Value;
                _servicio.ActualizarUsuario(usuario);
                CargarUsuarios();
                LimpiarFormulario();
                MessageBox.Show("Usuario actualizado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0) { MostrarError("Selecciona un usuario."); return; }
            if (MessageBox.Show("¿Eliminar este usuario?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                int id = (int)dgvUsuarios.SelectedRows[0].Cells["Id"].Value;
                _servicio.EliminarUsuario(id);
                CargarUsuarios();
                LimpiarFormulario();
                MessageBox.Show("Usuario eliminado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private UsuarioBiblioteca LeerFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(txtCarnet.Text)) throw new ArgumentException("El carnet es obligatorio.");
            if (string.IsNullOrWhiteSpace(txtCorreo.Text)) throw new ArgumentException("El correo es obligatorio.");
            if (!txtCorreo.Text.Contains("@")) throw new ArgumentException("El correo no es válido.");

            return new UsuarioBiblioteca(txtNombre.Text.Trim(),
                txtCarnet.Text.Trim(), txtCorreo.Text.Trim());
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear(); txtCarnet.Clear(); txtCorreo.Clear();
            dgvUsuarios.ClearSelection();
        }

        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}