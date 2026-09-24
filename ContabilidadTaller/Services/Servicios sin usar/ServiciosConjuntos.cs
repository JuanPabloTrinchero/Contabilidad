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
    public class ServiciosConjuntos : IServiciosConjuntos
    {
        private readonly ServiciosPiezas _serPiezas;
        public ServiciosConjuntos(ServiciosPiezas serPiezas)
        {
            _serPiezas = serPiezas;
        }
        // ----- CONJUNTOS -----
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
        public async Task NuevoConjunto(int idEmpresa, string descripcion, string? codigo, List<string>? notas)
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

            conjuntos.Add(nuevoConjunto);

            string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
            File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
        }
        public Conjunto SeleccionarConjunto(int idEmpresa, int idConjuntoBuscado)
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


        public void AsignarPiezasExistentesAConjunto(int idEmpresa, List<int>? piezasYaExistentes, int idConjunto)
        {
            List<Pieza> piezas = _serPiezas.ObtenerPiezasJson(idEmpresa);
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
                System.IO.File.WriteAllText(_serPiezas.PiezasRutaJson(idEmpresa), jsonActualizadoP);
            }
        }
        public async Task CrearPiezasEnConjunto(int idEmpresa, int idConjunto, List<Pieza>? piezasNuevas)
        {
            List<Pieza> piezas = _serPiezas.ObtenerPiezasJson(idEmpresa);
            if (piezasNuevas != null && piezasNuevas.Any())
            {
                foreach (Pieza p in piezasNuevas)
                {
                    await _serPiezas.NuevaPieza(idEmpresa, p.Descripcion, p.Codigo, p.Notas, idConjunto, p.ArchivosSubidos);
                }
                string jsonActualizadoP = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                System.IO.File.WriteAllText(_serPiezas.PiezasRutaJson(idEmpresa), jsonActualizadoP);
            }
        }
        public void BorrarConjunto(int idEmpresa, int idConjuntoBorrar)
        {
            List<Conjunto> conjuntos = ObtenerConjuntosJson(idEmpresa);
            foreach (Conjunto c in conjuntos)
            {
                if (c.IdConjunto == idConjuntoBorrar)
                {
                    conjuntos.Remove(c);
                    string jsonActualizado = JsonConvert.SerializeObject(conjuntos, Formatting.Indented);
                    System.IO.File.WriteAllText(ConjuntosRutaJson(idEmpresa), jsonActualizado);
                }
            }

            List<Pieza> piezas = _serPiezas.ObtenerPiezasJson(idEmpresa);
            foreach (Pieza p in piezas)
            {
                if (p.ConjuntoFK == idConjuntoBorrar)
                {
                    p.ConjuntoFK = null;
                }
            }
            string jsonActualizado2 = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            System.IO.File.WriteAllText(_serPiezas.PiezasRutaJson(idEmpresa), jsonActualizado2);
        }


    }
}

*/   
