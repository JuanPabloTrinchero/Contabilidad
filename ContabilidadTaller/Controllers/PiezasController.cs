using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContabilidadTaller.Controllers
{
    public class PiezasController : Controller
    {
        private readonly ServiciosJson _serJson;
        public PiezasController(ServiciosJson serJson) 
        {
            _serJson = serJson;
        }
        public IActionResult IndexPiezas(int idEmpresa, string nombreEmpresa, string texto)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.PiezasGuardadas = _serJson.ObtenerPiezasJson(idEmpresa).Count();
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            if (string.IsNullOrEmpty(texto))
            {
                ViewBag.Piezas = _serJson.PiezasOrdenadasPorId(idEmpresa);
            }
            else
            {
                ViewBag.Piezas = FiltarPiezas(idEmpresa, texto);
            }
            return View();
        }
        public IActionResult IndexNuevaPieza(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            return View();
        }
        public IActionResult IndexVerPieza(int idEmpresa, string nombreEmpresa, int idPieza)
        {
            Pieza p = _serJson.ElegirPieza(idEmpresa, idPieza);

            ViewBag.Pieza = p;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.NombresArchivos = _serJson.NombresDeArchivosDePieza(idEmpresa, idPieza);
            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarPieza(idEmpresa, idPieza);

            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaPieza(int idEmpresa, string nombreEmpresa, string descripcion, string? codigo, int? idConjunto, List<string> notas, List<IFormFile> archivos)
        {
            await _serJson.NuevaPieza(idEmpresa, descripcion, codigo, notas, idConjunto, archivos);
            return RedirectToAction("IndexPiezas", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa});
        }
        public List<Pieza> FiltarPiezas(int idEmpresa, string texto)
        {
            return _serJson.ObtenerPiezasJson(idEmpresa).Where(e => e.Descripcion.Contains(texto, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        [HttpPost]
        public IActionResult Pieza_ModificarDescripcion(int idEmpresa, int idPieza, string nuevaDescripcion, string nombreEmpresa)
        {
            _serJson.ModificarDescripcionPieza(idEmpresa, idPieza, nuevaDescripcion);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza});
        }
        [HttpPost]
        public IActionResult Pieza_AgregarNota(int idEmpresa, int idPieza, string nuevaNota, string nombreEmpresa)
        {
            _serJson.AgregarNotaPieza(idEmpresa, idPieza, nuevaNota);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public IActionResult Pieza_ModificarNota(int idEmpresa, string nombreEmpresa, int idPieza, string notaActual, string notaModificada)
        {
            _serJson.ModificarNotaPieza(idEmpresa, idPieza, notaActual, notaModificada);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public IActionResult Pieza_EliminarNota(int idEmpresa, string nombreEmpresa, int idPieza, string notaEliminar)
        {
            _serJson.EliminarNotaPieza(idEmpresa, idPieza, notaEliminar);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPieza(int idEmpresa, string nombreEmpresa, int idPieza, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);
            Pieza p = _serJson.ElegirPieza(idPieza, idEmpresa);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                if (p.Archivos != null && p.Archivos.Any())
                {
                    foreach (string arch in p.Archivos)
                    {
                        _serJson.BorrarArchivo(idEmpresa, "Pieza", idPieza, arch);
                    }
                }
                _serJson.BorrarPieza(idEmpresa, idPieza);
                TempData["Mensaje"] = "Pieza Eliminada";
                return RedirectToAction("IndexPiezas", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
            }
        }
        [HttpPost]
        public IActionResult Pieza_ModificarCodigo(int idEmpresa, string nombreEmpresa, int idPieza, string nuevoCodigo)
        {
            _serJson.Agregar_ModificarCodigo(idEmpresa, idPieza, nuevoCodigo);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public IActionResult Pieza_BorrarCodigo(int idEmpresa, string nombreEmpresa, int idPieza)
        {
            _serJson.BorrarCodigo(idEmpresa, idPieza);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public IActionResult Pieza_AgregarPiezaAConjunto(int idEmpresa, string nombreEmpresa, int idPieza, int idConjunto)
        {
            _serJson.AsignarPiezaAConjunto(idEmpresa, idPieza, idConjunto);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }
        [HttpPost]
        public IActionResult Pieza_QuitarPiezaDeConjunto(int idEmpresa, string nombreEmpresa, int idPiezaQuitar, int idConjunto)
        {
            _serJson.QuitarPiezaDeConjunto(idEmpresa, idConjunto, idPiezaQuitar);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPiezaQuitar });
        }



        //--- ARCHIVOS ---
        [HttpPost]
        public async Task<IActionResult> SubirArchivoPieza(IFormFile archivo, int idEmpresa, int idPieza, string nombreEmpresa, bool desdePedido, int idPedido, int? idEmpleado)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido");

            string carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Piezas");
            string carpetaAbsoluta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", carpetaRelativa);

            if (!Directory.Exists(carpetaAbsoluta))
                Directory.CreateDirectory(carpetaAbsoluta);

            string nombreArchivo = archivo.FileName;
            string rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombreArchivo);

            if (System.IO.File.Exists(rutaAbsoluta))
            {
                string nombreOriginal = Path.GetFileNameWithoutExtension(nombreArchivo);
                string extension = Path.GetExtension(nombreArchivo);
                int contador = 1;

                while (System.IO.File.Exists(rutaAbsoluta))
                {
                    nombreArchivo = $"{nombreOriginal}({contador}){extension}";
                    rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombreArchivo);
                    contador++;
                }
            }

            using (var stream = new FileStream(rutaAbsoluta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            string rutaParaJson = Path.Combine("\\", carpetaRelativa, nombreArchivo);
            _serJson.AgregarArchivoPieza(idEmpresa, idPieza, rutaParaJson);

            if (desdePedido == true)
            {
                return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
            }
            else
            {
                return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
            }
        }
        [HttpGet]
        public IActionResult AbrirPiezaUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Pieza_EliminarArchivo(int idEmpresa, string nombreEmpresa, int idPieza, string rutaArchivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Pieza", idPieza, rutaArchivo);
            _serJson.EliminarArchivoPieza(idEmpresa, idPieza, rutaArchivo);
            return RedirectToAction("IndexVerPieza", "Piezas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPieza = idPieza });
        }


    }
}
