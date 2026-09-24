namespace ContabilidadTaller.Models
{
    public enum EstadoRecepcion { Activa = 1, Facturada = 2, Anulada = 3 }
    public class Recepcion
    {
        public int IdRecepcion { get; set; }
        public string NumeroRecepcion { get; set; }
        public EstadoRecepcion Estado { get; set; }
        public int? IdOdCFK { get; set; }
        public int? IdFacturaFK { get; set; }
        public double? Monto { get; set; }
    }
}
