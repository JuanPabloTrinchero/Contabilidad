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
    public class ServiciosNotasDeCredito : IServiciosNotasDeCredito
    {
        // ----- NOTAS DE CREDITO -----
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
        public NotaDeCredito NotaDeCreditoDeFactura(int idEmpresa, int idFactura)
        {
            List<NotaDeCredito> todas = ObtenerNotasDeCreditoJson(idEmpresa);
            NotaDeCredito ndc = null;

            foreach (NotaDeCredito n in todas)
            {
                if (n.FacturaAsociadaFK == idFactura)
                {
                    ndc = n;
                }
            }
            return ndc;
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

    }
}

*/
    
