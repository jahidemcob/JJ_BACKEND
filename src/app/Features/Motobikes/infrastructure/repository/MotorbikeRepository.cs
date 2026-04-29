using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.src.app.Features.Motobikes.infrastructure.repositories
{
    public class MotorbikesRepository : IMotorbikesRepository
    {
        private readonly MotorbikesDbContext _context;

        public MotorbikesRepository(MotorbikesDbContext context)
        {
            _context = context;
        }

        public async Task<(Motorbike moto, string nombreUsuario)?> GetMotorbikeWithUserByIdAsync(int id)
        {
            var query = await _context.Motorbikes
                .Where(m => m.idMoto == id)
                .Join(
                    _context.Users,
                    m => m.idUsuario,
                    u => u.IdUsuario,
                    (m, u) => new { m, u.NombreUsuario }
                )
                .FirstOrDefaultAsync();

            if (query == null)
                return null;

            return (query.m, query.NombreUsuario);
        }

        public async Task<IEnumerable<(Motorbike moto, string nombreUsuario)>> GetAllMotorbikesWithUserAsync()
        {
            var query = await _context.Motorbikes
                .Join(
                    _context.Users,
                    m => m.idUsuario,
                    u => u.IdUsuario,
                    (m, u) => new { m, u.NombreUsuario }
                )
                .ToListAsync();

            return query.Select(x => (x.m, x.NombreUsuario));
        }

        // ----------- Métodos existentes --------------
        public async Task<Motorbike?> GetMotorbikeByIdAsync(int id)
        {
            return await _context.Motorbikes.FindAsync(id);
        }

        public async Task<IEnumerable<Motorbike>> GetAllMotorbikesAsync()
        {
            return await _context.Motorbikes.ToListAsync();
        }

        public async Task<Motorbike> CreateMotorbikeAsync(Motorbike motorbike)
        {
            await _context.Motorbikes.AddAsync(motorbike);
            await _context.SaveChangesAsync();
            return motorbike;
        }

        public async Task<Motorbike> UpdateMotorbikeAsync(Motorbike motorbike)
        {
            _context.Motorbikes.Update(motorbike);
            await _context.SaveChangesAsync();
            return motorbike;
        }

        public async Task<bool> UpdateMotorbikeStatusAsync(int idMoto, bool activo)
        {
            var moto = await _context.Motorbikes.FindAsync(idMoto);

            if (moto == null)
                return false;

            moto.Activo = activo;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByPlateAsync(string placa)
        {
            return await _context.Motorbikes.AnyAsync(m => m.placa == placa);
        }
    }
}