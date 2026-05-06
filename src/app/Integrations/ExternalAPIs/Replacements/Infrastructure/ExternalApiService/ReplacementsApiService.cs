using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Exceptions;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Interface;


namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.Infrastructure.ExternalApiService
{
    using Backend.src.app.Integrations.ExternalAPIs.Replacements.Domain.Entity;
    using Microsoft.Extensions.Configuration;

    public class ReplacementsApiService : IReplacementsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ReplacementsApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var baseUrl = configuration["ReplacementsApi:BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ExternalServiceException("ReplacementsApi:BaseUrl no está configurada.");
            }
            _baseUrl = baseUrl;
        }

        public async Task<List<Replacement>> GetAllReplacementsAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Replacement>>(
                    $"{_baseUrl}/api/Repuestos"
                );

                return result ?? new List<Replacement>();
            }
            catch (HttpRequestException ex)
            {
                throw new ExternalServiceException("Error consumiendo API externa", ex);
            }
        }
    }
}
