using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GestionBiblioteca_Desafio1.Services;

namespace GestionBiblioteca_Desafio1.Forms
{
    public class FormEstadisticas : UserControl
    {
        private readonly BibliotecaService _servicio;

        public FormEstadisticas(BibliotecaService servicio)
        {
            _servicio = servicio;
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            var lblTitulo = new Label
            {
                Text = "Estadisticas",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 15),
                AutoSize = true
            };

            int totalLibros = 0, librosDisp = 0, librosPrest = 0;
            int totalUsuarios = 0, prestActivos = 0, prestTotal = 0;

            foreach (var m in _servicio.ObtenerMateriales())
            {
                totalLibros++;
                if (m.Prestado) librosPrest++;
                else librosDisp++;
            }
            foreach (var u in _servicio.ObtenerUsuarios()) totalUsuarios++;
            foreach (var p in _servicio.ObtenerPrestamos())
            {
                prestTotal++;
                if (p.Activo) prestActivos++;
            }

            var panelTarjetas = new Panel
            {
                Location = new Point(15, 55),
                Size = new Size(950, 90),
                BackColor = Color.Transparent
            };

            panelTarjetas.Controls.Add(CrearTarjeta("Total Libros", totalLibros, 0, Color.FromArgb(30, 58, 95)));
            panelTarjetas.Controls.Add(CrearTarjeta("Disponibles", librosDisp, 1, Color.FromArgb(34, 139, 34)));
            panelTarjetas.Controls.Add(CrearTarjeta("Prestados", librosPrest, 2, Color.FromArgb(200, 120, 0)));
            panelTarjetas.Controls.Add(CrearTarjeta("Usuarios", totalUsuarios, 3, Color.FromArgb(100, 50, 150)));
            panelTarjetas.Controls.Add(CrearTarjeta("Prestamos Activos", prestActivos, 4, Color.FromArgb(180, 50, 50)));

            var lblGraf1 = new Label
            {
                Text = "Top Libros Mas Prestados",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 160),
                AutoSize = true
            };

            var chart1 = new Chart { Location = new Point(15, 185), Size = new Size(440, 280), BackColor = Color.White };
            chart1.ChartAreas.Add(new ChartArea("area1") { BackColor = Color.White });
            var series1 = new Series("Prestamos") { ChartType = SeriesChartType.Bar, Color = Color.FromArgb(30, 58, 95), IsValueShownAsLabel = true };
            chart1.Series.Add(series1);
            foreach (var item in _servicio.TopLibrosPrestados(5))
            {
                string titulo = item.Titulo.Length > 20 ? item.Titulo.Substring(0, 20) + "..." : item.Titulo;
                series1.Points.AddXY(titulo, item.Veces);
            }

            var lblGraf2 = new Label
            {
                Text = "Usuarios Mas Activos",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(475, 160),
                AutoSize = true
            };

            var chart2 = new Chart { Location = new Point(475, 185), Size = new Size(440, 280), BackColor = Color.White };
            chart2.ChartAreas.Add(new ChartArea("area2") { BackColor = Color.White });
            var series2 = new Series("Prestamos") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(34, 139, 34), IsValueShownAsLabel = true };
            chart2.Series.Add(series2);
            foreach (var item in _servicio.UsuariosMasActivos(5))
            {
                string nombre = item.Usuario.Length > 15 ? item.Usuario.Substring(0, 15) + "..." : item.Usuario;
                series2.Points.AddXY(nombre, item.Veces);
            }

            var lblGraf3 = new Label
            {
                Text = "Estado de Prestamos",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                Location = new Point(15, 480),
                AutoSize = true
            };

            var chart3 = new Chart { Location = new Point(15, 505), Size = new Size(280, 220), BackColor = Color.White };
            chart3.ChartAreas.Add(new ChartArea("area3") { BackColor = Color.White });
            var series3 = new Series("Estado") { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            chart3.Series.Add(series3);
            chart3.Legends.Add(new Legend { Font = new Font("Segoe UI", 8f) });
            if (prestActivos > 0) series3.Points.AddXY("Activos", prestActivos);
            if (prestTotal - prestActivos > 0) series3.Points.AddXY("Devueltos", prestTotal - prestActivos);
            if (series3.Points.Count > 0) series3.Points[0].Color = Color.FromArgb(34, 139, 34);
            if (series3.Points.Count > 1) series3.Points[1].Color = Color.FromArgb(30, 58, 95);

            this.Controls.AddRange(new Control[]
            {
                lblTitulo, panelTarjetas,
                lblGraf1, chart1,
                lblGraf2, chart2,
                lblGraf3, chart3
            });
        }

        private Panel CrearTarjeta(string titulo, int valor, int posicion, Color color)
        {
            var panel = new Panel
            {
                Location = new Point(posicion * 195, 0),
                Size = new Size(185, 80),
                BackColor = color
            };
            panel.Controls.Add(new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            });
            panel.Controls.Add(new Label
            {
                Text = valor.ToString(),
                Font = new Font("Segoe UI", 22f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 30),
                AutoSize = true
            });
            return panel;
        }
    }
}