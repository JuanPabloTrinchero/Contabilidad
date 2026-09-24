using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContabilidadTaller.Controllers
{
    public class RecepcionesController : Controller
    {
        private readonly ServiciosJson _serJson;
        public RecepcionesController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexRecepciones(int idEmpresa, string nombreEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Recepciones = _serJson.ObtenerRecepcionesJson(idEmpresa);
            ViewBag.Presupuestos = _serJson.ObtenerPresupuestosJson(idEmpresa);
            ViewBag.OrdenesDeCompra = _serJson.ObtenerOrdenesDeCompraJson(idEmpresa);

            return View();
        }

        public IActionResult BorrarRecepcion(int idEmpresa, string nombreEmpresa, int idRecepcionBorrar)
        {
            _serJson.BorrarRecepcion(idEmpresa, idRecepcionBorrar);
            return RedirectToAction("IndexRecepciones", "Recepciones", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
        }
    }
}
