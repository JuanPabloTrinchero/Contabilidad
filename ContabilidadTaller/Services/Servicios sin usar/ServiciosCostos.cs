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
    public class ServiciosCostos : IServiciosCostos
    {
        // ----- COSTOS -----
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
        public void NuevoCosto(int idEmpresa, string descripcion, double precio, List<string>? notas, int idPedido, string? archivo)
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

    }
}

*/   
