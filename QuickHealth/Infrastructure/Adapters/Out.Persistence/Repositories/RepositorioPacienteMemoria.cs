using QuickHealth.Application.Ports.Out;
using QuickHealth.Domain.Model;

namespace QuickHealth.Infrastructure.Adapters.Out.Persistence.Repositories
{
    public class RepositorioPacienteMemoria:IRepositorioPaciente
    {
        private readonly List<Paciente> _pacientes = new();
        private int _contadorId = 1;

        public Task<Paciente> CrearAsync(Paciente paciente)
        {
            // Simulamos el autoincremento de SQL
            paciente.AsignarId(_contadorId++);
            _pacientes.Add(paciente);
            return Task.FromResult(paciente);
        }

        public Task<Paciente?> ObtenerPorIdAsync(int id)
        {
            return Task.FromResult(_pacientes.FirstOrDefault(p => p.Id == id));
        }

        public Task<List<Paciente>> ObtenerTodosAsync()
        {
            return Task.FromResult(_pacientes.ToList());
        }

        public Task<Paciente> ActualizarAsync(Paciente paciente)
        {
            return Task.FromResult(paciente);
        }

        public Task EliminarAsync(Paciente paciente)
        {
            _pacientes.Remove(paciente);
            return Task.CompletedTask;
        }
    }
}
