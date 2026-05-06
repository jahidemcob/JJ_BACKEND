using Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Entity;

namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Interface
{
    public interface IReplacementsRepository
    {
        Task<List<Replacement>> GetAllReplacementsAsync();
    }
}
