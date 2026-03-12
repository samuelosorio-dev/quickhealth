using QuickHealth.Domain.Model;

namespace QuickHealth.Application.Ports.Out
{
    public interface IRepositorioPaciente
    {
        Task<Paciente> CrearAsync(Paciente paciente);
        Task<Paciente?> ObtenerPorIdAsync(int id);
        Task<List<Paciente>> ObtenerTodosAsync();
        Task<Paciente> ActualizarAsync(Paciente paciente);
        Task EliminarAsync(Paciente paciente);
    }
}
