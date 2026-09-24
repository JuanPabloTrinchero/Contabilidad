namespace ContabilidadTaller.Models
{
    public enum EstadoNotaDeCredito
    {
        Activa = 0,
        Confirmada = 1,
        Anulada = 2
    }
    public class CodigoNDC
    {
        public string PuestoNDC { get; set; }
        public string NumeroNDC { get; set; }
        public string GetCodigoNDC()
        {
            return $"{PuestoNDC}{NumeroNDC}";
        }
    }
    public class NotaDeCredito
    {
        public int IdNotaCredito { get; set; }
        public DateOnly Fecha { get; set; }
        public CodigoNDC Codigo { get; set; }
        public List<string>? Notas { get; set; }
        public string? UbicacionArchivo { get; set; }
        public EstadoNotaDeCredito Estado { get; set; }
        public List<int>? FacturasRespaldo { get; set; }
        //FKs
    }
}
