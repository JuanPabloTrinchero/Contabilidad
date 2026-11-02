using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContabilidadTaller.Controllers
{
    public class NotasDeCreditoController : Controller
    {
        private readonly ServiciosJson _serJson;
        public NotasDeCreditoController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexNotasDeCredito(int idEmpresa, string nombreEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.NotasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa);

            //Filtro por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.NotasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa).Where(ndc => ndc.Fecha == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<NotaDeCredito> notasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa);
                List<NotaDeCredito> filtradas = new List<NotaDeCredito>();

                foreach (NotaDeCredito ndc in notasDeCredito)
                {
                    if (ndc.Fecha >= desde && ndc.Fecha <= hasta)
                    {
                        filtradas.Add(ndc);
                    }
                }
                ViewBag.NotasDeCredito = filtradas;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.NotasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa).OrderBy(ndc => ndc.Fecha).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.NotasDeCredito = _serJson.ObtenerNotasDeCreditoJson(idEmpresa).OrderByDescending(ndc => ndc.Fecha).ToList();
                }
            }

            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);
            
            return View();
        }
        public IActionResult IndexNuevaNotaDeCredito(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Id = _serJson.PrimerIdFaltanteNotaDeCredito(idEmpresa);
            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);

            return View();
        }

        public IActionResult IndexVerNotaDeCredito(int idEmpresa, string nombreEmpresa, int idNotaDeCredito)
        {
            ViewBag.IdNotaDeCredito = idNotaDeCredito;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            ViewBag.NotaDeCredito = _serJson.SeleccionarNotaDeCredito(idEmpresa, idNotaDeCredito);

            ViewBag.FacturasEnNDC = _serJson.FacturasEnNotaDeCredito(idEmpresa, idNotaDeCredito);
            ViewBag.Facturas = _serJson.ObtenerFacturasJson(idEmpresa);

            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.SePuedeReactivar = _serJson.SePuedeReactivarNotaDeCredito(idEmpresa, idNotaDeCredito);
            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarNotaDeCredito(idEmpresa, idNotaDeCredito);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNotaDeCredito(int idEmpresa, string nombreEmpresa, DateOnly fecha, string puesto, string numero, List<string>? notas, IFormFile archivo, List<int>? idsFacturasAsociadas)
        {
            int idNuevaNDC = await _serJson.NuevaNotaDeCredito(idEmpresa, fecha, puesto, numero, notas, archivo, idsFacturasAsociadas);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNuevaNDC });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_ModificarCodigo(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, string nuevoPuesto, string nuevoNumero)
        {
            _serJson.ModificarCodigoNotaDeCredito(idEmpresa, idNotaDeCredito, nuevoPuesto, nuevoNumero);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_ModificarFecha(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, DateOnly nuevaFecha)
        {
            _serJson.ModificarFechaNotaDeCredito(idEmpresa, idNotaDeCredito, nuevaFecha);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }

        [HttpPost]
        public IActionResult NotaDeCredito_ModificarEstado(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, EstadoNotaDeCredito nuevoEstado)
        {
            _serJson.ModificarEstadoNotaDeCredito(idEmpresa, idNotaDeCredito, nuevoEstado);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito});
        }
        [HttpPost]
        public IActionResult NotaDeCredito_QuitarFacturaDeNotaDeCredito(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, int idFactura)
        {
            _serJson.QuitarFacturaDeNotaDeCredito(idEmpresa, idNotaDeCredito, idFactura);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_AgregarFacturas(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, List<int>? idsFacturas)
        {
            _serJson.AgregarFacturasANotaDeCredito(idEmpresa, idNotaDeCredito, idsFacturas);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_AgregarNota(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, string nuevaNota)
        {
            _serJson.AgregarNotaNotaDeCredito(idEmpresa, idNotaDeCredito, nuevaNota);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_ModificarNota(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, string notaAModificar, string notaModificada)
        {
            _serJson.ModificarNotaNotaDeCredito(idEmpresa, idNotaDeCredito, notaAModificar, notaModificada);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }
        [HttpPost]
        public IActionResult NotaDeCredito_BorrarNota(int idEmpresa, string nombreEmpresa, int idNotaDeCredito, string notaBorrar)
        {
            _serJson.BorrarNotaNotaDeCredito(idEmpresa, idNotaDeCredito, notaBorrar);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCredito });
        }

        [HttpPost]
        public IActionResult NotaDeCredito_Borrar(int idEmpresa, string nombreEmpresa, int idNotaDeCreditoBorrar, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarNotaDeCredito(idEmpresa, idNotaDeCreditoBorrar);
                TempData["Mensaje"] = "Nota de Credito Borrada";
                return RedirectToAction("IndexNotasDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idNotaDeCredito = idNotaDeCreditoBorrar });
            }  
        }


        public async Task<IActionResult> NotaDeCredito_AgregarArchivo(int idEmpresa, string nombreEmpresa, int idNDC, List<IFormFile> archivos)
        {
            await _serJson.GuardarArchivos(idEmpresa, "NotaDeCredito", idNDC, archivos);
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
        public async Task<IActionResult> NotaDeCredito_BorrarArchivo(int idEmpresa, string nombreEmpresa, int idNDC)
        {
            NotaDeCredito ndc = _serJson.ElegirNotaDeCredito(idEmpresa, idNDC);
            if (ndc.UbicacionArchivo != null)
            {
                _serJson.BorrarArchivo(idEmpresa, "NotaDeCredito", idNDC, ndc.UbicacionArchivo);
            }
            return RedirectToAction("IndexVerNotaDeCredito", "NotasDeCredito", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }

        public IActionResult NotaDeCretido_AbrirArchivoEnUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }


    }
}
