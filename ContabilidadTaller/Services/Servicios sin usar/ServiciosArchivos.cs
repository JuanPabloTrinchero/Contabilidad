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
    public class ServiciosArchivos : IServiciosArchivos
    {
        // ----- SERVICIOS (INYECCION DE DEPENDENCIAS) -----
        private readonly ServiciosRemitos _serRemitos;
        private readonly ServiciosPresupuestos _serPresupuestos;
        private readonly ServiciosPiezas _serPiezas;
        private readonly ServiciosPedidos _serPedidos;
        private readonly ServiciosOrdenesDeCompra _serOrdenesDeCompra;
        private readonly ServiciosNotasDeCredito _serNotasDeCredito;
        private readonly ServiciosFacturas _serFacturas;
        private readonly ServiciosCostos _serCostos;
        public ServiciosArchivos(ServiciosRemitos serRemitos, ServiciosPresupuestos serPresupuestos, ServiciosPiezas serPiezas, ServiciosPedidos serPedidos, ServiciosOrdenesDeCompra serOrdenesDeCompra, ServiciosNotasDeCredito serNotasDeCredito, ServiciosFacturas serFacturas, ServiciosCostos serCostos)
        {
            _serRemitos = serRemitos;
            _serPresupuestos = serPresupuestos;
            _serPiezas = serPiezas;
            _serPedidos = serPedidos;
            _serOrdenesDeCompra = serOrdenesDeCompra;
            _serNotasDeCredito = serNotasDeCredito;
            _serFacturas = serFacturas;
            _serCostos = serCostos;
        }
        // ----- ARCHIVOS -----
        public void AbrirArchivoEnSuUbicacion(string rutaArchivo)
        {
            string rutaCompleta = Directory.GetCurrentDirectory() + "\\wwwroot" + rutaArchivo;
            if (File.Exists(rutaCompleta))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{rutaCompleta}\"",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start(startInfo);
            }
        }
        public async Task GuardarArchivos(List<IFormFile> archivos, int idEmpresa, string nombreElemento, int idElemento)
        {
            if (archivos != null && archivos.Any())
            {
                // GUARDAR LOS ARCHIVOS EN LAS CARPETAS CORRESPONDIENTES
                foreach (IFormFile archivo in archivos)
                {
                    string carpetaRelativa = "";

                    switch (nombreElemento)
                    {
                        case "Costo":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Costos");
                            break;
                        case "Factura":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Facturas");
                            break;
                        case "NotaDeCredito":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Notas de Credito");
                            break;
                        case "OrdenDeCompra":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Ordenes de Compra");
                            break;
                        case "Pedido":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Pedidos");
                            break;
                        case "Pieza":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Piezas");
                            break;
                        case "Presupuesto":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Presupuestos");
                            break;
                        case "Remito":
                            carpetaRelativa = Path.Combine("Empresas", idEmpresa.ToString(), $"{idEmpresa} - Remitos");
                            break;
                    }

                    string carpetaAbsoluta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", carpetaRelativa);

                    if (!Directory.Exists(carpetaAbsoluta))
                        Directory.CreateDirectory(carpetaAbsoluta);

                    string nombreArchivo = archivo.FileName;
                    string rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombreArchivo);

                    if (System.IO.File.Exists(rutaAbsoluta))
                    {
                        string nombreOriginal = Path.GetFileNameWithoutExtension(nombreArchivo);
                        string extension = Path.GetExtension(nombreArchivo);
                        int contador = 1;

                        // EN CASO DE NOMBRE EXISTENTE AGREGARLE NUMERO (1, 2, 3, ...)
                        while (System.IO.File.Exists(rutaAbsoluta))
                        {
                            nombreArchivo = $"{nombreOriginal}({contador}){extension}";
                            rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombreArchivo);
                            contador++;
                        }
                    }

                    using (var stream = new FileStream(rutaAbsoluta, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }

                    // GUARDAR LAS RUTAS EN LOS JSON
                    string rutaParaJson = Path.Combine("\\", carpetaRelativa, nombreArchivo);

                    switch (nombreElemento)
                    {
                        case "Costo":
                            // 1 solo archivo
                            List<Costo> costos = _serCostos.ObtenerCostosJson(idEmpresa);
                            foreach (Costo c in costos)
                            {
                                if (c.IdCosto == idElemento)
                                {
                                    c.Archivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoCostos = JsonConvert.SerializeObject(costos, Formatting.Indented);
                            File.WriteAllText(_serCostos.CostosRutaJson(idEmpresa), jsonActualizadoCostos);
                            break;
                        case "Factura":
                            // 1 solo archivo
                            List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
                            foreach (Factura f in facturas)
                            {
                                if (f.IdFactura == idElemento)
                                {
                                    f.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoFacturas = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                            File.WriteAllText(_serFacturas.FacturasRutaJson(idEmpresa), jsonActualizadoFacturas);
                            break;
                        case "NotaDeCredito":
                            // 1 solo archivo
                            List<NotaDeCredito> notasDeCredito = _serNotasDeCredito.ObtenerNotasDeCreditoJson(idEmpresa);
                            foreach (NotaDeCredito ndc in notasDeCredito)
                            {
                                if (ndc.IdNotaCredito == idElemento)
                                {
                                    ndc.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoNotasDeCredito = JsonConvert.SerializeObject(notasDeCredito, Formatting.Indented);
                            File.WriteAllText(_serNotasDeCredito.NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoNotasDeCredito);
                            break;
                        case "OrdenDeCompra":
                            // 1 solo archivo
                            List<OrdenDeCompra> ordenesDeCompra = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
                            foreach (OrdenDeCompra odc in ordenesDeCompra)
                            {
                                if (odc.IdOC == idElemento)
                                {
                                    odc.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoOrdenesDeCompra = JsonConvert.SerializeObject(ordenesDeCompra, Formatting.Indented);
                            File.WriteAllText(_serNotasDeCredito.NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoOrdenesDeCompra);
                            break;
                        case "Pedido":
                            // varios archivos
                            List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
                            foreach (Pedido p in pedidos)
                            {
                                if (p.IdPedido == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                            File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                            break;
                        case "Pieza":
                            // varios archivos
                            List<Pieza> piezas = _serPiezas.ObtenerPiezasJson(idEmpresa);
                            foreach (Pieza p in piezas)
                            {
                                if (p.IdPieza == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoPiezas = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                            File.WriteAllText(_serPiezas.PiezasRutaJson(idEmpresa), jsonActualizadoPiezas);
                            break;
                        case "Presupuesto":
                            // varios archivos
                            List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
                            foreach (Presupuesto p in presupuestos)
                            {
                                if (p.IdPresupuesto == idElemento)
                                {
                                    if (p.Archivos != null && p.Archivos.Any())
                                    {
                                        p.Archivos.Add(rutaParaJson);
                                    }
                                    else
                                    {
                                        p.Archivos = new List<string>();
                                        p.Archivos.Add(rutaParaJson);

                                    }
                                }
                            }
                            string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                            File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                            break;
                        case "Remito":
                            // 1 solo archivo
                            List<Remito> remitos = _serRemitos.ObtenerRemitosJson(idEmpresa);
                            foreach (Remito r in remitos)
                            {
                                if (r.IdRemito == idElemento)
                                {
                                    r.UbicacionArchivo = rutaParaJson;
                                }
                            }
                            string jsonActualizadoRemitos = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                            File.WriteAllText(_serRemitos.RemitosRutaJson(idEmpresa), jsonActualizadoRemitos);
                            break;
                    }


                }
            }
        }
        public void BorrarArchivo(int idEmpresa, string nombreElemento, int idElemento, string rutaArchivo)
        {
            // BORRAR ARCHIVO
            string rutaCompleta = Directory.GetCurrentDirectory() + "\\wwwroot" + rutaArchivo;
            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }

            // BORRAR RUTA DEL JSON
            switch (nombreElemento)
            {
                case "Costo":
                    List<Costo> costos = _serCostos.ObtenerCostosJson(idEmpresa);
                    foreach (Costo c in costos)
                    {
                        if (c.IdCosto == idElemento)
                        {
                            c.Archivo = null;
                        }
                    }
                    string jsonActualizadoCostos = JsonConvert.SerializeObject(costos, Formatting.Indented);
                    File.WriteAllText(_serCostos.CostosRutaJson(idEmpresa), jsonActualizadoCostos);
                    break;
                case "Factura":
                    List<Factura> facturas = _serFacturas.ObtenerFacturasJson(idEmpresa);
                    foreach (Factura f in facturas)
                    {
                        if (f.IdFactura == idElemento)
                        {
                            f.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoFacturas = JsonConvert.SerializeObject(facturas, Formatting.Indented);
                    File.WriteAllText(_serFacturas.FacturasRutaJson(idEmpresa), jsonActualizadoFacturas);
                    break;
                case "NotaDeCredito":
                    List<NotaDeCredito> notasDeCredito = _serNotasDeCredito.ObtenerNotasDeCreditoJson(idEmpresa);
                    foreach (NotaDeCredito ndc in notasDeCredito)
                    {
                        if (ndc.IdNotaCredito == idElemento)
                        {
                            ndc.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoNotasDeCredito = JsonConvert.SerializeObject(notasDeCredito, Formatting.Indented);
                    File.WriteAllText(_serNotasDeCredito.NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoNotasDeCredito);
                    break;
                case "OrdenDeCompra":
                    List<OrdenDeCompra> ordenesDeCompra = _serOrdenesDeCompra.ObtenerOrdenesDeCompraJson(idEmpresa);
                    foreach (OrdenDeCompra odc in ordenesDeCompra)
                    {
                        if (odc.IdOC == idElemento)
                        {
                            odc.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoOrdenesDeCompra = JsonConvert.SerializeObject(ordenesDeCompra, Formatting.Indented);
                    File.WriteAllText(_serNotasDeCredito.NotasDeCreditoRutaJson(idEmpresa), jsonActualizadoOrdenesDeCompra);
                    break;
                case "Pedido":
                    List<Pedido> pedidos = _serPedidos.ObtenerPedidosJson(idEmpresa);
                    foreach (Pedido p in pedidos)
                    {
                        if (p.IdPedido == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPedidos = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                    File.WriteAllText(_serPedidos.PedidosRutaJson_2(idEmpresa), jsonActualizadoPedidos);
                    break;
                case "Pieza":
                    List<Pieza> piezas = _serPiezas.ObtenerPiezasJson(idEmpresa);
                    foreach (Pieza p in piezas)
                    {
                        if (p.IdPieza == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPiezas = JsonConvert.SerializeObject(piezas, Formatting.Indented);
                    File.WriteAllText(_serPiezas.PiezasRutaJson(idEmpresa), jsonActualizadoPiezas);
                    break;
                case "Presupuesto":
                    List<Presupuesto> presupuestos = _serPresupuestos.ObtenerPresupuestosJson(idEmpresa);
                    foreach (Presupuesto p in presupuestos)
                    {
                        if (p.IdPresupuesto == idElemento)
                        {
                            if (p.Archivos != null && p.Archivos.Any())
                            {
                                p.Archivos.Remove(rutaArchivo);
                            }
                        }
                    }
                    string jsonActualizadoPresupuestos = JsonConvert.SerializeObject(presupuestos, Formatting.Indented);
                    File.WriteAllText(_serPresupuestos.PresupuestosRutaJson(idEmpresa), jsonActualizadoPresupuestos);
                    break;
                case "Remito":
                    List<Remito> remitos = _serRemitos.ObtenerRemitosJson(idEmpresa);
                    foreach (Remito r in remitos)
                    {
                        if (r.IdRemito == idElemento)
                        {
                            r.UbicacionArchivo = null;
                        }
                    }
                    string jsonActualizadoRemitos = JsonConvert.SerializeObject(remitos, Formatting.Indented);
                    File.WriteAllText(_serRemitos.RemitosRutaJson(idEmpresa), jsonActualizadoRemitos);
                    break;
            }
        }

    }
}

    
*/