using ContabilidadTaller.Interfaces;
using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ContabilidadTaller.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ServiciosJson _serJson;
        private readonly IWebHostEnvironment _env;
        public EmpresasController(ServiciosJson serJson, IWebHostEnvironment env)
        {
            _serJson = serJson;
            _env = env;
        }
        public IActionResult IndexEmpresas()
        {

            ViewBag.EmpresasGuardadas = _serJson.ObtenerEmpresasJson().Count();
            ViewBag.Empresas = _serJson.EmpresasOrdenadasPorId();

            return View();
        }
        public IActionResult IndexNuevaEmpresa()
        {
            return View();
        }
        public IActionResult IndexVerEmpresa(int idEmpresa, bool mostrarNotas, bool estiloBotonVerNotas)
        {
            ViewBag.Empresa = _serJson.ElegirEmpresa(idEmpresa);
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.MostrarNotas = mostrarNotas;
            ViewBag.EstiloBoton = estiloBotonVerNotas;

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarEmpresa(idEmpresa);

            return View();
        }
        public IActionResult IrAEmpleados()
        {
            return RedirectToAction("IndexEmpleados", "Empleados");
        }
        [HttpPost]
        public IActionResult NuevaEmpresa(string nombreEmpresa, int inicioCuit, int medioCuit, int finCuit, string direccion, List<string> notas)
        {
            int idEmpresaNueva = _serJson.GuardarNuevaEmpresa(nombreEmpresa, inicioCuit, medioCuit, finCuit, direccion, notas);

            CrearCarpetasEmpresa(idEmpresaNueva);

            return RedirectToAction("IndexEmpresas", "Empresas");
        }

        [HttpPost]
        public IActionResult ModificarNombre(int idEmpresa, string nuevoNombre)
        {
            _serJson.ModificarNombreEmpresa(idEmpresa, nuevoNombre);
            return RedirectToAction("IndexVerEmpresa", new { idEmpresa = idEmpresa, mostrarNotas = false, estiloBotonVerNotas = true });
        }

        [HttpPost]
        public IActionResult ModificarDireccion(int idEmpresa, string nuevaDireccion)
        {
            _serJson.ModificarDireccionEmpresa(idEmpresa, nuevaDireccion);
            return RedirectToAction("IndexVerEmpresa", new { idEmpresa = idEmpresa, mostrarNotas = false, estiloBotonVerNotas = true });
        }
        [HttpPost]
        public IActionResult Empresa_ModificarCuit(int idEmpresa, int inicioCuit, int medioCuit, int finCuit)
        {
            _serJson.ModificarCuitEmpresa(idEmpresa, inicioCuit, medioCuit, finCuit);
            return RedirectToAction("IndexVerEmpresa", new { idEmpresa = idEmpresa, mostrarNotas = false, estiloBotonVerNotas = true });
        }


        [HttpPost]
        public IActionResult ModificarNota(int idEmpresa, string nombreEmpresa, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaEmpresa(idEmpresa, notaVieja, notaModificada);
            return RedirectToAction("IndexVerEmpresa", "Empresas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, mostrarNotas = true, estiloBotonVerNotas = false });
        }
        [HttpPost]
        public IActionResult AgregarNota(int idEmpresa, string nombreEmpresa, string nuevaNota)
        {
            _serJson.AgregarNotaEmpresa(idEmpresa, nuevaNota);
            return RedirectToAction("IndexVerEmpresa", "Empresas", new { idEmpresa  = idEmpresa, nombreEmpresa = nombreEmpresa, mostrarNotas = true, estiloBotonVerNotas = false });
        }
        [HttpPost]
        public IActionResult EliminarNota(int idEmpresa, string nombreEmpresa, string notaEliminar)
        {
            _serJson.EliminarNotaEmpresa(idEmpresa, notaEliminar);
            return RedirectToAction("IndexVerEmpresa", "Empresas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, mostrarNotas = true, estiloBotonVerNotas = false });
        }
        
        
        
        public int ultimoIdEmpresa(List<Empresa> lista)
        {
            int ultimoId = 0;
            foreach (Empresa e in lista)
            {
                ultimoId = e.IdEmpresa;
            }
            return ultimoId;
        }


        public void CrearCarpetasEmpresa(int idEmpresaNueva)
        {
            string id = idEmpresaNueva.ToString();
            string ruta = Path.Combine(_env.WebRootPath, "Empresas", id);

            if (!Directory.Exists(ruta))
            {
                // Creamos la carpeta de la empresa
                Directory.CreateDirectory(ruta);

                // Elementos a crear
                string[] elementos = { "Conjuntos", "Costos", "Facturas", "Notas de Credito", "Ordenes de Compra", "Pedidos", "Piezas", "Presupuestos", "Remitos", "Utilajes" };

                foreach (string e in elementos)
                {
                    string carpeta = id + " - " + e;
                    string carpetaArchivos = id + " - Archivos " + e;

                    string carpetaElemento = Path.Combine(ruta, carpeta);
                    string rutaArchivos = Path.Combine(carpetaElemento, carpetaArchivos);

                    // Creamos las carpetas base
                    Directory.CreateDirectory(rutaArchivos);

                    string nombreArchivoJson = $"{id} - {e}.json";
                    string rutaCompletaJson = Path.Combine(carpetaElemento, nombreArchivoJson);

                    // Creamos el archivo json
                    System.IO.File.WriteAllText(rutaCompletaJson, "[]");
                }
            }
        }


        public IActionResult EliminarCarpetaEmpresa(int idEmpresaEliminar)
        {
            string ruta = Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresaEliminar;

            if (Directory.Exists(ruta))
            {
                Directory.Delete(ruta, true);
                return Content($"Carpeta de empresa eliminada: {ruta}");
            }
            else
            {
                return Content($"Carpeta inexistente: {ruta}");
            }
        }
        public IActionResult Empresa_Eliminar(int idEmpresa, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.EliminarEmpresa(idEmpresa);
                EliminarCarpetaEmpresa(idEmpresa);
                TempData["Mensaje"] = "Empresa Borrada";
                return RedirectToAction("IndexEmpresas");
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerEmpresa", new { idEmpresa = idEmpresa });
            }
        }

    }
}






