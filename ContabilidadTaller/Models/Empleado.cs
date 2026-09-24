using System.ComponentModel.DataAnnotations;

namespace ContabilidadTaller.Models
{
    public enum EstadoEmpleado { Inactivo = 0, Activo = 1 }
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public List<string>? Telefonos { get; set; }
        [EmailAddress]
        public List<string>? Emails { get; set; }
        public List<string>? Notas { get; set; }
        public EstadoEmpleado Estado { get; set; }
    }
}
