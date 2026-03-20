using System;

namespace GestionBiblioteca_Desafio1.Domain
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public bool Activo => FechaDevolucion == null;

        public string TituloMaterial { get; set; }
        public string NombreUsuario { get; set; }
    }
}