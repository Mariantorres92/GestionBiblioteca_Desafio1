using System;

namespace GestionBiblioteca_Desafio1.Domain
{
    public class UsuarioBiblioteca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Carnet { get; set; }
        public string Correo { get; set; }

        public UsuarioBiblioteca(string nombre, string carnet, string correo)
        {
            Nombre = nombre?.Trim();
            Carnet = carnet?.Trim();
            Correo = correo?.Trim();
        }

        public override string ToString() => $"{Nombre} ({Carnet})";
    }
}