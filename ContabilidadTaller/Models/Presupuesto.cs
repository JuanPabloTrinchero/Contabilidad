namespace ContabilidadTaller.Models
{
    public enum EstadoPresupuesto { Borrador = 0, Confirmado = 1, Cancelado = 2}
    public class EstadoEnvio()
    {
        public bool enviado;
        public List<Envio>? envios;
    }
    public class Envio()
    {
        public int idEnvio;
        public DateOnly fechaEnvio;
        public List<int>? empleados;
    }
    public class Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public int Numero { get; set; }
        public DateOnly Fecha { get; set; }
        public double? Monto { get; set; }
        public List<string>? Notas { get; set; }
        public List<string>? Archivos { get; set; }
        public EstadoPresupuesto Estado { get; set; }
        public List<int>? CopiaPedidos { get; set; }
        public EstadoEnvio EstadoEnvio { get; set; }
        //FKs
        // Orden de Compra o Factura?
            public int? OrdenDeCompraFK { get; set; }
            public int? FacturaFK { get; set; }
        public int? SocioFK { get; set; }
    }
}
