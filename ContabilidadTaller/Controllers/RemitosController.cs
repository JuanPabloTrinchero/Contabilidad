using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ContabilidadTaller.Controllers
{
    public class RemitosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public RemitosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexRemitos(int idEmpresa, string nombreEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/)
        {
            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);

            //Filtro por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa).Where(r => r.FechaEntrega == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<Remito> remitos = _serJson.ObtenerRemitosJson(idEmpresa);
                List<Remito> filtrados = new List<Remito>();

                foreach (Remito r in remitos)
                {
                    if (r.FechaEntrega >= desde && r.FechaEntrega <= hasta)
                    {
                        filtrados.Add(r);
                    }
                }
                ViewBag.Remitos = filtrados;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa).OrderBy(r => r.FechaEntrega).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa).OrderByDescending(r => r.FechaEntrega).ToList();
                }
            }

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            return View();
        }
        public IActionResult IndexNuevoRemito(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.NumeroRemito = _serJson.SiguienteNumeroRemito(_serJson.ObtenerRemitosJson(idEmpresa));
            ViewBag.PedidosConEntregasSinRemitir = _serJson.PedidosConEntregasPendientesDeRemitir(idEmpresa);
            ViewBag.IdRemito = _serJson.UltimoIdRemito(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
            
            return View();
        }

        public IActionResult IndexVerRemito(int idEmpresa, string nombreEmpresa, int idRemito)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Remito = _serJson.ElegirRemito(idEmpresa, idRemito);
            ViewBag.PedidosDeRemito = _serJson.PedidosDeRemito(idEmpresa, idRemito);
            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.PedidosConEntregasSinRemitir = _serJson.PedidosConEntregasPendientesDeRemitir(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
            ViewBag.Socios = _serJson.ObtenerSociosJson();
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarRemito(idEmpresa, idRemito);

            return View();
        }

        public IActionResult IndexRemitoGenerado(int idEmpresa, string nombreEmpresa, int idRemito)
        {
            ViewBag.Remito = _serJson.ElegirRemito(idEmpresa, idRemito);
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.PedidosDeRemito = _serJson.PedidosDeRemito(idEmpresa, idRemito);

            return View();
        }

        public async Task<IActionResult> GuardarRemito(int idEmpresa, int numeroRemito, DateOnly fecha, IFormFile archivo, List<string> notas, int? entregadoA, int idRemito, List<PedidoEntregas> items, string nombreEmpresa)
        {
            bool guardado = _serJson.GuardarNuevoRemito(idEmpresa, numeroRemito, fecha, notas, entregadoA);

            if (guardado == true)
            {
                _serJson.AgregarItemsARemito(idEmpresa, idRemito, items);

                if (archivo != null && archivo.Length > 0)
                {
                    await SubirArchivoRemito(archivo, idEmpresa, idRemito, nombreEmpresa); ;
                }
                //TempData["Mensaje"] = "Remito Guardado con Exito";

                return RedirectToAction("IndexRemitos", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = $"Remito no Guardado (El Numero de Remito: [{numeroRemito}] ya existe)";

                return RedirectToAction("IndexNuevoRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
        }
        [HttpPost]
        public IActionResult AgregarItemsARemito(int idEmpresa, string nombreEmpresa, int idRemito, List<PedidoEntregas> items)
        {
            _serJson.AgregarItemsARemito(idEmpresa, idRemito, items);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult QuitarPedidoYEntregasDeRemito(int idEmpresa, string nombreEmpresa, int idPedidoBorrar, int idRemito)
        {
            _serJson.QuitarPedidoDeRemito(idEmpresa, idPedidoBorrar, idRemito);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult QuitarEntregaDeRemito(int idEmpresa, string nombreEmpresa, int idPedidoBorrar, int idRemito, int idEntregaQuitar)
        {
            _serJson.QuitarEntregaDeRemito(idEmpresa, idPedidoBorrar, idEntregaQuitar);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public async Task SubirArchivoRemito(IFormFile archivo, int idEmpresa, int idRemito, string nombreEmpresa)
        {
            string carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Remitos");
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
            _serJson.AgregarArchivoRemito(idEmpresa, idRemito, rutaParaJson);
        }
        [HttpPost]
        public async Task<IActionResult> ActualizarArchivoRemito(int idEmpresa, string nombreEmpresa, IFormFile archivo, int idRemito)
        {
            if (archivo != null && archivo.Length > 0)
            {
                await SubirArchivoRemito(archivo, idEmpresa, idRemito, nombreEmpresa);
            }
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public async Task<IActionResult> BorrarArchivoRemito(int idEmpresa, string nombreEmpresa, int idRemito, string rutaArchivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Remito", idRemito, rutaArchivo);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        public IActionResult AbrirRemitoEnUbicacion(string ruta)
        {
            _serJson.AbrirArchivoEnSuUbicacion(ruta);
            return NoContent();
        }
        public IActionResult Remito_Borrar(int idEmpresa, string nombreEmpresa, int idRemito, int idSocio, string contraseñaSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);
            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarRemito(idEmpresa, idRemito);
                TempData["Mensaje"] = "Remito Eliminado";
                return RedirectToAction("IndexRemitos", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
            }
        }
        [HttpPost]
        public IActionResult Remito_AgregarNota(int idEmpresa, string nombreEmpresa, int idRemito, string nuevaNota)
        {
            _serJson.AgregarNotaRemito(idEmpresa, idRemito, nuevaNota);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_ModificarNota(int idEmpresa, string nombreEmpresa, int idRemito, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaRemito(idEmpresa, idRemito, notaVieja, notaModificada);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_BorrarNota(int idEmpresa, string nombreEmpresa, int idRemito, string notaBorrar)
        {
            _serJson.EliminarNotaRemito(idEmpresa, idRemito, notaBorrar);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_CambiarEstado(int idEmpresa, string nombreEmpresa, int idRemito)
        {
            _serJson.CambiarEstadoRemito(idEmpresa, idRemito);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_ModificarFecha(int idEmpresa, string nombreEmpresa, int idRemito, DateOnly nuevaFecha)
        {
            _serJson.ModificarFechaRemito(idEmpresa, idRemito, nuevaFecha);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_ModificarNumero(int idEmpresa, string nombreEmpresa, int idRemito, int nuevoNumero)
        {
            bool sePuede = _serJson.NumeroRemitoExiste(idEmpresa, nuevoNumero);
            if (sePuede == false)
            {
                _serJson.ModificarNumeroRemito(idEmpresa, idRemito, nuevoNumero);
            }
            else
            {
                TempData["Mensaje"] = $"El Numero {nuevoNumero} ya existe";
            }
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_EntregadoA(int idEmpresa, string nombreEmpresa, int idRemito, int? idEmpleado)
        {
            _serJson.RemitoEntregadoA(idEmpresa, idRemito, idEmpleado);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }
        [HttpPost]
        public IActionResult Remito_QuitarEntregado(int idEmpresa, string nombreEmpresa, int idRemito)
        {
            _serJson.RemitoQuitarEntregado(idEmpresa, idRemito);
            return RedirectToAction("IndexVerRemito", "Remitos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idRemito = idRemito });
        }




        //BorrarRemito



    }
}
