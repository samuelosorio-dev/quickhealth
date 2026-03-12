using QuickHealth.Application.Ports.Out;
using Microsoft.EntityFrameworkCore;
using QuickHealth.Domain.Model;
using QuickHealth.Infrastructure.Adapters.Out.Persistence.Context;

namespace QuickHealth.Infrastructure.Adapters.Out.Persistence.Repositories
{
    public class RepositorioPaciente:IRepositorioPaciente
    {
        private readonly QuickHealthDbContext _context;

        public RepositorioPaciente(QuickHealthDbContext context)
        {
            _context = context;
        }

        public async Task<Paciente> CrearAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<List<Paciente>> ObtenerTodosAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> ActualizarAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task EliminarAsync(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }
    }
}
