using System;
using System.Drawing;
using System.Windows.Forms;
using GestionBiblioteca_Desafio1.Services;

namespace GestionBiblioteca_Desafio1
{
    public partial class Form1 : Form
    {
        private readonly BibliotecaService _servicio = new BibliotecaService();

        public Form1()
        {
            InitializeComponent();
            ConfigurarFormulario();
            MostrarBienvenida();
        }

        private void ConfigurarFormulario()
        {
            panelMenu.BackColor = Color.FromArgb(30, 58, 95);

            btnLibros.BackColor = Color.FromArgb(30, 58, 95);
            btnUsuarios.BackColor = Color.FromArgb(30, 58, 95);
            btnPrestamos.BackColor = Color.FromArgb(30, 58, 95);
            btnEstadisticas.BackColor = Color.FromArgb(30, 58, 95);

            btnLibros.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(52, 90, 138);
            btnLibros.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(30, 58, 95);
            btnUsuarios.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(52, 90, 138);
            btnUsuarios.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(30, 58, 95);
            btnPrestamos.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(52, 90, 138);
            btnPrestamos.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(30, 58, 95);
            btnEstadisticas.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(52, 90, 138);
            btnEstadisticas.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(30, 58, 95);
        }

        private void MostrarBienvenida()
        {
            LimpiarPanel();

            var lblBienvenida = new Label
            {
                Text = "Sistema de Gestion de Biblioteca",
                Font = new Font("Segoe UI", 20f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                AutoSize = true,
                Location = new Point(30, 40)
            };

            var lblSubtitulo = new Label
            {
                Text = "Selecciona una opcion del menu para comenzar.",
                Font = new Font("Segoe UI", 12f),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(30, 90)
            };

            var panelStats = new Panel
            {
                Location = new Point(30, 150),
                Size = new Size(700, 100),
                BackColor = Color.Transparent
            };

            int totalLibros = 0, totalUsuarios = 0, prestActivos = 0;
            foreach (var m in _servicio.ObtenerMateriales()) totalLibros++;
            foreach (var u in _servicio.ObtenerUsuarios()) totalUsuarios++;
            foreach (var p in _servicio.ObtenerPrestamos(true)) prestActivos++;

            panelStats.Controls.Add(CrearTarjetaInfo("Libros", totalLibros.ToString(), 0, Color.FromArgb(30, 58, 95)));
            panelStats.Controls.Add(CrearTarjetaInfo("Usuarios", totalUsuarios.ToString(), 1, Color.FromArgb(34, 139, 34)));
            panelStats.Controls.Add(CrearTarjetaInfo("Prestamos Activos", prestActivos.ToString(), 2, Color.FromArgb(200, 120, 0)));

            panelContenido.Controls.AddRange(new Control[]
                { lblBienvenida, lblSubtitulo, panelStats });
        }

        private Panel CrearTarjetaInfo(string titulo, string valor, int posicion, Color color)
        {
            var panel = new Panel
            {
                Location = new Point(posicion * 200, 0),
                Size = new Size(180, 80),
                BackColor = color
            };
            panel.Controls.Add(new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            });
            panel.Controls.Add(new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 22f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 30),
                AutoSize = true
            });
            return panel;
        }

        private void LimpiarPanel()
        {
            panelContenido.Controls.Clear();
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var formLibros = new Forms.FormLibros(_servicio);
            panelContenido.Controls.Add(formLibros);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var formUsuarios = new Forms.FormUsuarios(_servicio);
            panelContenido.Controls.Add(formUsuarios);
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var formPrestamos = new Forms.FormPrestamos(_servicio);
            panelContenido.Controls.Add(formPrestamos);
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var formEstadisticas = new Forms.FormEstadisticas(_servicio);
            panelContenido.Controls.Add(formEstadisticas);
        }
    }
}