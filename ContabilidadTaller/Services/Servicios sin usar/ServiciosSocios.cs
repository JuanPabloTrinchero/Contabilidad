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
    public class ServiciosSocios : IServiciosSocios
    {
        // ----- SOCIOS -----
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
        public void GuardarNuevoSocio(string nombres, string apellido, string cuit, string direccion, List<string> emails, string telefono, string contraseña)
        {
            List<Socio> Socios = ObtenerSociosJson();
            Socio nuevoSocio = new Socio();

            nuevoSocio.IdSocio = Socios.Any() ? Socios.Max(x => x.IdSocio) + 1 : 1;
            nuevoSocio.Nombres = nombres;
            nuevoSocio.Apellido = apellido;
            nuevoSocio.Cuit = cuit;
            nuevoSocio.Direccion = direccion;
            if (emails != null && emails.Any())
            {
                nuevoSocio.Emails = emails;
            }
            else
            {
                nuevoSocio.Emails = new List<string>();
            }
            nuevoSocio.Telefono = telefono;
            nuevoSocio.ContraseñaConfirmacion = contraseña;

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

    }
}

*/
    
