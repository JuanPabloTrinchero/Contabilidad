namespace ContabilidadTaller.Models
{
    public class RespuestaBorrar
    {
        public bool SePuede { get; set; }
        public List<string> Mensajes { get; set; }

        // Constructor
        public RespuestaBorrar()
        {
            Mensajes = new List<string>();
        }
    }
}
