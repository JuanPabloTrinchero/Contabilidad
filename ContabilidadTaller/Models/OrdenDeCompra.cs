namespace ContabilidadTaller.Models
{
    public enum Relacion { ConRecepcion = 1, SinRecepcion = 2 }
    public enum EstadoOC { Activa = 1, Anulada = 2, Facturada = 3 }
    public class OrdenDeCompra
    {
        public int IdOC { get; set; }
        public string Codigo { get; set; }
        public DateOnly Fecha { get; set; }
        public string? UbicacionArchivo { get; set; }
        public List<string>? Notas { get; set; }
        //FKs
        public Relacion Relacion { get; set; }
        public EstadoOC Estado { get; set; }
        public List<int>? ItemsRespaldo { get; set; }
    }
}
