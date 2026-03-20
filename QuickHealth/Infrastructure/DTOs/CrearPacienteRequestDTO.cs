using System.ComponentModel.DataAnnotations;

namespace QuickHealth.Infrastructure.DTOs
{
    public class CrearPacienteRequestDTO
    {
        [Required]
        public required string Nombre { get; set; }
        public int Edad { get; set; }
        [Required]
        public required string Documento { get; set; }
        public int FrecuenciaCardiaca { get; set; }
        public double Temperatura { get; set; }
        public int PresionSistolica { get; set; }
        public int PresionDiastolica { get; set; }
    }
}
