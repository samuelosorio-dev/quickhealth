using FluentValidation;
using QuickHealth.Infrastructure.DTOs;

namespace QuickHealth.Application.Validators
{
    public class PacienteValidator:AbstractValidator<CrearPacienteRequestDTO>
    {
        public PacienteValidator() {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(150).WithMessage("El nombre no puede superar 150 caracteres");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El documento es obligatorio")
                .MaximumLength(20).WithMessage("El documento no puede superar 20 caracteres");

            RuleFor(x => x.Edad)
                .GreaterThan(0).WithMessage("La edad debe ser mayor a 0")
                .LessThanOrEqualTo(120).WithMessage("La edad no puede superar 120 años");
        }
    }
}
