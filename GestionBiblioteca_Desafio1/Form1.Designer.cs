namespace GestionBiblioteca_Desafio1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnPrestamos = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnLibros = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.Navy;
            this.panelMenu.Controls.Add(this.btnEstadisticas);
            this.panelMenu.Controls.Add(this.btnPrestamos);
            this.panelMenu.Controls.Add(this.btnUsuarios);
            this.panelMenu.Controls.Add(this.btnLibros);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 450);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Click += new System.EventHandler(this.btnEstadisticas_Click);
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstadisticas.ForeColor = System.Drawing.Color.White;
            this.btnEstadisticas.Location = new System.Drawing.Point(0, 221);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(200, 68);
            this.btnEstadisticas.TabIndex = 3;
            this.btnEstadisticas.Text = "📊 Estadísticas";
            this.btnEstadisticas.UseVisualStyleBackColor = true;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            // 
            // btnPrestamos
            // 
            this.btnPrestamos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrestamos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrestamos.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrestamos.ForeColor = System.Drawing.Color.White;
            this.btnPrestamos.Location = new System.Drawing.Point(0, 151);
            this.btnPrestamos.Name = "btnPrestamos";
            this.btnPrestamos.Size = new System.Drawing.Size(200, 70);
            this.btnPrestamos.TabIndex = 2;
            this.btnPrestamos.Text = "📋 Préstamos";
            this.btnPrestamos.UseVisualStyleBackColor = true;
            this.btnPrestamos.Click += new System.EventHandler(this.btnPrestamos_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 77);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(200, 74);
            this.btnUsuarios.TabIndex = 1;
            this.btnUsuarios.Text = "👤 Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnLibros
            // 
            this.btnLibros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLibros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLibros.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLibros.ForeColor = System.Drawing.Color.White;
            this.btnLibros.Location = new System.Drawing.Point(0, 0);
            this.btnLibros.Name = "btnLibros";
            this.btnLibros.Size = new System.Drawing.Size(200, 77);
            this.btnLibros.TabIndex = 0;
            this.btnLibros.Text = "📚 Libros";
            this.btnLibros.UseVisualStyleBackColor = true;
            this.btnLibros.Click += new System.EventHandler(this.btnLibros_Click);
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(200, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(600, 450);
            this.panelContenido.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.Text = "Sistema de Gestión de Biblioteca";
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnLibros;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnPrestamos;
        private System.Windows.Forms.Panel panelContenido;
    }
}

