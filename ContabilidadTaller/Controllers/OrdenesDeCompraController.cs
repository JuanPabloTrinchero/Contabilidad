using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContabilidadTaller.Controllers
{
    public class OrdenesDeCompraController : Controller
    {
        private readonly ServiciosJson _serJson;
        public OrdenesDeCompraController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexOrdenesDeCompra(int idEmpresa, string nombreEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.OCs = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);

            //Filtro por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.OCs = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa).Where(odc => odc.Fecha == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<OrdenDeCompra> odcs = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);
                List<OrdenDeCompra> filtrados = new List<OrdenDeCompra>();

                foreach (OrdenDeCompra odc in odcs)
                {
                    if (odc.Fecha >= desde && odc.Fecha <= hasta)
                    {
                        filtrados.Add(odc);
                    }
                }
                ViewBag.OCs = filtrados;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.OCs = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa).OrderBy(odc => odc.Fecha).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.OCs = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa).OrderByDescending(odc => odc.Fecha).ToList();
                }
            }

            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);

            return View();
        }
        public IActionResult IndexNuevaOrdenDeCompra(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.PresupuestosSinAsignar = _serJson.PresupuestosSinAsignar(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.IdOC = _serJson.SiguienteIdOC(idEmpresa);
            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);

            return View();
        }
        public IActionResult IndexVerOrdenDeCompra(int idEmpresa, string nombreEmpresa, int idOrdenDeCompra)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.OrdenDeCompra = _serJson.ElegirOrdenDeCompra(idEmpresa, idOrdenDeCompra);
            ViewBag.PresupuestosDeOC = _serJson.PresupuestosDeOC(idEmpresa, idOrdenDeCompra);
            ViewBag.PresupuestosSinAsignar = _serJson.PresupuestosSinAsignar(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.RelacionesOCF = _serJson.ObtenerRelacionesODCFactura(idEmpresa);
            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarOC(idEmpresa, idOrdenDeCompra);
            ViewBag.SePuedeReactivar = _serJson.SePuedeReactivar(idEmpresa, idOrdenDeCompra);

            ViewBag.Socios = _serJson.ObtenerSociosJson();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarOC(int idEmpresa, string nombreEmpresa, string codigo, DateOnly fecha, IFormFile? archivo, int idOC, int[]? items, string[]? recepciones, List<string> notas)
        {

            int id;
            if (recepciones != null && recepciones.Any())
            {
                id = _serJson.GuardarNuevaOC(idEmpresa, codigo, fecha, Relacion.ConRecepcion, notas);
            }
            else
            {
                id = _serJson.GuardarNuevaOC(idEmpresa, codigo, fecha, Relacion.SinRecepcion, notas);
            }

            if (items != null && items.Any())
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    _serJson.AsignarPresupuestoAOC(idEmpresa, idOC, items[i]);
                }
            }

            if (recepciones != null && recepciones.Any())
            {
                for (int i = 0; i < recepciones.Count(); i++)
                {
                    _serJson.GuardarNuevaRecepcion(idEmpresa, recepciones[i], id);
                }
            }
            if (archivo != null && archivo.Length > 0)
            {
                await SubirArchivoOC(archivo, idEmpresa, idOC, nombreEmpresa);
            }
            return RedirectToAction("IndexOrdenesDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        public async Task<IActionResult> SubirArchivoOCExistente(int idEmpresa, string nombreEmpresa, int idOC, IFormFile archivo)
        {
            if (archivo != null && archivo.Length > 0)
            {
                await SubirArchivoOC(archivo, idEmpresa, idOC, nombreEmpresa);
            }
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOC });
        }

        [HttpPost]
        public IActionResult AgregarNuevaRecepcion(int idEmpresa, string nombreEmpresa, int idOc, string numeroRecepcion)
        {
            _serJson.GuardarNuevaRecepcion(idEmpresa, numeroRecepcion, idOc);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult EliminarRecepcion(int idEmpresa, string nombreEmpresa, int idOc, int idRecepcionBorrar)
        {
            _serJson.BorrarRecepcion(idEmpresa, idRecepcionBorrar);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult CambiarEstadoRecepcion(int idEmpresa, string nombreEmpresa, int idOc, int idRecepcion, EstadoRecepcion nuevoEstado)
        {
            _serJson.CambiarEstadoRecepcion(idEmpresa, idRecepcion, nuevoEstado);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_CambiarEstado(int idEmpresa, string nombreEmpresa, int idOc, EstadoOC estadoNuevo)
        {
            _serJson.CambiarEstadoOCsinRecepciones(idEmpresa, idOc, estadoNuevo);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_ModificarCodigo(int idEmpresa, string nombreEmpresa, int idOc, string nuevoCodigo)
        {
            _serJson.ModificarCodigoOC(idEmpresa, idOc, nuevoCodigo);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_ModificarFecha(int idEmpresa, string nombreEmpresa, int idOc, DateOnly nuevaFecha)
        {
            _serJson.ModificarFechaOC(idEmpresa, idOc, nuevaFecha);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_AgregarNota(int idEmpresa, string nombreEmpresa, int idOc, string nuevaNota)
        {
            _serJson.AgregarNotaOC(idEmpresa, idOc, nuevaNota);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_ModificarNota(int idEmpresa, string nombreEmpresa, int idOc, string notaModificar, string notaModificada)
        {
            _serJson.ModificarNotaOC(idEmpresa, idOc, notaModificar, notaModificada);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_BorrarNota(int idEmpresa, string nombreEmpresa, int idOc, string notaBorrar)
        {
            _serJson.BorrarNotaOC(idEmpresa, idOc, notaBorrar);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_AgregarPresupuestos(int idEmpresa, string nombreEmpresa, int idOc, List<int>? items)
        {
            if (items != null && items.Any())
            {
                foreach (int i in items)
                {
                    _serJson.AsignarPresupuestoAOC(idEmpresa, idOc, i);
                }
            }  
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }
        [HttpPost]
        public IActionResult OC_QuitarPresupuesto(int idEmpresa, string nombreEmpresa, int idOc, int idPresupuestoQuitar)
        {
            _serJson.QuitarPresupuestoDeOC(idEmpresa, idPresupuestoQuitar);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }

        [HttpPost]
        public IActionResult OC_Borrar(int idEmpresa, string nombreEmpresa, int idOcBorrar, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarOC(idEmpresa, idOcBorrar);
                TempData["Mensaje"] = "Orden de Compra Borrada";
                return RedirectToAction("IndexOrdenesDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOcBorrar });
            } 
        }
        [HttpPost]
        public async Task SubirArchivoOC(IFormFile archivo, int idEmpresa, int idOC, string nombreEmpresa)
        {
            string carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Ordenes de Compra");
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
            _serJson.AgregarArchivoOC(idEmpresa, idOC, rutaParaJson);
        }

        [HttpGet]
        public IActionResult OC_AbrirArchivoEnUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }

        [HttpPost]
        public IActionResult OC_BorrarArchivo(int idEmpresa, string nombreEmpresa, int idOc, string ruta)
        {
            _serJson.BorrarArchivo(idEmpresa, "OrdenDeCompra", idOc, ruta);
            return RedirectToAction("IndexVerOrdenDeCompra", "OrdenesDeCompra", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idOrdenDeCompra = idOc });
        }

    }
}
