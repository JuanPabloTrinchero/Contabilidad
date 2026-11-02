using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using ContabilidadTaller.Services;
using ContabilidadTaller.Models;

namespace ContabilidadTaller.Controllers
{
    public class UtilajesController : Controller
    {
        private readonly ServiciosJson _serJson;
        public UtilajesController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexUtilajes(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.Utilajes = _serJson.ObtenerUtilajesJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);

            return View();
        }

        public IActionResult IndexVerUtilaje(int idEmpresa, string nombreEmpresa, int idUtilaje)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            Utilaje utilaje = _serJson.ElegirUtilaje(idEmpresa, idUtilaje);
            ViewBag.Utilaje = utilaje;

            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.NombresArchivos = _serJson.NombresDeArchivos(utilaje.Archivos);

            if (utilaje.IdPiezaFK != null)
            {
                ViewBag.Pieza = _serJson.ElegirPieza(idEmpresa, (int)utilaje.IdPiezaFK);
            }

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarUtilaje(idEmpresa, idUtilaje);

            return View();
        }

        public IActionResult IndexNuevoUtilaje(int idEmpresa, string nombreEmpresa, int? idPieza)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            if (idPieza != null)
            {
                ViewBag.Pieza = idPieza;;
            }
            else
            {
                ViewBag.Pieza = -1; 
            }

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);

            return View();
        }

        public async Task<IActionResult> Utilaje_GuardarNuevo(int idEmpresa, string nombreEmpresa, string descripcion, List<string>? notas, List<IFormFile>? archivos, int? idPiezaAsociada)
        {
            await _serJson.NuevoUtilaje(idEmpresa, descripcion, notas, archivos, idPiezaAsociada);

            return RedirectToAction("IndexUtilajes", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }

        public IActionResult Utilaje_ModificarDescripcion(int idEmpresa, string nombreEmpresa, int idUtilaje, string nuevaDescripcion)
        {
            _serJson.ModificarDescripcionUtilaje(idEmpresa, idUtilaje, nuevaDescripcion);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }
        [HttpPost]
        public IActionResult Utilaje_AgregarNota(int idEmpresa, string nombreEmpresa, int idUtilaje, string nuevaNota)
        {
            _serJson.AgregarNotaUtilaje(idEmpresa, idUtilaje, nuevaNota);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }
        [HttpPost]
        public IActionResult Utilaje_ModificarNota(int idEmpresa, string nombreEmpresa, int idUtilaje, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaUtilaje(idEmpresa, idUtilaje, notaVieja, notaModificada);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }
        [HttpPost]
        public IActionResult Utilaje_BorrarNota(int idEmpresa, string nombreEmpresa, int idUtilaje, string notaBorrar)
        {
            _serJson.BorrarNotaUtilaje(idEmpresa, idUtilaje, notaBorrar);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }

        [HttpPost]
        public IActionResult Utilaje_AsignarPieza(int idEmpresa, string nombreEmpresa, int idUtilaje, int idPieza)
        {
            _serJson.AsignarUtilajeAPieza(idEmpresa, idUtilaje, idPieza);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }
        [HttpPost]
        public IActionResult Utilaje_QuitarPieza(int idEmpresa, string nombreEmpresa, int idUtilaje)
        {
            _serJson.QuitarPiezaDeUtilaje(idEmpresa, idUtilaje);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }

        public async Task<IActionResult> Utilaje_SubirArchivo(int idEmpresa, string nombreEmpresa, int idUtilaje, List<IFormFile> archivo)
        {
            await _serJson.GuardarArchivos(idEmpresa, "Utilaje", idUtilaje, archivo);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }
        [HttpGet]
        public IActionResult Utilaje_AbrirArchivoEnUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Utilaje_EliminarArchivo(int idEmpresa, string nombreEmpresa, int idUtilaje, string rutaArchivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Utilaje", idUtilaje, rutaArchivo);

            return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilaje });
        }

        public IActionResult Utilaje_Borrar(int idEmpresa, string nombreEmpresa, int idUtilajeBorrar, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarUtilaje(idEmpresa, idUtilajeBorrar);

                TempData["Mensaje"] = "Utilaje Eliminado";

                return RedirectToAction("IndexUtilajes", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";

                return RedirectToAction("IndexVerUtilaje", "Utilajes", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idUtilaje = idUtilajeBorrar });
            }
        }


    }
}
