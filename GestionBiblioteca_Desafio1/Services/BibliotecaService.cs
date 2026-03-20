using System;
using System.Collections.Generic;
using System.Linq;
using GestionBiblioteca_Desafio1.Domain;

namespace GestionBiblioteca_Desafio1.Services
{
    public class BibliotecaService
    {
        private int _seqMat = 0, _seqUser = 0, _seqPrest = 0;

        private readonly Dictionary<int, MaterialBiblioteca> _materiales =
            new Dictionary<int, MaterialBiblioteca>();
        private readonly Dictionary<int, UsuarioBiblioteca> _usuarios =
            new Dictionary<int, UsuarioBiblioteca>();
        private readonly List<Prestamo> _prestamos =
            new List<Prestamo>();

        public BibliotecaService()
        {
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            AgregarMaterial(new Libro("Cien anos de soledad", "Gabriel Garcia Marquez", 1967, 432, "978-0307474728", "Novela"));
            AgregarMaterial(new Libro("El principito", "Antoine de Saint-Exupery", 1943, 96, "978-0156012195", "Literatura"));
            AgregarMaterial(new Libro("Don Quijote de la Mancha", "Miguel de Cervantes", 1605, 863, "978-8420412146", "Clasico"));
            AgregarMaterial(new Libro("Harry Potter", "J.K. Rowling", 1997, 309, "978-0439708180", "Fantasia"));
            AgregarMaterial(new Libro("El codigo Da Vinci", "Dan Brown", 2003, 454, "978-0307474278", "Thriller"));
            AgregarMaterial(new Libro("Orgullo y prejuicio", "Jane Austen", 1813, 432, "978-0141439518", "Romance"));
            AgregarMaterial(new Libro("1984", "George Orwell", 1949, 328, "978-0451524935", "Distopia"));
            AgregarMaterial(new Libro("El alquimista", "Paulo Coelho", 1988, 208, "978-0062315007", "Novela"));

            AgregarUsuario(new UsuarioBiblioteca("Maria Torres", "2021-001", "maria@email.com"));
            AgregarUsuario(new UsuarioBiblioteca("Carlos Lopez", "2021-002", "carlos@email.com"));
            AgregarUsuario(new UsuarioBiblioteca("Ana Martinez", "2021-003", "ana@email.com"));
            AgregarUsuario(new UsuarioBiblioteca("Pedro Ramirez", "2021-004", "pedro@email.com"));
            AgregarUsuario(new UsuarioBiblioteca("Laura Gonzalez", "2021-005", "laura@email.com"));

            RegistrarPrestamo(1, 1);
            RegistrarPrestamo(2, 2);
            RegistrarPrestamo(3, 3);
            RegistrarPrestamo(4, 1);
            RegistrarPrestamo(5, 4);
            RegistrarPrestamo(6, 5);
            RegistrarPrestamo(7, 2);

            RegistrarDevolucion(1);
            RegistrarDevolucion(3);
            RegistrarDevolucion(6);
        }

        // ── CRUD Materiales ──────────────────────────────────────────────
        public MaterialBiblioteca AgregarMaterial(MaterialBiblioteca m)
        {
            // Validacion de ISBN duplicado
            if (m is Libro libroNuevo)
            {
                bool isbnDuplicado = _materiales.Values
                    .OfType<Libro>()
                    .Any(l => l.ISBN.Equals(libroNuevo.ISBN, StringComparison.OrdinalIgnoreCase));
                if (isbnDuplicado)
                    throw new InvalidOperationException($"Ya existe un libro con el ISBN '{libroNuevo.ISBN}'.");
            }

            // Validacion de carnet duplicado
            if (string.IsNullOrWhiteSpace(m.Titulo))
                throw new ArgumentException("El titulo es obligatorio.");
            if (string.IsNullOrWhiteSpace(m.Autor))
                throw new ArgumentException("El autor es obligatorio.");
            if (m.Anio < 1000 || m.Anio > DateTime.Now.Year)
                throw new ArgumentException("El año no es valido.");

            m.Id = ++_seqMat;
            _materiales[m.Id] = m;
            return m;
        }

        public bool ActualizarMaterial(MaterialBiblioteca m)
        {
            if (!_materiales.ContainsKey(m.Id)) return false;

            // Validacion de ISBN duplicado al actualizar
            if (m is Libro libroActualizado)
            {
                bool isbnDuplicado = _materiales.Values
                    .OfType<Libro>()
                    .Any(l => l.ISBN.Equals(libroActualizado.ISBN, StringComparison.OrdinalIgnoreCase)
                              && l.Id != libroActualizado.Id);
                if (isbnDuplicado)
                    throw new InvalidOperationException($"Ya existe otro libro con el ISBN '{libroActualizado.ISBN}'.");
            }

            _materiales[m.Id] = m;
            return true;
        }

        public bool EliminarMaterial(int id)
        {
            if (_materiales.ContainsKey(id) && _materiales[id].Prestado)
                throw new InvalidOperationException("No se puede eliminar: el libro esta prestado.");
            return _materiales.Remove(id);
        }

        public IEnumerable<MaterialBiblioteca> ObtenerMateriales()
            => _materiales.Values.OrderBy(x => x.Titulo);

