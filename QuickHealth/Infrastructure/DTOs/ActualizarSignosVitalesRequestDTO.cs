namespace QuickHealth.Infrastructure.DTOs
{
    public class ActualizarSignosVitalesRequestDTO
    {
        public int FrecuenciaCardiaca { get; set; }
        public double Temperatura { get; set; }
        public int PresionSistolica { get; set; }
        public int PresionDiastolica { get; set; }
    }
}
