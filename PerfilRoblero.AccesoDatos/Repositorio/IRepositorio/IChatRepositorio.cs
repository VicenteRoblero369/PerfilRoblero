using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Repositorio.IRepositorio
{
    public interface IChatRepositorio : IRepositorio<Chat>
    {
        void Actualizar(Chat chat);

        //IEnumerable<SelectListItem> ObtenerTodosDropdownLista(String obj);
    }
}
