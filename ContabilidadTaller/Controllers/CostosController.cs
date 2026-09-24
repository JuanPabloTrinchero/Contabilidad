using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContabilidadTaller.Controllers
{
    public class CostosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public CostosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexNuevoCosto(int idEmpresa, int idPedido, int? idEmpleado)
        {
            ViewBag.Empresa = _serJson.ElegirEmpresa(idEmpresa);
            Pedido pedido = _serJson.BuscarPedido(idEmpresa, idPedido);

            ViewBag.Pedido = pedido;
            
            if (pedido.PiezaExistenteFK != null)
            {
                Pieza pieza = new Pieza();

                List<Pieza> piezas = _serJson.ObtenerPiezasJson(idEmpresa);
                if (piezas != null && piezas.Any())
                {
                    foreach (Pieza p in piezas)
                    {
                        if (pedido.PiezaExistenteFK == p.IdPieza)
                        {
                            pieza = p;
                        }
                    }
                }

                if (pieza.Descripcion != null)
                {
                    ViewBag.Descripcion = pieza.Descripcion;
                }
                else
                {
                    ViewBag.Descripcion = "- Sin Descripcion -";
                }

            }else if (pedido.ConjuntoExistenteFK != null)
            {
                Conjunto conjunto = new Conjunto();

                List<Conjunto> conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
                if (conjuntos != null && conjuntos.Any())
                {
                    foreach (Conjunto c in conjuntos)
                    {
                        if (pedido.ConjuntoExistenteFK == c.IdConjunto)
                        {
                            conjunto = c;
                        }
                    }
                }

                if (conjunto.Descripcion != null )
                {
                    ViewBag.Descripcion = conjunto.Descripcion;
                }
                else
                {
                    ViewBag.Descripcion = "- Sin Descripcion -";
                }
            }
            else if(pedido.Descripcion != null)
            {
                ViewBag.Descripcion = pedido.Descripcion;
            }
            else
            {
                ViewBag.Descripcion = "- Sin Descripcion -";
            }

            ViewBag.IdEmpleado = idEmpleado;

            return View();
        }
        public IActionResult IndexVerCosto(int idEmpresa, int idPedido, int idCosto, int? idEmpleado)
        {
            ViewBag.Empresa = _serJson.ElegirEmpresa(idEmpresa);

            Pedido pedido = _serJson.BuscarPedido(idEmpresa, idPedido);
            ViewBag.Pedido = pedido;

            if (pedido.PiezaExistenteFK != null)
            {
                Pieza pieza = new Pieza();

                List<Pieza> piezas = _serJson.ObtenerPiezasJson(idEmpresa);
                if (piezas != null && piezas.Any())
                {
                    foreach (Pieza p in piezas)
                    {
                        if (pedido.PiezaExistenteFK == p.IdPieza)
                        {
                            pieza = p;
                        }
                    }
                }

                if (pieza.Descripcion != null)
                {
                    ViewBag.Descripcion = pieza.Descripcion;
                }
                else
                {
                    ViewBag.Descripcion = "- Sin Descripcion -";
                }

            }
            else if (pedido.ConjuntoExistenteFK != null)
            {
                Conjunto conjunto = new Conjunto();

                List<Conjunto> conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);
                if (conjuntos != null && conjuntos.Any())
                {
                    foreach (Conjunto c in conjuntos)
                    {
                        if (pedido.ConjuntoExistenteFK == c.IdConjunto)
                        {
                            conjunto = c;
                        }
                    }
                }

                if (conjunto.Descripcion != null)
                {
                    ViewBag.Descripcion = conjunto.Descripcion;
                }
                else
                {
                    ViewBag.Descripcion = "- Sin Descripcion -";
                }
            }
            else if (pedido.Descripcion != null)
            {
                ViewBag.Descripcion = pedido.Descripcion;
            }
            else
            {
                ViewBag.Descripcion = "- Sin Descripcion -";
            }

            ViewBag.Costo = _serJson.BuscarCosto(idEmpresa, idCosto);

            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.IdEmpleado = idEmpleado;

            return View();
        }
        [HttpPost]
        public IActionResult GuardarNuevoCosto(Empresa empresa, string descripcion, double precio, List<string>? notas, int idPedido, string? archivo, int? idEmpleado)
        {
            _serJson.NuevoCosto(empresa.IdEmpresa, descripcion, precio, notas, idPedido, archivo);
            return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa = empresa.IdEmpresa, nombreEmpresa = empresa.NombreEmpresa, idPedido = idPedido, idEmpleado = idEmpleado });
        }

        [HttpPost]
        public IActionResult Costo_ModificarDescripcion(Empresa empresa, int? idEmpleado, int idCosto, string? nuevaDescripcion, int idPedido)
        {
            _serJson.ModificarDescripcionCosto(empresa.IdEmpresa, idCosto, nuevaDescripcion);
            return RedirectToAction("IndexVerCosto", "Costos", new { idEmpresa = empresa.IdEmpresa, nombreEmpresa = empresa.NombreEmpresa, idCosto = idCosto, idEmpleado = idEmpleado, idPedido = idPedido });
        }
        [HttpPost]
        public IActionResult Costo_ModificarPrecio(Empresa empresa, int? idEmpleado, int idCosto, double? nuevoPrecio, int idPedido)
        {
            _serJson.ModificarPrecioCosto(empresa.IdEmpresa, idCosto, nuevoPrecio);
            return RedirectToAction("IndexVerCosto", "Costos", new { idEmpresa = empresa.IdEmpresa, nombreEmpresa = empresa.NombreEmpresa, idCosto = idCosto, idEmpleado = idEmpleado, idPedido = idPedido });
        }

    }
}
