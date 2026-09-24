namespace ContabilidadTaller.Models
{
    public class Conjunto
    {
        public int IdConjunto { get; set; }
        public string Descripcion { get; set; }
        public string? Codigo { get; set; }
        public List<string>? Notas { get; set; }
        public List<string>? Archivos { get; set; }
    }
}
