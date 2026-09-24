using ContabilidadTaller.Models;
using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContabilidadTaller.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ServiciosJson _serJson;
        public EmpleadosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexEmpleados(string nombreEmpresa, int idEmpresa)
        {
            ViewBag.Empleados = _serJson.ObtenerEmpleadosJson(idEmpresa);
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.IdEmpresa = idEmpresa;
            return View();
        }
        public IActionResult IndexNuevoEmpleado(string nombreEmpresa, int idEmpresa)
        {
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.IdEmpresa = idEmpresa;
            return View();
        }
        public IActionResult IndexVerEmpleado(int idEmpresa, string nombreEmpresa, int idEmpleado)
        {
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.Empleado = _serJson.ElegirEmpleado(idEmpresa, idEmpleado);

            ViewBag.Pedidos = _serJson.ObtenerPedidosJson(idEmpresa);
            ViewBag.Piezas = _serJson.ObtenerPiezasJson(idEmpresa);
            ViewBag.Conjuntos = _serJson.ObtenerConjuntosJson(idEmpresa);

            ViewBag.Socios = _serJson.ObtenerSociosJson();

            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarEmpleado(idEmpresa, idEmpleado);

            return View();
        }
        [HttpPost]
        public IActionResult NuevoEmpleado(int idEmpresa, string nombreEmpresa, string nombres, string apellido, string telefono, string email, List<string> notas)
        {
            _serJson.GuardarNuevoEmpleado(idEmpresa, nombres, apellido, telefono, email, notas);
            return RedirectToAction("IndexEmpleados", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa});
        }
        [HttpPost]
        public IActionResult AgregarTelefono(int idEmpresa, string nombreEmpresa, int idEmpleado, string nuevoTelefono)
        {
            _serJson.AgregarTelefono(idEmpresa, idEmpleado, nuevoTelefono);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult ModificarTelefono(int idEmpresa, string nombreEmpresa, int idEmpleado, string telefonoViejo, string telefonoModificado)
        {
            _serJson.ModificarTelefono(idEmpresa, idEmpleado, telefonoViejo, telefonoModificado);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult BorrarTelefono(int idEmpresa, string nombreEmpresa, int idEmpleado, string telefonoBorrar)
        {
            _serJson.BorrarTelefono(idEmpresa, idEmpleado, telefonoBorrar);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult AgregarEmail(int idEmpresa, string nombreEmpresa, int idEmpleado, string nuevoEmail)
        {
            _serJson.AgregarEmail(idEmpresa, idEmpleado, nuevoEmail);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult ModificarEmail(int idEmpresa, string nombreEmpresa, int idEmpleado, string emailViejo, string emailModificado)
        {
            _serJson.ModificarEmail(idEmpresa, idEmpleado, emailViejo, emailModificado);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult BorrarEmail(int idEmpresa, string nombreEmpresa, int idEmpleado, string emailBorrar)
        {
            _serJson.BorrarEmail(idEmpresa, idEmpleado, emailBorrar);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult ModificarNombres(int idEmpresa, string nombreEmpresa, int idEmpleado, string nuevosNombres)
        {
            _serJson.ModificarNombres(idEmpresa, idEmpleado, nuevosNombres);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult ModificarApellido(int idEmpresa, string nombreEmpresa, int idEmpleado, string nuevoApellido)
        {
            _serJson.ModificarApellido(idEmpresa, idEmpleado, nuevoApellido);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult ModificarEstado(int idEmpresa, string nombreEmpresa, int idEmpleado)
        {
            _serJson.ModificarEstado(idEmpresa, idEmpleado);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Empleado_AgregarNota(int idEmpresa, string nombreEmpresa, int idEmpleado, string nuevaNota)
        {
            _serJson.AgregarNotaEmpleado(idEmpresa, idEmpleado, nuevaNota);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Empleado_ModificarNota(int idEmpresa, string nombreEmpresa, int idEmpleado, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaEmpleado(idEmpresa, idEmpleado, notaVieja, notaModificada);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Empleado_BorrarNota(int idEmpresa, string nombreEmpresa, int idEmpleado, string notaBorrar)
        {
            _serJson.BorrarNotaEmpleado(idEmpresa, idEmpleado, notaBorrar);
            return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleado });
        }
        [HttpPost]
        public IActionResult Empleado_BorrarEmpleado(int idEmpresa, string nombreEmpresa, int idSocio, string contraseñaSocio, int idEmpleadoBorrar)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocio.ToLower()))
            {
                _serJson.BorrarEmpleado(idEmpresa, idEmpleadoBorrar);
                TempData["Mensaje"] = "Empleado Borrado";
                return RedirectToAction("IndexEmpleados", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa });
            }
            else
            {
                TempData["Mensaje"] = "Contraseña Incorrecta";
                return RedirectToAction("IndexVerEmpleado", "Empleados", new { idEmpresa = idEmpresa, nombreEmpresa = nombreEmpresa, idEmpleado = idEmpleadoBorrar });
            }
        }

    }
}
