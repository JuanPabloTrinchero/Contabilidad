namespace ContabilidadTaller.Models
{
    public class Pieza
    {
        public int IdPieza { get; set; }
        public string Descripcion { get; set; }
        public string? Codigo { get; set; }
        public int? ConjuntoFK { get; set; }
        public List<string>? Notas { get; set; }
        public List<IFormFile>? ArchivosSubidos { get; set; } // SOLO PARA RECIBIR DATOS, NO SE GUARDAN EN JSON
        public List<string>? Archivos { get; set; }
        //FKs
    }
}
