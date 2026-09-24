namespace ContabilidadTaller.Models
{
    public enum EstadoPedido { Pendiente = 0, Entregado = 1, EntregadoParcial = 2, Cancelado = 3 }
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double? PrecioUnitario { get; set; }
        public DateOnly FechaEncargo { get; set; }
        public EstadoPedido Estado { get; set; }
        public List<Entrega>? Entregas { get; set; }
        public List<string>? Archivos { get; set; }
        public List<string>? Notas { get; set; }
        //FKs
        public int? EncargadoPorFK { get; set; }
        public int? PiezaExistenteFK { get; set; }
        public int? ConjuntoExistenteFK { get; set; }
            //Presupuesto o Factura?
            public int? PresupuestoFK { get; set; }
            public int? FacturaFK { get; set; }
        //public int? RemitoFK { get; set; }


        // public int? SocioFK { get; set; }
    }
}
