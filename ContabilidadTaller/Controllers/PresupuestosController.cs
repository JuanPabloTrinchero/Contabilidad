using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Publicar vista como .PDF
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace ContabilidadTaller.Controllers
{
    public class PresupuestosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public PresupuestosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexPresupuestos(int idEmpresa, string nombreEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/)
        {
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.IdEmpresa = idEmpresa;

            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);

            //Filtro por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa).Where(p => p.Fecha == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<Presupuesto> presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
                List<Presupuesto> filtrados = new List<Presupuesto>();

                foreach (Presupuesto p in presupuestos)
                {
                    if (p.Fecha >= desde && p.Fecha <= hasta)
                    {
                        filtrados.Add(p);
                    }
                }
                ViewBag.Presupuestos = filtrados;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa).OrderBy(p => p.Fecha).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa).OrderByDescending(p => p.Fecha).ToList();
                }
            }

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);

            return View();
        }
        public IActionResult IndexNuevoPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuestoAReamplazar)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);

            ViewBag.SiguienteIdPresupuesto = _serJson.SiguienteIdPresupuesto(_serJson.ObtenerPresupuestosJson(idEmpresa));

            ViewBag.SiguienteNumeroPresupuesto = _serJson.SiguienteNumeroPresupuesto(idEmpresa);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            return View();
        }
        public IActionResult IndexVerPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            
            Presupuesto p = _serJson.ElegirPresupuesto(idEmpresa, idPresupuesto);
            ViewBag.Presupuesto = p;

            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);

            ViewBag.PedidosSinPresupuesto = _serJson.FiltarPedidosSinPresupuesto(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);

            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);
            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);

            ViewBag.PrecioTotal = _serJson.PrecioTotalPresupuesto(idEmpresa, idPresupuesto);

            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);

            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);

            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);

            ViewBag.SePuedeConfirmar = _serJson.SePuedeConfirmarPresupuesto(idEmpresa, idPresupuesto);
            ViewBag.SePuedeCancelar = _serJson.SePuedeCancelarPresupuesto(idEmpresa, idPresupuesto);
            ViewBag.SePuedeReacivar = _serJson.SePuedeReactivarPresupuesto(idEmpresa, idPresupuesto);

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarPresupuesto(idEmpresa, idPresupuesto);

            return View();
        }

        public IActionResult IndexPresupuestoGenerado(int idEmpresa, int idPresupuesto)
        {
            ViewBag.Empresa = _serJson.ElegirEmpresa(idEmpresa);

            Presupuesto p = _serJson.ElegirPresupuesto(idEmpresa, idPresupuesto);
            ViewBag.Presupuesto = p;

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);

            ViewBag.Socio = _serJson.ElegirSocio(p.SocioFK);


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPresupuesto(int idEmpresa, DateOnly fecha, List<int> idItems, int idPresupuesto, string nombreEmpresa, int numeroPresupuesto, List<string>? notas, Socio socio, List<IFormFile> archivos)
        {
            _serJson.GuardarNuevoPresupuesto(idEmpresa, fecha, idItems, idPresupuesto, numeroPresupuesto, notas, socio);
            await _serJson.GuardarArchivos(idEmpresa, "Presupuesto", idPresupuesto, archivos);
            return RedirectToAction("IndexPresupuestos", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public IActionResult AgregarItemsAPresupuesto(int idEmpresa, List<int> idItems, int idPresupuesto, string nombreEmpresa)
        {
            _serJson.GuardarItemsEnPresupuesto(idEmpresa, idItems, idPresupuesto);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto});
        }
        [HttpPost]
        public IActionResult QuitarItemDePresupuesto(int idEmpresa, int idPedido, int idPresupuesto, string nombreEmpresa)
        {
            _serJson.EliminarItemDePresupuesto(idEmpresa, idPedido, idPresupuesto);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult ConfirmarOEditar(int idEmpresa, string nombreEmpresa, int idPresupuesto, EstadoPresupuesto estado)
        {
            _serJson.EditarOConfirmar(idEmpresa, idPresupuesto, estado);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult CancelarPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto)
        {
            _serJson.BajaPresupuesto(idEmpresa, idPresupuesto);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult RehabilitarPresupuesto(int idEmpresa, int idPresupuesto, string nombreEmpresa)
        {
            _serJson.AltaPresupuesto(idEmpresa, idPresupuesto);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Presupuesto_Emiminar(int idEmpresa, string nombreEmpresa, int idPresupuesto, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);
            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarPresupuesto(idEmpresa, idPresupuesto);
                TempData["Mensaje"] = "Presupuesto Eliminado";
                return RedirectToAction("IndexPresupuestos", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AgregarArchivoPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, List<IFormFile> archivo)
        {
            await _serJson.GuardarArchivos(idEmpresa, "Presupuesto", idPresupuesto, archivo);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        public IActionResult AbrirArchivoPresupuestoEnSuUbicacion(string rutaArchivo)
        {
            _serJson.AbrirArchivoEnSuUbicacion(rutaArchivo);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> BorrarArchivoDePresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, string rutaArchivoBorrar)
        {
            _serJson.BorrarArchivo(idEmpresa, "Presupuesto", idPresupuesto, rutaArchivoBorrar);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult GuardarEnvio(int idEmpresa, string nombreEmpresa, int idPresupuesto, DateOnly fechaEnvio, List<int> empleados)
        {
            _serJson.PresupuestoEnviadoA(idEmpresa, idPresupuesto, fechaEnvio, empleados);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }

        [HttpPost]
        public IActionResult BorrarEnvio(int idEmpresa, string nombreEmpresa, int idPresupuesto, int idEnvioBorrar)
        {
            _serJson.BorrarEnvioPresupuesto(idEmpresa, idPresupuesto, idEnvioBorrar);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }


        [HttpPost]
        public IActionResult GuardarNuevaNotaPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, string notaNueva)
        {
            _serJson.AgregarNotaPresupuesto(idEmpresa, idPresupuesto, notaNueva);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult ModificarNotaPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, string notaAModificar, string notaModificada)
        {
            _serJson.ModificarNotaPresupuesto(idEmpresa, idPresupuesto, notaAModificar, notaModificada);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult BorrarNotaPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, string notaABorrar)
        {
            _serJson.BorrarNotaPresupuesto(idEmpresa, idPresupuesto, notaABorrar);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult ModificarNumeroPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, int nuevoNumero)
        {
            bool numeroExiste = _serJson.NumeroPresupuestoExiste(idEmpresa, nuevoNumero);
            if (numeroExiste == false)
            {
                _serJson.ModificarNumeroPresupuesto(idEmpresa, idPresupuesto, nuevoNumero);
            }
            else
            {
                TempData["Mensaje"] = $"El Numero ingresado [{nuevoNumero}] ya existe";
            }
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }

        [HttpPost]
        public IActionResult ModificarFechaPresupuesto(int idEmpresa, string nombreEmpresa, int idPresupuesto, DateOnly nuevaFecha)
        {
            _serJson.ModificarFechaPresupuesto(idEmpresa, idPresupuesto, nuevaFecha);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }

        [HttpPost]
        public IActionResult Presupuesto_AsignarSocio(int idEmpresa, string nombreEmpresa, int idPresupuesto, int idSocio)
        {
            _serJson.AsignarSocioAPresupuesto(idEmpresa, idPresupuesto, idSocio);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }
        [HttpPost]
        public IActionResult Presupuesto_QuitarSocio(int idEmpresa, string nombreEmpresa, int idPresupuesto)
        {
            _serJson.QuitarSocioAPresupuesto(idEmpresa, idPresupuesto);
            return RedirectToAction("IndexVerPresupuesto", "Presupuestos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPresupuesto = idPresupuesto });
        }




      

    }
}
