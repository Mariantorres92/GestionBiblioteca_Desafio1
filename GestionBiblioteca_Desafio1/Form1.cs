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

        private void LimpiarPanel()
        {
            panelContenido.Controls.Clear();
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var lbl = new Label
            {
                Text = "📚 Módulo de Libros - Próximamente",
                Font = new Font("Segoe UI", 14f),
                ForeColor = Color.FromArgb(30, 58, 95),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            panelContenido.Controls.Add(lbl);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var lbl = new Label
            {
                Text = "👤 Módulo de Usuarios - Próximamente",
                Font = new Font("Segoe UI", 14f),
                ForeColor = Color.FromArgb(30, 58, 95),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            panelContenido.Controls.Add(lbl);
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var lbl = new Label
            {
                Text = "📋 Módulo de Préstamos - Próximamente",
                Font = new Font("Segoe UI", 14f),
                ForeColor = Color.FromArgb(30, 58, 95),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            panelContenido.Controls.Add(lbl);
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            var lbl = new Label
            {
                Text = "📊 Módulo de Estadísticas - Próximamente",
                Font = new Font("Segoe UI", 14f),
                ForeColor = Color.FromArgb(30, 58, 95),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            panelContenido.Controls.Add(lbl);
        }
    }
}