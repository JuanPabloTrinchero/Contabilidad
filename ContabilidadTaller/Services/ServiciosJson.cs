using Aspose.CAD.FileFormats.Collada.FileParser.Elements;
using Aspose.CAD.FileFormats.Tiff.Enums;
using ContabilidadTaller.Controllers;
using ContabilidadTaller.Interfaces;
using ContabilidadTaller.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ContabilidadTaller.Services
{
    /* SIN USAR
    public class ArchivoJson
    {
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
        public string ContenidoBase64 { get; set; }
    }
    */

    public class ServiciosJson : IServiciosJson
    {
        // ----- EMPRESAS! -----
        public string EmpresasRutaJson()
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\Empresas.json";
        }
        public List<Empresa> ObtenerEmpresasJson()
        {
            List<Empresa> Empresas = new List<Empresa>();
            string RutaJsonEmpresas = EmpresasRutaJson();

            if (System.IO.File.Exists(RutaJsonEmpresas))
            {
                if (System.IO.File.ReadAllText(RutaJsonEmpresas) != "" || System.IO.File.ReadAllText(RutaJsonEmpresas) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonEmpresas);
                    Empresas = JsonConvert.DeserializeObject<List<Empresa>>(jsonRecuperado);
                }
            }
            return Empresas;  
        }
        public List<Empresa> EmpresasOrdenadasPorId()
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            return empresas.OrderBy(e => e.IdEmpresa).ToList();
        }
        public Empresa ElegirEmpresa(int idEmpresa)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            return empresas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);
        }
        public int PrimerIdEmpresaFaltante()
        {
            List<int> ids = ObtenerEmpresasJson().Select(e => e.IdEmpresa).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public int GuardarNuevaEmpresa(string nombreEmpresa, int inicioCuit, int medioCuit, int finCuit, string direccion, List<string> notas)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            Empresa e = new Empresa();

            e.IdEmpresa = PrimerIdEmpresaFaltante();

            e.NombreEmpresa = nombreEmpresa;

            e.Cuit = new Cuit
            {
                InicioCuit = inicioCuit,
                MedioCuit = medioCuit,
                FinCuit = finCuit
            };

            e.Direccion = direccion;
            e.Notas = new List<string>();

            if (notas != null && notas.Any())
            {
                e.Notas = notas;
            }

            empresas.Add(e);

            ActualizarJson_Empresas(empresas);

            return e.IdEmpresa;
        }
        public void ModificarNombreEmpresa(int idEmpresa, string nuevoNombre)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();

            foreach (Empresa e in empresas)
            {
                if (e.IdEmpresa == idEmpresa)
                {
                    e.NombreEmpresa = nuevoNombre;

                    ActualizarJson_Empresas(empresas);
                }
            }
        }
        public void ModificarDireccionEmpresa(int idEmpresa, string nuevaDireccion)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();

            foreach (Empresa e in empresas)
            {
                if (e.IdEmpresa == idEmpresa)
                {
                    e.Direccion = nuevaDireccion;

                    ActualizarJson_Empresas(empresas);
                }
            }
        }
        public void ModificarCuitEmpresa(int idEmpresa, int inicioCuit, int medioCuit, int finCuit)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            foreach (Empresa e in empresas)
            {
                if (e.IdEmpresa == idEmpresa)
                {
                    e.Cuit.InicioCuit = inicioCuit;
                    e.Cuit.MedioCuit = medioCuit;
                    e.Cuit.FinCuit = finCuit;

                    ActualizarJson_Empresas(empresas);
                }
            }
        }
        public void AgregarNotaEmpresa(int idEmpresa, string nuevaNota)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            foreach (Empresa e in empresas)
            {
                if (e.IdEmpresa == idEmpresa)
                {
                    if (e.Notas != null && e.Notas.Any())
                    {
                        e.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        e.Notas = new List<string>() { nuevaNota };
                    }
                    ActualizarJson_Empresas(empresas);
                }
            }    
        }
        public void EliminarNotaEmpresa(int idEmpresa, string notaEliminar)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            Empresa e = empresas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (e.Notas != null && e.Notas.Any())
            {
                e.Notas.Remove(notaEliminar);
            }
            ActualizarJson_Empresas(empresas);
        }
        public void ModificarNotaEmpresa(int idEmpresa, string notaVieja, string notaModificada)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            Empresa empresa = empresas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (notaModificada != null)
            {
                if (empresa != null && empresa.Notas != null && empresa.Notas.Any())
                {
                    int indiceNota = empresa.Notas.IndexOf(notaVieja);

                    if (indiceNota != -1)
                    {
                        empresa.Notas[indiceNota] = notaModificada;

                        ActualizarJson_Empresas(empresas);
                    }
                }
            } 
        }
        public RespuestaBorrar SePuedeBorrarEmpresa(int idEmpresa)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };

            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            if (empleados != null && empleados.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene empleados guardados.");
            }
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            if (piezas != null && piezas.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene piezas guardadas.");
            }
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            if (conjuntos != null && conjuntos.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene conjuntos guardados.");
            }
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (pedidos != null && pedidos.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene pedidos guardados.");
            }
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            if (presupuestos != null && presupuestos.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene presupuestos guardados.");
            }
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            if (remitos != null && remitos.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene remitos guardados.");
            }
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            if (odcs != null && odcs.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene ordenes de compra guardadas.");
            }
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            if (facturas != null && facturas.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene facturas guardadas.");
            }
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            if (ndcs != null && ndcs.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene notas de credito guardadas.");
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- No hay nada guardado aun -");
            }

            return respuesta;
        }
        public void EliminarEmpresa(int idEmpresaEliminar)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            empresas.RemoveAll(e => e.IdEmpresa == idEmpresaEliminar);
            ActualizarJson_Empresas(empresas);
        }

        // ----- SOCIOS! -----
        public string SociosRutaJson()
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Socios.json";
        }
        public List<Socio> ObtenerSociosJson()
        {
            List<Socio> Socios = new List<Socio>();
            string RutaJsonSocios = SociosRutaJson();

            if (System.IO.File.Exists(RutaJsonSocios))
            {
                if (System.IO.File.ReadAllText(RutaJsonSocios) != "" || System.IO.File.ReadAllText(RutaJsonSocios) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonSocios);
                    Socios = JsonConvert.DeserializeObject<List<Socio>>(jsonRecuperado);
                }
            }

            return Socios;
        }
        public void GuardarNuevoSocio(string nombres, string apellido, int inicioCuit, int medioCuit, int finCuit, string direccion, List<string>? emails, string telefono, string contraseña, List<string>? notas)
        {
            List<Socio> Socios = ObtenerSociosJson();
            Socio nuevoSocio = new Socio();

            nuevoSocio.IdSocio = Socios.Any() ? Socios.Max(x => x.IdSocio) + 1 : 1;
            nuevoSocio.Nombres = nombres;
            nuevoSocio.Apellido = apellido;

            nuevoSocio.Cuit = new Cuit
            {
                InicioCuit = inicioCuit,
                MedioCuit = medioCuit,
                FinCuit = finCuit
            };

            nuevoSocio.Direccion = direccion;
            nuevoSocio.Emails = new List<string>();

            if (emails != null && emails.Any())
            {
                nuevoSocio.Emails = emails;
            }

            nuevoSocio.Telefono = telefono;
            nuevoSocio.ContraseñaConfirmacion = contraseña;
            nuevoSocio.Notas = new List<string>();
            if (notas != null && notas.Any())
            {
                nuevoSocio.Notas = notas;
            }

            Socios.Add(nuevoSocio);

            string jsonActualizado = JsonConvert.SerializeObject(Socios, Formatting.Indented);
            File.WriteAllText(SociosRutaJson(), jsonActualizado);
        }
        public Socio ElegirSocio(int idSocio)
        {
            List<Socio> todos = ObtenerSociosJson();
            Socio socio = new Socio();

            foreach (Socio s in todos)
            {
                if (s.IdSocio == idSocio)
                {
                    socio = s;
                }
            }
            return socio;
        }
        public void ModificarNombreSocio(int idSocio, string nuevosNombres)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Nombres = nuevosNombres;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void ModificarApellidoSocio(int idSocio, string nuevoApellido)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Apellido = nuevoApellido;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void ModificarCUITSocio(int idSocio, int inicioCuit, int medioCuit, int finCuit)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Cuit.InicioCuit = inicioCuit;
                    s.Cuit.MedioCuit = medioCuit;
                    s.Cuit.FinCuit = finCuit;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void ModificarDireccionSocio(int idSocio, string nuevaDireccion)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Direccion = nuevaDireccion;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void AgregarEmailSocio(int idSocio, string nuevoEmail)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Emails != null && s.Emails.Any())
                    {
                        s.Emails.Add(nuevoEmail);
                    }
                    else
                    {
                        s.Emails = new List<string>() { nuevoEmail };
                    }

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void ModificarEmailSocio(int idSocio, string emailViejo, string emailModificado)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Emails != null && s.Emails.Any())
                    {
                        int indexEmail = s.Emails.IndexOf(emailViejo);

                        if (indexEmail != -1)
                        {
                            s.Emails[indexEmail] = emailModificado;

                            ActualizarJson_Socios(socios);
                        }
                    }                   
                }
            }
        }
        public void BorrarEmailSocio(int idSocio, string emailBorrar)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Emails != null && s.Emails.Any())
                    {
                        int eliminado = s.Emails.RemoveAll(e => e == emailBorrar);

                        ActualizarJson_Socios(socios);
                    }
                }
            }
        }
        public void ModificarTelefonoSocio(int idSocio, string nuevoTelefono)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Telefono = nuevoTelefono;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void BorrarTelefono(int idSocio)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.Telefono = null;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void AgregarNotaSocio(int idSocio, string nuevaNota)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Notas != null && s.Notas.Any())
                    {
                        s.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        s.Notas = new List<string>() { nuevaNota };
                    }

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public void ModificarNotaSocio(int idSocio, string notaVieja, string notaModificada)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Notas != null && s.Notas.Any())
                    {
                        int indexNota = s.Notas.IndexOf(notaVieja);

                        if (indexNota != -1)
                        {
                            s.Notas[indexNota] = notaModificada;

                            ActualizarJson_Socios(socios);
                        }
                    }
                }
            }
        }
        public void BorrarNotaSocio(int idSocio, string notaBorrar)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    if (s.Notas != null && s.Notas.Any())
                    {
                        int eliminado = s.Notas.RemoveAll(n => n == notaBorrar);

                        ActualizarJson_Socios(socios);
                    }
                }
            }
        }
        public bool RevisarContraseñaActual(int idSocio, string contraseñaActual)
        {
            bool valido = false;
            Socio s = ElegirSocio(idSocio);
            if (s.ContraseñaConfirmacion.ToLower().Equals(contraseñaActual.ToLower()))
            {
                valido = true;
            }
            return valido;
        }
        public bool RevisarContraseñaNueva(string contraseñaNueva, string contraseñaNuevaRepetida)
        {
            bool valido = false;
            if (contraseñaNueva.ToLower().Equals(contraseñaNuevaRepetida.ToLower()))
            {
                valido = true;
            }
            return valido;
        }
        public void ModificarContraseñaSocio(int idSocio, string contraseñaNueva)
        {
            List<Socio> socios = ObtenerSociosJson();
            foreach (Socio s in socios)
            {
                if (s.IdSocio == idSocio)
                {
                    s.ContraseñaConfirmacion = contraseñaNueva;

                    ActualizarJson_Socios(socios);
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarSocio(int idSocio)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };

            List<Empresa> empresas = ObtenerEmpresasJson();
            foreach (Empresa e in empresas)
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(e.IdEmpresa);
                foreach (Presupuesto pr in presupuestos)
                {
                    if (pr.SocioFK == idSocio)
                    {
                        respuesta.SePuede = false;
                        respuesta.Mensajes.Add("Tiene presupuestos asignados.");
                        break;
                    }
                }
            }
            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }
            return respuesta;
        }
        public void BorrarSocio(int idSocio)
        {
            if (SePuedeBorrarSocio(idSocio).SePuede == true)
            {
                List<Socio> socios = ObtenerSociosJson();

                socios.RemoveAll(s => s.IdSocio == idSocio);

                ActualizarJson_Socios(socios);
            }
        }



        // ----- EMPLEADOS! -----
        public string EmpleadosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Empleados.json";
        }
        public List<Empleado> ObtenerEmpleadosJson(int idEmpresa)
        {
            List<Empleado> Empleados = new List<Empleado>();
            string RutaJsonEmpleados = EmpleadosRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonEmpleados))
            {
                if (System.IO.File.ReadAllText(RutaJsonEmpleados) != "" || System.IO.File.ReadAllText(RutaJsonEmpleados) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonEmpleados);
                    Empleados = JsonConvert.DeserializeObject<List<Empleado>>(jsonRecuperado);
                }
            }
            return Empleados;
        }
        public void GuardarNuevoEmpleado(int idEmpresa, string nombres, string apellido, string telefono, string email, List<string> notas)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            Empleado e = new Empleado();

            e.IdEmpleado = empleados.Any() ? empleados.Max(x => x.IdEmpleado) + 1 : 0;
            e.NombreEmpleado = nombres;
            e.ApellidoEmpleado = apellido;

            e.Telefonos = new List<string>();
            if (!string.IsNullOrEmpty(telefono))
            {
                e.Telefonos.Add(telefono);
            }

            e.Emails = new List<string>();
            if (!string.IsNullOrEmpty(email))
            {
                e.Emails.Add(email);
            }

            e.Notas = new List<string>();
            if (notas != null && notas.Any())
            {
                e.Notas = notas;
            }

            e.Estado = EstadoEmpleado.Activo;

            empleados.Add(e);

            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
        }
        public Empleado ElegirEmpleado(int idEmpresa, int? idEmpleadoBuscado)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            Empleado empleado = new Empleado() { IdEmpleado = -1};
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleadoBuscado)
                {
                    empleado = e;
                }
            }
            return empleado;
            //return todos.FirstOrDefault(e => e.IdEmpleado == idEmpleado);
        }
        public Empleado SinEnmpleado()
        {
            return new Empleado { IdEmpleado = -1 };
        }
        public void ModificarNombres(int idEmpresa, int idEmpleado, string nuevosNombres)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    e.NombreEmpleado = nuevosNombres;

                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarApellido(int idEmpresa, int idEmpleado, string nuevoApellido)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    e.ApellidoEmpleado = nuevoApellido;

                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void AgregarTelefono(int idEmpresa, int idEmpleado, string nuevoTelefono)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Telefonos != null && e.Telefonos.Any())
                    {

                        e.Telefonos.Add(nuevoTelefono);
                    }
                    else
                    {
                        e.Telefonos = new List<string>() { nuevoTelefono };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarTelefono(int idEmpresa, int idEmpleado, string telefonoViejo, string telefonoModificado)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Telefonos != null && e.Telefonos.Any())
                    {
                        int indexTelefono = e.Telefonos.IndexOf(telefonoViejo);

                        if (indexTelefono != -1)
                        {
                            e.Telefonos[indexTelefono] = telefonoModificado;

                            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                        }
                    }  
                }
            }
        }
        public void BorrarTelefono(int idEmpresa, int idEmpleado, string telefonoBorrar)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Telefonos != null && e.Telefonos.Any())
                    {
                        int eliminados = e.Telefonos.RemoveAll(t => t == telefonoBorrar);

                        string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                        File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void AgregarEmail(int idEmpresa, int idEmpleado, string nuevoEmail)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Emails != null && e.Emails.Any())
                    {

                        e.Emails.Add(nuevoEmail);
                    }
                    else
                    {
                        e.Emails = new List<string>() { nuevoEmail };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarEmail(int idEmpresa, int idEmpleado, string emailViejo, string emailModificado)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Emails != null && e.Emails.Any())
                    {
                        int indexEmail = e.Emails.IndexOf(emailViejo);

                        if (indexEmail != -1)
                        {
                            e.Emails[indexEmail] = emailModificado;

                            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public void BorrarEmail(int idEmpresa, int idEmpleado, string emailBorrar)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Emails != null && e.Emails.Any())
                    {
                        int eliminados = e.Emails.RemoveAll(e => e == emailBorrar);

                        string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                        File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void ModificarEstado(int idEmpresa, int idEmpleado)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Estado == EstadoEmpleado.Activo)
                    {
                        e.Estado = EstadoEmpleado.Inactivo;
                    }
                    else
                    {
                        e.Estado = EstadoEmpleado.Activo;
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarEmpleado(int idEmpresa, int idEmpleado)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };

            Empleado empleado = ElegirEmpleado(idEmpresa, idEmpleado);
            if (empleado.Estado == EstadoEmpleado.Activo)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Esta activo.");
            }
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.EncargadoPorFK == idEmpleado)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene pedidos asociados.");
                    break;
                }
            }

            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.EntregadoA == idEmpleado)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene remitos asociados.");
                    break;
                }
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }

            return respuesta;
        }
        public void BorrarEmpleado(int idEmpresa, int idEmpleadoBorrar)
        {
            if (SePuedeBorrarEmpleado(idEmpresa, idEmpleadoBorrar).SePuede == true)
            {
                List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
                empleados.RemoveAll(x => x.IdEmpleado == idEmpleadoBorrar);

                ActualizarJson_Empleados(empleados, idEmpresa); ;
            }   
        }
        public void AgregarNotaEmpleado(int idEmpresa, int idEmpleado, string nuevaNota)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Notas != null && e.Notas.Any())
                    {
                        e.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        e.Notas = new List<string>() { nuevaNota };
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNotaEmpleado(int idEmpresa, int idEmpleado, string notaVieja, string notaModificada)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Notas != null && e.Notas.Any())
                    {
                        int indexNota = e.Notas.IndexOf(notaVieja);

                        if (indexNota != -1)
                        {
                            e.Notas[indexNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public void BorrarNotaEmpleado(int idEmpresa, int idEmpleado, string notaBorrar)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            foreach (Empleado e in empleados)
            {
                if (e.IdEmpleado == idEmpleado)
                {
                    if (e.Notas != null && e.Notas.Any())
                    {
                        int eliminados = e.Notas.RemoveAll(n => n == notaBorrar);

                        string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                        File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }


        // ----- PIEZAS! -----
        public string PiezasRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Piezas\" + idEmpresa.ToString() + @" - Piezas.json";
        }
        public List<Pieza> ObtenerPiezasJson(int idEmpresa)
        {
            List<Pieza> Piezas = new List<Pieza>();
            string RutaJsonPiezas = PiezasRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonPiezas))
            {
                if (System.IO.File.ReadAllText(RutaJsonPiezas) != "" || System.IO.File.ReadAllText(RutaJsonPiezas) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonPiezas);
                    Piezas = JsonConvert.DeserializeObject<List<Pieza>>(jsonRecuperado);
                }
            }
            return Piezas;
        }
        public List<Pieza> PiezasOrdenadasPorId(int idEmpresa)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            return piezas.OrderBy(p => p.IdPieza).ToList();
        }
        public Pieza ElegirPieza(int idEmpresa, int idPieza)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            return piezas.FirstOrDefault(p => p.IdPieza == idPieza);
        }
        public void AgregarArchivoPieza(int idEmpresa, int idPieza, string rutaArchivo)
        {
            List<Pieza> Piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = Piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            if (p != null)
            {
                if (p.Archivos == null || !p.Archivos.Any())
                {
                    p.Archivos = new List<string>();
                    p.Archivos.Add(rutaArchivo);
                }
                else
                {
                    p.Archivos.Add(rutaArchivo);
                }
                string jsonActualizado = JsonConvert.SerializeObject(Piezas, Formatting.Indented);
                File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
            } 
        }
        public List<Pedido> BuscarPiezaEnPedidos(int idEmpresa, string descripcionPieza)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            char[] delimitadores = { ' ', ',', '.', '(', ')', '[', ']', '-', '_', ':', '=', '?', 'Ø', 'ø', 'O' };

            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> coincidencias = new List<Pedido>();

            descripcionPieza.Replace("Ø", "");
            string[] palabras = descripcionPieza.ToLower().Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

            foreach (Pedido pedido in todos)
            {
                foreach (string palabra in palabras) 
                {
                    if (pedido.Descripcion.Contains(palabra) && !coincidencias.Contains(pedido))
                    {
                        coincidencias.Add(pedido);
                    }
                }
            }
            return coincidencias;
        }
        public void ModificarDescripcionPieza(int idEmpresa, int idPieza, string nuevaDescripcion)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            if (nuevaDescripcion != null && p != null)
            {
                p.Descripcion = nuevaDescripcion;

                string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
            }
        }
        public void AgregarNotaPieza(int idEmpresa, int idPieza, string nuevaNota)
        {
            {
                List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
                Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

                if (nuevaNota != null && p != null)
                {
                    p.Notas.Add(nuevaNota);

                    string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNotaPieza(int idEmpresa, int idPieza, string notaActual, string notaModificada)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            int index = p.Notas.IndexOf(notaActual);

            if (index != -1)
            {
                p.Notas[index] = notaModificada;
            }

            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void EliminarNotaPieza(int idEmpresa, int idPieza, string notaEliminar)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            p.Notas.Remove(notaEliminar);

            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public List<string>? NombresDeArchivosDePieza(int idEmpresa, int idPieza)
        {
            Pieza p = ElegirPieza(idEmpresa, idPieza);
            List<string> nombres = new List<string>();
            if (p.Archivos != null && p.Archivos.Any())
            {
                foreach (string arch in p.Archivos)
                {
                    nombres.Add(Path.GetFileName(arch));
                }
            }
            return nombres;
        }
        public int PrimerIdPiezaFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerPiezasJson(idEmpresa).Select(p => p.IdPieza).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public async Task NuevaPieza(int idEmpresa, string descripcion, string? codigo, List<string>? notas, int? idConjunto, List<IFormFile>? archivos)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza pNueva = new Pieza();

            int idPiezaNueva = PrimerIdPiezaFaltante(idEmpresa);

            pNueva.IdPieza = idPiezaNueva;
            pNueva.Descripcion = descripcion;
            if (!string.IsNullOrEmpty(codigo))
            {
                pNueva.Codigo = codigo;
            }
            else
            {
                pNueva.Codigo = null;
            }
            pNueva.Notas = new List<string>();
            if (notas != null && notas.Any())
            {
                pNueva.Notas = notas;
            }
            if (idConjunto != null)
            {
                pNueva.ConjuntoFK = idConjunto;
            }
            else
            {
                pNueva.ConjuntoFK = null;
            }
            piezas.Add(pNueva);

            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);

            if (archivos != null && archivos.Any())
            {
                await GuardarArchivos(idEmpresa, "Pieza", idPiezaNueva, archivos);
            }
        }
        public RespuestaBorrar SePuedeBorrarPieza(int idEmpresa, int idPieza)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            // Si la pieza pertenece a algun pedido
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.PiezaExistenteFK == idPieza)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene pedidos asociados.");
                    break;
                }
            }

            // Si la pieza pertenece a algun conjunto
            Pieza p = ElegirPieza(idEmpresa, idPieza);
            if (p.ConjuntoFK != null)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Pertenece a algun conjunto.");
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- No tiene ningun documento asociado -");
            }

            return respuesta;
        }
        public void BorrarPieza(int idEmpresa, int idPiezaBorrar)
        {
            if (SePuedeBorrarPieza(idEmpresa, idPiezaBorrar).SePuede == true)
            {
                Pieza p = ElegirPieza(idEmpresa, idPiezaBorrar);
                if (p.Archivos != null && p.Archivos.Any())
                {
                    foreach (string arch in p.Archivos)
                    {
                        BorrarArchivo(idEmpresa, "Pieza", idPiezaBorrar, arch);
                    }
                }

                List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);

                if (piezas != null && piezas.Any())
                {
                    int eliminados = piezas.RemoveAll(p => p.IdPieza == idPiezaBorrar);

                    if (eliminados > 0)
                    {
                        string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                        File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void EliminarArchivoPieza(int idEmpresa, int idPieza, string rutaArchivo)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            p.Archivos.Remove(rutaArchivo);

            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void Agregar_ModificarCodigo(int idEmpresa, int idPieza, string nuevoCodigo)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            foreach (Pieza p in piezas)
            {
                if (p.IdPieza == idPieza)
                {
                    p.Codigo = nuevoCodigo;

                    string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void BorrarCodigo(int idEmpresa, int idPieza)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            foreach (Pieza p in piezas)
            {
                if (p.IdPieza == idPieza)
                {
                    p.Codigo = null;

                    string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public List<Pieza> PiezasSinConjunto(int idEmpresa)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            List<Pieza> piezasSinConjunto = new List<Pieza>();
            foreach (Pieza p in piezas)
            {
                if (p.ConjuntoFK == null)
                {
                    piezasSinConjunto.Add(p);
                }
            }
            return piezasSinConjunto;
        }

        // ----- CONJUNTOS! -----
        public string ConjuntosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Piezas\" + idEmpresa.ToString() + @" - Conjuntos.json";
        }
        public List<Conjunto> ObtenerConjuntosJson(int idEmpresa)
        {
            List<Conjunto> Conjuntos = new List<Conjunto>();
            string RutaJsonConjuntos = ConjuntosRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonConjuntos))
            {
                if (System.IO.File.ReadAllText(RutaJsonConjuntos) != "" || System.IO.File.ReadAllText(RutaJsonConjuntos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonConjuntos);
                    Conjuntos = JsonConvert.DeserializeObject<List<Conjunto>>(jsonRecuperado);
                }
            }
            return Conjuntos;
        }
        public int PrimerIdConjuntoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerConjuntosJson(idEmpresa).Select(c => c.IdConjunto).OrderBy(id => id).ToList();

            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public async Task<int> NuevoConjunto(int idEmpresa, string descripcion, string? codigo, List<string>? notas, List<string>? archivos)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            Conjunto nuevoConjunto = new Conjunto();

            int idNuevoConjunto = PrimerIdConjuntoFaltante(idEmpresa);

            nuevoConjunto.IdConjunto = idNuevoConjunto;
            nuevoConjunto.Descripcion = descripcion;
            if (!string.IsNullOrEmpty(codigo))
            {
                nuevoConjunto.Codigo = codigo;
            }
            else
            {
                nuevoConjunto.Codigo = null;
            }

            nuevoConjunto.Notas = new List<string>();
            if (notas != null && notas.Any())
            {
                nuevoConjunto.Notas = notas;
            }

            if (archivos != null && archivos.Any())
            {
                nuevoConjunto.Archivos = archivos;
            }
            else
            {
                nuevoConjunto.Archivos = null;
            }

            conjuntos.Add(nuevoConjunto);

            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);

            return idNuevoConjunto;
        }
        public void AsignarPiezasExistentesAConjunto(int idEmpresa, List<int>? piezasYaExistentes, int idConjunto)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            if (piezasYaExistentes != null && piezasYaExistentes.Any())
            {
                foreach (Pieza p in piezas)
                {
                    foreach (int i in piezasYaExistentes)
                    {
                        if (p.IdPieza == i)
                        {
                            p.ConjuntoFK = idConjunto;
                        }
                    }
                }
                string jsonActualizadoP = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizadoP);
            }
        }
        public Conjunto ElegirConjunto(int idEmpresa, int idConjuntoBuscado)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            Conjunto conjunto = new Conjunto() { IdConjunto = -1 };
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjuntoBuscado)
                {
                    conjunto = c;
                }
            }
            return conjunto;
        }
        public Conjunto UltimoConjunto(int idEmpresa)
        {
            return ObtenerConjuntosJson(idEmpresa)[^1];
        }
        public List<Pieza> PiezasDeConjunto(int idEmpresa, int idConjunto)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            List<Pieza> piezasDeConjunto = new List<Pieza>();
            foreach (Pieza p in piezas)
            {
                if (p.ConjuntoFK == idConjunto)
                {
                    piezasDeConjunto.Add(p);
                }
            }
            return piezasDeConjunto;
        }
        public void AsignarPiezaAConjunto(int idEmpresa, int idPieza, int idConjunto)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            foreach (Pieza p in piezas)
            {
                if (p.IdPieza == idPieza)
                {
                    if (p.ConjuntoFK == null)
                    {
                        p.ConjuntoFK = idConjunto;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void AgregarPiezasAConjunto(int idEmpresa, int idConjunto, List<int>? idsPiezas)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            if (idsPiezas != null && idsPiezas.Any())
            {
                foreach (int i in idsPiezas)
                {
                    foreach (Pieza p in piezas)
                    {
                        if (p.IdPieza == i)
                        {
                            p.ConjuntoFK = idConjunto;
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void QuitarPiezaDeConjunto(int idEmpresa, int idConjunto, int idPiezaQuitar)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            foreach (Pieza p in piezas)
            {
                if (p.IdPieza == idPiezaQuitar && p.ConjuntoFK == idConjunto)
                {
                    p.ConjuntoFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void ModificarDescripcionConjunto(int idEmpresa, int idConjunto, string nuevaDescripcion)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto)
                {
                    c.Descripcion = nuevaDescripcion;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ModificarCodigoConjunto(int idEmpresa, int idConjunto, string nuevoCodigo)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto)
                {
                    c.Codigo = nuevoCodigo;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
        }
        public void QuitarCodigoConjunto(int idEmpresa, int idConjunto)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto)
                {
                    c.Codigo = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
        }
        public void AgregarNotaConjunto(int idEmpresa, int idConjunto, string? nuevaNota)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto && nuevaNota != null)
                {
                    if (c.Notas != null && c.Notas.Any())
                    {
                        c.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        c.Notas = new List<string>() { nuevaNota };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                    File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNotaConjunto(int idEmpresa, int idConjunto, string notaAModificar, string notaModificada)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto)
                {
                    if (c.Notas != null && c.Notas.Any())
                    {
                        int indiceNota = c.Notas.IndexOf(notaAModificar);

                        if (indiceNota != -1)
                        {
                            c.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
                        }
                    } 
                }
            }
        }
        public void BorrarNotaConjunto(int idEmpresa, int idConjunto, string notaBorrar)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjunto)
                {
                    if (c.Notas != null && c.Notas.Any())
                    {
                        int indiceNota = c.Notas.IndexOf(notaBorrar);

                        if (indiceNota != -1)
                        {
                            c.Notas.Remove(notaBorrar);

                            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarConjunto(int idEmpresa, int idConjunto)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            bool sePuede = true;
            // COMPROBAR SI CONJUNTO TIENE PIEZAS
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            foreach (Pieza pi in piezas)
            {
                if (pi.ConjuntoFK == idConjunto)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene piezas asociadas.");
                    break;
                }
            }
            // COMPROBAR SI CONJUNTO FUE PEDIDO
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.ConjuntoExistenteFK == idConjunto)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Esta asociado a algun/os pedidos.");
                    break;
                }
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- No tiene documentos asociados -.");
            }

            return respuesta;
        }
        public void BorrarConjunto(int idEmpresa, int idConjuntoBorrar)
        {
            if (SePuedeBorrarConjunto(idEmpresa, idConjuntoBorrar).SePuede == true)
            {
                List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
                Conjunto c = ElegirConjunto(idEmpresa, idConjuntoBorrar);

                conjuntos.RemoveAll(c => c.IdConjunto == idConjuntoBorrar);

                string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);

                // DESVINCULAR PIEZAS DE CONJUNTO / INECESARIO YA QUE NO SE PODRA BORRAR CONJUNTO CON PIEZAS ASOCIADAS
                List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
                foreach (Pieza p in piezas)
                {
                    if (p.ConjuntoFK == idConjuntoBorrar)
                    {
                        p.ConjuntoFK = null;
                    }
                }
                string jsonActualizado2 = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado2);
            }  
        }

        // ----- PEDIDOS! -----
        public string PedidosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Pedidos.json";
        }
        public string PedidosRutaJson_2(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Pedidos\" + idEmpresa.ToString() + @" - Pedidos.json";
        }
        public List<Pedido> ObtenerPedidosJson(int idEmpresa)
        {
            List<Pedido> Pedidos = new List<Pedido>();
            string RutaJsonPedidos = PedidosRutaJson_2(idEmpresa);

            if (System.IO.File.Exists(RutaJsonPedidos))
            {
                if (System.IO.File.ReadAllText(RutaJsonPedidos) != "" || System.IO.File.ReadAllText(RutaJsonPedidos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonPedidos);
                    Pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonRecuperado);
                }
            }
            return Pedidos;
        }
        public List<Pedido> PedidosOrdenadosPorId(int idEmpresa)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            return pedidos.OrderBy(p => p.IdPedido).ToList();
        }
        public Pedido ElegirPedido(int idEmpresa, int idPedidoBuscado)
        {
            Pedido pedido = new Pedido();
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedidoBuscado)
                {
                    pedido = p;
                }
            }
            return pedido;
        }
        public int PrimerIdPedidoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerPedidosJson(idEmpresa).Select(p => p.IdPedido).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void GuardarNuevoPedido(string descripcion, int cantidad, double precioUnitario, DateOnly fechaEncargo, EstadoPedido estado, List<string> notas, int idEmpresa, int? encargadoPor, int? piezaExistente, int? conjuntoExistente)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = new Pedido();

            p.IdPedido = PrimerIdPedidoFaltante(idEmpresa);
            p.Descripcion = descripcion;

            p.Cantidad = cantidad;
            p.PrecioUnitario = precioUnitario;
            if (fechaEncargo != DateOnly.MinValue)
            {
                p.FechaEncargo = fechaEncargo;
            }
            else
            {
                p.FechaEncargo = DateOnly.FromDateTime(DateTime.Now);
            }

            p.PiezaExistenteFK = piezaExistente;
            p.ConjuntoExistenteFK = conjuntoExistente;
            p.Estado = estado;
            p.Entregas = new List<Entrega>();
            p.Notas = new List<string>();

            if (notas != null && notas.Any())
            {
                p.Notas = notas;
            }

            p.EncargadoPorFK = encargadoPor;

            pedidos.Add(p);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public Pedido BuscarPedido(int idEmpresa, int idPedidoBuscado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido pedido = new Pedido() { IdPedido = -1};

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedidoBuscado)
                {
                    pedido = p;
                }
            }
            return pedido;
        }
        public void PedidoModificarFechaEncargo(int idEmpresa, int idPedido, DateOnly nuevaFecha)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.FechaEncargo = nuevaFecha;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarCantidad(int idEmpresa, int idPedido, int nuevaCantidad)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Cantidad = nuevaCantidad;

                    int cantEntregagasALaFecha = 0;
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            cantEntregagasALaFecha += e.cantidadEntrega;
                        }
                    }
                    if (cantEntregagasALaFecha == nuevaCantidad)
                    {
                        p.Estado = EstadoPedido.Entregado;
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarDescripcion(int idEmpresa, int idPedido, string nuevaDescripcion)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Descripcion = nuevaDescripcion;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public string DescripcionDePedido(int idEmpresa, int idPedido)
        {
            string descripcion = "- Sin Descripcion -";
            Pedido pedido = ElegirPedido(idEmpresa, idPedido);
            if (pedido.PiezaExistenteFK != null)
            {
                List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
                foreach (Pieza p in piezas)
                {
                    if (p.IdPieza == pedido.PiezaExistenteFK && p.Descripcion != null)
                    {
                        descripcion = p.Descripcion;
                    }
                }
            }
            else if (pedido.ConjuntoExistenteFK != null)
            {
                List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
                foreach (Conjunto c in conjuntos)
                {
                    if (c.IdConjunto == pedido.ConjuntoExistenteFK && c.Descripcion != null)
                    {
                        descripcion = c.Descripcion;
                    }
                }
            }
            else if(pedido.Descripcion != null)
            {
                descripcion = pedido.Descripcion;
            }

            return descripcion;
        }
        public void PedidoModificarPrecioUnitario(int idEmpresa, int idPedido, double nuevoPrecio)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.PrecioUnitario = nuevoPrecio;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoCancelar(int idEmpresa, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Estado = EstadoPedido.Cancelado;

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoReactivar(int idEmpresa, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    if (p.Entregas == null || !p.Entregas.Any())
                    {
                        p.Estado = EstadoPedido.Pendiente;
                    }
                    else
                    {
                        int cantidadEntregadas = 0;
                        foreach (Entrega e in p.Entregas)
                        {
                            cantidadEntregadas += e.cantidadEntrega;
                        }

                        if (cantidadEntregadas == p.Cantidad)
                        {
                            p.Estado = EstadoPedido.Entregado;
                        }
                        else
                        {
                            p.Estado = EstadoPedido.EntregadoParcial;
                        }
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarEncargado(int idEmpresa, int idPedido, int? idEmpleado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.EncargadoPorFK = idEmpleado;

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public List<Pedido> FiltarPedidosPorPresupuesto(int idPresupuesto, int idEmpresa)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosFiltrados = new List<Pedido>();
            foreach (Pedido p in pedidos)
            {
                if (p.PresupuestoFK == idPresupuesto)
                {
                    pedidosFiltrados.Add(p);
                }
            }
            return pedidosFiltrados;
        }
        public List<Pedido> FiltarPedidosSinPresupuesto(int idEmpresa)
        {
            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> filtrados = new List<Pedido>();

            foreach (Pedido p in todos)
            {
                if (p.PresupuestoFK == null)
                {
                    filtrados.Add(p);
                }
            }
            return filtrados;
        }
        public Presupuesto PresupuestoDePedido(int idEmpresa, Pedido pedido)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            Presupuesto estePresupuesto = new Presupuesto() { IdPresupuesto = -1};
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == pedido.PresupuestoFK)
                {
                    estePresupuesto = p;
                }
            }
            return estePresupuesto;
        }
        public List<Pedido> PedidosSinRemito(int idEmpresa)
        {
            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> sinRemito = new List<Pedido>();
            foreach (Pedido p in todos)
            {
                if (p.Entregas == null && !p.Entregas.Any())
                {
                    sinRemito.Add(p);
                }
            }
            return sinRemito;
        }
        public RespuestaBorrar SePuedeBorrarPedido(int idEmpresa, int idPedidoBorrar)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            Pedido p = ElegirPedido(idEmpresa, idPedidoBorrar);
            // Si fue semi entregado
            if (p.Estado == EstadoPedido.EntregadoParcial)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Fue entregado parcialmente.");
            }
            // Si fue entregado por completo
            if (p.Estado == EstadoPedido.Entregado)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Fue entregado totalmente.");
            }
            // Si esta remitido
            if (p.Entregas != null && p.Entregas.Any())
            {
                int cantEntregas = p.Entregas.Count();
                int cantRemitidas = 0;

                foreach (Entrega e in p.Entregas)
                {
                    if (e.idRemitoFK != null)
                    {
                        cantRemitidas++;
                        respuesta.SePuede = false;
                    }
                }
                if (cantEntregas == cantRemitidas)
                {
                    respuesta.Mensajes.Add("Ya esta remitido.");
                }
                else if(cantEntregas > cantRemitidas && cantRemitidas != 0)
                {
                    respuesta.Mensajes.Add("Ya esta parcialmente remitido.");
                }
            }
            // Si esta presupuestado
            if (p.PresupuestoFK != null)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Ya esta presupuestado.");
            }
            // Si esta facturado
            if (p.FacturaFK != null)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Ya esta facturado.");
            }
            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- No tiene documentos asociados -");
            }
            return respuesta;
        }
        public void BorrarPedido(int idEmpresa, int idPedidoEliminar)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido pe = pedidos.FirstOrDefault(p => p.IdPedido == idPedidoEliminar);

            if (pe.Archivos != null && pe.Archivos.Any())
            {
                foreach (string archivo in pe.Archivos)
                {
                    BorrarArchivo(idEmpresa, "Pedido", idPedidoEliminar, archivo);
                }
            }

            pedidos.Remove(pe);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void AgregarNotaPedido(int idEmpresa, string notaNueva, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (pedidos != null && pedidos.Any() && !string.IsNullOrEmpty(notaNueva))
            {
                foreach (Pedido p in pedidos)
                {
                    if (p.IdPedido == idPedido)
                    {
                        if (p.Notas == null)
                        {
                            p.Notas = new List<string>();
                            p.Notas.Add(notaNueva);
                        }
                        else
                        {
                            p.Notas.Add(notaNueva);
                        }
                        string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                        File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void ModificarNotaPedido(int idEmpresa, int idPedido, string notaVieja, string notaModificada)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (notaModificada != null)
            {
                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.IdPedido == idPedido)
                    {
                        int indiceNota = pedido.Notas.IndexOf(notaVieja);

                        if (indiceNota != -1)
                        {
                            pedido.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                        }
                    }
                }
            } 
        }
        public void BorrarNotaPedido(int idEmpresa, int idPedido, string notaBorrar)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (pedidos != null && pedidos.Any())
            {
                foreach (Pedido p in pedidos)
                {
                    if (p.IdPedido == idPedido)
                    {
                        if (p.Notas != null && p.Notas.Any())
                        {
                            int indiceNota = p.Notas.IndexOf(notaBorrar);

                            if (indiceNota != -1)
                            {
                                p.Notas.Remove(notaBorrar);
                                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                            }
                        } 
                    }
                }
            }
        }
        public List<Pedido> PedidosDeEmpleado(int idEmpresa, int? idEmpleado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> encargados = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.EncargadoPorFK == idEmpleado)
                {
                    encargados.Add(p);
                }
            }
            return encargados;
        }
        public List<Pedido> PedidosPorEstado(List<Pedido> pedidos, EstadoPedido estado)
        {
            List<Pedido> pedidosEstado = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.Estado == estado)
                {
                    pedidosEstado.Add(p);
                }
            }
            return pedidosEstado;
        }
        public void AgregarArchivoPedido(int idEmpresa, int idPedido, string rutaArchivo)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);

            if (p != null)
            {
                if (p.Archivos == null || !p.Archivos.Any())
                {
                    p.Archivos = new List<string>();
                    p.Archivos.Add(rutaArchivo);
                }
                else
                {
                    p.Archivos.Add(rutaArchivo);
                }
                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
            }
        }
        public void EliminarArchivoPedido(int idEmpresa, int idPedido, string rutaArchivo)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);

            p.Archivos.Remove(rutaArchivo);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public Pieza PiezaDePedido(int idEmpresa, Pedido pedido)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza pieza = new Pieza() { IdPieza = -1 };
            foreach (Pieza p in piezas)
            {
                if (p.IdPieza == pedido.PiezaExistenteFK)
                {
                    pieza = p;
                }
            }
            return pieza;
        }
        public Conjunto ConjuntoDePedido(int idEmpresa, Pedido pedido)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            Conjunto conjunto = new Conjunto() { IdConjunto = -1 };
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == pedido.ConjuntoExistenteFK)
                {
                    conjunto = c;
                }
            }
            return conjunto;
        }
        

        // ----- ENTREGAS! -----
        public void NuevaEntregaPedido(int idEmpresa, int idPedido, DateOnly fechaEntrega, int cantidasEntrega, int? idRemito)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    Entrega e = new Entrega();

                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        e.idEntrega = PrimerIdEntregaFaltante(idEmpresa, p);
                    }
                    else
                    {
                        e.idEntrega = 0;
                    }

                    if (fechaEntrega != DateOnly.MinValue)
                    {
                        e.fechaEntrega = fechaEntrega;
                    }
                    else
                    {
                        e.fechaEntrega = DateOnly.FromDateTime(DateTime.Now);
                    }

                    e.cantidadEntrega = cantidasEntrega;

                    if (idRemito != null)
                    {
                        e.idRemitoFK = idRemito;
                    }
                    else
                    {
                        e.idRemitoFK = null;
                    }

                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        p.Entregas.Add(e);
                    }
                    else
                    {
                        p.Entregas = new List<Entrega>() { e };
                    }

                    int cantidadActual = 0;
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega entrega in p.Entregas)
                        {
                            cantidadActual += entrega.cantidadEntrega;
                        }
                    }

                    if (cantidadActual == p.Cantidad)
                    {
                        p.Estado = EstadoPedido.Entregado;
                    }
                    else
                    {
                        p.Estado = EstadoPedido.EntregadoParcial;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public int PrimerIdEntregaFaltante(int idEmpresa, Pedido pedido)
        {
            List<int> ids = pedido.Entregas.Select(e => e.idEntrega).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void AsignarRemitoAEntrega(int idEmpresa, int idPedido, int idRemito, int idEntrega)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            if (e.idEntrega == idEntrega)
                            {
                                e.idRemitoFK = idRemito;
                            }
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void BorrarEntrega(int idEmpresa, int idEntrega, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.IdPedido == idPedido)
                {
                    if (pe.Entregas != null && pe.Entregas.Any())
                    {
                        int indiceEntrega = -1;
                        foreach (Entrega e in pe.Entregas)
                        {
                            if (e.idEntrega == idEntrega)
                            {
                                indiceEntrega = e.idEntrega;
                            }
                        }

                        if (indiceEntrega != -1)
                        {
                            pe.Entregas.RemoveAll(e => e.idEntrega == indiceEntrega);

                            int cant = 0;
                            foreach (Entrega e in pe.Entregas)
                            {
                                cant += e.cantidadEntrega;
                            }

                            if (cant == 0)
                            {
                                pe.Estado = EstadoPedido.Pendiente;
                            }
                            else if(cant > 0 && cant < pe.Cantidad)
                            {
                                pe.Estado = EstadoPedido.EntregadoParcial;
                            }

                            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }

        // ----- COSTOS! -----
        public string CostosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Costos\" + idEmpresa.ToString() + @" - Costos.json";
        }
        public List<Costo> ObtenerCostosJson(int idEmpresa)
        {
            List<Costo> Costos = new List<Costo>();
            string RutaJsonCostos = CostosRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonCostos))
            {
                if (System.IO.File.ReadAllText(RutaJsonCostos) != "" || System.IO.File.ReadAllText(RutaJsonCostos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonCostos);
                    Costos = JsonConvert.DeserializeObject<List<Costo>>(jsonRecuperado);
                }
            }
            return Costos;
        }
        public List<Costo> CostosDePedido(int idEmpresa, int idPedido)
        {
            List<Costo> costos = ObtenerCostosJson(idEmpresa);
            List<Costo> costosDePedido = new List<Costo>();
            foreach (Costo c in costos)
            {
                if (c.PedidoFK == idPedido)
                {
                    costosDePedido.Add(c);
                }
            }
            return costosDePedido;
        }
        public int PrimerIdCostoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerCostosJson(idEmpresa).Select(c => c.IdCosto).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void NuevoCosto(int idEmpresa, string descripcion, double? precio, List<string>? notas, int idPedido, string? archivo)
        {
            List<Costo> costos = ObtenerCostosJson(idEmpresa);
            Costo c = new Costo();

            c.IdCosto = PrimerIdCostoFaltante(idEmpresa);
            c.Descripcion = descripcion;
            c.Precio = precio;
            c.Notas = new List<string>();

            if (notas != null)
            {
                c.Notas = notas;
            }

            c.PedidoFK = idPedido;

            if (archivo != null)
            {
                c.Archivo = archivo;
            }
            else
            {
                c.Archivo = null;
            }

            costos.Add(c);

            string jsonActualizado = JsonConvert.SerializeObject(costos, Formatting.Indented);
            File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizado);
        }
        public Costo BuscarCosto(int idEmpresa, int idCostoBuscado)
        {
            List<Costo> costos = ObtenerCostosJson(idEmpresa);
            Costo costo = new Costo() { IdCosto = -1 };

            foreach (Costo c in costos)
            {
                if (c.IdCosto == idCostoBuscado)
                {
                    costo = c;
                }
            }
            return costo;
        }
        public void ModificarDescripcionCosto(int idEmpresa, int idCosto, string? nuevaDescripcion)
        {
            if (nuevaDescripcion != null)
            {
                List<Costo> costos = ObtenerCostosJson(idEmpresa);
                foreach (Costo c in costos)
                {
                    if (c.IdCosto == idCosto)
                    {
                        c.Descripcion = nuevaDescripcion;
                    }
                }
                string jsonActualizado = JsonConvert.SerializeObject(costos, Formatting.Indented);
                File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizado);
            }   
        }
        public void ModificarPrecioCosto(int idEmpresa, int idCosto, double? nuevoPrecio)
        {
            if (nuevoPrecio != null)
            {
                List<Costo> costos = ObtenerCostosJson(idEmpresa);
                foreach (Costo c in costos)
                {
                    if (c.IdCosto == idCosto)
                    {
                        c.Precio = nuevoPrecio;
                    }
                }
                string jsonActualizado = JsonConvert.SerializeObject(costos, Formatting.Indented);
                File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizado);
            }
        }
        public void BorrarCosto(int idEmpresa, int idCostoBorrar)
        {
            List<Costo> costos = ObtenerCostosJson(idEmpresa);

            if (costos != null && costos.Any())
            {
                int eliminados = costos.RemoveAll(c => c.IdCosto == idCostoBorrar);

                if (eliminados > 0)
                {
                    string jsonActualizado = JsonConvert.SerializeObject(costos, Formatting.Indented);
                    File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }

        // ----- REMITOS! -----
        public string RemitosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Remitos\"+ idEmpresa.ToString() + @" - Remitos.json";
        }
        public List<Remito> ObtenerRemitosJson(int idEmpresa)
        {
            List<Remito> Remitos = new List<Remito>();
            string RutaJsonPedidos = RemitosRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonPedidos))
            {
                if (System.IO.File.ReadAllText(RutaJsonPedidos) != "" || System.IO.File.ReadAllText(RutaJsonPedidos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonPedidos);
                    Remitos = JsonConvert.DeserializeObject<List<Remito>>(jsonRecuperado);
                }
            }
            return Remitos;
        }
        public Remito ElegirRemito(int idEmpresa, int idRemito){
            List<Remito> todos = ObtenerRemitosJson(idEmpresa);
            Remito remitoBuscado = new Remito();

            foreach (Remito r in todos)
            {
                if (r.IdRemito == idRemito)
                {
                    remitoBuscado = r;
                }
            }
            return remitoBuscado;
        }
        public int UltimoIdRemito(int idEmpresa)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            int ultimoId = 0;
            foreach (Remito r in remitos)
            {
                ultimoId++;
            }
            return ultimoId;
        }
        public int PrimerIdRemitoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerRemitosJson(idEmpresa).Select(r => r.IdRemito).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public List<Pedido> PedidosDeRemito(int idEmpresa, int idRemito)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosDeRemito = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.Entregas != null && p.Entregas.Any())
                {
                    foreach (Entrega e in p.Entregas)
                    {
                        if (e.idRemitoFK == idRemito)
                        {
                            if (!pedidosDeRemito.Contains(p))
                            {
                                pedidosDeRemito.Add(p);
                            }  
                        }
                    }   
                }
            }
            return pedidosDeRemito;
        }
        public List<Pedido> PedidosConEntregasPendientesDeRemitir(int idEmpresa)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosEntregasPendienteRemito = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.Entregas != null && p.Entregas.Any())
                {
                    foreach (Entrega e in p.Entregas)
                    {
                        if (e.idRemitoFK == null)
                        {
                            if (!pedidosEntregasPendienteRemito.Contains(p))
                            {
                                pedidosEntregasPendienteRemito.Add(p);
                            }
                        }
                    }
                }
            }
            return pedidosEntregasPendienteRemito;
        }
        public int SiguienteNumeroRemito(List<Remito> remitos)
        {
            return remitos.Any() ? remitos.Max(x => x.NumeroRemito) + 1 : 1;
        }
        public bool NumeroRemitoExiste(int idEmpresa, int numeroPrueba)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            bool existe = false;
            foreach (Remito r in remitos)
            {
                if (r.NumeroRemito == numeroPrueba)
                {
                    existe = true;
                }
            }
            return existe;
        }
        public bool GuardarNuevoRemito(int idEmpresa, int numeroRemito, DateOnly fecha, List<string> notas, int? EntregadoA)
        {
            bool guardadoConExito = false;
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            Remito r = new Remito();

            if (NumeroRemitoExiste(idEmpresa, numeroRemito) == false)
            {
                r.IdRemito = PrimerIdRemitoFaltante(idEmpresa);
                r.NumeroRemito = numeroRemito;

                if (fecha != DateOnly.MinValue)
                {
                    r.FechaEntrega = fecha;
                }
                else
                {
                    r.FechaEntrega = DateOnly.FromDateTime(DateTime.Now);
                }

                r.Notas = new List<string>();
                if (notas != null && notas.Any())
                {
                    r.Notas = notas;
                }
                if (EntregadoA != null)
                {
                    r.EntregadoA = EntregadoA;
                }
                else
                {
                    r.EntregadoA = null;
                }

                remitos.Add(r);

                string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);

                guardadoConExito = true;
            }
            else
            {
                guardadoConExito = false;
            }
            return guardadoConExito;
        }
        public void AgregarArchivoRemito(int idEmpresa, int idRemito, string rutaArchivo)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    r.UbicacionArchivo = rutaArchivo;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
            File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
        }
        public void AgregarItemsARemito(int idEmpresa, int idRemito, List<PedidoEntregas> items)
        {
            Remito r = ElegirRemito(idEmpresa, idRemito);
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                foreach (PedidoEntregas item in items)
                {
                    if (p.IdPedido == item.IdPedido)
                    {
                        if (p.Entregas != null && p.Entregas.Any())
                        {
                            foreach (Entrega e in p.Entregas)
                            {
                                if (item.Entregas != null && item.Entregas.Any())
                                {
                                    foreach (Entrega ee in item.Entregas)
                                    {
                                        if (e.idEntrega == ee.idEntrega)
                                        {
                                            e.idRemitoFK = idRemito;
                                        }
                                    }
                                } 
                            }
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void QuitarPedidoDeRemito(int idEmpresa, int idPedidoBorrar, int idRemito)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedidoBorrar)
                {
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            if (e.idRemitoFK == idRemito)
                            {
                                e.idRemitoFK = null;
                            }
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void QuitarEntregaDeRemito(int idEmpresa, int idPedido, int idEntregaQuitar)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            if (e.idEntrega == idEntregaQuitar)
                            {
                                e.idRemitoFK = null;
                            }
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void AgregarNotaRemito(int idEmpresa, int idRemito, string nuevaNota)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    if (r.Notas != null && r.Notas.Any())
                    {
                        r.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        r.Notas = new List<string>() { nuevaNota };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNotaRemito(int idEmpresa, int idRemito, string notaVieja, string notaModificada)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    int indiceNota = r.Notas.IndexOf(notaVieja);

                    if (indiceNota != -1)
                    {
                        r.Notas[indiceNota] = notaModificada;

                        string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                        File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void EliminarNotaRemito(int idEmpresa, int idRemito, string notaBorrar)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    int indiceNota = r.Notas.IndexOf(notaBorrar);

                    if (indiceNota != -1)
                    {
                        r.Notas.Remove(notaBorrar);
                        string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                        File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void CambiarEstadoRemito(int idEmpresa, int idRemito)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    if (r.Estado == EstadoRemito.Borrador)
                    {
                        r.Estado = EstadoRemito.Confirmado;
                    }
                    else
                    {
                        r.Estado = EstadoRemito.Borrador;
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarFechaRemito(int idEmpresa, int idRemito, DateOnly nuevaFecha)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    r.FechaEntrega = nuevaFecha;

                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNumeroRemito(int idEmpresa, int idRemito, int nuevoNumero)
        {
            bool existe = NumeroRemitoExiste(idEmpresa, nuevoNumero);
            if (existe == false)
            {
                List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
                foreach (Remito r in remitos)
                {
                    if (r.IdRemito == idRemito)
                    {
                        r.NumeroRemito = nuevoNumero;

                        string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                        File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void RemitoEntregadoA(int idEmpresa, int idRemito, int? idEmpleado)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    if (idEmpleado != null)
                    {
                        r.EntregadoA = idEmpleado;
                    }
                    else
                    {
                        r.EntregadoA = null;
                    }
                    
                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void RemitoQuitarEntregado(int idEmpresa, int idRemito)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            foreach (Remito r in remitos)
            {
                if (r.IdRemito == idRemito)
                {
                    r.EntregadoA = null;

                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarRemito(int idEmpresa, int idRemito)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            Remito r = ElegirRemito(idEmpresa, idRemito);

            // Si esta confirmado
            if (r.Estado == EstadoRemito.Confirmado)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Esta confirmado.");
            }

            if (r.EntregadoA != null)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Fue entregado.");
            }

            // Si tiene pedidos asociados
            List<Pedido> pedidosDeRemito = PedidosDeRemito(idEmpresa, idRemito);
            if (pedidosDeRemito != null && pedidosDeRemito.Any())
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene pedidos/entregas asociados.");
            }
            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }

            return respuesta;
        }
        public void BorrarRemito(int idEmpresa, int idRemitoBorrar)
        {
            if (SePuedeBorrarRemito(idEmpresa, idRemitoBorrar).SePuede == true)
            {
                List<Remito> remitos = ObtenerRemitosJson(idEmpresa);

                if (remitos != null && remitos.Any())
                {
                    int eliminados = remitos.RemoveAll(r => r.IdRemito == idRemitoBorrar);

                    if (eliminados > 0)
                    {
                        string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                        File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
                // Esto nunca lo va a hacer, ya que se comrpueba primero si hay entregas remitidas
                List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                foreach (Pedido pe in pedidos)
                {
                    if (pe.Entregas != null && pe.Entregas.Any())
                    {
                        foreach (Entrega e in pe.Entregas)
                        {
                            if (e.idRemitoFK == idRemitoBorrar)
                            {
                                e.idRemitoFK = null;
                            }
                        }
                    }
                }
                string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
            }
        }

        // ----- PRESUPUESTOS! -----
        public string PresupuestosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Presupuestos\" + idEmpresa.ToString() + @" - Presupuestos.json";
        }
        public List<Presupuesto> ObtenerPresupuestosJson(int idEmpresa)
        {
            List<Presupuesto> Presupuestos = new List<Presupuesto>();
            string RutaJsonPresupuestos = PresupuestosRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonPresupuestos))
            {
                if (System.IO.File.ReadAllText(RutaJsonPresupuestos) != "" || System.IO.File.ReadAllText(RutaJsonPresupuestos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonPresupuestos);
                    Presupuestos = JsonConvert.DeserializeObject<List<Presupuesto>>(jsonRecuperado);
                }
            }
            return Presupuestos;
        }
        public Presupuesto BuscarPresupuesto(int idEmpresa, string numeroBuscar)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            Presupuesto encontrado = null;
            foreach (Presupuesto p in presupuestos)
            {
                if (p.Numero.ToString().Equals(numeroBuscar))
                {
                    encontrado = p;
                }
            }
            if (encontrado == null)
            {
                encontrado = new Presupuesto() { IdPresupuesto = -1 };
            }
            return encontrado;
        }
        public int SiguienteNumeroPresupuesto(int idEmpresa)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            return presupuestos.Any() ? presupuestos.Max(x => x.Numero) + 1 : 1;
        }
        public int PrimerNumeroPresupuestoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerPresupuestosJson(idEmpresa).Select(p => p.Numero).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public int PrimerIdPresupuestoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerPresupuestosJson(idEmpresa).Select(p => p.IdPresupuesto).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public int SiguienteIdPresupuesto(List<Presupuesto> presupuestos)
        {
            return presupuestos.Any() ? presupuestos.Max(x => x.IdPresupuesto) + 1 : 0;
        }
        public Presupuesto ElegirPresupuesto(int idEmpresa, int idPresupuestoBuscado)
        {
            List<Presupuesto> todos = ObtenerPresupuestosJson(idEmpresa);
            Presupuesto presupuesto = new Presupuesto();

            foreach (Presupuesto p in todos)
            {
                if (p.IdPresupuesto == idPresupuestoBuscado)
                {
                    presupuesto = p;
                }
            }
            return presupuesto;
        }
        public void GuardarNuevoPresupuesto(int idEmpresa, DateOnly fecha, List<int> idItems, int idPresupuesto, int numeroPresupuesto, List<string>? notas, Socio? socio)
        {
            if (File.Exists(PresupuestosRutaJson(idEmpresa)))
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                Presupuesto pNuevo = new Presupuesto();

                int id = PrimerIdPresupuestoFaltante(idEmpresa);
                pNuevo.IdPresupuesto = id;

                pNuevo.Numero = numeroPresupuesto;

                if (fecha != DateOnly.MinValue)
                {
                    pNuevo.Fecha = fecha;
                }
                else
                {
                    pNuevo.Fecha = DateOnly.FromDateTime(DateTime.Now);
                }

                pNuevo.Notas = new List<string>();
                if (notas != null && notas.Any())
                {
                    pNuevo.Notas = notas;
                }
                pNuevo.Archivos = new List<string>();
                pNuevo.Estado = EstadoPresupuesto.Borrador;

                pNuevo.CopiaPedidos = new List<int>();
                pNuevo.EstadoEnvio = new EstadoEnvio() { enviado = false, envios = new List<Envio>() };

                pNuevo.OrdenDeCompraFK = null;
                pNuevo.FacturaFK = null;

                if (socio != null)
                {
                    pNuevo.SocioFK = socio.IdSocio;
                }
                else
                {
                    pNuevo.SocioFK = null;
                }

                presupuestos.Add(pNuevo);

                string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);

                GuardarItemsEnPresupuesto(idEmpresa, idItems, idPresupuesto);
            }
        }
        public void GuardarItemsEnPresupuesto(int idEmpresa, List<int> idItems, int idPresupuesto)
        {
            if (idItems != null && idItems.Count() != 0)
            {
                List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                foreach (Pedido pedido in pedidos)
                {
                    if (idItems.Contains(pedido.IdPedido))
                    {
                        pedido.PresupuestoFK = idPresupuesto;
                    }
                }
                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
            }
        }
        public void EliminarItemDePresupuesto(int idEmpresa, int idPedido, int idPresupuesto)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.PresupuestoFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void EditarOConfirmar(int idEmpresa, int idPresupuesto, EstadoPresupuesto estado)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    p.Estado = estado;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public void BajaPresupuesto(int idEmpresa, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);

            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    p.Estado = EstadoPresupuesto.Cancelado;
                    p.CopiaPedidos = new List<int>();

                    foreach (Pedido pedido in pedidos)
                    {
                        if (pedido.PresupuestoFK == idPresupuesto)
                        {
                            p.CopiaPedidos.Add(pedido.IdPedido);
                        }
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                }
            }

            foreach (Pedido p in pedidos)
            {
                if (p.PresupuestoFK == idPresupuesto)
                {
                    p.PresupuestoFK = null;
                }
            }
            string jsonActualizado2 = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado2);
        }
        // SOLO SE REHABILITA SI TODOS LOS ITEMS NO FUERON REASIGNADOS
        public void AltaPresupuesto(int idEmpresa, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    int pedidosGuardados = p.CopiaPedidos.Count();
                    int pedidosSinReasignar = 0;
                    List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                    foreach (Pedido pedido in pedidos)
                    {
                        foreach (int item in p.CopiaPedidos)
                        {
                            if (item == pedido.IdPedido && pedido.PresupuestoFK == null)
                            {
                                pedidosSinReasignar++;
                            }
                        }
                    }

                    if (pedidosGuardados == pedidosSinReasignar)
                    { 
                        p.Estado = EstadoPresupuesto.Borrador;
                        foreach (Pedido pedido in pedidos)
                        {
                            foreach (int item in p.CopiaPedidos)
                            {
                                if (item == pedido.IdPedido)
                                {
                                    pedido.PresupuestoFK = idPresupuesto;
                                }
                            }
                        }
                        string jsonActualizado2 = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                        File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado2);

                        p.CopiaPedidos = new List<int>();
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public List<Presupuesto> PresupuestosSinAsignar(int idEmpresa)
        {
            List<Presupuesto> todos = ObtenerPresupuestosJson(idEmpresa);
            List<Presupuesto> sinOC = new List<Presupuesto>();
            foreach (Presupuesto p in todos)
            {
                if (p.OrdenDeCompraFK == null && p.FacturaFK == null)  
                {
                    sinOC.Add(p);
                }
            }
            return sinOC;
        }
        public List<Presupuesto> PresupuestosDeOC(int idEmpresa, int idOC)
        {
            List<Presupuesto> todos = ObtenerPresupuestosJson(idEmpresa);
            List<Presupuesto> presupuestosDeOC = new List<Presupuesto>();

            foreach (Presupuesto p in todos)
            {
                if (p.OrdenDeCompraFK == idOC)
                {
                    presupuestosDeOC.Add(p);
                }
            }
            return presupuestosDeOC;
        }
        public void PresupuestoEnviadoA(int idEmpresa, int idPresupuesto, DateOnly fechaEnvio, List<int> empleados)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    if (p.EstadoEnvio.enviado == false)
                    {
                        p.EstadoEnvio.enviado = true;

                        Envio e = new Envio() { fechaEnvio = fechaEnvio, empleados = empleados };
                        p.EstadoEnvio.envios.Add(e);
                    }
                    else
                    {
                        Envio e = new Envio() { fechaEnvio = fechaEnvio, empleados = empleados };
                        p.EstadoEnvio.envios.Add(e);
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public double? PrecioTotalPresupuesto(int idEmpresa, int idPresupuesto)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            double? total = 0;
            foreach (Pedido p in pedidos)
            {
                if (p.PresupuestoFK == idPresupuesto && p.PrecioUnitario != null)
                {
                    total += (p.Cantidad * p.PrecioUnitario);
                }
            }
            return total;
        }
        public void AgregarNotaPresupuesto(int idEmpresa, int idPresupuesto, string notaNueva)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    if (p.Notas != null && p.Notas.Any())
                    {
                        p.Notas.Add(notaNueva);
                    }
                    else
                    {
                        p.Notas = new List<string>() { notaNueva };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                    break;
                }
            } 
        }
        public void ModificarNotaPresupuesto(int idEmpresa, int idPresupuesto, string notaAModificar, string notaModificada)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    if (p.Notas != null && p.Notas.Any())
                    {
                        int indiceNota = p.Notas.IndexOf(notaAModificar);

                        if (indiceNota != -1)
                        {
                            p.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                    break;
                }
            }
        }
        public void BorrarNotaPresupuesto(int idEmpresa, int idPresupuesto, string notaBorrar)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    if (p.Notas != null && p.Notas.Any())
                    {
                        int indiceNota = p.Notas.IndexOf(notaBorrar);
                        if (indiceNota != -1)
                        {
                            p.Notas.Remove(notaBorrar);

                            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                        } 
                    }
                    break;
                }
            }
        }
        public List<Presupuesto> PresupuestosEnOC(int idEmpresa, int idOC)
        {
            List<Presupuesto> preEnOC = new List<Presupuesto>();
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.OrdenDeCompraFK == idOC)
                {
                    preEnOC.Add(p);
                }
            }
            return preEnOC;
        }
        public bool NumeroPresupuestoExiste(int idEmpresa, int numeroPrueba)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            bool existe = false;
            foreach (Presupuesto p in presupuestos)
            {
                if (p.Numero == numeroPrueba)
                {
                    existe = true;
                }
            }
            return existe;
        }
        public void ModificarNumeroPresupuesto(int idEmpresa, int idPresupuesto, int nuevoNumero)
        {
            if (NumeroPresupuestoExiste(idEmpresa, nuevoNumero) == false)
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                foreach (Presupuesto p in presupuestos)
                {
                    if (p.IdPresupuesto == idPresupuesto)
                    {
                        p.Numero = nuevoNumero;

                        string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                        File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void ModificarFechaPresupuesto(int idEmpresa, int idPresupuesto, DateOnly nuevaFecha)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    if (nuevaFecha != DateOnly.MinValue)
                    {
                        p.Fecha = nuevaFecha;
                    }
                    else
                    {
                        p.Fecha = DateOnly.FromDateTime(DateTime.Now);
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarPresupuesto(int idEmpresa, int idPresupuesto)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            Presupuesto p = ElegirPresupuesto(idEmpresa, idPresupuesto);
            // Si esta confirmado
            if (p.Estado ==  EstadoPresupuesto.Confirmado)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Esta confirmado.");
            }
            // Si tiene pedidos asignados
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.PresupuestoFK == idPresupuesto)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene pedidos asignados.");
                }
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- No tiene documentos asociados -");
            }
            return respuesta;
        }
        public void BorrarPresupuesto(int idEmpresa, int idPresupuestoEliminar)
        {
            if (SePuedeBorrarPresupuesto(idEmpresa, idPresupuestoEliminar).SePuede == true)
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                Presupuesto pr = presupuestos.FirstOrDefault(p => p.IdPresupuesto == idPresupuestoEliminar);

                if (pr.Archivos != null && pr.Archivos.Any())
                {
                    foreach (string archivo in pr.Archivos)
                    {
                        BorrarArchivo(idEmpresa, "Presupuesto", pr.IdPresupuesto, archivo);
                    }
                }

                presupuestos.Remove(pr);

                string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);

                // Esto nunca se hace ya que primero se comprueba si hay pedidos asignados
                List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                foreach (Pedido pe in pedidos)
                {
                    if (pe.PresupuestoFK == idPresupuestoEliminar)
                    {
                        pe.PresupuestoFK = null;
                    }
                }
                string jsonActualizado2 = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado2);
            }  
        }
        public void AsignarSocioAPresupuesto(int idEmpresa, int idPresupuesto, int idSocio)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto pr in presupuestos)
            {
                if (pr.IdPresupuesto == idPresupuesto)
                {
                    pr.SocioFK = idSocio;
                }
            }
            ActualizarJson_Presupuestos(presupuestos, idEmpresa);
        }
        public void QuitarSocioAPresupuesto(int idEmpresa, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto pr in presupuestos)
            {
                if (pr.IdPresupuesto == idPresupuesto)
                {
                    pr.SocioFK = null;
                }
            }
            ActualizarJson_Presupuestos(presupuestos, idEmpresa);
        }

        // ----- ORDENES DE COMPRA! -----
        public string OrdenesDeCompraRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Ordenes de Compra\" + idEmpresa.ToString() + @" - Ordenes de Compra.json";
        }
        public List<OrdenDeCompra> ObtenerOrdenesDeCompraJson(int idEmpresa)
        {
            List<OrdenDeCompra> OCs = new List<OrdenDeCompra>();
            string RutaJsonOCs = OrdenesDeCompraRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonOCs))
            {
                if (System.IO.File.ReadAllText(RutaJsonOCs) != "" || System.IO.File.ReadAllText(RutaJsonOCs) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonOCs);
                    OCs = JsonConvert.DeserializeObject<List<OrdenDeCompra>>(jsonRecuperado);
                }
            }
            return OCs;
        }
        public OrdenDeCompra ElegirOrdenDeCompra(int idEmpresa, int idOrdenDeCompra)
        {
            List<OrdenDeCompra> todas = ObtenerOrdenesDeCompraJson(idEmpresa);
            OrdenDeCompra orderDeCompraBuscada = new OrdenDeCompra();

            foreach (OrdenDeCompra odc in todas)
            {
                if (odc.IdOC == idOrdenDeCompra)
                {
                    orderDeCompraBuscada = odc;
                }
            }
            return orderDeCompraBuscada;
        }
        public int SiguienteIdOC(int idEmpresa)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            return odcs.Count > 0 ? odcs.Max(odc => odc.IdOC) + 1 : 0;
        }
        public int PrimerIdFaltanteOrdenDeCompra(int idEmpresa)
        {
            List<int> ids = ObtenerOrdenesDeCompraJson(idEmpresa).Select(odc => odc.IdOC).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public int GuardarNuevaOC(int idEmpresa, string codigo, DateOnly fecha, Relacion relacionOC, List<string>? notas)
        {
            List<OrdenDeCompra> ocs = ObtenerOrdenesDeCompraJson(idEmpresa);
            OrdenDeCompra nuevaOC = new OrdenDeCompra();

            int idOC = PrimerIdFaltanteOrdenDeCompra(idEmpresa);
            nuevaOC.IdOC = idOC;
            
            nuevaOC.Codigo = codigo;

            if (fecha != DateOnly.MinValue)
            {
                nuevaOC.Fecha = fecha;
            }
            else
            {
                nuevaOC.Fecha = DateOnly.FromDateTime(DateTime.Now);
            }

            if (notas != null && notas.Any())
            {
                nuevaOC.Notas = notas;
            }
            else
            {
                nuevaOC.Notas = new List<string>();
            }

            nuevaOC.Relacion = relacionOC;
            nuevaOC.Estado = EstadoOC.Activa;
            nuevaOC.ItemsRespaldo = new List<int>();

            ocs.Add(nuevaOC);

            string jsonActualizado = JsonConvert.SerializeObject(ocs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);

            return idOC;
        }
        public void AgregarArchivoOC(int idEmpresa, int idOC, string rutaArchivo)
        {
            List<OrdenDeCompra> ocs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra oc in ocs)
            {
                if (oc.IdOC == idOC)
                {
                    oc.UbicacionArchivo = rutaArchivo;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(ocs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
        }     
        public void BorrarOrdenDeCompra(int idEmpresa, int idOcEliminar)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            if (odcs != null && odcs.Any())
            {
                int eliminados = odcs.RemoveAll(oc => oc.IdOC == idOcEliminar);

                if (eliminados > 0)
                {
                    string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                    File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                }
            }

            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.OrdenDeCompraFK == idOcEliminar)
                {
                    p.OrdenDeCompraFK = null;
                }
            }
            string jsonActualizado2 = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado2);
        }
        public void ModificarCodigoOC(int idEmpresa, int idOC, string nuevoCodigo)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOC)
                {
                    odc.Codigo = nuevoCodigo;

                    string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                    File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarFechaOC(int idEmpresa, int idOC, DateOnly nuevaFecha)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOC)
                {
                    odc.Fecha = nuevaFecha;

                    string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                    File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void AgregarNotaOC(int idEmpresa, int idOC, string notaNueva)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOC)
                {
                    if (odc.Notas != null && odc.Notas.Any())
                    {
                        odc.Notas.Add(notaNueva);
                    }
                    else
                    {
                        odc.Notas = new List<string>() { notaNueva };
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                    File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarNotaOC(int idEmpresa, int idOC, string notaAModificar, string notaModificada)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOC)
                {
                    if (odc.Notas != null && odc.Notas.Any())
                    {
                        int indiceNota = odc.Notas.IndexOf(notaAModificar);

                        if (indiceNota != -1)
                        {
                            odc.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public void BorrarNotaOC(int idEmpresa, int idOC, string notaABorrar)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOC)
                {
                    if (odc.Notas != null && odc.Notas.Any())
                    {
                        int indiceNota = odc.Notas.IndexOf(notaABorrar);

                        if (indiceNota != -1)
                        {
                            odc.Notas.Remove(notaABorrar);

                            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public void AsignarPresupuestoAOC(int idEmpresa, int idOc, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    p.OrdenDeCompraFK = idOc;

                    string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        } 
        public void QuitarPresupuestoDeOC(int idEmpresa, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    p.OrdenDeCompraFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public bool OrdenDeCompraFacturada(int idEmpresa, int idOC)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);

            bool facturada = false;
            List<RelacionODCFactura> relacionesOCF = ObtenerRelacionesODCFactura(idEmpresa);
            foreach (RelacionODCFactura rocf in relacionesOCF)
            {
                // DESARROLLAR
            }

            return facturada;
        }
        public void CambiarEstadoOCsinRecepciones(int idEmpresa, int idOc, EstadoOC nuevoEstado)
        {
            // SOLO OCs SIN RECEPCIONES
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra oc in odcs)
            {
                if (oc.IdOC == idOc && oc.Relacion == Relacion.SinRecepcion)
                {
                    if (nuevoEstado == EstadoOC.Anulada)
                    {
                        List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                        foreach (Presupuesto pr in presupuestos)
                        {
                            if (pr.OrdenDeCompraFK == idOc)
                            {
                                if (oc.ItemsRespaldo != null && oc.ItemsRespaldo.Any())
                                {
                                    oc.ItemsRespaldo.Add(pr.IdPresupuesto);
                                }
                                else
                                {
                                    oc.ItemsRespaldo = new List<int>() { pr.IdPresupuesto };
                                }
                                pr.OrdenDeCompraFK = null;
                            }
                        }
                        string jsonActualizado2 = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                        File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado2);
                    }
                    else if(nuevoEstado == EstadoOC.Activa)
                    {
                        if (oc.ItemsRespaldo != null && oc.ItemsRespaldo.Any())
                        {
                            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                            foreach (Presupuesto pr in presupuestos)
                            {
                                foreach (int i in oc.ItemsRespaldo)
                                {
                                    if (pr.IdPresupuesto == i)
                                    {
                                        pr.OrdenDeCompraFK = idOc;
                                    }
                                }
                            }
                            string jsonActualizado2 = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado2);

                            oc.ItemsRespaldo = new List<int>();
                        }
                    }
                    oc.Estado = nuevoEstado;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
        }
        public void CambiarEstadoOCConRecepciones(int idEmpresa, int idOc)
        {
            // SOLO OCs CON RECEPCIONES
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOc)
                {
                    if (odc.Relacion == Relacion.ConRecepcion)
                    {
                        if (RecepcionesEnOC(idEmpresa, idOc).Count() == 0)
                        {
                            odc.Estado = EstadoOC.Anulada;
                        }
                        else if (TodasRecepcionesEnOCFacturadas(idEmpresa, idOc) == true)
                        {
                            odc.Estado = EstadoOC.Facturada;
                        }
                        else if (TodasRecepcionesEnOCAnuladas(idEmpresa, idOc) == true)
                        {
                            odc.Estado = EstadoOC.Anulada;
                        }
                        else
                        {
                            odc.Estado = EstadoOC.Activa;
                        }

                        string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                        File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
                    }       
                }
            }
        }
        public double? MontoOrdenDeCompra(int idEmpresa, int idOC)
        {
            double? montoOC = 0;

            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Presupuesto pr in presupuestos)
            {
                if (pr.OrdenDeCompraFK == idOC)
                {
                    foreach (Pedido pe in pedidos)
                    {
                        if (pe.PresupuestoFK == pr.IdPresupuesto)
                        {
                            montoOC += (pe.Cantidad * pe.PrecioUnitario);
                        }
                    }
                }
            }
            return montoOC;
        }
        public double? MontoFacturadoDeOCConRecepciones(int idEmpresa, int idOC)
        {
            double? montoFacturado = 0;
            OrdenDeCompra odc = ElegirOrdenDeCompra(idEmpresa, idOC);

            if (odc.Relacion == Relacion.ConRecepcion)
            {
                List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
                foreach (Recepcion r in recepciones)
                {
                    if (r.IdOdCFK == idOC && r.Estado == EstadoRecepcion.Facturada)
                    {
                        montoFacturado += r.Monto;
                    }
                }
            }
            return montoFacturado;
        }
        public RespuestaBorrar SePuedeBorrarOC(int idEmpresa, int idOC)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            OrdenDeCompra odc = ElegirOrdenDeCompra(idEmpresa, idOC);
            // SI LA OC TIENE PRESUPUESTOS ASOCIADOS
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto pr in presupuestos)
            {
                if (pr.OrdenDeCompraFK == idOC)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene presupuestos asociados.");
                    break;
                }
            }
            // SI LA OC ESTA FACTURADA
            if (odc.Estado == EstadoOC.Facturada)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Ya fue facturada.");
            }
            // SI LA OC ESTA ASOCIADA A UNA FACTURA
            /*
            List<RelacionODCFactura> relaciones = ObtenerRelacionesODCFactura(idEmpresa);
            foreach (RelacionODCFactura rocf in relaciones)
            {
                if (rocf.IdOdcFK == idOC)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Ya fue facturada.");
                    break;
                }
            }
            */
            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }
            return respuesta;
        }
        public bool SePuedeReactivar(int idEmpresa, int idOC)
        {
            bool sePuedeReactivar = true;
            OrdenDeCompra odc = ElegirOrdenDeCompra(idEmpresa, idOC);
            if (odc.Estado == EstadoOC.Anulada && odc.ItemsRespaldo != null && odc.ItemsRespaldo.Any())
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                foreach (Presupuesto pr in presupuestos)
                {
                    foreach (int i in odc.ItemsRespaldo)
                    {
                        if (pr.IdPresupuesto == i)
                        {
                            if (pr.OrdenDeCompraFK != null)
                            {
                                sePuedeReactivar = false;
                            }
                        }
                    }
                }
            }
            return sePuedeReactivar;
        }
        public void BorrarOC(int idEmpresa, int idOCBorrar)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            OrdenDeCompra odc = odcs.FirstOrDefault(o => o.IdOC == idOCBorrar);

            if (odc == null) return;

            if (SePuedeBorrarOC(idEmpresa, idOCBorrar).SePuede == true)
            {
                // BORRAR RECEPCIONES ASOCIADAS
                if (odc.Relacion == Relacion.ConRecepcion)
                {
                    List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
                    recepciones.RemoveAll(r => r.IdOdCFK == idOCBorrar);

                    ActualizarJson_Recepciones(recepciones, idEmpresa);
                }

                // QUITAR PRESUPUESTOS ASOCIADOS
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                foreach (Presupuesto p in presupuestos)
                {
                    if (p.OrdenDeCompraFK == idOCBorrar)
                    {
                        p.OrdenDeCompraFK = null;
                    }
                }
                ActualizarJson_Presupuestos(presupuestos, idEmpresa);
                
                odcs.Remove(odc);

                string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
            }
        }

        // ----- RECEPCIONES! -----
        public string RecepcionesRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Ordenes de Compra\" + idEmpresa.ToString() + @" - Recepciones.json";
        }
        public List<Recepcion> ObtenerRecepcionesJson(int idEmpresa)
        {
            List<Recepcion> recepciones = new List<Recepcion>();
            string RutaJsonOCs = RecepcionesRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonOCs))
            {
                if (System.IO.File.ReadAllText(RutaJsonOCs) != "" || System.IO.File.ReadAllText(RutaJsonOCs) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonOCs);
                    recepciones = JsonConvert.DeserializeObject<List<Recepcion>>(jsonRecuperado);
                }
            }
            return recepciones;
        }
        public int PrimerIdRecepcionFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerRecepcionesJson(idEmpresa).Select(r => r.IdRecepcion).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public int IdUltimaRecepcion(int idEmpresa)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            return recepciones.Last().IdRecepcion;
        }
        public void GuardarNuevaRecepcion(int idEmpresa, string numeroRecepcion, int idOC)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            Recepcion r = new Recepcion();

            int idNuevo = PrimerIdRecepcionFaltante(idEmpresa);
            r.IdRecepcion = idNuevo;
            r.NumeroRecepcion = numeroRecepcion;
            r.Estado = EstadoRecepcion.Activa;
            r.IdOdCFK = idOC;
            r.IdFacturaFK = null;
            r.Monto = null;

            recepciones.Add(r);

            string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
            File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);

            CambiarEstadoOCConRecepciones(idEmpresa, idOC);
        }
        public void BorrarRecepcion(int idEmpresa, int idRecepcionBorrar)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            Recepcion recepcionAux = new Recepcion();
            foreach (Recepcion r in recepciones)
            {
                if (r.IdRecepcion == idRecepcionBorrar)
                {
                    recepcionAux = r;
                }
            }

            int eliminados = recepciones.RemoveAll(r => r.IdRecepcion == idRecepcionBorrar);

            if (eliminados > 0)
            {
                string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);
            }

            if (recepcionAux.IdOdCFK != null)
            {
                int id = recepcionAux.IdOdCFK ?? -1;
                if (id > -1)
                {
                    CambiarEstadoOCConRecepciones(idEmpresa, id);
                }
            }
        }
        public void AsignarRecepcionAFactura(int idEmpresa, int idRecepcion, int idFactura, double? monto)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            foreach (Recepcion r in recepciones)
            {
                if (r.IdRecepcion == idRecepcion)
                {
                    r.IdFacturaFK = idFactura;
                    r.Estado = EstadoRecepcion.Facturada;
                    r.Monto = monto;

                    string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                    File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void QuitarRecepcionDeFactura(int idEmpresa, int idRecepcion)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            foreach (Recepcion r in recepciones)
            {
                if (r.IdRecepcion == idRecepcion)
                {
                    r.IdFacturaFK = null;
                    r.Monto = null;
                    r.Estado = EstadoRecepcion.Activa;

                    string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                    File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public Recepcion UltimaRecepcion(int idEmpresa)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            return recepciones.Last();
        }
        public void CambiarEstadoRecepcion(int idEmpresa, int idRecepcion, EstadoRecepcion nuevoEstadoRecepcion)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);

            foreach (Recepcion r in recepciones)
            {
                if (r.IdRecepcion == idRecepcion)
                {
                    r.Estado = nuevoEstadoRecepcion;

                    string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                    File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);

                    if (r.IdOdCFK != null)
                    {
                        int id = r.IdOdCFK ?? -1;
                        if (id > -1)
                        {
                            CambiarEstadoOCConRecepciones(idEmpresa, id);
                        }
                    }  
                }
            } 
        }
        public List<Recepcion> RecepcionesEnOC(int idEmpresa, int idOC)
        {
            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
            List<Recepcion> recepcionesEnOC = new List<Recepcion>();

            foreach (Recepcion r in recepciones)
            {
                if (r.IdOdCFK == idOC)
                {
                    recepcionesEnOC.Add(r);
                }
            }
            return recepcionesEnOC;
        }
        public int CantidadRecepcionesFacturadasEnOC(int idEmpresa, int idOC)
        {
            int facturadas = 0;
            List<Recepcion> recepcionesEnOc = RecepcionesEnOC(idEmpresa, idOC);
            foreach (Recepcion r in recepcionesEnOc)
            {
                if (r.Estado == EstadoRecepcion.Facturada)
                {
                    facturadas++;
                }
            }
            return facturadas;
        }
        public int CantidadRecepcionesAnuladasEnOC(int idEmpresa, int idOC)
        {
            int anuladas = 0;
            List<Recepcion> recepcionesEnOc = RecepcionesEnOC(idEmpresa, idOC);
            foreach (Recepcion r in recepcionesEnOc)
            {
                if (r.Estado == EstadoRecepcion.Anulada)
                {
                    anuladas++;
                }
            }
            return anuladas;
        }
        public int CantidadRecepcionesActivasEnOC(int idEmpresa, int idOC)
        {
            int activas = 0;
            List<Recepcion> recepcionesEnOc = RecepcionesEnOC(idEmpresa, idOC);
            foreach (Recepcion r in recepcionesEnOc)
            {
                if (r.Estado == EstadoRecepcion.Activa)
                {
                    activas++;
                }
            }
            return activas;
        }
        public bool TodasRecepcionesEnOCFacturadas(int idEmpresa, int idOC)
        {
            bool facturadas = false;
            if (RecepcionesEnOC(idEmpresa, idOC).Count() == CantidadRecepcionesFacturadasEnOC(idEmpresa, idOC))
            {
                facturadas = true;
            }
            return facturadas;
        }
        public bool TodasRecepcionesEnOCAnuladas(int idEmpresa, int idOC)
        {
            bool anuladas = false;
            if (RecepcionesEnOC(idEmpresa, idOC).Count() == CantidadRecepcionesAnuladasEnOC(idEmpresa, idOC))
            {
                anuladas = true;
            }
            return anuladas;
        }
        public bool TodasRecepcionesEnOCActivas(int idEmpresa, int idOC)
        {
            bool activas = false;
            if (RecepcionesEnOC(idEmpresa, idOC).Count() == CantidadRecepcionesActivasEnOC(idEmpresa, idOC))
            {
                activas = true;
            }
            return activas;
        }

        // ----- FACTURAS! -----
        public string FacturasRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Facturas\" + idEmpresa.ToString() + @" - Facturas.json";
        }
        public List<Factura> ObtenerFacturasJson(int idEmpresa)
        {
            List<Factura> Facturas = new List<Factura>();
            string RutaJsonFacturas = FacturasRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonFacturas))
            {
                if (System.IO.File.ReadAllText(RutaJsonFacturas) != "" || System.IO.File.ReadAllText(RutaJsonFacturas) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonFacturas);
                    Facturas = JsonConvert.DeserializeObject<List<Factura>>(jsonRecuperado);
                }
            }
            return Facturas;
        }
        public Factura ElegirFactura(int idEmpresa, int idFactura)
        {
            List<Factura> todas = ObtenerFacturasJson(idEmpresa);
            Factura factura = new Factura();

            foreach (Factura f in todas)
            {
                if (f.IdFactura == idFactura)
                {
                    factura = f;
                }
            }
            return factura;
        }
        public int PrimerIdFacturaFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerFacturasJson(idEmpresa).Select(f => f.IdFactura).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void GuardarFactura(int idEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, double? montoManual, Contiene contiene, List<int>? idsPedidos, List<int>? idsPresupuestos, List<OCRecepciones>? OCsRecepciones, string? descripcion)
        {
            bool facturaGuardada = true; // POR SI HUBO ALGUN PROBLEMA

            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura f = new Factura();

            f.IdFactura = idFactura;

            if (fecha != DateOnly.MinValue)
            {
                f.Fecha = fecha;
            }
            else
            {
                f.Fecha = DateOnly.FromDateTime(DateTime.Now);
            }
            f.Codigo = new Codigo()
            {
                Puesto = puesto,
                Numero = numero
            };   
            f.Estado = EstadoFactura.Impaga;

            f.Notas = new List<string>();

            f.ItemsRespaldo = new List<int>();
            f.RecepionesRespaldo = new List<Recepcion>();
            f.NotaDeCredito = null;

            f.Contiene = contiene;
            switch (contiene)
            {
                case Contiene.SinDefinir:
                    if (montoManual != null)
                    {
                        f.MontoManual = montoManual;
                    }
                    else
                    {
                        f.MontoManual = 0;
                    }

                    if (descripcion != null)
                    {
                        f.Descripcion = descripcion;
                    }
                    else
                    {
                        f.Descripcion = null;
                    }
                break;
                case Contiene.Pedidos:
                    AsignarPedidosAFactura(idEmpresa, idFactura, idsPedidos);
                break;
                case Contiene.Presupuestos:
                    AsignarPresupuestosAFactura(idEmpresa, idFactura, idsPresupuestos);
                break;
                case Contiene.OrdenesDeCompra:
                    // DATOS PRUEBA
                    /*
                    List<OCRecepciones> lista = new List<OCRecepciones>()
                    {
                        new OCRecepciones()
                        {
                            idOC = 1,
                            Recepciones = new List<Recepcion>()
                            {
                                new Recepcion()
                                {
                                    IdRecepcion = 0,
                                    Monto = 666
                                },
                                new Recepcion()
                                {
                                    IdRecepcion = 1,
                                    Monto = 79
                                }
                            }
                        },
                        new OCRecepciones()
                        {
                            idOC = 0,
                            Recepciones = null
                        }
                    };
                    */

                    if (OCsRecepciones != null && OCsRecepciones.Any())
                    {   
                        foreach (OCRecepciones ocr in OCsRecepciones)
                        {
                            if (ocr.Recepciones == null || !ocr.Recepciones.Any())
                            {
                                NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);
                                CambiarEstadoOCsinRecepciones(idEmpresa, ocr.idOC, EstadoOC.Facturada);
                            }

                            int recepcionesActivasEnOC = CantidadRecepcionesActivasEnOC(idEmpresa, ocr.idOC);

                            int cantAgregadas = 0;

                            if (ocr.Recepciones != null && ocr.Recepciones.Any())
                            {
                                cantAgregadas = ocr.Recepciones.Count();
                            }

                            double? montoTotalOC = MontoOrdenDeCompra(idEmpresa, ocr.idOC);
                            double? montoYaFacturado = MontoFacturadoDeOCConRecepciones(idEmpresa, ocr.idOC);
                            double? montoRestante = (montoTotalOC - montoYaFacturado);

                            double? montoAuxiliar = 0;

                            if (ocr.Recepciones != null && ocr.Recepciones.Any())
                            {
                                foreach (Recepcion r in ocr.Recepciones)
                                {
                                    montoAuxiliar += r.Monto;
                                }
                            }

                            if (recepcionesActivasEnOC >= cantAgregadas && montoRestante >= montoAuxiliar)
                            {
                                NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);

                                if (ocr.Recepciones != null && ocr.Recepciones.Any())
                                {
                                    foreach (Recepcion r in ocr.Recepciones)
                                    {
                                        AsignarRecepcionAFactura(idEmpresa, r.IdRecepcion, idFactura, r.Monto);
                                    }
                                }

                                CambiarEstadoOCConRecepciones(idEmpresa, ocr.idOC);
                            }
                        }
                    }
                    break;
            }
            
            facturas.Add(f);

            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
        }
        public bool ComprobarSiTodasOCsIngresadasSePuedenGuardar(int idEmpresa, List<OCRecepciones>? OCsRecepciones)
        {
            bool sePuedenGuardar;

            int cantidadOCsIngresadas = OCsRecepciones.Count();
            int cantidadConfirmadas = 0;

            foreach (OCRecepciones ocr in OCsRecepciones)
            {
                if (ocr.Recepciones != null && ocr.Recepciones.Any())
                {
                    double? montoTotalOC = MontoOrdenDeCompra(idEmpresa, ocr.idOC);
                    double? montoYaFacturado = MontoFacturadoDeOCConRecepciones(idEmpresa, ocr.idOC);
                    double? montoRestante = (montoTotalOC - montoYaFacturado);

                    double? montoTotalIngresado = 0;
                    foreach (Recepcion r in ocr.Recepciones)
                    {
                        montoTotalIngresado += r.Monto;
                    }

                    if (CantidadRecepcionesActivasEnOC(idEmpresa, ocr.idOC) == cantidadOCsIngresadas)
                    {
                        // SI SE AGREGARON TODAS LA RECEPCIONES EN UNA OC
                        if (montoRestante == montoTotalIngresado)
                        {
                            cantidadConfirmadas++;
                        }
                    }
                    else if(CantidadRecepcionesActivasEnOC(idEmpresa, ocr.idOC) > cantidadOCsIngresadas)
                    {
                        // SI SE AGREGARON MENOS RECEPCIONES DE LAS TOTALES DE LA OC
                        if (montoRestante > montoTotalIngresado)
                        {
                            cantidadConfirmadas++;
                        }
                    }
                }
                else
                {
                    // SI NO TIENE RECEPCIONES, SE PUEDE GUARDAR SIN COMPROBAR
                    cantidadConfirmadas++;
                }
            }

            if (cantidadOCsIngresadas == cantidadConfirmadas)
            {
                // SI ABSOLUTAMENTE TODAS SE CONFIRMAN, SE PUEDEN GUARDAR EN LA FACTURA
                sePuedenGuardar = true;
            }
            else
            {
                // SI AL MENOS UNA NO SE CONFIRMA, LA CANTIDAD NO COINCIDE Y NO SE PUEDE GUARDAR
                sePuedenGuardar = false;
            }

            return sePuedenGuardar;
        }
        // AQUI
        public void GuardarFactura_2(int idEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, double? montoManual, Contiene contiene, List<int>? idsPedidos, List<int>? idsPresupuestos, List<OCRecepciones>? OCsRecepciones, string? descripcion)
        {
            bool facturaGuardada = true; // POR SI HUBO ALGUN PROBLEMA

            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura f = new Factura();

            f.IdFactura = idFactura;

            if (fecha != DateOnly.MinValue)
            {
                f.Fecha = fecha;
            }
            else
            {
                f.Fecha = DateOnly.FromDateTime(DateTime.Now);
            }
            f.Codigo = new Codigo()
            {
                Puesto = puesto,
                Numero = numero
            };
            f.Estado = EstadoFactura.Impaga;

            f.Notas = new List<string>();

            f.ItemsRespaldo = new List<int>();
            f.RecepionesRespaldo = new List<Recepcion>();
            f.NotaDeCredito = null;

            f.Contiene = contiene;
            switch (contiene)
            {
                case Contiene.SinDefinir:
                    if (montoManual != null)
                    {
                        f.MontoManual = montoManual;
                    }
                    else
                    {
                        f.MontoManual = 0;
                    }

                    if (descripcion != null)
                    {
                        f.Descripcion = descripcion;
                    }
                    else
                    {
                        f.Descripcion = null;
                    }
                    break;
                case Contiene.Pedidos:
                    AsignarPedidosAFactura(idEmpresa, idFactura, idsPedidos);
                    break;
                case Contiene.Presupuestos:
                    AsignarPresupuestosAFactura(idEmpresa, idFactura, idsPresupuestos);
                    break;
                case Contiene.OrdenesDeCompra:
                    // DATOS PRUEBA
                    /*
                    List<OCRecepciones> lista = new List<OCRecepciones>()
                    {
                        new OCRecepciones()
                        {
                            idOC = 1,
                            Recepciones = new List<Recepcion>()
                            {
                                new Recepcion()
                                {
                                    IdRecepcion = 0,
                                    Monto = 666
                                },
                                new Recepcion()
                                {
                                    IdRecepcion = 1,
                                    Monto = 79
                                }
                            }
                        },
                        new OCRecepciones()
                        {
                            idOC = 0,
                            Recepciones = null
                        }
                    };
                    */

                    if (OCsRecepciones != null && OCsRecepciones.Any())
                    {
                        foreach (OCRecepciones ocr in OCsRecepciones)
                        {
                            if (ocr.Recepciones != null && ocr.Recepciones.Any())
                            {
                                NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);

                                foreach (Recepcion r in ocr.Recepciones)
                                {
                                    AsignarRecepcionAFactura(idEmpresa, r.IdRecepcion, idFactura, r.Monto);
                                }

                                CambiarEstadoOCConRecepciones(idEmpresa, ocr.idOC);
                            }
                            else
                            {
                                NuevaRelacionOCDF(idEmpresa, idFactura, ocr.idOC);
                                CambiarEstadoOCsinRecepciones(idEmpresa, ocr.idOC, EstadoOC.Facturada);
                            }
                        }
                    }
                    break;
            }

            facturas.Add(f);

            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
        }





        public void AgregarArchivoAFactura(int idEmpresa, int idFactura, string rutaArchivo)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura f = facturas.FirstOrDefault(x => x.IdFactura == idFactura);

            if (f != null)
            {
                if (f.UbicacionArchivo == null || f.UbicacionArchivo == "")
                {
                    f.UbicacionArchivo = rutaArchivo;
                }
                string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
            }
        }
        public void AsignarPedidosAFactura(int idEmpresa, int idFactura, List<int>? items)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (items != null && items.Any())
            {
                foreach (Pedido p in pedidos)
                {
                    foreach (int item in items)
                    {
                        if (p.IdPedido == item)
                        {
                            p.FacturaFK = idFactura;
                        }
                    }
                }
                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
            }
        }
        public void QuitarPedidoDeFactura(int idEmpresa, int idFactura, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido && p.FacturaFK == idFactura)
                {
                    p.FacturaFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void AsignarPresupuestosAFactura(int idEmpresa, int idFactura, List<int>? items)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (items != null && items.Any())
                {
                    foreach (int item in items)
                    {
                        if (p.IdPresupuesto == item)
                        {
                            p.FacturaFK = idFactura;
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public void QuitarPresupuestoDeFactura(int idEmpresa, int idFactura, int idPresupuesto)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == idPresupuesto)
                {
                    p.FacturaFK = null;    
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public Factura FacturaDePedido(int idEmpresa, Pedido pedido)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura facturaDePedido = new Factura() { IdFactura = -1};

            foreach (Factura f in facturas)
            {
                if (f.IdFactura == pedido.FacturaFK)
                {
                    facturaDePedido = f;
                }
            }
            return facturaDePedido;
        }
        public void FacturaPagada(int idEmpresa, int idFactura)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    if (f.Estado != EstadoFactura.Anulada && f.Estado == EstadoFactura.Impaga)
                    {
                        f.Estado = EstadoFactura.Pagada;

                        string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                        File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                    }
                    break;
                }
            }
        }
        public void FacturaCambiarAImpaga(int idEmpresa, int idFactura)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    if (f.Estado != EstadoFactura.Anulada && f.Estado == EstadoFactura.Pagada)
                    {
                        f.Estado = EstadoFactura.Impaga;

                        string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                        File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                    }
                    break;
                }
            }
        }
        public void AnularFactura(int idEmpresa, int idFacturaAnular)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaAnular && f.Estado != EstadoFactura.Anulada)
                {
                    f.Estado = EstadoFactura.Anulada;

                    switch (f.Contiene)
                    {
                        case Contiene.Pedidos:
                        List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                        foreach (Pedido pe in pedidos)
                        {
                            if (pe.FacturaFK == idFacturaAnular)
                            {
                                pe.FacturaFK = null;

                                if (f.ItemsRespaldo != null && f.ItemsRespaldo.Any())
                                {
                                    if (!f.ItemsRespaldo.Contains(pe.IdPedido))
                                    {
                                        f.ItemsRespaldo.Add(pe.IdPedido);
                                    }
                                }
                                else
                                {
                                    f.ItemsRespaldo = new List<int>() { pe.IdPedido };
                                }
                            }
                        }
                        string jsonActualizadoPe = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                        File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizadoPe);

                            break;
                        case Contiene.Presupuestos:
                            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                            foreach (Presupuesto pr in presupuestos)
                            {
                                if (pr.FacturaFK == idFacturaAnular)
                                {
                                    pr.FacturaFK = null;

                                    if (f.ItemsRespaldo != null && f.ItemsRespaldo.Any())
                                    {
                                        if (!f.ItemsRespaldo.Contains(pr.IdPresupuesto))
                                        {
                                            f.ItemsRespaldo.Add(pr.IdPresupuesto);
                                        }    
                                    }
                                    else
                                    {
                                        f.ItemsRespaldo = new List<int>() { pr.IdPresupuesto };
                                    }
                                }
                            }
                            string jsonActualizadoPr = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizadoPr);

                            break;
                        case Contiene.OrdenesDeCompra:
                            List<RelacionODCFactura> relacionesODCF = ObtenerRelacionesODCFactura(idEmpresa);
                            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);   

                            foreach (RelacionODCFactura rodcf in relacionesODCF)
                            {
                                if (rodcf.IdFacturaFK == idFacturaAnular)
                                {
                                    // CAMBIAR ESTADO DE LA RELACION OC-FACTURA
                                    rodcf.Estado = EstadoRelacionFOC.Anulada;

                                    OrdenDeCompra odc = ElegirOrdenDeCompra(idEmpresa, rodcf.IdOdcFK);
                                    if (odc.Relacion == Relacion.SinRecepcion)
                                    {
                                        CambiarEstadoOCsinRecepciones(idEmpresa, odc.IdOC, EstadoOC.Activa);
                                    }
                                    else
                                    {
                                        CambiarEstadoOCConRecepciones(idEmpresa, odc.IdOC);

                                        foreach (Recepcion r in recepciones)
                                        {
                                            if (r.IdOdCFK == rodcf.IdOdcFK && r.IdFacturaFK == f.IdFactura)
                                            {
                                                Recepcion recepResp = new Recepcion()
                                                {
                                                    IdRecepcion = r.IdRecepcion,
                                                    NumeroRecepcion = r.NumeroRecepcion,
                                                    Estado = r.Estado,
                                                    IdOdCFK = r.IdOdCFK,
                                                    IdFacturaFK = r.IdFacturaFK,
                                                    Monto = r.Monto
                                                };

                                                if (f.RecepionesRespaldo != null && f.RecepionesRespaldo.Any())
                                                {
                                                    f.RecepionesRespaldo.Add(recepResp);
                                                }
                                                else
                                                {
                                                    f.RecepionesRespaldo = new List<Recepcion>() { recepResp };
                                                }

                                                r.Estado = EstadoRecepcion.Activa;
                                                r.IdFacturaFK = null;
                                                r.Monto = null;
                                            }
                                        }
                                    }

                                    // GUARDAR LOS ITESM DE RESPALDO
                                    if (f.ItemsRespaldo != null && f.ItemsRespaldo.Any())
                                    {
                                        if (!f.ItemsRespaldo.Contains(rodcf.IdOdcFK))
                                        {
                                            f.ItemsRespaldo.Add(rodcf.IdOdcFK);
                                        }
                                    }
                                    else
                                    {
                                        f.ItemsRespaldo = new List<int>() { rodcf.IdOdcFK };
                                    }

                                    string jsonActualizadoR = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                                    File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizadoR);

                                    CambiarEstadoOCConRecepciones(idEmpresa, rodcf.IdOdcFK);
                                }
                            }
 
                            string jsonActualizadoRelacionesODCF = JsonConvert.SerializeObject(relacionesODCF, Formatting.Indented);
                            File.WriteAllText(RelacionODCFacturaRutaJson(idEmpresa), jsonActualizadoRelacionesODCF);

                            break;
                        case Contiene.SinDefinir:
                            f.Estado = EstadoFactura.Anulada;
                            // No hace falta hacer nada
                            break;
                    }
                }
            }
            string jsonActualizadoFacturas = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoFacturas);
        }
        public void ReactivarFactura(int idEmpresa, int idFacturaRectivar)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaRectivar)
                {
                    switch (f.Contiene)
                    {
                        case Contiene.Pedidos:
                            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

                            if (f.ItemsRespaldo != null && f.ItemsRespaldo.Any())
                            {
                                int pedidosRespaldo = f.ItemsRespaldo.Count();
                                int pedidosSinReasignar = 0;

                                foreach (Pedido pe in pedidos)
                                {
                                    foreach (int i in f.ItemsRespaldo)
                                    {
                                        if (pe.IdPedido == i)
                                        {
                                            if (pe.FacturaFK == null && pe.PresupuestoFK == null)
                                            {
                                                pedidosSinReasignar++;
                                            }
                                        }
                                    }
                                }
                                if (pedidosRespaldo == pedidosSinReasignar)
                                {
                                    // Permitir reactivar
                                    f.Estado = EstadoFactura.Impaga;

                                    foreach (Pedido pe in pedidos)
                                    {
                                        foreach (int i in f.ItemsRespaldo)
                                        {
                                            if (pe.IdPedido == i)
                                            {
                                                pe.FacturaFK = idFacturaRectivar;
                                            }
                                        }
                                    }

                                    f.ItemsRespaldo = new List<int>();

                                    string jsonActualizadoPe = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizadoPe);
                                }
                            }
                            else
                            {
                                f.Estado = EstadoFactura.Impaga;
                            }
                            break;
                        case Contiene.Presupuestos:
                            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                            List<Pedido> pedidos2 = ObtenerPedidosJson(idEmpresa); 

                            if (f.ItemsRespaldo != null && f.ItemsRespaldo.Any())
                            {
                                int presupuestosRespaldo = f.ItemsRespaldo.Count();
                                int presupuestosSinReasignar = 0;

                                foreach (Presupuesto pr in presupuestos)
                                {
                                    foreach (int i in f.ItemsRespaldo)
                                    {
                                        if (pr.IdPresupuesto == i)
                                        {
                                            if (pr.FacturaFK == null && pr.OrdenDeCompraFK == null)
                                            {
                                                presupuestosSinReasignar++;
                                            }
                                        }
                                    }
                                }
                                if (presupuestosRespaldo == presupuestosSinReasignar)
                                {
                                    // Permitir reactivar
                                    f.Estado = EstadoFactura.Impaga;

                                    foreach (Presupuesto pr in presupuestos)
                                    {
                                        foreach (int i in f.ItemsRespaldo)
                                        {
                                            if (pr.IdPresupuesto == i)
                                            {
                                                pr.FacturaFK = idFacturaRectivar;
                                            }
                                        }
                                    }

                                    f.ItemsRespaldo = new List<int>();

                                    string jsonActualizadoPr = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizadoPr);
                                }
                            }
                            else
                            {
                                f.Estado = EstadoFactura.Impaga;
                            }
                            break;
                        case Contiene.OrdenesDeCompra:
                            f.Estado = EstadoFactura.Impaga;

                            List<RelacionODCFactura> relacionesODCF = ObtenerRelacionesODCFactura(idEmpresa);

                            foreach (RelacionODCFactura rodcf in relacionesODCF)
                            {
                                if (rodcf.IdFacturaFK == idFacturaRectivar)
                                {
                                    rodcf.Estado = EstadoRelacionFOC.Activa;

                                    CambiarEstadoOCConRecepciones(idEmpresa, rodcf.IdOdcFK);
                                }
                            }

                            /*
                            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
                            foreach (OrdenDeCompra odc in odcs)
                            {
                                if (odc.IdOC == rodcf.IdOdcFK)
                                {
                                    odc.Estado = EstadoOC.Activa;
                                }
                            }
                            ActualizarJson_OrdenesDeCompra(odcs, idEmpresa);
                            */

                            string jsonActualizadoRelacionesODCF = JsonConvert.SerializeObject(relacionesODCF, Formatting.Indented);
                            File.WriteAllText(RelacionODCFacturaRutaJson(idEmpresa), jsonActualizadoRelacionesODCF);

                            List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);

                            if (f.RecepionesRespaldo != null && f.RecepionesRespaldo.Any())
                            {
                                foreach (Recepcion rr in f.RecepionesRespaldo)
                                {
                                    foreach (Recepcion r in recepciones)
                                    {
                                        if (rr.IdRecepcion == r.IdRecepcion)
                                        {
                                            r.Estado = EstadoRecepcion.Facturada;
                                            r.IdFacturaFK = rr.IdFacturaFK;
                                            r.Monto = rr.Monto;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                f.Estado = EstadoFactura.Impaga;
                            }
                            string jsonActualizadoRecepciones = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
                            File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizadoRecepciones);

                            f.ItemsRespaldo = new List<int>();
                            f.RecepionesRespaldo = new List<Recepcion>();

                            break;
                        case Contiene.SinDefinir:
                            f.Estado = EstadoFactura.Impaga;
                            break;
                    }
                }
            }
            string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
        }
        public List<Pedido> PedidosEnFactura(int idEmpresa, int idFactura)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosEnFactura = new List<Pedido>();
            foreach(Pedido p in pedidos){
                if (p.FacturaFK == idFactura)
                {
                    pedidosEnFactura.Add(p);
                }
            }
            return pedidosEnFactura;
        }
        public List<Presupuesto> PresupuestosEnFactura(int idEmpresa, int idFactura)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            List<Presupuesto> presupuestosEnFactura = new List<Presupuesto>();
            foreach (Presupuesto p in presupuestos)
            {
                if (p.FacturaFK == idFactura)
                {
                    presupuestosEnFactura.Add(p);
                }
            }
            return presupuestosEnFactura;
        }
        public bool ModificarCodigoFactura(int idEmpresa, int idFactura, string puesto, string numero)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            bool codigoConfirmado = true;

            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    Codigo nuevoCodigo = new Codigo() { Puesto = puesto, Numero = numero};

                    foreach (Factura fa in facturas)
                    {
                        if (fa.Codigo.Puesto == nuevoCodigo.Puesto && fa.Codigo.Numero == nuevoCodigo.Numero)
                        {
                            codigoConfirmado = false;
                        }
                    }
                    if (codigoConfirmado == true)
                    {
                        f.Codigo = nuevoCodigo;
                        string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                        File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                    } 
                }break;
            }
            return codigoConfirmado;
        }
        public void ModificarFechaFactura(int idEmpresa, int idFactura, DateOnly fechaNueva)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    f.Fecha = fechaNueva;

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarDescripcionFactura(int idEmpresa, int idFactura, string nuevaDescripcion)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    f.Descripcion = nuevaDescripcion;

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);

                    break;
                }
            }
        }
        public void ModificarMontoFactura(int idEmpresa, int idFactura, double? nuevoMonto)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    f.MontoManual = nuevoMonto;

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);

                    break;
                }
            }
        }
        public void QuitarOCDeFactura(int idEmpresa, int idFactura, int idOCQuitar)
        {
            List<RelacionODCFactura> relaciones = ObtenerRelacionesODCFactura(idEmpresa);

            relaciones.RemoveAll(x => x.IdFacturaFK == idFactura && x.IdOdcFK == idOCQuitar);

            string jsonActualizado = JsonConvert.SerializeObject(relaciones, Formatting.Indented);
            File.WriteAllText(RelacionODCFacturaRutaJson(idEmpresa), jsonActualizado);

            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.IdOC == idOCQuitar)
                {
                    odc.Estado = EstadoOC.Activa;

                    if (odc.Relacion == Relacion.ConRecepcion)
                    {
                        List<Recepcion> recepciones = ObtenerRecepcionesJson(idEmpresa);
                        foreach (Recepcion r in recepciones)
                        {
                            if (r.IdOdCFK == idOCQuitar)
                            {
                                QuitarRecepcionDeFactura(idEmpresa, r.IdRecepcion);
                            }
                        }
                    }
                }
            }
            string jsonActualizado2 = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado2);
        }
        public void AgregarNotaFactura(int idEmpresa, int idFactura, string nuevaNota)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    if (f.Notas != null && f.Notas.Any())
                    {
                        f.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        f.Notas = new List<string>() { nuevaNota };
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);

                    break;
                }
            }
        }
        public void ModificarNotaFactura(int idEmpresa, int idFactura, string notaAModificar, string notaModificada)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    if (f.Notas != null && f.Notas.Any())
                    {
                        int indiceNota = f.Notas.IndexOf(notaAModificar);

                        if (indiceNota != -1)
                        {
                            f.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                    break;
                }
            } 
        }
        public void BorrarNotaFactura(int idEmpresa, int idFactura, string notaBorrar)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    if (f.Notas != null && f.Notas.Any())
                    {
                        int indiceNota = f.Notas.IndexOf(notaBorrar);

                        if (indiceNota != -1)
                        {
                            f.Notas.Remove(notaBorrar);

                            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                    break;
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarFactura(int idEmpresa, int idFactura)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };
            Factura f = ElegirFactura(idEmpresa, idFactura);

            if (f.Estado == EstadoFactura.Pagada)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Ya esta pagada.");
            }

            switch (f.Contiene)
            {
                case Contiene.SinDefinir:
                    // no hace falta hacer nada
                    break;
                case Contiene.Pedidos:
                    List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                    foreach (Pedido pe in pedidos)
                    {
                        if (pe.FacturaFK == idFactura)
                        {
                            respuesta.SePuede = false;
                            respuesta.Mensajes.Add("Tiene pedidos asociados.");
                            break;
                        }
                    }
                    break;
                case Contiene.Presupuestos:
                    List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                    foreach (Presupuesto pr in presupuestos)
                    {
                        if (pr.FacturaFK == idFactura)
                        {
                            respuesta.SePuede = false;
                            respuesta.Mensajes.Add("Tiene presupuestos asociados.");
                            break;
                        }
                    }
                    break;
                case Contiene.OrdenesDeCompra:
                    List<RelacionODCFactura> relaciones = ObtenerRelacionesODCFactura(idEmpresa);
                    foreach (RelacionODCFactura rocf in relaciones)
                    {
                        if (rocf.IdFacturaFK == idFactura)
                        {
                            respuesta.SePuede = false;
                            respuesta.Mensajes.Add("Tiene ordenes de compra asociadas.");
                            break;
                        }
                    }
                    break;
            }

            if (f.NotaDeCredito != null)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Tiene una nota de credito asociada.");
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }
            return respuesta;
        }
        public void BorrarFactura(int idEmpresa, int idFacturaBorrar)
        {
            if (SePuedeBorrarFactura(idEmpresa, idFacturaBorrar).SePuede == true)
            {
                Factura f = ElegirFactura(idEmpresa, idFacturaBorrar);
                switch (f.Contiene)
                {
                    case Contiene.SinDefinir:
                        // No hace falta hacer nada
                        break;
                    case Contiene.Pedidos:
                        List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                        foreach (Pedido pe in pedidos)
                        {
                            if (pe.FacturaFK == idFacturaBorrar)
                            {
                                pe.FacturaFK = null;
                            }
                        }

                        ActualizarJson_Pedidos(pedidos, idEmpresa);
                        break;
                    case Contiene.Presupuestos:
                        List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                        foreach (Presupuesto pr in presupuestos)
                        {
                            if (pr.FacturaFK == idFacturaBorrar)
                            {
                                pr.FacturaFK = null;
                            }
                        }

                        ActualizarJson_Presupuestos(presupuestos, idEmpresa);
                        break;
                    case Contiene.OrdenesDeCompra:
                        List<RelacionODCFactura> relaciones = ObtenerRelacionesODCFactura(idEmpresa);

                        relaciones.RemoveAll(r => r.IdFacturaFK == idFacturaBorrar);

                        ActualizarJson_Relaciones_ODC_Factura(relaciones, idEmpresa);
                        break;
                }

                List<Factura> facturas = ObtenerFacturasJson(idEmpresa);

                facturas.RemoveAll(f => f.IdFactura == idFacturaBorrar);

                ActualizarJson_Facturas(facturas, idEmpresa);
            }
        }


        // ----- RELACION ODC Y FACTURA -----
        public string RelacionODCFacturaRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Ordenes de Compra\" + idEmpresa.ToString() + @" - RelacionODCFactura.json";
        }
        public List<RelacionODCFactura> ObtenerRelacionesODCFactura(int idEmpresa)
        {
            List<RelacionODCFactura> RODCF = new List<RelacionODCFactura>();
            string RutaJsonRelacionODCF = RelacionODCFacturaRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonRelacionODCF))
            {
                if (System.IO.File.ReadAllText(RutaJsonRelacionODCF) != "" || System.IO.File.ReadAllText(RutaJsonRelacionODCF) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonRelacionODCF);
                    RODCF = JsonConvert.DeserializeObject<List<RelacionODCFactura>>(jsonRecuperado);
                }
            }
            return RODCF;
        }
        public int PrimerIdFaltanteRelacionOCF(int idEmpresa)
        {
            List<int> ids = ObtenerRelacionesODCFactura(idEmpresa).Select(r => r.IdRelacionODCFactura).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void NuevaRelacionOCDF(int idEmpresa, int idFactura, int idODC)
        {
            List<RelacionODCFactura> relacionOCF = ObtenerRelacionesODCFactura(idEmpresa);
            RelacionODCFactura rodcf = new RelacionODCFactura();

            rodcf.IdRelacionODCFactura = PrimerIdFaltanteRelacionOCF(idEmpresa);
            rodcf.IdFacturaFK = idFactura;
            rodcf.IdOdcFK = idODC;
            rodcf.Estado = EstadoRelacionFOC.Activa;

            if (CompararRelacion(idEmpresa, idFactura, idODC) == false)
            {
                relacionOCF.Add(rodcf);

                string jsonActualizado = JsonConvert.SerializeObject(relacionOCF, Formatting.Indented);
                File.WriteAllText(RelacionODCFacturaRutaJson(idEmpresa), jsonActualizado);
 
            }
        }
        public bool CompararRelacion(int idEmpresa, int idFactura, int idOC)
        {
            List<RelacionODCFactura> relaciones = ObtenerRelacionesODCFactura(idEmpresa);
            bool sonIguales = false;
            foreach (RelacionODCFactura rfoc in relaciones)
            {
                if (rfoc.IdFacturaFK == idFactura && rfoc.IdOdcFK == idOC)
                {
                    sonIguales = true;
                    break;
                }
            }
            return sonIguales;
        }
        public List<OrdenDeCompra> OrdenesDeCompraEnFactura(int idEmpresa, int idFacturaBuscada)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            List<OrdenDeCompra> odcsEnFactura = new List<OrdenDeCompra>();

            List<RelacionODCFactura> relacionODCF = ObtenerRelacionesODCFactura(idEmpresa);
            List<int> idsODCs = new List<int>();

            foreach (RelacionODCFactura r in relacionODCF)
            {
                if (r.IdFacturaFK == idFacturaBuscada)
                {
                    idsODCs.Add(r.IdOdcFK);
                }
            }

            foreach (OrdenDeCompra odc in odcs)
            {
                foreach (int i in idsODCs)
                {
                    if (odc.IdOC == i)
                    {
                        odcsEnFactura.Add(odc);
                    }
                }
            }

            return odcsEnFactura;
        }

        // PROBAR
        public List<Factura> FacturasDeOrdenDeCompra(int idEmpresa, int idOdcBuscada)
            {
                List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                List<Factura> facturasDeODC = new List<Factura>();

                List<RelacionODCFactura> relacionODCF = ObtenerRelacionesODCFactura(idEmpresa);
                List<int> idsFacturas = new List<int>();

                foreach (RelacionODCFactura r in relacionODCF)
                {
                    if (r.IdOdcFK == idOdcBuscada)
                    {
                        idsFacturas.Add(r.IdFacturaFK);
                    }
                }

                foreach (Factura f in facturas)
                {
                    foreach (int i in idsFacturas)
                    {
                        if (f.IdFactura == i)
                        {
                            facturasDeODC.Add(f);
                        }
                    }
                }

                return facturasDeODC;
            }
        
        

        // ----- NOTAS DE CREDITO! -----
        public string NotasDeCreditoRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Notas de Credito\" + idEmpresa.ToString() + @" - Notas de Credito.json";
        }
        public List<NotaDeCredito> ObtenerNotasDeCreditoJson(int idEmpresa)
        {
            List<NotaDeCredito> NotasDeCredito = new List<NotaDeCredito>();
            string RutaJsonNotasDeCredito = NotasDeCreditoRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonNotasDeCredito))
            {
                if (System.IO.File.ReadAllText(RutaJsonNotasDeCredito) != "" || System.IO.File.ReadAllText(RutaJsonNotasDeCredito) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonNotasDeCredito);
                    NotasDeCredito = JsonConvert.DeserializeObject<List<NotaDeCredito>>(jsonRecuperado);
                }
            }
            return NotasDeCredito;
        }
        public NotaDeCredito ElegirNotaDeCredito(int idEmpresa, int idNotaDeCredito)
        {
            NotaDeCredito ndc = new NotaDeCredito();
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito n in ndcs)
            {
                if (n.IdNotaCredito == idNotaDeCredito)
                {
                    ndc = n;
                }
            }
            return ndc;
        }
        public NotaDeCredito? NotaDeCreditoDeFactura(int idEmpresa, int idFactura)
        {
            Factura factura = ElegirFactura(idEmpresa, idFactura);
            List<NotaDeCredito> notasDeCredito = ObtenerNotasDeCreditoJson(idEmpresa);
            NotaDeCredito? ndcFactura = null;

            if (factura.NotaDeCredito != null)
            {
                foreach (NotaDeCredito ndc in notasDeCredito)
                {
                    if (factura.NotaDeCredito == ndc.IdNotaCredito)
                    {
                        ndcFactura = ndc;
                    }
                }
            }
            return ndcFactura;
        }
        public int PrimerIdFaltanteNotaDeCredito(int idEmpresa)
        {
            List<int> ids = ObtenerNotasDeCreditoJson(idEmpresa).Select(ndc => ndc.IdNotaCredito).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public async Task<int> NuevaNotaDeCredito(int idEmpresa, DateOnly fecha, string puesto, string numero, List<string>? notas, IFormFile? archivo, List<int>? idsFacturasAsociadas)
        {
            List<NotaDeCredito> notasDeCredito = ObtenerNotasDeCreditoJson(idEmpresa);
            NotaDeCredito ndc = new NotaDeCredito();

            int idNdc = PrimerIdFaltanteNotaDeCredito(idEmpresa);
            ndc.IdNotaCredito = idNdc;

            if (fecha != DateOnly.MinValue)
            {
                ndc.Fecha = fecha;
            }
            else
            {
                ndc.Fecha = DateOnly.FromDateTime(DateTime.Now);
            }

            ndc.Codigo = new CodigoNDC();
            ndc.Codigo.PuestoNDC = puesto;
            ndc.Codigo.NumeroNDC = numero;
            
            if (notas != null && notas.Any())
            {
                ndc.Notas = notas;
            }
            else
            {
                ndc.Notas = new List<string>();
            }
            
            ndc.FacturasRespaldo = new List<int>();
            
            if (archivo != null)
            {
                List<IFormFile> archivos = new List<IFormFile>() { archivo };
                await GuardarArchivos(idEmpresa, "NotaDeCredito", idNdc, archivos);
            }
            else
            {
                ndc.UbicacionArchivo = null;
            }
            
            if (idsFacturasAsociadas != null && idsFacturasAsociadas.Any())
            {
                List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                foreach (Factura f in facturas)
                {
                    foreach (int i in idsFacturasAsociadas)
                    {
                        if (f.IdFactura == i)
                        {
                            f.NotaDeCredito = idNdc;
                        }
                    }
                }
                string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
            }
            
            notasDeCredito.Add(ndc);

            string jsonActualizado = JsonConvert.SerializeObject(notasDeCredito, Formatting.Indented);
            File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);

            return idNdc;
        }
        public NotaDeCredito SeleccionarNotaDeCredito(int idEmpresa, int idNotaDeCreditoBuscada)
        {
            List<NotaDeCredito> notasDeCredito = ObtenerNotasDeCreditoJson(idEmpresa);
            NotaDeCredito ndc = new NotaDeCredito() { IdNotaCredito = -1 };
            foreach (NotaDeCredito n in notasDeCredito)
            {
                if (n.IdNotaCredito == idNotaDeCreditoBuscada)
                {
                    ndc = n;
                }
            }
            return ndc;
        }
        public void ModificarFechaNotaDeCredito(int idEmpresa, int idNotaDeCredito, DateOnly nuevaFecha)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);

            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    ndc.Fecha = nuevaFecha;

                    string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                    File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarCodigoNotaDeCredito(int idEmpresa, int idNotaDeCredito, string nuevoPuesto, string nuevoNumero)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);

            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    ndc.Codigo.PuestoNDC = nuevoPuesto;
                    ndc.Codigo.NumeroNDC = nuevoNumero;

                    string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                    File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void ModificarEstadoNotaDeCredito(int idEmpresa, int idNotaDeCredito, EstadoNotaDeCredito nuevoEstado)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    ndc.Estado = nuevoEstado;

                    if (nuevoEstado == EstadoNotaDeCredito.Anulada)
                    {
                        List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                        foreach (Factura f in facturas)
                        {
                            if (f.NotaDeCredito == idNotaDeCredito)
                            {
                                if (ndc.FacturasRespaldo != null && ndc.FacturasRespaldo.Any())
                                {
                                    if (!ndc.FacturasRespaldo.Contains(f.IdFactura))
                                    {
                                        ndc.FacturasRespaldo.Add(f.IdFactura);
                                    }    
                                }
                                else
                                {
                                    ndc.FacturasRespaldo = new List<int>() { f.IdFactura };
                                }
                                f.NotaDeCredito = null;

                                string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                                File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
                            }
                        }
                    }
                    else if(nuevoEstado == EstadoNotaDeCredito.Activa)
                    {
                        List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                        if (ndc.FacturasRespaldo != null && ndc.FacturasRespaldo.Any())
                        {
                            foreach (Factura f in facturas)
                            {
                                foreach (int i in ndc.FacturasRespaldo)
                                {
                                    if (f.IdFactura == i)
                                    {
                                        f.NotaDeCredito = idNotaDeCredito;

                                        string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                                        File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
                                    }
                                }
                            }
                        }
                        ndc.FacturasRespaldo = new List<int>();
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                    File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
                }
            }     
        }
        public void QuitarFacturaDeNotaDeCredito(int idEmpresa, int idNotaDeCredito, int idFactura)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura && f.NotaDeCredito == idNotaDeCredito) 
                {
                    f.NotaDeCredito = null;
                }
            }
            string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
        }
        public void AgregarFacturasANotaDeCredito(int idEmpresa, int idNotaDeCredito, List<int>? idsFacturas)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            if (idsFacturas != null && idsFacturas.Any())
            {
                foreach (Factura f in facturas)
                {
                    foreach (int i in idsFacturas)
                    {
                        if (f.IdFactura == i)
                        {
                            f.NotaDeCredito = idNotaDeCredito;
                        }
                    }
                }
                string jsonActualizadoF = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoF);
            }
        }
        public void AgregarNotaNotaDeCredito(int idEmpresa, int idNotaDeCredito, string nuevaNota)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    if (ndc.Notas != null && ndc.Notas.Any())
                    {
                        ndc.Notas.Add(nuevaNota);
                    }
                    else
                    {
                        ndc.Notas = new List<string>() { nuevaNota };
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                    File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);

                    break;
                }
            }
        }
        public void ModificarNotaNotaDeCredito(int idEmpresa, int idNotaDeCredito, string notaAModificar, string notaModificada)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    if (ndc.Notas != null && ndc.Notas.Any())
                    {
                        int indiceNota = ndc.Notas.IndexOf(notaAModificar);

                        if (indiceNota != -1)
                        {
                            ndc.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                            File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                    break;
                }
            }
        } 
        public void BorrarNotaNotaDeCredito(int idEmpresa, int idNotaDeCredito, string notaBorrar)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNotaDeCredito)
                {
                    if (ndc.Notas != null && ndc.Notas.Any())
                    {
                        int indiceNota = ndc.Notas.IndexOf(notaBorrar);

                        if (indiceNota != -1)
                        {
                            ndc.Notas.Remove(notaBorrar);

                            string jsonActualizado = JsonConvert.SerializeObject(ndcs, Formatting.Indented);
                            File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
                        }
                    }
                    break;
                }
            }
        }
        public RespuestaBorrar SePuedeBorrarNotaDeCredito(int idEmpresa, int idNotaDeCredito)
        {
            RespuestaBorrar respuesta = new RespuestaBorrar() { SePuede = true };

            NotaDeCredito ndc = ElegirNotaDeCredito(idEmpresa, idNotaDeCredito);
            if (ndc.Estado == EstadoNotaDeCredito.Confirmada)
            {
                respuesta.SePuede = false;
                respuesta.Mensajes.Add("Esta confirmada.");
            }

            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.NotaDeCredito == idNotaDeCredito)
                {
                    respuesta.SePuede = false;
                    respuesta.Mensajes.Add("Tiene facturas asociadas.");
                    break;
                }
            }

            if (respuesta.SePuede == true)
            {
                respuesta.Mensajes.Add("- Sin documentos asociados -");
            }
            return respuesta;
        }
        public void BorrarNotaDeCredito(int idEmpresa, int idNotaDeCreditoBorrar)
        {
            if (SePuedeBorrarNotaDeCredito(idEmpresa, idNotaDeCreditoBorrar).SePuede == true)
            {
                List<NotaDeCredito> notasDeCredito = ObtenerNotasDeCreditoJson(idEmpresa);

                notasDeCredito.RemoveAll(ndc => ndc.IdNotaCredito == idNotaDeCreditoBorrar);

                ActualizarJson_NotasDeCredito(notasDeCredito, idEmpresa);
            }
        }
        public void AgregarArchivoANDC(int idEmpresa, int idNDC, string ruta)
        {
            List<NotaDeCredito> ndcs = ObtenerNotasDeCreditoJson(idEmpresa);
            foreach (NotaDeCredito ndc in ndcs)
            {
                if (ndc.IdNotaCredito == idNDC)
                {
                    if (ndc.UbicacionArchivo == null)
                    {
                        ndc.UbicacionArchivo = ruta;
                        ActualizarJson_NotasDeCredito(ndcs, idEmpresa);
                    }
                }
            }
        }

        // ----- ARCHIVOS GENERAL! ----
        public void AbrirArchivoEnSuUbicacion(string rutaArchivo)
        {
            string rutaCompleta = Directory.GetCurrentDirectory() + "\\wwwroot" + rutaArchivo;
            if (File.Exists(rutaCompleta))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{rutaCompleta}\"",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start(startInfo);
            }
        }
        public async Task GuardarArchivos(int idEmpresa, string nombreElemento, int idElemento, List<IFormFile> archivos)
        {
            if (archivos != null && archivos.Any())
            {
                // GUARDAR LOS ARCHIVOS EN LAS CARPETAS CORRESPONDIENTES
                foreach (IFormFile archivo in archivos)
                {
                    string carpetaRelativa = "";

                    switch (nombreElemento)
                    {
                        case "Costo":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Costos");
                            break;
                        case "Factura":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Facturas");
                            break;
                        case "NotaDeCredito":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Notas de Credito");
                            break;
                        case "OrdenDeCompra":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Ordenes de Compra");
                            break;
                        case "Pedido":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Pedidos");
                            break;
                        case "Pieza":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Piezas");
                            break;
                        case "Conjunto":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Piezas");
                            break;
                        case "Presupuesto":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Presupuestos");
                            break;
                        case "Remito":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Remitos");
                            break;
                    }

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

                        // EN CASO DE NOMBRE EXISTENTE AGREGARLE NUMERO (1, 2, 3, ...)
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

                    // GUARDAR LAS RUTAS EN LOS JSON
                    string rutaParaJson = Path.Combine("\\",carpetaRelativa, nombreArchivo);

                    switch (nombreElemento)
                    {
                        case "Costo":
                            // 1 solo archivo
                            List<Costo> costos = ObtenerCostosJson(idEmpresa);
                            foreach (Costo c in costos)
                            {
                                if(c.IdCosto == idElemento)
                                {
                                    c.Archivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoCostos = JsonConvert.SerializeObject(costos, Formatting.Indented);
                            File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizadoCostos);
                            break;
                        case "Factura":
                            // 1 solo archivo
                            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                            foreach (Factura f in facturas)
                            {
                                if (f.IdFactura == idElemento)
                                {
                                    f.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoFacturas = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoFacturas);
                            break;
                        case "NotaDeCredito":
                            // 1 solo archivo
                            AgregarArchivoANDC(idEmpresa, idElemento, rutaParaJson);
                            break;
                        case "OrdenDeCompra":
                            // 1 solo archivo
                            List<OrdenDeCompra> ordenesDeCompra = ObtenerOrdenesDeCompraJson(idEmpresa);
                            foreach (OrdenDeCompra odc in ordenesDeCompra)
                            {
                                if (odc.IdOC == idElemento)
                                {
                                    odc.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoOrdenesDeCompra = JsonConvert.SerializeObject(ordenesDeCompra, Formatting.Indented);
                            File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoOrdenesDeCompra);
                            break;
                        case "Pedido":
                            // varios archivos
                            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                            foreach (Pedido p in pedidos)
                            {
                                if (p.IdPedido == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                            break;
                        case "Pieza":
                            // varios archivos
                            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
                            foreach (Pieza p in piezas)
                            {
                                if (p.IdPieza == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoPiezas = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizadoPiezas);
                            break;
                        case "Conjunto":
                            // varios archivos
                            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
                            foreach (Conjunto c in conjuntos)
                            {
                                if (c.IdConjunto == idElemento)
                                {
                                    if (c.Archivos != null && c.Archivos.Any())
                                    {
                                        c.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        c.Archivos = new List<string>();
                                        c.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoConjuntos = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizadoConjuntos);
                            break;
                        case "Presupuesto":
                            // varios archivos
                            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                            foreach (Presupuesto p in presupuestos)
                            {
                                if (p.IdPresupuesto == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                }
                            }
                            string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                            break;
                        case "Remito":
                            // 1 solo archivo
                            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
                            foreach (Remito r in remitos)
                            {
                                if (r.IdRemito == idElemento)
                                {
                                    r.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoRemitos = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                            File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizadoRemitos);
                            break;
                    }
                } 
            }
        }

        public void BorrarArchivo(int idEmpresa, string nombreElemento, int idElemento, string rutaArchivo)
        {
            // BORRAR ARCHIVO
            string rutaCompleta = Directory.GetCurrentDirectory() + "\\wwwroot" + rutaArchivo;
            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }

            // BORRAR RUTA DEL JSON
            switch (nombreElemento)
            {
                case "Costo":
                    List<Costo> costos = ObtenerCostosJson(idEmpresa);
                    foreach (Costo c in costos)
                    {
                        if (c.IdCosto == idElemento)
                        {
                            c.Archivo = null;
                        }
                    }
                    string jsonActualizadoCostos = JsonConvert.SerializeObject(costos, Formatting.Indented);
                    File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizadoCostos);
                    break;
                case "Factura":
                    List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
                    foreach (Factura f in facturas)
                    {
                        if (f.IdFactura == idElemento)
                        {
                            f.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoFacturas = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizadoFacturas);
                    break;
                case "NotaDeCredito":
                    List<NotaDeCredito> notasDeCredito = ObtenerNotasDeCreditoJson(idEmpresa);
                    foreach (NotaDeCredito ndc in notasDeCredito)
                    {
                        if (ndc.IdNotaCredito == idElemento)
                        {
                            ndc.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoNotasDeCredito = JsonConvert.SerializeObject(notasDeCredito, Formatting.Indented);
                    File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoNotasDeCredito);
                    break;
                case "OrdenDeCompra":
                    List<OrdenDeCompra> ordenesDeCompra = ObtenerOrdenesDeCompraJson(idEmpresa);
                    foreach (OrdenDeCompra odc in ordenesDeCompra)
                    {
                        if (odc.IdOC == idElemento)
                        {
                            odc.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoOrdenesDeCompra = JsonConvert.SerializeObject(ordenesDeCompra, Formatting.Indented);
                    File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizadoOrdenesDeCompra);
                    break;
                case "Pedido":
                    List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
                    foreach (Pedido p in pedidos)
                    {
                        if (p.IdPedido == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                    break;
                case "Pieza":
                    List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
                    foreach (Pieza p in piezas)
                    {
                        if (p.IdPieza == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPiezas = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizadoPiezas);
                    break;
                case "Conjunto":
                    List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
                    foreach (Conjunto c in conjuntos)
                    {
                        if (c.IdConjunto == idElemento)
                        {
                            if (c.Archivos != null && c.Archivos.Any())
                            {
                                c.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoConjuntos = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                    File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizadoConjuntos);
                    break;
                case "Presupuesto":
                    List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                    foreach (Presupuesto p in presupuestos)
                    {
                        if (p.IdPresupuesto == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                    break;
                case "Remito":
                    List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
                    foreach (Remito r in remitos)
                    {
                        if (r.IdRemito == idElemento)
                        {
                            r.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoRemitos = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizadoRemitos);
                    break;
            }
        }


        // ----- ACTUALIZAR JSONS! -----

        public void ActualizarJson_Empresas(List<Empresa> empresas)
        {
            string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
            File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
        }
        public void ActualizarJson_Socios(List<Socio> socios)
        {
            string jsonActualizado = JsonConvert.SerializeObject(socios, Formatting.Indented);
            File.WriteAllText(SociosRutaJson(), jsonActualizado);
        }
        public void ActualizarJson_Empleados(List<Empleado> empleados, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Piezas(List<Pieza> piezas, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Conjuntos(List<Conjunto> conjuntos, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Pedidos(List<Pedido> pedidos, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Costos(List<Costo> costos, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(costos, Formatting.Indented);
            File.WriteAllText(CostosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Remitos(List<Remito> remitos, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
            File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Presupuestos(List<Presupuesto> presupuestos, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_OrdenesDeCompra(List<OrdenDeCompra> odcs, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Recepciones(List<Recepcion> recepciones, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(recepciones, Formatting.Indented);
            File.WriteAllText(RecepcionesRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Facturas(List<Factura> facturas, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_Relaciones_ODC_Factura(List<RelacionODCFactura> relacionOCF, int idEmpresa) 
        { 
            string jsonActualizado = JsonConvert.SerializeObject(relacionOCF, Formatting.Indented);
            File.WriteAllText(RelacionODCFacturaRutaJson(idEmpresa), jsonActualizado);
        }
        public void ActualizarJson_NotasDeCredito(List<NotaDeCredito> notasDeCredito, int idEmpresa)
        {
            string jsonActualizado = JsonConvert.SerializeObject(notasDeCredito, Formatting.Indented);
            File.WriteAllText(NotasDeCreditoRutaJson(idEmpresa), jsonActualizado);
        }






        // JSON AUXILIAR

        /*
        public string AuxiliarRutaJson()
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\data\Auxiliar.json";
        }
        public void ConvertirArchivoAJson(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo proporcionado no es válido.");
            }

            byte[] bytesArchivo;

            using (var memoryStream = new MemoryStream())
            {
                archivo.CopyTo(memoryStream);
                bytesArchivo = memoryStream.ToArray();
            }

            string textoBase64 = Convert.ToBase64String(bytesArchivo);

            var miArchivoJson = new ArchivoJson
            {
                NombreArchivo = Path.GetFileName(archivo.FileName),
                Extension = Path.GetExtension(archivo.FileName),
                ContenidoBase64 = textoBase64
            };

            string jsonActualizado = JsonConvert.SerializeObject(miArchivoJson, Formatting.Indented);

            File.WriteAllText(AuxiliarRutaJson(), jsonActualizado);
        }
        public async Task<string> RecuperarArchivoDesdeJson()
        {
            string rutaJson = AuxiliarRutaJson();

            if (!System.IO.File.Exists(rutaJson))
            {
                throw new FileNotFoundException("El archivo JSON no fue encontrado.");
            }

            string jsonTexto = await System.IO.File.ReadAllTextAsync(rutaJson);

            ArchivoJson archivoInfo = JsonConvert.DeserializeObject<ArchivoJson>(jsonTexto);

            byte[] bytesArchivo = Convert.FromBase64String(archivoInfo.ContenidoBase64);

            string carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data");

            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string rutaCompletaArchivo = Path.Combine(carpetaDestino, archivoInfo.NombreArchivo);

            using (var stream = new FileStream(rutaCompletaArchivo, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await stream.WriteAsync(bytesArchivo, 0, bytesArchivo.Length);
            }

            //string rutaForzada = "C:/Users/User/Desktop/Proyectos/ContabilidadTaller REORGANIZADO/ContabilidadTaller/wwwroot/data";

            string rutaFinal = $@"\\data\\{archivoInfo.NombreArchivo}";

            return rutaFinal;
        }
        */





    }

}

    
