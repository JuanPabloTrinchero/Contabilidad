namespace ContabilidadTaller.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public Cuit Cuit { get; set; }
        public string? Direccion { get; set; }
        public List<string>? Notas { get; set; }
    }
}
