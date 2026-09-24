namespace ContabilidadTaller.Models
{
    public class Socio
    {
        public int IdSocio { get; set; }
        public string? Nombres { get; set; }
        public string? Apellido { get; set; }
        public Cuit Cuit { get; set; }
        public string? Direccion { get; set; }
        public List<string>? Emails { get; set; }
        public string? Telefono { get; set; }
        public List<string>? Notas { get; set; }
        public string ContraseñaConfirmacion { get; set; }
    }
}
