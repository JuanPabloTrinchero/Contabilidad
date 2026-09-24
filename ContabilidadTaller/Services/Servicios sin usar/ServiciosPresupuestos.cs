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
    public class ServiciosPresupuestos : IServiciosPresupuestos
    {
        private readonly ServiciosPedidos _serPedidos;
        private readonly ServiciosConjuntos _serConjuntos;
        private readonly ServiciosArchivos _serArchivos;
        private readonly ServiciosPresupuestos _serPresupuestos;
        public ServiciosPresupuestos(ServiciosPedidos serPedidos, ServiciosConjuntos serConjuntos, ServiciosArchivos serArchivos, ServiciosPresupuestos serPresupuestos)
        {
            _serPedidos = serPedidos;
            _serConjuntos = serConjuntos;
            _serArchivos = serArchivos;
            _serPresupuestos = serPresupuestos;
        }

        // ----- PRESUPUESTOS -----
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
        public int SiguienteNumeroPresupuesto(List<Presupuesto> presupuestos)
        {
            return presupuestos.Any() ? presupuestos.Max(x => x.Numero) + 1 : 1;
        }
        public int SiguienteIdPresupuesto(List<Presupuesto> presupuestos)
        {
            return presupuestos.Any() ? presupuestos.Max(x => x.IdPresupuesto) + 1 : 0;
        }
        public Presupuesto ElegirPresupuesto(int idPresupuesto, int idEmpresa)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            return presupuestos.FirstOrDefault(p => p.IdPresupuesto == idPresupuesto);
        }
        public void GuardarNuevoPresupuesto(int idEmpresa, DateOnly fecha, List<int> idItems, int idPresupuesto, int numeroPresupuesto, List<string>? notas, Socio? socio)
        {
            if (File.Exists(PresupuestosRutaJson(idEmpresa)))
            {
                List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
                Presupuesto pNuevo = new Presupuesto();

                pNuevo.IdPresupuesto = presupuestos.Any() ? presupuestos.Max(x => x.IdPresupuesto) + 1 : 0;

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
                pNuevo.idPresupuestoReemplazo = null;
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
                List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
                foreach (Pedido pedido in pedidos)
                {
                    if (idItems.Contains(pedido.IdPedido))
                    {
                        pedido.PresupuestoFK = idPresupuesto;
                    }
                }
                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
            }
        }
        public void EliminarItemDePresupuesto(int idEmpresa, int idPedido, int idPresupuesto)
        {
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.PresupuestoFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
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
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            List<Conjunto> conjuntos = _serConjuntos.ObtenerConjuntosJson(idEmpresa);

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
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado2);
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
                    List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
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
                        File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado2);

                        p.CopiaPedidos = new List<int>();
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public List<Presupuesto> PresupuestosSinOC(int idEmpresa)
        {
            List<Presupuesto> todos = ObtenerPresupuestosJson(idEmpresa);
            List<Presupuesto> sinOC = new List<Presupuesto>();
            foreach (Presupuesto p in todos)
            {
                if (p.OrdenDeCompraFK == null)
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
        public void BorrarPresupuesto(int idEmpresa, int idPresupuestoEliminar)
        {
            List<Presupuesto> presupuestos = ObtenerPresupuestosJson(idEmpresa);
            Presupuesto pr = presupuestos.FirstOrDefault(p => p.IdPresupuesto == idPresupuestoEliminar);

            if (pr.Archivos != null && pr.Archivos.Any())
            {
                foreach (string archivo in pr.Archivos)
                {
                    _serArchivos.BorrarArchivo(idEmpresa, "Presupuesto", pr.IdPresupuesto, archivo);
                }
            }

            presupuestos.Remove(pr);

            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            File.WriteAllText(PresupuestosRutaJson(idEmpresa), jsonActualizado);

            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido pe in pedidos)
            {
                if (pe.PresupuestoFK == idPresupuestoEliminar)
                {
                    pe.PresupuestoFK = null;
                }
            }
            string jsonActualizado2 = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado2);
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
        public Presupuesto PresupuestoDePedido(int idEmpresa, Pedido pedido)
        {
            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
            Presupuesto estePresupuesto = new Presupuesto() { IdPresupuesto = -1 };
            foreach (Presupuesto p in presupuestos)
            {
                if (p.IdPresupuesto == pedido.PresupuestoFK)
                {
                    estePresupuesto = p;
                }
            }
            return estePresupuesto;
        }
    }
}

*/
