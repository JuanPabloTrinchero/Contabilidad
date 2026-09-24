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
    public class ServiciosEmpresas : IServiciosEmpresas
    {
        // ----- EMPRESAS -----
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
        public int GuardarNuevaEmpresa(string nombreEmpresa, string cuit, string direccion, List<string> notas)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            Empresa e = new Empresa();

            e.IdEmpresa = PrimerIdEmpresaFaltante();

            //e.IdEmpresa = empresas.Any() ? empresas.Max(x => x.IdEmpresa) + 1 : 1;
            e.NombreEmpresa = nombreEmpresa;
            e.Cuit = cuit;
            e.Direccion = direccion;
            e.Notas = new List<string>();

            if (notas != null && notas.Any())
            {
                e.Notas = notas;
            }

            empresas.Add(e);

            string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
            File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
            return e.IdEmpresa;
        }
        public void ModificarDireccionEmpresa(int idEmpresa, string nuevaDireccion)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();

            foreach (Empresa e in empresas)
            {
                if (e.IdEmpresa == idEmpresa)
                {
                    e.Direccion = nuevaDireccion;

                    string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
                    File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
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
                    string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
                    File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
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
            string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
            File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
        }
        public void ModificarNotaEmpresa(int idEmpresa, string notaVieja, string notaModificada)
        {
            List<Empresa> empresas = ObtenerEmpresasJson();
            Empresa empresa = empresas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (notaModificada != null)
            {
                if (empresa != null && empresa.Notas != null)
                {
                    int indiceNota = empresa.Notas.IndexOf(notaVieja);

                    if (indiceNota != -1)
                    {
                        empresa.Notas[indiceNota] = notaModificada;

                        string jsonActualizado = JsonConvert.SerializeObject(empresas, Formatting.Indented);
                        File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
                    }
                }
            }
        }
        public void EliminarEmpresa(int idEmpresaEliminar)
        {
            List<Empresa> todas = ObtenerEmpresasJson();
            List<Empresa> actualizada = new List<Empresa>();

            foreach (Empresa e in todas)
            {
                if (e.IdEmpresa != idEmpresaEliminar)
                {
                    actualizada.Add(e);
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(actualizada, Formatting.Indented);
            File.WriteAllText(EmpresasRutaJson(), jsonActualizado);
        }

    }
}

*/
