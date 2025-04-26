using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Data;
using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Repositorio
{
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {

        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}