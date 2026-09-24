namespace ContabilidadTaller.Models
{
    public class Costo
    {
        public int IdCosto { get; set; }
        public string? Descripcion { get; set; }
        public double? Precio { get; set; }
        public List<string>? Notas { get; set; }

        //FKs
        public int? PedidoFK { get; set; }
        public string? Archivo { get; set; }
    }
}
