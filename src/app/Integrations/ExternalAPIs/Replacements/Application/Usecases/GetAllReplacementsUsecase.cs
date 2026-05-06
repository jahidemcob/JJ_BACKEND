using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.DTOs;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Mappers;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Interface;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Exceptions;

namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Usecases
{
    public class GetAllReplacementsUsecase
    {
        private readonly IReplacementsRepository _replacementRepository;

        public GetAllReplacementsUsecase(IReplacementsRepository replacementRepository)
        {
            _replacementRepository = replacementRepository;
        }

        public async Task<List<GetAllReplacementsDto>> Execute()
        {
            var replacements = await _replacementRepository.GetAllReplacementsAsync();

            if (!replacements.Any())
                throw new ReplacementNotFoundException("No se encontraron repuestos.");

            return ReplacementMapper.ToDtoList(replacements);
        }
    }
}
