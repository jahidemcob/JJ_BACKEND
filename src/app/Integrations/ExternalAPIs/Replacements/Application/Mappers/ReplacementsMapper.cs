using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.DTOs;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Entity;

namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Mappers
{
    public static class ReplacementMapper
    {
        public static GetAllReplacementsDto ToDto(Replacement entity)
        {
            return new GetAllReplacementsDto
            {
                Nombre = entity.Nombre,
                Precio = entity.Precio
            };
        }

        public static List<GetAllReplacementsDto> ToDtoList(List<Replacement> entities)
        {
            return entities.Select(ToDto).ToList();
        }
    }
}