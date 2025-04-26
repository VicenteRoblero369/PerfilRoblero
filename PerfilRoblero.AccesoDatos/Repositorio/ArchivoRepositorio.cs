using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Data;
using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Repositorio
{
    public class ArchivoRepositorio : Repositorio<Archivo>, IArchivoRepositorio
    {

        private readonly ApplicationDbContext _db;

        public ArchivoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Archivo archivo)
        {
            var archivoBD = _db.Archivos.FirstOrDefault(b => b.Id == archivo.Id);
            if (archivoBD != null)
            {
                if (archivo.ArchivoUrl != null)
                {
                    archivoBD.ArchivoUrl = archivo.ArchivoUrl;
                }
                archivoBD.Nombre = archivo.Nombre;
                archivoBD.Descripcion = archivo.Descripcion;
                archivoBD.Estado = archivo.Estado;
                _db.SaveChanges();
            }
        }
    }
}