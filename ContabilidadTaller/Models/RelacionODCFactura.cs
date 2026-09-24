namespace ContabilidadTaller.Models
{
    public enum EstadoRelacionFOC
    {
        Activa = 1,
        Anulada = 2
    }
    public class RelacionODCFactura
    {
        public int IdRelacionODCFactura { get; set; }
        public int IdFacturaFK { get; set; }
        public int IdOdcFK { get; set; }
        public EstadoRelacionFOC Estado { get; set; }
    }
}
