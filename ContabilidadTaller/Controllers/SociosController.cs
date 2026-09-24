using ContabilidadTaller.Services;
using Microsoft.AspNetCore.Mvc;
using ContabilidadTaller.Models;

namespace ContabilidadTaller.Controllers
{
    public class SociosController : Controller
    {
        private readonly ServiciosJson _serJson;

        public SociosController(ServiciosJson serJson)
        {
            _serJson = serJson;
        }
        public IActionResult IndexSocios()
        {
            ViewBag.Socios = _serJson.ObtenerSociosJson();
            return View();
        }
        public IActionResult IndexNuevoSocio()
        {
            return View();
        }

        public IActionResult IndexVerSocio(int idSocio)
        {
            Socio s = _serJson.ElegirSocio(idSocio);

            ViewBag.Socio = s;

            ViewBag.Socios = _serJson.ObtenerSociosJson();
            ViewBag.SePuedeBorrar = _serJson.SePuedeBorrarSocio(idSocio);

            return View();
        }
        [HttpPost]
        public IActionResult GuardarSocio(string nombres, string apellido, int inicioCuit, int medioCuit, int finCuit, string direccion, List<string>? emails, string telefono, string contraseña, List<string>? notas)
        {
            _serJson.GuardarNuevoSocio(nombres, apellido, inicioCuit, medioCuit, finCuit, direccion, emails, telefono, contraseña, notas);
            return RedirectToAction("IndexSocios", "Socios");
        }


        [HttpPost]
        public IActionResult Socio_ModificarNombre(int idSocio, string nuevosNombres)
        {
            _serJson.ModificarNombreSocio(idSocio, nuevosNombres);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarApellido(int idSocio, string nuevoApellido)
        {
            _serJson.ModificarApellidoSocio(idSocio, nuevoApellido);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarCUIT(int idSocio, int inicioCuit, int medioCuit, int finCuit)
        {
            _serJson.ModificarCUITSocio(idSocio, inicioCuit, medioCuit, finCuit);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarDireccion(int idSocio, string nuevaDireccion)
        {
            _serJson.ModificarDireccionSocio(idSocio, nuevaDireccion);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_AgregarEmail(int idSocio, string nuevoEmail)
        {
            _serJson.AgregarEmailSocio(idSocio, nuevoEmail);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarEmail(int idSocio, string emailViejo, string nuevoEmail)
        {
            _serJson.ModificarEmailSocio(idSocio, emailViejo, nuevoEmail);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_BorrarEmail(int idSocio, string emailBorrar)
        {
            _serJson.BorrarEmailSocio(idSocio, emailBorrar);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarTelefono(int idSocio, string nuevoTelefono)
        {
            _serJson.ModificarTelefonoSocio(idSocio, nuevoTelefono);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_BorrarTelefono(int idSocio, string nuevoTelefono)
        {
            _serJson.BorrarTelefono(idSocio);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }

        [HttpPost]
        public IActionResult Socio_AgregarNota(int idSocio, string nuevaNota)
        {
            _serJson.AgregarNotaSocio(idSocio, nuevaNota);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarNota(int idSocio, string notaVieja, string notaModificada)
        {
            _serJson.ModificarNotaSocio(idSocio, notaVieja, notaModificada);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_BorrarNota(int idSocio, string notaBorrar)
        {
            _serJson.BorrarNotaSocio(idSocio, notaBorrar);
            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }
        [HttpPost]
        public IActionResult Socio_ModificarContraseña(int idSocio, string contraseñaActual, string contraseñaNueva, string contraseñaNuevaRepetida)
        {
            bool contActualCorrecta = false;
            bool contNuevaCorrecta = false;

            if (_serJson.RevisarContraseñaActual(idSocio, contraseñaActual) == true)
            {
                contActualCorrecta = true;
            }
            if (_serJson.RevisarContraseñaNueva(contraseñaNueva, contraseñaNuevaRepetida) == true)
            {
                contNuevaCorrecta = true;
            }

                if (contActualCorrecta == false && contNuevaCorrecta == false)
                {
                    TempData["Mensaje"] = "Contraseñas Actual y Nueva invalidas";
                } 
                else if (contActualCorrecta == false && contNuevaCorrecta == true)
                {
                    TempData["Mensaje"] = "Contraseña Actual invalida";
                }
                else if (contActualCorrecta == true && contNuevaCorrecta == false)
                {
                    TempData["Mensaje"] = "Contraseña Nueva invalida";
                }
                else if (contActualCorrecta == true && contNuevaCorrecta == true)
                {
                    _serJson.ModificarContraseñaSocio(idSocio, contraseñaNueva);
                    TempData["Mensaje"] = "Contraseña Modificada Correctamente!";
                }

            return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocio });
        }

        [HttpPost]
        public IActionResult Socio_Borrar(int idSocioBorrar, int idSocioUsuario, string contraseñaSocioUsuario)
        {
            Socio s = _serJson.ElegirSocio(idSocioUsuario);
            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaSocioUsuario.ToLower()))
            {
                _serJson.BorrarSocio(idSocioBorrar);
                TempData["Mensaje"] = "Socio borrado";
                return RedirectToAction("IndexSocios", "Socios");
            }
            else
            {
                TempData["Mensaje"] = "Contraseña incorrecta";
                return RedirectToAction("IndexVerSocio", "Socios", new { idSocio = idSocioBorrar });
            }
        }


        

    }
}
