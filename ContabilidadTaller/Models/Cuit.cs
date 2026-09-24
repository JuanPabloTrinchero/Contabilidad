namespace ContabilidadTaller.Models
{
    public class Cuit
    {
        public int InicioCuit { get; set; }
        public int MedioCuit { get; set; }
        public int FinCuit { get; set; }

        public string GetCuitSinGuiones()
        {
            return InicioCuit.ToString() + MedioCuit.ToString() + FinCuit.ToString();
        }
        public string GetCuitConGuiones()
        {
            return InicioCuit.ToString() + "-" + MedioCuit.ToString() + "-" + FinCuit.ToString();
        }
    }
}
