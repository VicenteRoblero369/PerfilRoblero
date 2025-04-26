using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Modelos;
using PerfilRoblero.Utilidades;

namespace PerfilRoblero.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ChatController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Upsert(int? id)
        {

            Chat chat = new Chat();

            if (id == null)
            {
                // Crear una nueva Bodega
                return View(chat);
            }
            // Actualizamos Bodega
            chat = await _unidadTrabajo.Chat.Obtener(id.GetValueOrDefault());
            if (chat == null)
            {
                return NotFound();
            }
            return View(chat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Chat chat)
        {
            var claimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = claimIdentidad.FindFirst(ClaimTypes.NameIdentifier);
            chat.UsuarioAplicacionId = claim.Value;


            if (ModelState.IsValid)
            {
                if (chat.Id == 0)
                {
                    await _unidadTrabajo.Chat.Agregar(chat);
                    TempData[DS.Exitosa] = "Chat creada Exitosamente";
                }
                else
                {

                    _unidadTrabajo.Chat.Actualizar(chat);
                    TempData[DS.Exitosa] = "Chat actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Chat";
            return View(chat);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Chat.ObtenerTodos(incluirPropiedades: "UsuarioAplicacion");
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var chatDb = await _unidadTrabajo.Chat.Obtener(id);
            if (chatDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Chat" });
            }
            _unidadTrabajo.Chat.Remover(chatDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Chat borrada exitosamente" });
        }

        #endregion
    }
}
