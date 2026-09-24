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
    public class ServiciosRemitos : IServiciosRemitos
    {
        // ----- SERVICIOS (INYECCION DE DEPENDENCIAS) -----
        private readonly ServiciosPedidos _serPedidos;
        public ServiciosRemitos(ServiciosPedidos serPedidos)
        {
            _serPedidos = serPedidos;
        }

        // ----- REMITOS -----
        public string RemitosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Remitos\" + idEmpresa.ToString() + @" - Remitos.json";
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
        public Remito BuscarRemito(int idEmpresa, int idRemito)
        {
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
        public Remito RemitoDePedido(int idEmpresa, Pedido pedido)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            Remito remito = new Remito() { IdRemito = -1 };

            foreach (Remito r in remitos)
            {
                if (r.IdRemito == pedido.RemitoFK)
                {
                    remito = r;
                }
            }
            return remito;
        }
        public int SiguienteNumeroRemito(List<Remito> remitos)
        {
            return remitos.Any() ? remitos.Max(x => x.NumeroRemito) + 1 : 1;
        }
        public void GuardarNuevoRemito(int idEmpresa, int numeroRemito, DateOnly fecha, List<string> notas, int? EntregadoA)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            Remito r = new Remito();

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
        public void BorrarRemito(int idEmpresa, int idRemitoEliminar)
        {
            List<Remito> remitos = ObtenerRemitosJson(idEmpresa);
            if (remitos != null && remitos.Any())
            {
                int eliminados = remitos.RemoveAll(r => r.IdRemito == idRemitoEliminar);

                if (eliminados > 0)
                {
                    string jsonActualizado = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(RemitosRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void AgregarItemsARemito(int idEmpresa, int idRemito, List<int> items)
        {
            Remito r = BuscarRemito(idEmpresa, idRemito);
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                foreach (int item in items)
                {
                    if (p.IdPedido == item)
                    {
                        p.RemitoFK = r.IdRemito;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void BorrarItemsRemito(int idEmpresa, int idPedidoBorrar)
        {
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedidoBorrar)
                {
                    p.RemitoFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
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
        public void AsignarPedidosARemito(int idEmpresa, int idRemito, List<int> idsItems)
        {
            List<Pedido> todos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in todos)
            {
                foreach (int item in idsItems)
                {
                    if (p.IdPedido == item)
                    {
                        p.RemitoFK = idRemito;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(todos, Formatting.Indented);
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }

    }
}

*/
    
