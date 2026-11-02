using Aspose.CAD;
using Aspose.CAD.ImageOptions;
using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ContabilidadTaller.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public PedidosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }

        public IActionResult IndexPedidos(string nombreEmpresa, int idEmpresa, DateOnly? fechaEspecifica, DateOnly? desde, DateOnly? hasta, string ordenar /* asc o des*/, EstadoPedido? estado)
        {
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Costos = _serJson.ObtenerCostosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.PedidosGuardados = _serJson.ObtenerPedidosJson(idEmpresa).Count();

            ViewBag.Pedidos = _serJson.PedidosOrdenadosPorId(idEmpresa);

            //Filtros por fecha especifica
            if (fechaEspecifica.HasValue)
            {
                ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa).Where(p => p.FechaEncargo == fechaEspecifica).ToList();
            }

            //Filtros por rango de fechas
            if (desde.HasValue && hasta.HasValue)
            {
                List<Pedido> pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
                List<Pedido> filtrados = new List<Pedido>();

                foreach (Pedido p in pedidos)
                {
                    if (p.FechaEncargo >= desde && p.FechaEncargo <= hasta)
                    {
                        filtrados.Add(p);
                    }
                }
                ViewBag.Pedidos = filtrados;
            }

            //Filtros por antiguedad
            if (!string.IsNullOrEmpty(ordenar))
            {
                if (ordenar == "asc")
                {
                    ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa).OrderBy(p => p.FechaEncargo).ToList();
                }
                else if (ordenar == "des")
                {
                    ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa).OrderByDescending(p => p.FechaEncargo).ToList();
                }
            }

            //Filtro por estado
            
            if (estado != null)
            {
                switch (estado)
                {
                    case EstadoPedido.Pendiente:
                        ViewBag.Pedidos = _serJson.PedidosPorEstado(_serJson.ObtenerPedidosJson(idEmpresa), EstadoPedido.Pendiente);
                        break;
                    case EstadoPedido.Entregado:
                        ViewBag.Pedidos = _serJson.PedidosPorEstado(_serJson.ObtenerPedidosJson(idEmpresa), EstadoPedido.Entregado);
                        break;
                    case EstadoPedido.Cancelado:
                        ViewBag.Pedidos = _serJson.PedidosPorEstado(_serJson.ObtenerPedidosJson(idEmpresa), EstadoPedido.Cancelado);
                        break;
                }
            }


            return View();
        }
        public IActionResult IndexNuevoPedido(string nombreEmpresa, int idEmpresa, int idEmpleado)
        {
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.Empleado = idEmpleado;

            return View();
        }
        public IActionResult IndexVerPedido(int idEmpresa, string nombreEmpresa, int idPedido)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;

            Pedido p = _serJson.BuscarPedido(idEmpresa, idPedido);
            ViewBag.Pedido = p;

            ViewBag.Descripcion = _serJson.DescripcionDePedido(idEmpresa, idPedido);

            ViewBag.PiezaDePedido = _serJson.PiezaDePedido(idEmpresa, p);
            ViewBag.ConjuntoDePedido = _serJson.ConjuntoDePedido(idEmpresa, p);
            ViewBag.PiezasDeConjunto = _serJson.PiezasDeConjunto(idEmpresa, _serJson.ConjuntoDePedido(idEmpresa, p).IdConjunto);

            ViewBag.CostosDePedido = _serJson.CostosDePedido(idEmpresa, idPedido);

            if (p.EncargadoPorFK != null)
            {
                ViewBag.Empleado = _serJson.ElegirEmpleado(idEmpresa, p.EncargadoPorFK);
            }
            else
            {
                ViewBag.Empleado = _serJson.SinEmpleado();
            }
            
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.PresupuestoDePedido = _serJson.PresupuestoDePedido(idEmpresa, p);

            ViewBag.FacturaDePedido = _serJson.FacturaDePedido(idEmpresa, p);

            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarPedido(idEmpresa, p.IdPedido);

            return View();
        }


        public List<Pedido> BuscarPedidosPorNombre(int idEmpresa, string texto)
        {
            return _serJson.ObtenerPedidosJson(idEmpresa).Where(p => p.Descripcion.ToLower().Contains(texto.ToLower(), StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Pedido> FiltrarPedidosPorEmpleado(int idEmpresa, int idEmpleado)
        {
            List<Pedido> todos = _serJson.ObtenerPedidosJson(idEmpresa);
            List<Pedido> filtradorPorEmpleado = new List<Pedido>();

            foreach (Pedido p in todos)
            {
                if (p.EncargadoPorFK == idEmpleado)
                {
                    filtradorPorEmpleado.Add(p);
                }
            }
            return filtradorPorEmpleado;
        }
        [HttpPost]
        public IActionResult NuevoPedidoPiezaNueva(string descripcion, int cantidad, double precioUnitario, DateOnly fechaEncargo, EstadoPedido estado, List<string> notas, int idEmpresa, string nombreEmpresa, int? encargadoPor, int? idEmpleado)
        {
            _serJson.GuardarNuevoPedido(descripcion, cantidad, precioUnitario, fechaEncargo, estado, notas, idEmpresa, encargadoPor, null, null);
            return RedirectToAction("IndexPedidos", "Pedidos", new { nombreEmpresa = nombreEmpresa, idEmpresa = idEmpresa, idEmpleado = idEmpleado });
        }

        [HttpPost]
        public IActionResult NuevoPedidoPiezaExistente(string descripcion, int cantidad, double precioUnitario, DateOnly fechaEncargo, EstadoPedido estado, List<string> notas, int idEmpresa, string nombreEmpresa, int? encargadoPor, int idPiezaExistente, int? idEmpleado)
        {
            _serJson.GuardarNuevoPedido(descripcion, cantidad, precioUnitario, fechaEncargo, estado, notas, idEmpresa, encargadoPor, idPiezaExistente, null);
            return RedirectToAction("IndexPedidos", "Pedidos", new { nombreEmpresa = nombreEmpresa, idEmpresa = idEmpresa, idEmpleado = idEmpleado });
        }

        [HttpPost]
        public IActionResult NuevoPedidoConjuntoExistente(string descripcion, int cantidad, double precioUnitario, DateOnly fechaEncargo, EstadoPedido estado, List<string> notas, int idEmpresa, string nombreEmpresa, int? encargadoPor, int? idEmpleado, int? idConjuntoExistente)
        {
            _serJson.GuardarNuevoPedido(descripcion, cantidad, precioUnitario, fechaEncargo, estado, notas, idEmpresa, encargadoPor, null, idConjuntoExistente);
            return RedirectToAction("IndexPedidos", "Pedidos", new { nombreEmpresa = nombreEmpresa, idEmpresa = idEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_ModificarFechaEncargo(int idEmpresa, string nombreEmpresa, int idPedido, DateOnly nuevaFecha, int idEmpleado)
        {
            _serJson.PedidoModificarFechaEncargo(idEmpresa, idPedido, nuevaFecha);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_ModificarCantidad(int idEmpresa, int idPedido, int nuevaCantidad, string nombreEmpresa, int idEmpleado)
        {
            _serJson.PedidoModificarCantidad(idEmpresa, idPedido, nuevaCantidad);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_ModificarDescripcion(int idEmpresa, int idPedido, string nuevaDescripcion, string nombreEmpresa, int idEmpleado)
        {
            _serJson.PedidoModificarDescripcion(idEmpresa, idPedido, nuevaDescripcion);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_ModificarPrecioUnitario(int idEmpresa, int idPedido, double nuevoPrecio, string nombreEmpresa, int idEmpleado)
        {
            _serJson.PedidoModificarPrecioUnitario(idEmpresa, idPedido, nuevoPrecio);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        public IActionResult Pedido_Cancelar(int idEmpresa, string nombreEmpresa, int idPedido, int idEmpleado)
        {
            _serJson.PedidoCancelar(idEmpresa, idPedido);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        public IActionResult Pedido_Reactivar(int idEmpresa, string nombreEmpresa, int idPedido, int idEmpleado)
        {
            _serJson.PedidoReactivar(idEmpresa, idPedido);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_ModificarEncargado(int idEmpresa, int idPedido, int? idEmpleado, string nombreEmpresa)
        {
            _serJson.PedidoModificarEncargado(idEmpresa, idPedido, idEmpleado);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_Borrar(int idEmpresa, string nombreEmpresa, int idPedidoEliminar, int idSocio, string contraseñaSocio, int? idEmpleado)
        {
            Socio s = _serJson.ElegirSocio(idSocio);
            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarPedido(idEmpresa, idPedidoEliminar);
                TempData["Mensaje"] = "Pedido Eliminado";
                return RedirectToAction("IndexPedidos", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedidoEliminar, idEmpleado = idEmpleado });
            }  
        }
        [HttpPost]
        public IActionResult Pedido_AgregarNota(int idEmpresa, string nombreEmpresa, int idPedido, int idEmpleado, string notaNueva)
        {
            _serJson.AgregarNotaPedido(idEmpresa, notaNueva, idPedido);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });

            //string url = Url.Action("IndexVerPedido", "Pedidos");
            //return RedirectPermanent(url);
        }
        [HttpPost]
        public IActionResult Pedido_ModificarNota(int idEmpresa, string nombreEmpresa, int idPedido, int? idEmpleado, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaPedido(idEmpresa, idPedido, notaVieja, notaModificada);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Pedido_BorrarNota(int idEmpresa, string nombreEmpresa, int idPedido, string notaBorrar, int? idEmpleado)
        {
            _serJson.BorrarNotaPedido(idEmpresa, idPedido, notaBorrar);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public async Task<IActionResult> Pedido_AgregarArchivo(int idEmpresa, string nombreEmpresa, int idPedido, IFormFile archivo, int? idEmpleado)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido");

            string carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Pedidos");
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
            _serJson.AgregarArchivoPedido(idEmpresa, idPedido, rutaParaJson);

            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }

        // SIN ROTULO Y LINEAS CRUZADAS SOLO CON LICENCIA OFICIAL
        public IActionResult vistaPreviaCad_AsporeCAD(string rutaArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                return BadRequest("La ruta del archivo no puede estar vacía.");
            }

            string rutaFinal = "wwwroot" + rutaArchivo;

            if (!System.IO.File.Exists(rutaFinal))
            {
                return Content($"ARCHIVO NO ENCONTRADO.\nEl servidor buscó físicamente en:\n{rutaFinal}");
            }

            MemoryStream ms = new MemoryStream();

            using (Image image = Image.Load(rutaFinal))
            {
                CadRasterizationOptions rasterizationOptions = new CadRasterizationOptions();
                rasterizationOptions.PageWidth = 1600;
                rasterizationOptions.PageHeight = 1200;
                rasterizationOptions.AutomaticLayoutsScaling = true;

                Aspose.CAD.ImageOptions.PngOptions pngOptions = new Aspose.CAD.ImageOptions.PngOptions();
                pngOptions.VectorRasterizationOptions = rasterizationOptions;

                image.Save(ms, pngOptions);
            }
            ms.Position = 0;

            return File(ms, "image/png");
        }

        // LibreCAD /FreeCAD    AUN NO FUNCIONA!!!
        public IActionResult vistaPreviaCad_LibreCAD(string rutaArchivo)
        {
            string rutaLimpia = rutaArchivo.TrimStart('\\', '/');
            string rootPath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(rootPath, "wwwroot", rutaLimpia);

            if (!System.IO.File.Exists(filePath)) return NotFound();

            string outputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\LibreCAD\librecad.exe",
                Arguments = $"-c \"{filePath}\" -o \"{outputPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit(10000);
            }
            if (!System.IO.File.Exists(outputPath))
            {
                return StatusCode(500, "LibreCAD no pudo exportar la imagen. Verifica que el formato sea soportado.");
            }

            MemoryStream ms = new MemoryStream();
            using (FileStream fs = new FileStream(outputPath, FileMode.Open))
            {
                fs.CopyTo(ms);
            }
            ms.Position = 0;

            System.IO.File.Delete(outputPath);

            return File(ms, "image/png");
        }
        public IActionResult AbrirArchivoPedidoEnSuUbicacion(string archivo)
        {
            _serJson.AbrirArchivoEnSuUbicacion(archivo);
            return NoContent();
        }
        public IActionResult EliminarArchivoDePedido(int idEmpresa, string nombreEmpresa, int idPedido, string rutaArchivo)
        {
            _serJson.BorrarArchivo(idEmpresa, "Pedido", idPedido, rutaArchivo);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
        }
        public IActionResult EliminarArchivoDePiezaDesdePedido(int idEmpresa, string nombreEmpresa, int idPedido, string rutaArchivo, int idPieza)
        {
            _serJson.BorrarArchivo(idEmpresa, "Pieza", idPieza, rutaArchivo);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
        }

    }
}



