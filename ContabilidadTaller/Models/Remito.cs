namespace ContabilidadTaller.Models
{
    public enum EstadoRemito { Borrador = 0, Confirmado = 1}
    public class Remito
    {
        public int IdRemito { get; set; }
        public int NumeroRemito { get; set; }
        public DateOnly FechaEntrega { get; set; }
        public EstadoRemito Estado { get; set; }
        //public List<int> Pedidos { get; set; }
        public string? UbicacionArchivo { get; set; }
        public List<string>? Notas { get; set; }
        public int? EntregadoA { get; set; }
    }
}
