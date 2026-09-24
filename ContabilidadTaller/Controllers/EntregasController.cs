using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContabilidadTaller.Controllers
{
    public class EntregasController : Controller
    {
        private readonly ServiciosJson _serJson;
        public EntregasController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexNuevaEntrega(int idEmpresa, string nombreEmpresa, int idPedido)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Pedido = _serJson.BuscarPedido(idEmpresa, idPedido);
            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);
            ViewBag.Descripcion = _serJson.DescripcionDePedido(idEmpresa, idPedido);

            return View();
        }
        public IActionResult IndexVerEntregasDePedido(int idEmpresa, string nombreEmpresa, int idPedido)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Pedido = _serJson.BuscarPedido(idEmpresa, idPedido);
            ViewBag.Remitos = _serJson.ObtenerRemitosJson(idEmpresa);
            ViewBag.Descripcion = _serJson.DescripcionDePedido(idEmpresa, idPedido);

            return View();
        }
        [HttpPost]
        public IActionResult GuardarNuevaEntrega(int idEmpresa, string nombreEmpresa, int idPedido, DateOnly fechaEntrega, int cantidadEntrega, int? idRemito)
        {
            _serJson.NuevaEntregaPedido(idEmpresa, idPedido, fechaEntrega, cantidadEntrega, idRemito);
            //return RedirectToAction("IndexVerPedido", "Pedidos", new { idEmpresa  = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
            return RedirectToAction("IndexVerEntregasDePedido", "Entregas", new { idEmpresa  = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
        }
        [HttpPost]
        public IActionResult AsignarRemitoAEntrega(int idEmpresa, string nombreEmpresa, int idPedido, int idRemito, int idEntrega)
        {
            _serJson.AsignarRemitoAEntrega(idEmpresa, idPedido, idRemito, idEntrega);
            return RedirectToAction("IndexVerEntregasDePedido", "Entregas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
        }

        [HttpPost]
        public IActionResult Entrega_Borrar(int idEmpresa, string nombreEmpresa, int idPedido, int idEntrega)
        {
            _serJson.BorrarEntrega(idEmpresa, idEntrega, idPedido);
            return RedirectToAction("IndexVerEntregasDePedido", "Entregas", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idPedido = idPedido });
        }


        
    }
}
