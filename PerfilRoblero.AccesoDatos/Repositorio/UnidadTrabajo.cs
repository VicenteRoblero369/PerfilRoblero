using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Data;

namespace PerfilRoblero.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IArchivoRepositorio Archivo { get; private set; }
        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }
        public IChatRepositorio Chat { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Archivo = new ArchivoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            Chat = new ChatRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
