using Microsoft.EntityFrameworkCore;
using Backend.src.app.Features.Services.domain.entities;
using Backend.src.app.Features.Services.domain.repositories;
using Backend.src.app.Features.Services.infrastructure.Context;

namespace Backend.src.app.Features.Services.infrastructure.repositories
{
    public class ServiceRepository : IServicesRepository
    {
        private readonly ServicesDbContext _context;

        public ServiceRepository(ServicesDbContext context)
        {
            _context = context;
        }

  
        public async Task<Service?> GetServiceById(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<int> CreateServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.idServicio;
        }

        
        public async Task<bool> UpdateServiceAsync(Service service)
        {
            var existing = await _context.Services.FindAsync(service.idServicio);

            if (existing == null)
                return false;

            existing.nombreServicio = service.nombreServicio;
            existing.descripcion = service.descripcion;
            existing.precioBase = service.precioBase;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateServiceStatusAsync(int id, bool Activo)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return false;

            service.Activo = Activo;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}