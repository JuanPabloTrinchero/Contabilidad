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
    public class ServiciosOrdenesDeCompra : IServiciosOrdenesDeCompra
    {
        // ----- ORDENES DE COMPRA -----
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
        public OrdenDeCompra BuscarOrdenDeCompra(int idEmpresa, int idOrdenDeCompra)
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
        public int SiguienteIdOC(int idEmpresa, List<OrdenDeCompra> ocs)
        {
            return ocs.Any() ? ocs.Max(x => x.IdOC) + 1 : 0;
        }
        public void GuardarNuevaOC(int idEmpresa, string codigo, DateOnly fecha, Relacion relacionOC, List<string>? recepciones, List<string>? notas)
        {
                ////// RETOCADA
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
        public void CambiarEstadoRecepcion(int idEmpresa, int idOc, string recepcion, int nuevoEstadoRecepcion)
        {
            // RETOCADA
        }
        public void AgregarRecepcion(int idEmpresa, int idOc, string numeroRecepcion)
        {
            // RETOCADA
        }
        public void BorrarRecepcion(int idEmpresa, int idOc, string recepcionABorrar)
        {
            // RETOCADA
        }
        public void CambiarEstadoOC(int idEmpresa, int idOc, int estado)
        {
            List<OrdenDeCompra> odcs = ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra oc in odcs)
            {
                if (oc.IdOC == idOc)
                {
                    oc.Estado = (EstadoOC)estado;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            File.WriteAllText(OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
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
    }
}

*/   
