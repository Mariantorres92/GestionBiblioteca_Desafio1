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