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
    public class ServiciosPiezas : IServiciosPiezas
    {
        // ----- SERVICIOS (INYECCION DE DEPENDENCIAS) -----
        private readonly ServiciosPedidos _serPedidos;
        private readonly ServiciosArchivos _serArchivos;
        public ServiciosPiezas(ServiciosPedidos serPedidos, ServiciosArchivos serArchivos)
        {
            _serPedidos = serPedidos;
            _serArchivos = serArchivos;
        }
        // ----- PIEZAS -----
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
        public Pieza ElegirPieza(int idPieza, int idEmpresa)
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
        public void EliminarArchivoPieza(int idEmpresa, int idPieza, string rutaArchivo)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            Pieza p = piezas.FirstOrDefault(x => x.IdPieza == idPieza);

            p.Archivos.Remove(rutaArchivo);

            string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
        }
        public List<string> NombresDeArchivosPiezas(int idEmpresa)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            List<string> nombres = new List<string>();
            foreach (Pieza p in piezas)
            {
                if (p.Archivos != null && p.Archivos.Any())
                {
                    foreach (string arch in p.Archivos)
                    {

                        nombres.Add(Path.GetFileName(arch));
                    }
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
                await _serArchivos.GuardarArchivos(archivos, idEmpresa, "Pieza", idPiezaNueva);
            }
        }
        public void BorrarPieza(int idEmpresa, int idPiezaEliminar)
        {
            List<Pieza> piezas = ObtenerPiezasJson(idEmpresa);
            if (piezas != null && piezas.Any())
            {
                int eliminados = piezas.RemoveAll(p => p.IdPieza == idPiezaEliminar);

                if (eliminados > 0)
                {
                    string jsonActualizado = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(PiezasRutaJson(idEmpresa), jsonActualizado);
                }
            }
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

    }
}

    
*/