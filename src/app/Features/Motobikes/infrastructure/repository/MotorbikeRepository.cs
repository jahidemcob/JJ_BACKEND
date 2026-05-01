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

        // GET ALL WITH USER
        public async Task<IEnumerable<(Motorbike moto, string nombreUsuario)>> GetAllMotorbikesWithUserAsync()
        {
            // 1. Traemos todas las motos
            var motos = await _motobikesContext.Motos
                .AsNoTracking()
                .ToListAsync();

            // 2. IDs de usuarios
            var idsUsuarios = motos.Select(m => m.idUsuario).Distinct().ToList();

            // 3. Traemos todos los usuarios necesarios
            var usuarios = await _usersContext.Usuarios
                .Where(u => idsUsuarios.Contains(u.IdUsuario))
                .AsNoTracking()
                .ToListAsync();

            // 4. Join en memoria
            var resultado = from m in motos
                            join u in usuarios
                            on m.idUsuario equals u.IdUsuario
                            select (m, u.Nombre);

            return resultado;
        }

        public async Task<(Motorbike moto, string nombreUsuario)?> GetMotorbikeWithUserByIdAsync(int id)
        {
            // 1. Traer la moto
            var moto = await _motobikesContext.Motos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.idMoto == id);

            if (moto == null)
                return null;

            // 2. Traer el usuario asociado
            var usuario = await _usersContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == moto.idUsuario);

            // 3. Proteger en caso de que el usuario ya no exista
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