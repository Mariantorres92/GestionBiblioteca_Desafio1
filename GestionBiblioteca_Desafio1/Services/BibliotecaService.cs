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

        // ── CRUD Materiales ──────────────────────────────────────────────
        public MaterialBiblioteca AgregarMaterial(MaterialBiblioteca m)
        {
            m.Id = ++_seqMat;
            _materiales[m.Id] = m;
            return m;
        }

        public bool ActualizarMaterial(MaterialBiblioteca m)
        {
            if (!_materiales.ContainsKey(m.Id)) return false;
            _materiales[m.Id] = m;
            return true;
        }

        public bool EliminarMaterial(int id)
        {
            if (_materiales.ContainsKey(id) && _materiales[id].Prestado)
                throw new InvalidOperationException("No se puede eliminar: el libro está prestado.");
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
            u.Id = ++_seqUser;
            _usuarios[u.Id] = u;
            return u;
        }

        public bool ActualizarUsuario(UsuarioBiblioteca u)
        {
            if (!_usuarios.ContainsKey(u.Id)) return false;
            _usuarios[u.Id] = u;
            return true;
        }

        public bool EliminarUsuario(int id)
        {
            bool tienePrestamos = _prestamos.Any(p => p.UsuarioId == id && p.Activo);
            if (tienePrestamos)
                throw new InvalidOperationException("No se puede eliminar: el usuario tiene préstamos activos.");
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
            if (m.Prestado) throw new InvalidOperationException("El libro ya está prestado.");

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
            if (!m.Prestado) throw new InvalidOperationException("El libro no está prestado.");

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