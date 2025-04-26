using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Data;
using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Repositorio
{
    public class ChatRepositorio : Repositorio<Chat>, IChatRepositorio
    {

        private readonly ApplicationDbContext _db;

        public ChatRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Chat chat)
        {
            var chatBD = _db.Chat.FirstOrDefault(b => b.Id == chat.Id);
            if (chatBD != null)
            {
                chatBD.Textos = chat.Textos;
                chatBD.UsuarioAplicacionId = chat.UsuarioAplicacionId;
                _db.SaveChanges();
            }
        }

    }
}