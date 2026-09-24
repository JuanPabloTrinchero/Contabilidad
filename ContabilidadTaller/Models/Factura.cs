namespace ContabilidadTaller.Models
{
    public enum EstadoFactura
    {
        Impaga = 1,
        Pagada = 2,
        Anulada = 3
    }
    public enum Contiene
    {
        SinDefinir = 0,
        Pedidos = 1,
        Presupuestos = 2,
        OrdenesDeCompra = 3
    }
    
    public class Codigo
    {
        public string Puesto { get; set; }
        public string Numero { get; set; }
        public string GetCodigo()
        {
            return $"{Puesto}{Numero}";
        }
    }

    public class Factura
    {
        public int IdFactura { get; set; }
        public DateOnly Fecha { get; set; }
        public Codigo Codigo { get; set; }
        public Contiene Contiene { get; set; }
        // EN CASO DE NO CONTENER NINGUN OTRO DOCUMENTO
            public string? Descripcion { get; set; }
            public double? MontoManual { get; set; }
        //----------------------------------------------
        public EstadoFactura Estado { get; set; }
        public List<string>? Notas { get; set; }
        public string? UbicacionArchivo { get; set; }
        //FKs
        public List<int>? ItemsRespaldo { get; set; }
        public List<Recepcion>? RecepionesRespaldo { get; set; }
        public int? NotaDeCredito { get; set; }
    }
}
