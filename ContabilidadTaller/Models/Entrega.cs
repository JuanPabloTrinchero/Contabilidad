namespace ContabilidadTaller.Models
{
    public class Entrega
    {
        public int idEntrega { get; set; }
        public DateOnly fechaEntrega { get; set; }
        public int cantidadEntrega { get; set; }
        public int? idRemitoFK { get; set; }
    }
}
