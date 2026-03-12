using Mapster;
using QuickHealth.Application.DTOs;
using QuickHealth.Domain.Model;
using QuickHealth.Domain.Service;

namespace QuickHealth.Application.Mappings
{
    public static class MapsterConfig
    {
        public static void Configure(TypeAdapterConfig config)
        {
            config.NewConfig<Paciente, PacienteResponseDTO>()
                .Map(dest => dest.DescripcionPrioridad,
                     org => CalculadoraTriaje.ObtenerDescripcion(org.NivelPrioridad));
        }
    }
}
