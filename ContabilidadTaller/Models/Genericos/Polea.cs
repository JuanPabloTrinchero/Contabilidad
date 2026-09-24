namespace ContabilidadTaller.Models.Genericos
{
    public class Polea
    {
        public int IdPolea { get; set; }
        public int Vuelo { get; set; }
        public string TipoCanal { get; set; }
        public double DiametroEje { get; set; }
        public Chaveta Chavetero { get; set; }
        public string Prisioneros { get; set; }
    }

}
