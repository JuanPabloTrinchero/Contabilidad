namespace ContabilidadTaller.Models.Genericos
{
    public class CabezalDivisor
    {
        public int Modulo { get; set; }     // Vueltas de manija para dar una vuelta completa
        public List<DiscoCabezalDivisor> Discos { get; set; }
    }
}

