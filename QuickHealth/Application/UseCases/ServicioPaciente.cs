using Mapster;
using QuickHealth.Application.DTOs;
using QuickHealth.Application.Ports.In;
using QuickHealth.Application.Ports.Out;
using QuickHealth.Domain.Exception;
using QuickHealth.Domain.Model;

namespace QuickHealth.Application.UseCases
{
    public class ServicioPaciente:ICasosUsoPaciente
    {
        private readonly IRepositorioPaciente _repositorio;

        public ServicioPaciente(IRepositorioPaciente repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<PacienteResponseDTO> CrearPacienteAsync(CrearPacienteRequestDTO request)
        {
            var paciente = Paciente.Crear(
                request.Nombre,
                request.Edad,
                request.Documento,
                request.FrecuenciaCardiaca,
                request.Temperatura,
                request.PresionSistolica,
                request.PresionDiastolica);

            var resultado = await _repositorio.CrearAsync(paciente);
            return resultado.Adapt<PacienteResponseDTO>();
        }

        public async Task<PacienteResponseDTO> ObtenerPacientePorIdAsync(int id)
        {
            var paciente = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new ExcepcionNegocio($"No se encontró un paciente con el id {id}.");

            return paciente.Adapt<PacienteResponseDTO>();
        }

        public async Task<List<PacienteResponseDTO>> ObtenerTodosLosPacientesAsync()
        {
            var pacientes = await _repositorio.ObtenerTodosAsync();
            return pacientes.Adapt<List<PacienteResponseDTO>>();
        }

        public async Task<PacienteResponseDTO> ActualizarSignosVitalesAsync(int id, ActualizarSignosVitalesRequestDTO request)
        {
            var paciente = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new ExcepcionNegocio($"No se encontró un paciente con el id {id}.");

            paciente.ActualizarSignosVitales(
                request.FrecuenciaCardiaca,
                request.Temperatura,
                request.PresionSistolica,
                request.PresionDiastolica);

            var resultado = await _repositorio.ActualizarAsync(paciente);
            return resultado.Adapt<PacienteResponseDTO>();
        }

        public async Task EliminarPacienteAsync(int id)
        {
            var paciente = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new ExcepcionNegocio($"No se encontró un paciente con el id {id}.");

            await _repositorio.EliminarAsync(paciente);
        }
    }
}