        public MaterialBiblioteca BuscarMaterialPorId(int id)
            => _materiales.ContainsKey(id) ? _materiales[id] : null;

        public IEnumerable<MaterialBiblioteca> BuscarMateriales(string termino)
        {
            termino = termino?.ToLower() ?? "";
            return _materiales.Values
                .Where(m => m.Titulo.ToLower().Contains(termino) ||
                            m.Autor.ToLower().Contains(termino))
                .OrderBy(m => m.Titulo);
        }

        // ── CRUD Usuarios ────────────────────────────────────────────────
        public UsuarioBiblioteca AgregarUsuario(UsuarioBiblioteca u)
        {
            // Validacion de carnet duplicado
            bool carnetDuplicado = _usuarios.Values
                .Any(x => x.Carnet.Equals(u.Carnet, StringComparison.OrdinalIgnoreCase));
            if (carnetDuplicado)
                throw new InvalidOperationException($"Ya existe un usuario con el carnet '{u.Carnet}'.");

            if (string.IsNullOrWhiteSpace(u.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(u.Carnet))
                throw new ArgumentException("El carnet es obligatorio.");
            if (string.IsNullOrWhiteSpace(u.Correo))
                throw new ArgumentException("El correo es obligatorio.");
            if (!u.Correo.Contains("@"))
                throw new ArgumentException("El correo no es valido.");

            u.Id = ++_seqUser;
            _usuarios[u.Id] = u;
            return u;
        }

        public bool ActualizarUsuario(UsuarioBiblioteca u)
        {
            if (!_usuarios.ContainsKey(u.Id)) return false;

            // Validacion de carnet duplicado al actualizar
            bool carnetDuplicado = _usuarios.Values
                .Any(x => x.Carnet.Equals(u.Carnet, StringComparison.OrdinalIgnoreCase)
                          && x.Id != u.Id);
            if (carnetDuplicado)
                throw new InvalidOperationException($"Ya existe otro usuario con el carnet '{u.Carnet}'.");

            _usuarios[u.Id] = u;
            return true;
        }

        public bool EliminarUsuario(int id)
        {
            bool tienePrestamos = _prestamos.Any(p => p.UsuarioId == id && p.Activo);
            if (tienePrestamos)
                throw new InvalidOperationException("No se puede eliminar: el usuario tiene prestamos activos.");
            return _usuarios.Remove(id);
        }

        public IEnumerable<UsuarioBiblioteca> ObtenerUsuarios()
            => _usuarios.Values.OrderBy(x => x.Nombre);

        public UsuarioBiblioteca BuscarUsuarioPorId(int id)
            => _usuarios.ContainsKey(id) ? _usuarios[id] : null;

        // ── Préstamos ────────────────────────────────────────────────────
        public Prestamo RegistrarPrestamo(int materialId, int usuarioId)
        {
            var m = _materiales.ContainsKey(materialId) ? _materiales[materialId] : null;
            var u = _usuarios.ContainsKey(usuarioId) ? _usuarios[usuarioId] : null;

            if (m == null) throw new InvalidOperationException("Libro no encontrado.");
            if (u == null) throw new InvalidOperationException("Usuario no encontrado.");
            if (m.Prestado) throw new InvalidOperationException("El libro ya esta prestado.");

            m.AsignarPrestamo(u);

            var p = new Prestamo
            {
                Id = ++_seqPrest,
                MaterialId = m.Id,
                UsuarioId = u.Id,
                FechaPrestamo = DateTime.Now,
                TituloMaterial = m.Titulo,
                NombreUsuario = u.Nombre
            };
            _prestamos.Add(p);
            return p;
        }

        public void RegistrarDevolucion(int materialId)
        {
            var m = _materiales.ContainsKey(materialId) ? _materiales[materialId] : null;
            if (m == null) throw new InvalidOperationException("Libro no encontrado.");
            if (!m.Prestado) throw new InvalidOperationException("El libro no esta prestado.");

            m.Devolver();
            var p = _prestamos.LastOrDefault(x => x.MaterialId == materialId && x.Activo);
            if (p != null) p.FechaDevolucion = DateTime.Now;
        }

        public IEnumerable<Prestamo> ObtenerPrestamos(bool soloActivos = false)
            => soloActivos ? _prestamos.Where(p => p.Activo) : (IEnumerable<Prestamo>)_prestamos;

        // ── Estadísticas ─────────────────────────────────────────────────
        public IEnumerable<(string Titulo, int Veces)> TopLibrosPrestados(int top = 5)
            => _prestamos
                .GroupBy(p => p.MaterialId)
                .Select(g => (Titulo: _materiales[g.Key].Titulo, Veces: g.Count()))
                .OrderByDescending(x => x.Veces)
                .Take(top);

        public IEnumerable<(string Usuario, int Veces)> UsuariosMasActivos(int top = 5)
            => _prestamos
                .GroupBy(p => p.UsuarioId)
                .Select(g => (Usuario: _usuarios[g.Key].Nombre, Veces: g.Count()))
                .OrderByDescending(x => x.Veces)
                .Take(top);
    }
}