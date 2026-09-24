using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContabilidadTaller.Controllers
{
    public class ConjuntosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public ConjuntosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }

        public IActionResult IndexConjuntos(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);

            return View();
        }

        public IActionResult IndexVerConjunto(int idEmpresa, string nombreEmpresa, int idConjunto, bool desdeConjuntoNuevo)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            if (desdeConjuntoNuevo == true)
            {
                ViewBag.Conjunto = _serJson.UltimoConjunto(idEmpresa);
            }
            else
            {
                ViewBag.Conjunto = _serJson.ElegirConjunto(idEmpresa, idConjunto);
                ViewBag.PiezasDeConjunto = _serJson.PiezasDeConjunto(idEmpresa, idConjunto);
            }

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarConjunto(idEmpresa, idConjunto);

            return View();
        }

        public IActionResult IndexNuevoConjunto(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.idEmpresa = idEmpresa;
            ViewBag.nombreEmpresa = nombreEmpresa;

            ViewBag.PiezasSinConjunto = _serJson.PiezasSinConjunto(idEmpresa);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevoConjunto(int idEmpresa, string nombreEmpresa, string descripcion, string? codigo, List<string>? notas, bool desdeConjuntoNuevo, List<string>? archivos)
        {
            int idNuevoConjunto = await _serJson.NuevoConjunto(idEmpresa, descripcion, codigo, notas, archivos);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idNuevoConjunto, desdeConjuntoNuevo = desdeConjuntoNuevo });
        }
        [HttpPost]
        public IActionResult Conjunto_ModificarDescripcion(int idEmpresa, string nombreEmpresa, int idConjunto, string nuevaDescripcion)
        {
            _serJson.ModificarDescripcionConjunto(idEmpresa, idConjunto, nuevaDescripcion);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public IActionResult Conjunto_ModificarCodigo(int idEmpresa, string nombreEmpresa, int idConjunto, string nuevoCodigo)
        {
            _serJson.ModificarCodigoConjunto(idEmpresa, idConjunto, nuevoCodigo);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public IActionResult Conjunto_QuitarCodigo(int idEmpresa, string nombreEmpresa, int idConjunto)
        {
            _serJson.QuitarCodigoConjunto(idEmpresa, idConjunto);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public IActionResult Conjunto_AgregarPiezasAConjunto(int idEmpresa, string nombreEmpresa, int idConjunto, List<int>? idsPiezas)
        {
            _serJson.AgregarPiezasAConjunto(idEmpresa, idConjunto, idsPiezas);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto});
        }
        [HttpPost]
        public async Task<IActionResult> Conjunto_CrearPiezaEnConjunto(int idEmpresa, string nombreEmpresa, int idConjunto, string descripcion, string? codigo, List<string>? notas, List<IFormFile>? archivos)
        {
            await _serJson.NuevaPieza(idEmpresa, descripcion, codigo, notas, idConjunto, archivos);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        [HttpPost]
        public IActionResult Conjunto_QuitarPiezaDeConjunto(int idEmpresa, string nombreEmpresa, int idConjunto, int idPiezaQuitar)
        {
            _serJson.QuitarPiezaDeConjunto(idEmpresa, idConjunto, idPiezaQuitar);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        public IActionResult Conjunto_BorrarPiezaDesdeConjunto(int idEmpresa, string nombreEmpresa, int idConjunto, int idPiezaBorrar)
        {
            Pieza p = _serJson.ElegirPieza(idEmpresa, idPiezaBorrar);
            if (p.Archivos != null && p.Archivos.Any())
            {
                foreach (string arch in p.Archivos)
                {
                    _serJson.EliminarArchivoPieza(idEmpresa, idPiezaBorrar, arch);
                    _serJson.BorrarArchivo(idEmpresa, "Pieza", idPiezaBorrar, arch);
                }
            }
            _serJson.BorrarPieza(idEmpresa, idPiezaBorrar);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        [HttpPost]
        public IActionResult Conjunto_Borrar(int idEmpresa, string nombreEmpresa, int idConjuntoEliminar, int idSocio, string? contraseñaSocio)
        {
            if (idSocio != null && contraseñaSocio != null)
            {
                Socio s = _serJson.ElegirSocio(idSocio);
                if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
                {
                    _serJson.BorrarConjunto(idEmpresa, idConjuntoEliminar);
                    TempData["Mensaje"] = "Conjunto Eliminado";
                    return RedirectToAction("IndexConjuntos", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
                }
                else
                {
                    TempData["Mensaje"] = "Contraseña Incorrecta";
                    return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjuntoEliminar });
                }
            }
            else
            {
                return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjuntoEliminar });
            } 
        }
        [HttpPost]
        public IActionResult Conjunto_AgregarNota(int idEmpresa, string nombreEmpresa, int idConjunto, string nuevaNota)
        {
            _serJson.AgregarNotaConjunto(idEmpresa, idConjunto, nuevaNota);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        [HttpPost]
        public IActionResult Conjunto_ModificarNota(int idEmpresa, string nombreEmpresa, int idConjunto, string notaAModificar, string notaModificada)
        {
            _serJson.ModificarNotaConjunto(idEmpresa, idConjunto, notaAModificar, notaModificada);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        [HttpPost]
        public IActionResult Conjunto_BorrarNota(int idEmpresa, string nombreEmpresa, int idConjunto, string notaBorrar)
        {
            _serJson.BorrarNotaConjunto(idEmpresa, idConjunto, notaBorrar);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }



        [HttpPost]
        public async Task<IActionResult> Conjunto_SubirArchivo(int idEmpresa, string nombreEmpresa, int idConjunto, List<IFormFile> archivos)
        {
            await _serJson.GuardarArchivos(idEmpresa, "Conjunto", idConjunto, archivos);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }
        [HttpGet]
        public IActionResult Conjunto_AbrirArchivoEnUbicacion(string archivo)
        {
            _serJson.AbrirArchivoEnSuUbicacion(archivo);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Conjunto_BorrarArchivo(int idEmpresa, string nombreEmpresa, int idConjunto, string archivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Conjunto", idConjunto, archivo);
            return RedirectToAction("IndexVerConjunto", "Conjuntos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idConjunto = idConjunto });
        }



        

    }
}
