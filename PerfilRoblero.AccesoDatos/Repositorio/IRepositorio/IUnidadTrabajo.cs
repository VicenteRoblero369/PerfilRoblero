namespace PerfilRoblero.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IArchivoRepositorio Archivo { get; }
        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }
        IChatRepositorio Chat { get; }

        Task Guardar();
    }
}
