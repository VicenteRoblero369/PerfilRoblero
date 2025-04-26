using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Repositorio.IRepositorio
{
    public interface IArchivoRepositorio : IRepositorio<Archivo>
    {
        void Actualizar(Archivo archivo);
    }
}
