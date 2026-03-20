using FluentValidation;
using QuickHealth.Infrastructure.DTOs;

namespace QuickHealth.Application.Validators
{
    public class ActualizarPacienteValidator:AbstractValidator<ActualizarSignosVitalesRequestDTO>
    {
        public ActualizarPacienteValidator()
        {
            RuleFor(x => x.FrecuenciaCardiaca)
                .GreaterThan(0)
                .WithMessage("La frecuencia cardíaca debe ser mayor a 0");

            RuleFor(x => x.Temperatura)
                .GreaterThan(0)
                .WithMessage("La temperatura debe ser mayor a 0");

            RuleFor(x => x.PresionSistolica)
                .GreaterThan(0)
                .WithMessage("La presión sistólica debe ser mayor a 0");

            RuleFor(x => x.PresionDiastolica)
                .GreaterThan(0)
                .WithMessage("La presión diastólica debe ser mayor a 0");
        }
    }
}
