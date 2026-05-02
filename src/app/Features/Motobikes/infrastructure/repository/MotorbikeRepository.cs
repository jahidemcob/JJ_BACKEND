using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.infrastructure.Context;
using Backend.src.app.Features.Users.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Motobikes.infrastructure.repositories
{
    public class MotorbikesRepository : IMotorbikesRepository
    {
        private readonly MotobikesDbContext _motobikesContext;
        private readonly UsersDbContext _usersContext;

        public MotorbikesRepository(
            MotobikesDbContext motobikesContext,
            UsersDbContext usersContext
        )
        {
            _motobikesContext = motobikesContext;
            _usersContext = usersContext;
        }

        public async Task<IEnumerable<(Motorbike moto, string nombreUsuario)>> GetAllMotorbikesWithUserAsync()
        {
            var motos = await _motobikesContext.Motos
                .AsNoTracking()
                .ToListAsync();

            var idsUsuarios = motos.Select(m => m.idUsuario).Distinct().ToList();

            var usuarios = await _usersContext.Usuarios
                .Where(u => idsUsuarios.Contains(u.IdUsuario))
                .AsNoTracking()
                .ToListAsync();

            var resultado = from m in motos
                            join u in usuarios
                            on m.idUsuario equals u.IdUsuario
                            select (m, u.Nombre);

            return resultado;
        }

        public async Task<IEnumerable<(Motorbike moto, string nombreUsuario)>> GetMotorbikesWithUserByUserIdAsync(int userId)
        {
            var motos = await _motobikesContext.Motos
                .Where(m => m.idUsuario == userId)
                .AsNoTracking()
                .ToListAsync();

            if (!motos.Any())
                return Enumerable.Empty<(Motorbike, string)>();

            var idsUsuarios = motos.Select(m => m.idUsuario).Distinct().ToList();

            var usuarios = await _usersContext.Usuarios
                .Where(u => idsUsuarios.Contains(u.IdUsuario))
                .AsNoTracking()
                .ToListAsync();

            var resultado = from m in motos
                            join u in usuarios
                            on m.idUsuario equals u.IdUsuario
                            select (m, u.Nombre);

            return resultado;
        }

        // GET BY ID WITH USER
        public async Task<(Motorbike moto, string nombreUsuario)?> GetMotorbikeWithUserByIdAsync(int id)
        {
            var moto = await _motobikesContext.Motos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.idMoto == id);

            if (moto == null)
                return null;

            var usuario = await _usersContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == moto.idUsuario);

            var nombreUsuario = usuario?.Nombre ?? "Usuario no encontrado";

            return (moto, nombreUsuario);
        }

        // GET BY ID
        public async Task<Motorbike?> GetMotorbikeByIdAsync(int id)
        {
            return await _motobikesContext.Motos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.idMoto == id);
        }

        // CREATE
        public async Task<Motorbike> CreateMotorbikeAsync(Motorbike motorbike)
        {
            _motobikesContext.Motos.Add(motorbike);
            await _motobikesContext.SaveChangesAsync();
            return motorbike;
        }

        // UPDATE
        public async Task<Motorbike> UpdateMotorbikeAsync(Motorbike motorbike)
        {
            _motobikesContext.Motos.Update(motorbike);
            await _motobikesContext.SaveChangesAsync();
            return motorbike;
        }

        // UPDATE STATUS
        public async Task<bool> UpdateMotorbikeStatusAsync(int idMoto, bool Activo)
        {
            var motorbike = await _motobikesContext.Motos.FindAsync(idMoto);
            if (motorbike == null)
                return false;

            motorbike.Activo = Activo;
            await _motobikesContext.SaveChangesAsync();
            return motorbike.Activo;
        }

        // EXISTS BY PLATE
        public async Task<bool> ExistsByPlateAsync(string placa)
        {
            return await _motobikesContext.Motos
                .AnyAsync(m => m.placa == placa);
        }
    }
}