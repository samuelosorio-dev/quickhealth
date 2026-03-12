using System.ComponentModel.DataAnnotations;

namespace QuickHealth.Application.DTOs
{
    public class PacienteResponseDTO
    {
        public int Id { get; set; }
        [Required]
        public required string Nombre { get; set; }
        public int Edad { get; set; }
        [Required]
        public required string Documento { get; set; }
        public int FrecuenciaCardiaca { get; set; }
        public double Temperatura { get; set; }
        public int PresionSistolica { get; set; }
        public int PresionDiastolica { get; set; }
        public int NivelPrioridad { get; set; }
        public string DescripcionPrioridad { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}
