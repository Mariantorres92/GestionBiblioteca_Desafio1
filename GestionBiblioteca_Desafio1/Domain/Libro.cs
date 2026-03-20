using System;

namespace GestionBiblioteca_Desafio1.Domain
{
    public class Libro : MaterialBiblioteca
    {
        public int Paginas { get; set; }
        public string ISBN { get; set; }
        public string Categoria { get; set; }

        public Libro(string titulo, string autor, int anio, int paginas, string isbn, string categoria)
            : base(titulo, autor, anio)
        {
            Paginas = paginas;
            ISBN = isbn?.Trim();
            Categoria = categoria?.Trim();
        }

        public override string ObtenerDescripcion()
            => $"[Libro] {Titulo} - {Autor} ({Anio}) | ISBN: {ISBN} | {Paginas} págs. | {Categoria}";
    }
}