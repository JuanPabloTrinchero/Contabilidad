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
    
    public class ServiciosFacturas : IServiciosFacturas
    {
        private readonly ServiciosFacturas _serFacturas;
        private readonly ServiciosPedidos _serPedidos;
        private readonly ServiciosPresupuestos _serPresupuestos;
        private readonly ServiciosOrdenesDeCompra _serOrdenesDeCompra;
        private readonly ServiciosNotasDeCredito _serNotasDeCredito;
        private readonly ServiciosPiezas _serPiezas;
        private readonly ServiciosConjuntos _serConjuntos;
        private readonly ServiciosEmpleados _serEmpleados;
        public ServiciosFacturas(ServiciosFacturas serFacturas,
                                    ServiciosPedidos serPedidos,
                                    ServiciosPresupuestos serPresupuestos,
                                    ServiciosOrdenesDeCompra serOrdenesDeCompra,
                                    ServiciosNotasDeCredito serNotasDeCredito,
                                    ServiciosPiezas serPiezas,
                                    ServiciosConjuntos serConjuntos,
                                    ServiciosEmpleados serEmpleados)
        {
            _serFacturas = serFacturas;
            _serPedidos = serPedidos;
            _serPresupuestos = serPresupuestos;
            _serOrdenesDeCompra = serOrdenesDeCompra;
            _serNotasDeCredito = serNotasDeCredito;
            _serPiezas = serPiezas;
            _serConjuntos = serConjuntos;
            _serEmpleados = serEmpleados;
        }
        // ----- FACTURAS -----
        public string FacturasRutaJson(int idEmpresa)
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\Empresas\" + idEmpresa.ToString() + @"\" + idEmpresa.ToString() + @" - Facturas\" + idEmpresa.ToString() + @" - Facturas.json";
        }
        public List<Factura> ObtenerFacturasJson(int idEmpresa)
        {
            List<Factura> Facturas = new List<Factura>();
            string RutaJsonFacturas = FacturasRutaJson(idEmpresa);

            if (System.IO.File.Exists(RutaJsonFacturas))
            {
                if (System.IO.File.ReadAllText(RutaJsonFacturas) != "" || System.IO.File.ReadAllText(RutaJsonFacturas) != "[]")
                {
                    string jsonRecuperado = System.IO.File.ReadAllText(RutaJsonFacturas);
                    Facturas = JsonConvert.DeserializeObject<List<Factura>>(jsonRecuperado);
                }
            }
            return Facturas;
        }
        public Factura ElegirFactura(int idEmpresa, int idFactura)
        {
            List<Factura> todas = ObtenerFacturasJson(idEmpresa);
            Factura factura = new Factura();

            foreach (Factura f in todas)
            {
                if (f.IdFactura == idFactura)
                {
                    factura = f;
                }
            }
            return factura;
        }
        public int PrimerIdFacturaFaltante(int idEmpresa)
        {
            List<int> ids = ObtenerFacturasJson(idEmpresa).Select(f => f.IdFactura).OrderBy(id => id).ToList();
            for (int i = 0; i <= ids.Count(); i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
            return ids.Count;
        }
        public void GuardarFactura(int idEmpresa, int idFactura, DateOnly fecha, string puesto, string numero, double montoManual, Contiene contiene)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura f = new Factura();

            f.IdFactura = idFactura;
            if (fecha != DateOnly.MinValue)
            {
                f.Fecha = fecha;
            }
            else
            {
                f.Fecha = DateOnly.FromDateTime(DateTime.Now);
            }
            f.Codigo = new Codigo()
            {
                Puesto = puesto,
                Numero = numero
            };

            if (montoManual != 0)
            {
                f.MontoManual = montoManual;
            }
            else
            {
                f.MontoManual = null;
            }

            f.Estado = EstadoFactura.Impaga;
            f.Notas = new List<string>();

            f.Contiene = contiene;
            f.ItemsRespaldo = new List<int>();

            facturas.Add(f);

            string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
            File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
        }
        public void AgregarArchivoAFactura(int idEmpresa, int idFactura, string rutaArchivo)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura f = facturas.FirstOrDefault(x => x.IdFactura == idFactura);

            if (f != null)
            {
                if (f.UbicacionArchivo == null || f.UbicacionArchivo == "")
                {
                    f.UbicacionArchivo = rutaArchivo;
                }
                string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
            }
        }
        public Factura FacturaDePedido(int idEmpresa, Pedido pedido)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            Factura facturaDePedido = new Factura() { IdFactura = -1 };

            foreach (Factura f in facturas)
            {
                if (f.IdFactura == pedido.FacturaFK)
                {
                    facturaDePedido = f;
                }
            }
            return facturaDePedido;
        }
        public bool ModificarCodigoFactura(int idEmpresa, int idFactura, string puesto, string numero)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            bool codigoConfirmado = true;

            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    Codigo nuevoCodigo = new Codigo() { Puesto = puesto, Numero = numero };

                    foreach (Factura fa in facturas)
                    {
                        if (fa.Codigo.Puesto == nuevoCodigo.Puesto && fa.Codigo.Numero == nuevoCodigo.Numero)
                        {
                            codigoConfirmado = false;
                        }
                    }
                    if (codigoConfirmado == true)
                    {
                        f.Codigo = nuevoCodigo;
                        string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                        File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                    }
                }
                break;
            }
            return codigoConfirmado;
        }
        public void ModificarFechaFactura(int idEmpresa, int idFactura, DateOnly fechaNueva)
        {
            List<Factura> facturas = ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFactura)
                {
                    f.Fecha = fechaNueva;

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(FacturasRutaJson(idEmpresa), jsonActualizado);
                }
            }
        }
        public void AsignarPresupuestosAFactura(int idEmpresa, int idFactura, List<int> items)
        {
            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                foreach (int item in items)
                {
                    if (p.IdPresupuesto == item)
                    {
                        p.FacturaFK = idFactura;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            System.IO.File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public void BorrarPresupuestosDeFactura(int idEmpresa, int idFactura)
        {
            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
            foreach (Presupuesto p in presupuestos)
            {
                if (p.FacturaFK == idFactura)
                {
                    p.FacturaFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
            System.IO.File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizado);
        }
        public void AnularDocumentosEnFactura(int idEmpresa, int idFacturaAnular)
        {
            List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaAnular)
                {
                    switch (f.Contiene)
                    {
                        case Contiene.Pedidos:
                            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
                            foreach (Pedido pe in pedidos)
                            {
                                if (pe.FacturaFK == idFacturaAnular)
                                {
                                    pe.FacturaFK = null;
                                    f.ItemsRespaldo.Add(pe.IdPedido);
                                }
                            }
                            string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            System.IO.File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                            break;
                        case Contiene.Presupuestos:
                            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
                            foreach (Presupuesto pr in presupuestos)
                            {
                                if (pr.FacturaFK == idFacturaAnular)
                                {
                                    pr.FacturaFK = null;
                                    f.ItemsRespaldo.Add(pr.IdPresupuesto);
                                }
                            }
                            string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            System.IO.File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                            break;
                        case Contiene.OrdenesDeCompra:
                            List<OrdenDeCompra> odcs = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
                            foreach (OrdenDeCompra odc in odcs)
                            {
                                if (odc.FacturaFK == idFacturaAnular)
                                {
                                    odc.FacturaFK = null;
                                    f.ItemsRespaldo.Add(odc.IdOC);
                                }
                            }
                            string jsonActualizadoODCs = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                            System.IO.File.WriteAllText(_serOrdenesDeCompra.OrdenesDeCompraRutaJson(idEmpresa), jsonActualizadoODCs);
                            break;
                    }
                }
            }
        }
        public void ReactivarFactura(int idEmpresa, int idFacturaActivar)
        {
            List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaActivar)
                {
                    if (f.Estado == EstadoFactura.Anulada)
                    {
                        switch (f.Contiene)
                        {
                            case Contiene.Pedidos:
                                List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);

                                int pedidosRespaldadosPe = f.ItemsRespaldo.Count();
                                int pedidosSinReasignarPe = 0;

                                foreach (int item in f.ItemsRespaldo)
                                {
                                    foreach (Pedido p in pedidos)
                                    {
                                        if (item == p.IdPedido && p.FacturaFK == null)
                                        {
                                            pedidosSinReasignarPe++;
                                        }
                                    }
                                }

                                if (pedidosRespaldadosPe == pedidosSinReasignarPe)
                                {
                                    foreach (int item in f.ItemsRespaldo)
                                    {
                                        foreach (Pedido p in pedidos)
                                        {
                                            if (item == p.IdPedido && p.FacturaFK == null)
                                            {
                                                p.FacturaFK = idFacturaActivar;
                                            }
                                        }
                                    }
                                    string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                                    System.IO.File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                                }
                                break;
                            case Contiene.Presupuestos:
                                List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);

                                int pedidosRespaldadosPr = f.ItemsRespaldo.Count();
                                int pedidosSinReasignarPr = 0;

                                foreach (int item in f.ItemsRespaldo)
                                {
                                    foreach (Presupuesto pr in presupuestos)
                                    {
                                        if (item == pr.IdPresupuesto && pr.FacturaFK == null)
                                        {
                                            pedidosSinReasignarPr++;
                                        }
                                    }
                                }

                                if (pedidosRespaldadosPr == pedidosSinReasignarPr)
                                {
                                    foreach (int item in f.ItemsRespaldo)
                                    {
                                        foreach (Presupuesto pr in presupuestos)
                                        {
                                            if (item == pr.IdPresupuesto && pr.FacturaFK == null)
                                            {
                                                pr.FacturaFK = idFacturaActivar;
                                            }
                                        }
                                    }
                                    string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                                    System.IO.File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                                }

                                break;
                            case Contiene.OrdenesDeCompra:
                                List<OrdenDeCompra> odcs = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);

                                int pedidosRespaldadoODC = f.ItemsRespaldo.Count();
                                int pedidosSinReasignarODC = 0;

                                foreach (int item in f.ItemsRespaldo)
                                {
                                    foreach (OrdenDeCompra odc in odcs)
                                    {
                                        if (item == odc.IdOC && odc.FacturaFK == null)
                                        {
                                            pedidosSinReasignarODC++;
                                        }
                                    }
                                }

                                if (pedidosRespaldadoODC == pedidosSinReasignarODC)
                                {
                                    foreach (int item in f.ItemsRespaldo)
                                    {
                                        foreach (OrdenDeCompra odc in odcs)
                                        {
                                            if (item == odc.IdOC && odc.FacturaFK == null)
                                            {
                                                odc.FacturaFK = idFacturaActivar;
                                            }
                                        }
                                    }
                                    string jsonActualizadoODCs = JsonConvert.SerializeObject(odcs, Formatting.Indented);
                                    System.IO.File.WriteAllText(_serOrdenesDeCompra.OrdenesDeCompraRutaJson(idEmpresa), jsonActualizadoODCs);
                                }
                                break;
                        }
                    }
                }
            }
        }
        // (SOLO SE REACTIVA SI TODOS LOS PEDIDOS DE RESAPALDO AUN NO SE HAN ASIGNADO A UNA NUEVA FACTURA)
        public List<Presupuesto> PresupuestosEnFactura(int idEmpresa, int idFactura)
        {
            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
            List<Presupuesto> presupuestosEnFactura = new List<Presupuesto>();
            foreach (Presupuesto p in presupuestos)
            {
                if (p.FacturaFK == idFactura)
                {
                    presupuestosEnFactura.Add(p);
                }
            }
            return presupuestosEnFactura;
        }
        public void AsignarPedidosAFactura(int idEmpresa, int idFactura, List<int> items)
        {
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                foreach (int item in items)
                {
                    if (p.IdPedido == item)
                    {
                        p.FacturaFK = idFactura;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            System.IO.File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public void BorrarPedidosDeFactura(int idEmpresa, int idFactura)
        {
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            foreach (Pedido p in pedidos)
            {
                if (p.FacturaFK == idFactura)
                {
                    p.FacturaFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            System.IO.File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizado);
        }
        public List<Pedido> PedidosEnFactura(int idEmpresa, int idFactura)
        {
            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
            List<Pedido> pedidosEnFactura = new List<Pedido>();
            foreach (Pedido p in pedidos)
            {
                if (p.FacturaFK == idFactura)
                {
                    pedidosEnFactura.Add(p);
                }
            }
            return pedidosEnFactura;
        }
        public void AsignarOrdenesDeCompraAFactura(int idEmpresa, int idFactura, List<int> items)
        {
            List<OrdenDeCompra> odcs = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                foreach (int item in items)
                {
                    if (odc.IdOC == item)
                    {
                        odc.FacturaFK = idFactura;
                    }
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            System.IO.File.WriteAllText(_serOrdenesDeCompra.OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
        }
        public void BorrarOrdenesDeCompraDeFactura(int idEmpresa, int idFactura)
        {
            List<OrdenDeCompra> odcs = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.FacturaFK == idFactura)
                {
                    odc.FacturaFK = null;
                }
            }
            string jsonActualizado = JsonConvert.SerializeObject(odcs, Formatting.Indented);
            System.IO.File.WriteAllText(_serOrdenesDeCompra.OrdenesDeCompraRutaJson(idEmpresa), jsonActualizado);
        }
        public List<OrdenDeCompra> OrdenesDeCompraEnFactura(int idEmpresa, int idFactura)
        {
            List<OrdenDeCompra> odcs = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
            List<OrdenDeCompra> odcDeFactura = new List<OrdenDeCompra>();
            foreach (OrdenDeCompra odc in odcs)
            {
                if (odc.FacturaFK == idFactura)
                {
                    odcDeFactura.Add(odc);
                }
            }
            return odcDeFactura;
        }
        public void AnularFactura(int idEmpresa, int idFacturaAnular)
        {
            List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaAnular)
                {
                    if (f.Estado != EstadoFactura.Anulada)
                    {
                        f.Estado = EstadoFactura.Anulada;

                        string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                        System.IO.File.WriteAllText(_serFacturas.FacturasRutaJson(idEmpresa), jsonActualizado);

                        AnularDocumentosEnFactura(idEmpresa, f.IdFactura);
                    }
                }
            }
        }
        public void BorrarFactura(int idEmpresa, int idFacturaBorrar)
        {
            List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
            foreach (Factura f in facturas)
            {
                if (f.IdFactura == idFacturaBorrar)
                {
                    facturas.Remove(f);

                    string jsonActualizado = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    System.IO.File.WriteAllText(_serFacturas.FacturasRutaJson(idEmpresa), jsonActualizado);

                    AnularDocumentosEnFactura(idEmpresa, f.IdFactura);
                }
            }
        }
    }
}

*/  
