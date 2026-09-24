namespace ContabilidadTaller.Models
{
    public class PedidoEntregas
    {
        public int IdPedido { get; set; }
        public List<Entrega>? Entregas { get; set; }
    }
}
