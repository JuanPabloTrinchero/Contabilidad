namespace ContabilidadTaller.Models
{
    public class Utilaje
    {
        public int IdUtilaje { get; set; }
        public string? Descripcion { get; set; }
        public List<string>? Notas { get; set; }
        public List<string>? Archivos { get; set; }
        // FK
        public int? IdPiezaFK { get; set; }
    }
}
