using System;

namespace GestionBiblioteca_Desafio1.Domain
{
    public abstract class MaterialBiblioteca
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public bool Prestado { get; private set; }
        public UsuarioBiblioteca UsuarioPrestamo { get; private set; }

        protected MaterialBiblioteca(string titulo, string autor, int anio)
        {
            Titulo = titulo?.Trim();
            Autor = autor?.Trim();
            Anio = anio;
            Prestado = false;
        }

        public void AsignarPrestamo(UsuarioBiblioteca usuario)
        {
            Prestado = true;
            UsuarioPrestamo = usuario;
        }

        public void Devolver()
        {
            Prestado = false;
            UsuarioPrestamo = null;
        }

        public virtual string ObtenerDescripcion()
            => $"{Titulo} - {Autor} ({Anio})";

        public string ObtenerEstado()
            => Prestado ? $"Prestado a {UsuarioPrestamo?.Nombre}" : "Disponible";
    }
}