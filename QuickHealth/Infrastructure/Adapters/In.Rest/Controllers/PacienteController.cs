using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using QuickHealth.Application.DTOs;
using QuickHealth.Application.Ports.In;
using QuickHealth.Domain.Exception;

namespace QuickHealth.Infrastructure.Adapters.In.Rest.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacienteController:ControllerBase
    {
        private readonly ICasosUsoPaciente _casosUso;
        private readonly IValidator<CrearPacienteRequestDTO> validatorCreacion;
        private readonly IValidator<ActualizarSignosVitalesRequestDTO> validatorActualizacion;

        public PacienteController(ICasosUsoPaciente casosUso,IValidator<CrearPacienteRequestDTO> validatorCrear, IValidator<ActualizarSignosVitalesRequestDTO> validatorActualizar)
        {
            _casosUso = casosUso;
            validatorCreacion = validatorCrear;
            validatorActualizacion = validatorActualizar;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearPacienteRequestDTO request)
        {
            var validation = await validatorCreacion.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(x => x.ErrorMessage));

            var resultado = await _casosUso.CrearPacienteAsync(request);
                
            return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Id }, resultado);
          
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
           
            var resultado = await _casosUso.ObtenerPacientePorIdAsync(id);
            return Ok(resultado);
           
            
        }

        [HttpGet("buscarTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _casosUso.ObtenerTodosLosPacientesAsync();
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarSignosVitales(int id, [FromBody] ActualizarSignosVitalesRequestDTO request)
        {
            var validation = await validatorActualizacion.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(x => x.ErrorMessage));

            var resultado = await _casosUso.ActualizarSignosVitalesAsync(id, request);
            return Ok(resultado);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            await _casosUso.EliminarPacienteAsync(id);
            return NoContent();
           
            
        }
    }
}
