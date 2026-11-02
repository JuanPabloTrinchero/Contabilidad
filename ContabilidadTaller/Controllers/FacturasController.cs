using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContabilidadTaller.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ServiciosJson _serJson;
        public FacturasController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexFacturas(int idEmpresa, string nombreEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);

            //Filtro por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa).Where(f => f.Fecha == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<Factura> facturas = _serJson.ObtenerFacturasJson(idEmpresa);
                List<Factura> filtradas = new List<Factura>();

                foreach (Factura f in facturas)
                {
                    if (f.Fecha >= desde && f.Fecha <= hasta)
                    {
                        filtradas.Add(f);
                    }
                }
                ViewBag.Facturas = filtradas;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa).OrderBy(f => f.Fecha).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa).OrderByDescending(f => f.Fecha).ToList();
                }
            }

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);
            ViewBag.NotasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa);

            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);
            ViewBag.RelacionesODCsFacturas = _serJson.ObtenerRelacionesODCFactura(idEmpresa);

            return View();
        }
        public IActionResult IndexNuevaFactura(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.IdFactura = _serJson.PrimerIdFacturaFaltante(idEmpresa); // AQUI SE GENERA EL ID

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);

            ViewBag.RelacionesOCF = _serJson.ObtenerRelacionesODCFactura(idEmpresa);

            return View();
        }
        public IActionResult IndexVerFactura(int idEmpresa, string nombreEmpresa, int idFactura)
        {
            Factura f = _serJson.ElegirFactura(idEmpresa, idFactura);
            ViewBag.Factura = f;

            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.NotaDeCreditoDeFactura = _serJson.NotaDeCreditoDeFactura(idEmpresa, idFactura);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);
            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);
            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.SePuedeReactivar = _serJson.SePuedeReactivarFactura(idEmpresa, idFactura);
            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarFactura(idEmpresa, f.IdFactura);

            switch (f.Contiene)
            {
                case Contiene.Pedidos:
                    ViewBag.PedidosEnFactura = _serJson.PedidosEnFactura(idEmpresa, f.IdFactura);
                    break;
                case Contiene.Presupuestos:
                    ViewBag.PresupuestosEnFactura = _serJson.PresupuestosEnFactura(idEmpresa, f.IdFactura);
                    break;
                case Contiene.OrdenesDeCompra:
                    ViewBag.OrdenesDeCompraEnFactura = _serJson.OrdenesDeCompraEnFactura(idEmpresa, f.IdFactura);
;                    break;
            }
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);

            ViewBag.RelacionesOCF = _serJson.ObtenerRelacionesODCFactura(idEmpresa);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaFactura_MontoManual(int idEmpresa, string nombreEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, double montoManual, IFormFile archivo, Contiene contiene, string? descripcion)
        {
            _serJson.GuardarFactura(idEmpresa, idFactura, fecha, puesto, numero, montoManual, contiene, null, null, null, descripcion);
            if (archivo != null && archivo.Length != 0)
            {
                await GuardarArchivoEnFacturaNueva(idEmpresa, archivo, idFactura);
            }
            return RedirectToAction("IndexFacturas", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        public async Task GuardarArchivoEnFacturaNueva(int idEmpresa, IFormFile archivo, int idFactura)
        {
            string carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Facturas");
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
            _serJson.AgregarArchivoAFactura(idEmpresa, idFactura, rutaParaJson);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaFactura_ConPedidos(int idEmpresa, string nombreEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, IFormFile archivo, Contiene contiene, List<int> idsPedidos)
        {
            _serJson.GuardarFactura(idEmpresa, idFactura, fecha, puesto, numero, null, contiene, idsPedidos, null, null, null);
            if (archivo != null && archivo.Length != 0)
            {
                await GuardarArchivoEnFacturaNueva(idEmpresa, archivo, idFactura);
            }
            return RedirectToAction("IndexFacturas", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaFactura_ConPresupuestos(int idEmpresa, string nombreEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, double montoManual, IFormFile archivo, Contiene contiene, List<int> idsPresupuestos)
        {
            _serJson.GuardarFactura(idEmpresa, idFactura, fecha, puesto, numero, null, contiene, null, idsPresupuestos, null, null);

            if (archivo != null && archivo.Length != 0)
            {
                await GuardarArchivoEnFacturaNueva(idEmpresa, archivo, idFactura);
            }
            return RedirectToAction("IndexFacturas", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaFactura_ConOrdenesDeCompra(int idEmpresa, string nombreEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, IFormFile archivo, Contiene contiene, List<OCRecepciones>? OCsRecepciones)
        {
            _serJson.GuardarFactura(idEmpresa, idFactura, fecha, puesto, numero, null, contiene, null, null, OCsRecepciones, null);

            /*
            if (_serJson.ComprobarSiTodasOCsIngresadasSePuedenGuardar(idEmpresa, OCsRecepciones) == true)
            {
                _serJson.GuardarFactura_2(idEmpresa, idFactura, fecha, puesto, numero, null, contiene, null, null, OCsRecepciones, null);

                TempData["MensajeExito"] = "Factura guardada correctamente.";
            }
            else
            {
                TempData["MensajeError"] = "No se guardo la factura, las recepciones ingresadas con sus montos no cumplen los requisitos";
            }
            */

            if (archivo != null && archivo.Length != 0)
            {
                await GuardarArchivoEnFacturaNueva(idEmpresa, archivo, idFactura);
            }
            return RedirectToAction("IndexFacturas", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        public IActionResult Factura_ModificarFecha(int idEmpresa, string nombreEmpresa, int idFactura, DateOnly fechaNueva)
        {
            _serJson.ModificarFechaFactura(idEmpresa, idFactura, fechaNueva);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_ModificarCodigo(int idEmpresa, string nombreEmpresa, int idFactura, string puesto, string numero)
        {
            bool resultado = _serJson.ModificarCodigoFactura(idEmpresa, idFactura, puesto, numero);

            if (resultado == true)
            {
                TempData["MensajeExito"] = "El código de la factura se modificó correctamente.";
            }
            else
            {
                TempData["MensajeError"] = "Codigo ya existente.";
            }
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_FacturaPagada(int idEmpresa, string nombreEmpresa, int idFactura)
        {
            _serJson.FacturaPagada(idEmpresa, idFactura);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_FacturaImpaga(int idEmpresa, string nombreEmpresa, int idFactura)
        {
            _serJson.FacturaCambiarAImpaga(idEmpresa, idFactura);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_AnularFactura(int idEmpresa, string nombreEmpresa, int idFactura)
        {
            _serJson.AnularFactura(idEmpresa, idFactura);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_ReactivarFactura(int idEmpresa, string nombreEmpresa, int idFactura)
        {
            _serJson.ReactivarFactura(idEmpresa, idFactura);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_AgregarMasPedidos(int idEmpresa, string nombreEmpresa, int idFactura, List<int> items)
        {
            _serJson.AsignarPedidosAFactura(idEmpresa, idFactura, items);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_QuitarPedido(int idEmpresa, string nombreEmpresa, int idFactura, int idPedido)
        {
            _serJson.QuitarPedidoDeFactura(idEmpresa, idFactura, idPedido);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_AgregarMasPresupuestos(int idEmpresa, string nombreEmpresa, int idFactura, List<int> items)
        {
            _serJson.AsignarPresupuestosAFactura(idEmpresa, idFactura, items);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        public IActionResult Factura_QuitarPresupuesto(int idEmpresa, string nombreEmpresa, int idFactura, int idPresupuesto)
        {
            _serJson.QuitarPresupuestoDeFactura(idEmpresa, idFactura, idPresupuesto);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_QuitarOC(int idEmpresa, string nombreEmpresa, int idFactura, int idOCQuitar)
        {
            _serJson.QuitarOCDeFactura(idEmpresa, idFactura, idOCQuitar);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_AgregarMasOcsSinRecepciopnes(int idEmpresa, string nombreEmpresa, int idFactura, List<int> items)
        {
            if (items != null && items.Any())
            {
                foreach (int i in items)
                {
                    _serJson.NuevaRelacionOCDF(idEmpresa, idFactura, i);
                    _serJson.CambiarEstadoOCsinRecepciones(idEmpresa, i, EstadoOC.Facturada);
                }
            }
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_AgregarMasOcsConRecepciones(int idEmpresa, string nombreEmpresa, int idFactura, List<OCRecepciones> OCsRecepciones)
        {
            /*
            List<OCRecepciones> lista = new List<OCRecepciones>()
            {
                new OCRecepciones()
                {
                    idOC = 0,
                    Recepciones = new List<Recepcion>()
                    {
                        new Recepcion()
                        {
                            IdRecepcion = 0,
                            Monto = 200
                        },
                        new Recepcion()
                        {
                            IdRecepcion = 1,
                            Monto = 700
                        }
                    }
                }
            };
            */

            foreach (OCRecepciones ocr in OCsRecepciones)
            {
                int recepcionesActivasEnOC = _serJson.CantidadRecepcionesActivasEnOC(idEmpresa, ocr.idOC);
                int cantAgregadas = 0;
                if (ocr.Recepciones != null && ocr.Recepciones.Any())
                {
                    cantAgregadas = ocr.Recepciones.Count();
                }
                
                double? montoTotalOC = _serJson.MontoOrdenDeCompra(idEmpresa, ocr.idOC);
                double? montoYaFacturado = _serJson.MontoFacturadoDeOCConRecepciones(idEmpresa, ocr.idOC);
                double? montoRestante = (montoTotalOC - montoYaFacturado);

                double? montoAuxiliar = 0;

                if (ocr.Recepciones != null && ocr.Recepciones.Any())
                {
                    foreach (Recepcion r in ocr.Recepciones)
                    {
                        montoAuxiliar += r.Monto;
                    }
                }

                if (recepcionesActivasEnOC > cantAgregadas)
                {
                    // 4 > 3,2,1 == SI
                    if (montoAuxiliar < montoRestante)
                    {
                        _serJson.NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);

                        if (ocr.Recepciones != null && ocr.Recepciones.Any())
                        {
                            foreach (Recepcion r in ocr.Recepciones)
                            {
                                _serJson.AsignarRecepcionAFactura(idEmpresa, r.IdRecepcion, idFactura, r.Monto);
                            }
                        }      
                    }
                }
                else if(recepcionesActivasEnOC == cantAgregadas)
                {
                    // 4 > 4 == NO
                    if (montoAuxiliar == montoRestante)
                    {
                        _serJson.NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);

                        if (ocr.Recepciones != null && ocr.Recepciones.Any())
                        {
                            foreach (Recepcion r in ocr.Recepciones)
                            {
                                _serJson.AsignarRecepcionAFactura(idEmpresa, r.IdRecepcion, idFactura, r.Monto);
                            }
                        }

                        _serJson.CambiarEstadoOCConRecepciones(idEmpresa, ocr.idOC);
                    }
                }
            }
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }

        [HttpPost]
        public IActionResult Factura_QuitarRecepcionDeFactura(int idEmpresa, string nombreEmpresa, int idFactura, int idRecepcion, int idOC)
        {
            //int recepsEnOC = _serJson.RecepcionesEnOC(idEmpresa, idOC).Count();
            int recepsDeOC_enFactura = 0;

            List<Recepcion> recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);
            foreach(Recepcion r in recepciones)
            {
                if (r.IdOdCFK == idOC && r.IdFacturaFK == idFactura)
                {
                    recepsDeOC_enFactura++;
                }
            }

            if ((recepsDeOC_enFactura - 1) == 0)
            {
                _serJson.QuitarRecepcionDeFactura(idEmpresa, idRecepcion);
                _serJson.QuitarOCDeFactura(idEmpresa, idFactura, idOC);
            }
            else
            {
                _serJson.QuitarRecepcionDeFactura(idEmpresa, idRecepcion);
            }

            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_ModificarDescripcion(int idEmpresa, string nombreEmpresa, int idFactura, string? nuevaDescripcion)
        {
            _serJson.ModificarDescripcionFactura(idEmpresa, idFactura, nuevaDescripcion);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_ModificarMonto(int idEmpresa, string nombreEmpresa, int idFactura, double? nuevoMonto)
        {
            _serJson.ModificarMontoFactura(idEmpresa, idFactura, nuevoMonto);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_AgregarNota(int idEmpresa, string nombreEmpresa, int idFactura, string nuevaNota)
        {
            _serJson.AgregarNotaFactura(idEmpresa, idFactura, nuevaNota);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_ModificarNota(int idEmpresa, string nombreEmpresa, int idFactura, string nuevaAModificar, string notaModificada)
        {
            _serJson.ModificarNotaFactura(idEmpresa, idFactura, nuevaAModificar, notaModificada);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpPost]
        public IActionResult Factura_BorrarNota(int idEmpresa, string nombreEmpresa, int idFactura, string notaBorrar)
        {
            _serJson.BorrarNotaFactura(idEmpresa, idFactura, notaBorrar);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }



        [HttpPost]
        public async Task<IActionResult> Factura_AgregarArchivo(int idEmpresa, string nombreEmpresa, int idFactura, List<IFormFile> archivos)
        {
            await _serJson.GuardarArchivos(idEmpresa, "Factura", idFactura, archivos);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }
        [HttpGet]
        public IActionResult Factura_AbrirArchivoEnUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Factura_BorrarArchivo(int idEmpresa, string nombreEmpresa, int idFactura, string archivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Factura", idFactura, archivo);
            return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFactura });
        }


        [HttpPost]
        public IActionResult Factura_BorrarFactura(int idEmpresa, string nombreEmpresa, int idFacturaBorrar, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarFactura(idEmpresa, idFacturaBorrar);
                TempData["Mensaje"] = "Factura Borrada";
                return RedirectToAction("IndexFacturas", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerFactura", "Facturas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idFactura = idFacturaBorrar });
            }    
        }

        
    }
}
