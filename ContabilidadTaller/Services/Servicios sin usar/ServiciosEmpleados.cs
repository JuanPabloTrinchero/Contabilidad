/*

using ContabilidadTaller.Controllers;
using ContabilidadTaller.Interfaces;
using ContabilidadTaller.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Runtime.InteropServices;

namespace ContabilidadTaller.Services
{
    public class ServiciosEmpleados : IServiciosEmpleados
    {
        // ----- EMPLEADOS -----
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

            e.Activo = true;

            empleados.Add(e);
            string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
            File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
        }
        public Empleado BuscarEmpleado(int idEmpresa, int? idEmpleadoBuscado)
        {
            List<Empleado> empleados = ObtenerEmpleadosJson(idEmpresa);
            Empleado empleado = new Empleado() { IdEmpleado = -1 };
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
                    if (e.Activo == true)
                    {
                        e.Activo = false;
                    }
                    else
                    {
                        e.Activo = true;
                    }
                    string jsonActualizado = JsonConvert.SerializeObject(empleados, Formatting.Indented);
                    File.WriteAllText(EmpleadosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }

    }
}

*/    
