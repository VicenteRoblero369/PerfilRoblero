using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using PerfilRoblero.AccesoDatos.Repositorio.IRepositorio;
using PerfilRoblero.Modelos;
using PerfilRoblero.Utilidades;

namespace PerfilRoblero.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchivoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;//
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArchivoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = DS.Role_Admin)]
        public async Task<IActionResult> Upsert(int? id)
        {
            Archivo archivo = new Archivo();
            if (id == null)
            {
                archivo.Estado = true;
                return View(archivo);
            }
            archivo = await _unidadTrabajo.Archivo.Obtener(id.GetValueOrDefault());
            if (archivo == null)
            {
                return NotFound();
               // return File(archivo.ArchivoUrl, "application/pdf", archivo.ArchivoUrl);
            }
            return View(archivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Archivo archivo)
        {

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (archivo.Id == 0)
                {
                    // Crear
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);//lo  almacema en el espacio de memmoria
                    }
                    archivo.ArchivoUrl = fileName + extension;
                    await _unidadTrabajo.Archivo.Agregar(archivo);
                }
                else
                {
                    // Actualizar
                    var objArchivo = await _unidadTrabajo.Archivo.ObtenerPrimero(p => p.Id == archivo.Id, isTracking: false);
                    if (files.Count > 0)  // 
                    {
                        string upload = webRootPath + DS.ImagenRuta;
                        string fileNAme = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objArchivo.ArchivoUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileNAme + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        archivo.ArchivoUrl = fileNAme + extension;
                       // archivo.ArchivoUrl = @"\Imagenes\Archivo\" + fileNAme + extension;
                    } // Caso contrario no se carga una nueva imagen
                    else
                    {
                        archivo.ArchivoUrl = objArchivo.ArchivoUrl;
                    }
                    _unidadTrabajo.Archivo.Actualizar(archivo);
                }
                TempData[DS.Exitosa] = "Transaccion Exitosa!";
                await _unidadTrabajo.Guardar();

                return RedirectToAction("Index");

            }
          
            return View(archivo);
              
        }
       

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Archivo.ObtenerTodos();
            return Json(new { data = todos });
        }
        [Authorize(Roles = DS.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var ArchivoDb = await _unidadTrabajo.Archivo.Obtener(id);
            if (ArchivoDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Archivo" });
            }
            _unidadTrabajo.Archivo.Remover(ArchivoDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Archivo borrada exitosamente" });
        }

        //esta funcion valida si exixte el mismo registro de ussuario en la base de tados entonces marca duplicado sin guardar
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Archivo.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }

        #endregion

    }
}

