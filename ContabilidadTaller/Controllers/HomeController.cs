using Aspose.CAD.FileFormats.Cgm.Commands;
using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace ContabilidadTaller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string? texto)
        {
            if (texto != null)
            {
                ViewBag.Texto = texto;
            }
            return View();
        }
        public IActionResult IrAEmpresas()
        {
            return RedirectToAction("IndexEmpresas", "Empresas");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    //------------------------------------------------------------------------------------

    












        // Crear Nueva Carpeta en Root
        private readonly IWebHostEnvironment _env;
        private readonly ServiciosJson _serJson;

        public HomeController(IWebHostEnvironment env, ServiciosJson serJson)
        {
            _env = env;
            _serJson = serJson;
        }

        [HttpPost]
        public IActionResult CrearCarpeta(string nombreCarpeta)
        {
            string subRuta = "\\files\\";

            string rutaNuevaCarpeta = Path.Combine(_env.WebRootPath + subRuta, nombreCarpeta);

            if (!Directory.Exists(rutaNuevaCarpeta))
            {
                Directory.CreateDirectory(rutaNuevaCarpeta);
            }
            else
            {
                return Content($"ALERTA: Carpeta ubicada en {rutaNuevaCarpeta} ya existente");
            }
            return Content($"Carpeta creada en: {rutaNuevaCarpeta}");
        }

        [HttpPost]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            string destino = "";
            if (archivo != null && archivo.Length > 0)
            {
                var rutaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", archivo.FileName);
                destino = rutaDestino;
                if (!System.IO.File.Exists(rutaDestino))
                {
                    using (var stream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }
                }
                else
                {
                    // No sobreescribir archivo y agregarle un (contador)
                    var nombreOriginal = Path.GetFileNameWithoutExtension(archivo.FileName);
                    var extension = Path.GetExtension(archivo.FileName);
                    var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\");
                    string rutaFinal = "";

                    int contador = 1;
                    while (System.IO.File.Exists(rutaFinal))
                    {
                        var nuevoNombre = $"{nombreOriginal}({contador}){extension}";
                        rutaFinal = Path.Combine(carpeta, nuevoNombre);
                        contador++;
                    }

                    using (var stream = new FileStream(rutaFinal, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }

                    return Content($"Archivo ya existe ({destino})");
                } 
            }
            //return Ok();
            return Content($"Archivo guardado en: {destino}");
        }







        // ----- PRUEBAS SUELTAS -----

        /*
        [HttpPost]
        public IActionResult ConvertirArchivoAJson(IFormFile archivo)
        {
            _serJson.ConvertirArchivoAJson(archivo);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("RecuperarArchivoJson unico")]
        public async Task<IActionResult> RecuperarArchivoJson()
        {
            string texto = await _serJson.RecuperarArchivoDesdeJson();
            return RedirectToAction("Index", "Home", new { texto = texto });
        }


        [HttpGet]
        public IActionResult RecuperarArchivoJson(string texto)
        {
            _serJson.AbrirArchivoEnSuUbicacion(texto);
            return NoContent();
        }
        */

    }
}
