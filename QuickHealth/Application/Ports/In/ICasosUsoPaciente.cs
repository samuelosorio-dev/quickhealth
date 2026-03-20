using QuickHealth.Infrastructure.DTOs;

namespace QuickHealth.Application.Ports.In
{
    public interface ICasosUsoPaciente
    {
        Task<PacienteResponseDTO> CrearPacienteAsync(CrearPacienteRequestDTO request);
        Task<PacienteResponseDTO> ObtenerPacientePorIdAsync(int id);
        Task<List<PacienteResponseDTO>> ObtenerTodosLosPacientesAsync();
        Task<PacienteResponseDTO> ActualizarSignosVitalesAsync(int id, ActualizarSignosVitalesRequestDTO request);
        Task EliminarPacienteAsync(int id);
    }
}
