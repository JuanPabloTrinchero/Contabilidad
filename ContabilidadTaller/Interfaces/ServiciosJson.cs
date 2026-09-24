
using Microsoft.AspNetCore.Mvc;

namespace ContabilidadTaller.Interfaces
{
    public interface IServiciosJson
    {
        // ----- EMPRESAS -----
        string EmpresasRutaJson();
        void ModificarDireccionEmpresa(int id, string nuevaDireccion);
        void ModificarNotaEmpresa(int id, string notaVieja, string nuevaNota);

        // ----- EMPLEADOS -----
        //List<Empleado> ObtenerEmpleadosJson();

        // ----- PIEZAS -----
        //List<Pieza> ObtenerPiezaJson();

        // ----- PEDIDOS -----
        //List<Pedido> ObtenerPedidosJson();

        // ----- REMITOS -----
        //List<Remito> ObtenerRemitoJson();

        // ----- PRESUPUESTOS -----
        public string PresupuestosRutaJson(int idEmpresa);
        

        // ----- ORDENES DE COMPRA -----
        //List<OrdenDeCompra> ObtenerOrdenDeCompraJson();

        // ----- FACTURAS -----
        //List<Factura> ObtenerFacturasJson();

        // ----- NOTAS DE CREDITO -----
        //List<NotaDeCredito> ObtenerNotasDeCreditoJson();
    }
}
