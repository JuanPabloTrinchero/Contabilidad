using ContabilidadTaller.Interfaces;
using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContabilidadTaller.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ServiciosJson _serJson;
        public EmpresasController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexEmpresas(string texto)
        {

            ViewBag.EmpresasGuardadas = _serJson.ObtenerEmpresasJson().Count();
            if (string.IsNullOrEmpty(texto))
            {
                ViewBag.Empresas = _serJson.EmpresasOrdenadasPorId();
            }
            else
            {
                ViewBag._serEmpresas = FiltradasEmpresas(texto);
            }
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
        public List<Empresa> FiltradasEmpresas(string texto)
        {
            return _serJson.ObtenerEmpresasJson().Where(e => e.NombreEmpresa.Contains(texto, StringComparison.OrdinalIgnoreCase)).ToList();
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
        public IActionResult CrearCarpetasEmpresa(int idEmpresaNueva)
        {
            string ruta = Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresaNueva;

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);

                // Creamos las demas carperas internas

                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Costos");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Facturas");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Notas de Credito");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Ordenes de Compra");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Pedidos");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Piezas");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Presupuestos");
                Directory.CreateDirectory(ruta + @"\" + idEmpresaNueva.ToString() + @" - Remitos");

                // Creamos los archivos Json

                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Costos\" + idEmpresaNueva.ToString() + @" - Costos.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Facturas\" + idEmpresaNueva.ToString() + @" - Facturas.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Notas de Credito\" + idEmpresaNueva.ToString() + @" - Notas de Credito.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Ordenes de Compra\" + idEmpresaNueva.ToString() + @" - Ordenes de Compra.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Pedidos\" + idEmpresaNueva.ToString() + @" - Pedidos.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Piezas\" + idEmpresaNueva.ToString() + @" - Piezas.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Piezas\" + idEmpresaNueva.ToString() + @" - Conjuntos.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Presupuestos\" + idEmpresaNueva.ToString() + @" - Presupuestos.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Remitos\" + idEmpresaNueva.ToString() + @" - Remitos.json", "[]");

                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Empleados.json", "[]");
                System.IO.File.WriteAllText(ruta + @"\" + idEmpresaNueva.ToString() + @" - Ordenes de Compra\RelacionODCFactura.json", "[]");
            }
            else
            {
                return Content($"ALERTA: Carpeta ubicada en {ruta} ya existente");
            }
            return Content($"Carpeta creada en: {ruta}");
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






