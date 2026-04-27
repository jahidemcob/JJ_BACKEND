using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.domain.entities;

namespace Backend.src.app.Features.Services.application.mappers
{
    public static class ServiceMapper
    {
        public static ServiceResponseDto ToDto(Service s)
        {
            return new ServiceResponseDto
            {
                IdServicio = s.idServicio,
                NombreServicio = s.nombreServicio,
                Descripcion = s.descripcion,
                PrecioBase = s.precioBase,
                Activo = s.Activo
            };
        }
        public static List<ServiceResponseDto> ToDtoList(IEnumerable<Service> services)
        {
            return services.Select(ToDto).ToList();
        }
    }
}
