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
    public class ServiciosPedidos : IServiciosPedidos
    {
        // ----- SERVICIOS (INYECCION DE DEPENDENCIAS) -----
        private readonly ServiciosPresupuestos _serPresupuestos;
        private readonly ServiciosPiezas _serPiezas;
        private readonly ServiciosConjuntos _serConjuntos;
        private readonly ServiciosArchivos _serArchivos;
        public ServiciosPedidos(ServiciosPresupuestos serPresupuestos,
                                ServiciosPiezas serPiezas,
                                ServiciosConjuntos serConjuntos,
                                ServiciosArchivos serArchivos)
        {
            _serPresupuestos = serPresupuestos;
            _serPiezas = serPiezas;
            _serConjuntos = serConjuntos;
            _serArchivos = serArchivos;
        }

        // ----- PEDIDOS -----
        public string PedidosRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Pedidos.json";
        }
        public string PedidosRutaJson_2(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Pedidos\" + idEmpresa.ToString() + @" - Pedidos.json";
        }
        public List<Pedido> ObtenerPedidosJson(int idEmpresa)
        {
            List<Pedido> Pedidos = new List<Pedido>();
            string RutaJsonPedidos = PedidosRutaJson_2(idEmpresa);

            if (System.IO.File.Exists(RutaJsonPedidos))
            {
                if (System.IO.File.ReadAllText(RutaJsonPedidos) != "" || System.IO.File.ReadAllText(RutaJsonPedidos) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonPedidos);
                    Pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonRecuperado);
                }
            }
            return Pedidos;
        }
        public List<Pedido> PedidosOrdenadosPorId(int idEmpresa)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            return pedidos.OrderBy(p => p.IdPedido).ToList();
        }
        public int PrimerIdPedidoFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerPedidosJson(idEmpresa).Select(p => p.IdPedido).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void GuardarNuevoPedido(string descripcion, int cantidad, double precioUnitario, DateOnly fechaEncargo, EstadoPedido estado, List<string> notas, int idEmpresa, int? encargadoPor, int? piezaExistente, int? conjuntoExistente)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = new Pedido();

            p.IdPedido = PrimerIdPedidoFaltante(idEmpresa);
            p.Descripcion = descripcion;

            p.Cantidad = cantidad;
            p.PrecioUnitario = precioUnitario;
            if (fechaEncargo != DateOnly.MinValue)
            {
                p.FechaEncargo = fechaEncargo;
            }
            else
            {
                p.FechaEncargo = DateOnly.FromDateTime(DateTime.Now);
            }

            p.PiezaExistenteFK = piezaExistente;
            p.ConjuntoExistenteFK = conjuntoExistente;
            p.Estado = estado;
            p.Entregas = new List<Entrega>();
            p.Notas = new List<string>();

            if (notas != null && notas.Any())
            {
                p.Notas = notas;
            }

            p.EncargadoPorFK = encargadoPor;

            pedidos.Add(p);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public Pedido BuscarPedido(int idEmpresa, int idPedidoBuscado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido pedido = new Pedido() { IdPedido = -1 };

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedidoBuscado)
                {
                    pedido = p;
                }
            }
            return pedido;
        }
        public void PedidoModificarFechaEncargo(int idEmpresa, int idPedido, DateOnly nuevaFecha)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.FechaEncargo = nuevaFecha;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarCantidad(int idEmpresa, int idPedido, int nuevaCantidad)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Cantidad = nuevaCantidad;

                    int cantEntregagasALaFecha = 0;
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            cantEntregagasALaFecha += e.cantidadEntrega;
                        }
                    }
                    if (cantEntregagasALaFecha == nuevaCantidad)
                    {
                        p.Estado = EstadoPedido.Entregado;
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarDescripcion(int idEmpresa, int idPedido, string nuevaDescripcion)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);

            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Descripcion = nuevaDescripcion;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarPrecioUnitario(int idEmpresa, int idPedido, double nuevoPrecio)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.PrecioUnitario = nuevoPrecio;
                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoCancelar(int idEmpresa, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.Estado = EstadoPedido.Cancelado;

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoReactivar(int idEmpresa, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    if (p.Entregas == null || !p.Entregas.Any())
                    {
                        p.Estado = EstadoPedido.Pendiente;
                    }
                    else
                    {
                        int cantidadEntregadas = 0;
                        foreach (Entrega e in p.Entregas)
                        {
                            cantidadEntregadas += e.cantidadEntrega;
                        }

                        if (cantidadEntregadas == p.Cantidad)
                        {
                            p.Estado = EstadoPedido.Entregado;
                        }
                        else
                        {
                            p.Estado = EstadoPedido.EntregadoParcial;
                        }
                    }

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public void PedidoModificarEncargado(int idEmpresa, int idPedido, int? idEmpleado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    p.EncargadoPorFK = idEmpleado;

                    string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                }
            }
        }
        public List<Pedido> FiltarPedidosPorPresupuesto(int idPresupuesto, int idEmpresa)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosFiltrados = new List<Pedido>();
            foreach (Pedido p in pedidos)
            {
                if (p.PresupuestoFK == idPresupuesto)
                {
                    pedidosFiltrados.Add(p);
                }
            }
            return pedidosFiltrados;
        }
        public List<Pedido> FiltarPedidosSinPresupuesto(int idEmpresa)
        {
            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> filtrados = new List<Pedido>();

            foreach (Pedido p in todos)
            {
                if (p.PresupuestoFK == null)
                {
                    filtrados.Add(p);
                }
            }
            return filtrados;
        }
        public List<Pedido> PedidosSinRemito(int idEmpresa)
        {
            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> sinRemito = new List<Pedido>();
            foreach (Pedido p in todos)
            {
                if (p.RemitoFK == null)
                {
                    sinRemito.Add(p);
                }
            }
            return sinRemito;
        }
        public void BorrarPedido(int idEmpresa, int idPedidoEliminar)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido pe = pedidos.FirstOrDefault(p => p.IdPedido == idPedidoEliminar);

            if (pe.Archivos != null && pe.Archivos.Any())
            {
                foreach (string archivo in pe.Archivos)
                {
                    _serArchivos.BorrarArchivo(idEmpresa, "Pedido", idPedidoEliminar, archivo);
                }
            }

            pedidos.Remove(pe);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void AgregarNotaPedido(int idEmpresa, string notaNueva, int idPedido)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (pedidos != null && pedidos.Any() && !string.IsNullOrEmpty(notaNueva))
            {
                foreach (Pedido p in pedidos)
                {
                    if (p.IdPedido == idPedido)
                    {
                        if (p.Notas == null)
                        {
                            p.Notas = new List<string>();
                            p.Notas.Add(notaNueva);
                        }
                        else
                        {
                            p.Notas.Add(notaNueva);
                        }
                        string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                        File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                    }
                }
            }
        }
        public void ModificarNotaPedido(int idEmpresa, int idPedido, string notaVieja, string notaModificada)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (notaModificada != null)
            {
                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.IdPedido == idPedido)
                    {
                        int indiceNota = pedido.Notas.IndexOf(notaVieja);

                        if (indiceNota != -1)
                        {
                            pedido.Notas[indiceNota] = notaModificada;

                            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                        }
                    }
                }
            }
        }
        public void BorrarNotaPedido(int idEmpresa, int idPedido, string notaBorrar)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            if (pedidos != null && pedidos.Any())
            {
                foreach (Pedido p in pedidos)
                {
                    if (p.IdPedido == idPedido)
                    {
                        if (p.Notas != null && p.Notas.Any())
                        {
                            int indiceNota = p.Notas.IndexOf(notaBorrar);

                            if (indiceNota != -1)
                            {
                                p.Notas.Remove(notaBorrar);
                                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
                            }
                        }
                    }
                }
            }
        }
        public List<Pedido> PedidosDeEmpleado(int idEmpresa, int? idEmpleado)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> encargados = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.EncargadoPorFK == idEmpleado)
                {
                    encargados.Add(p);
                }
            }
            return encargados;
        }
        public List<Pedido> PedidosPorEstado(List<Pedido> pedidos, EstadoPedido estado)
        {
            List<Pedido> pedidosEstado = new List<Pedido>();

            foreach (Pedido p in pedidos)
            {
                if (p.Estado == estado)
                {
                    pedidosEstado.Add(p);
                }
            }
            return pedidosEstado;
        }
        public void AgregarArchivoPedido(int idEmpresa, int idPedido, string rutaArchivo)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);

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
                string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
            }
        }
        public void EliminarArchivoPedido(int idEmpresa, int idPedido, string rutaArchivo)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            Pedido p = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);

            p.Archivos.Remove(rutaArchivo);

            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public List<Pedido> PedidosDeRemito(int idEmpresa, int idRemito)
        {
            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosDeRemito = new List<Pedido>();

            foreach (Pedido p in todos)
            {
                if (p.RemitoFK == idRemito)
                {
                    pedidosDeRemito.Add(p);
                }
            }
            return pedidosDeRemito;
        }
        public List<Pedido> BuscarPiezaEnPedidos(int idEmpresa, string descripcionPieza)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            char[] delimitadores = { ' ', ',', '.', '(', ')', '[', ']', '-', '_', ':', '=', '?', 'Ø', 'ø', 'O' };

            List<Pedido> todos = ObtenerPedidosJson(idEmpresa);
            List<Pedido> coincidencias = new List<Pedido>();

            descripcionPieza.Replace("Ø", "");
            string[] palabras = descripcionPieza.ToLower().Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

            foreach (Pedido pedido in todos)
            {
                foreach (string palabra in palabras)
                {
                    if (pedido.Descripcion.Contains(palabra) && !coincidencias.Contains(pedido))
                    {
                        coincidencias.Add(pedido);
                    }
                }
            }
            return coincidencias;
        }

        // ENTREGAS DE PEDIDOS
        public void NuevaEntregaPedido(int idEmpresa, int idPedido, DateOnly fechaEntrega, int cantidasEntrega, int? idRemito)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    Entrega e = new Entrega();

                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        e.idEntrega = PrimerIdEntregaFaltante(idEmpresa, p);
                    }
                    else
                    {
                        e.idEntrega = 0;
                    }

                    if (fechaEntrega != DateOnly.MinValue)
                    {
                        e.fechaEntrega = fechaEntrega;
                    }
                    else
                    {
                        e.fechaEntrega = DateOnly.FromDateTime(DateTime.Now);
                    }

                    e.cantidadEntrega = cantidasEntrega;

                    if (idRemito != null)
                    {
                        e.idRemitoFK = idRemito;
                    }
                    else
                    {
                        e.idRemitoFK = null;
                    }

                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        p.Entregas.Add(e);
                    }
                    else
                    {
                        p.Entregas = new List<Entrega>() { e };
                    }

                    int cantidadActual = 0;
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega entrega in p.Entregas)
                        {
                            cantidadActual += entrega.cantidadEntrega;
                        }
                    }

                    if (cantidadActual == p.Cantidad)
                    {
                        p.Estado = EstadoPedido.Entregado;
                    }
                    else
                    {
                        p.Estado = EstadoPedido.EntregadoParcial;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public int PrimerIdEntregaFaltante(int idEmpresa, Pedido pedido)
        {
            List<int> ids = pedido.Entregas.Select(e => e.idEntrega).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void AsignarRemitoAEntrega(int idEmpresa, int idPedido, int idRemito, int idEntrega)
        {
            List<Pedido> pedidos = ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.IdPedido == idPedido)
                {
                    if (p.Entregas != null && p.Entregas.Any())
                    {
                        foreach (Entrega e in p.Entregas)
                        {
                            if (e.idEntrega == idEntrega)
                            {
                                e.idRemitoFK = idRemito;
                            }
                        }
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }

    }
}

*/
