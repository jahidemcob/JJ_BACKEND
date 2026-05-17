using Microsoft.EntityFrameworkCore;
using Backend.src.app.auth.infrastructure.Context;
using Backend.src.app.Features.Users.infrastructure.Context;
using Backend.src.app.Features.Services.infrastructure.Context;
using Backend.src.app.Features.Motobikes.infrastructure.Context;
using Backend.src.app.Features.Appointments.Infrastructure.Context;
using Backend.src.app.auth.domain.entities;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Shared.Security;


namespace Backend.src.app.Shared.Infrastructure;
public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var retries = 10;
        var delay = TimeSpan.FromSeconds(5);

        while (retries > 0)
        {
            try
            {
                await MigrateDatabasesAsync(services);
                await SeedDataAsync(services);

                Console.WriteLine("Migraciones y seed aplicados correctamente");
                break;
            }
            catch (Exception ex)
            {
                retries--;
                Console.WriteLine($"Error conectando a DB, reintentos restantes: {retries}");
                Console.WriteLine(ex.Message);

                if (retries == 0) throw;

                await Task.Delay(delay);
            }
        }
    }

    private static async Task MigrateDatabasesAsync(IServiceProvider services)
    {
        var authDb = services.GetRequiredService<AuthDbContext>();
        var usersDb = services.GetRequiredService<UsersDbContext>();
        var servicesDb = services.GetRequiredService<ServicesDbContext>();
        var motobikesDb = services.GetRequiredService<MotobikesDbContext>();
        var appointmentsDb = services.GetRequiredService<AppointmentsDbContext>();

        await authDb.Database.MigrateAsync();
        await usersDb.Database.MigrateAsync();
        await servicesDb.Database.MigrateAsync();
        await motobikesDb.Database.MigrateAsync();
        await appointmentsDb.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(IServiceProvider services)
    {
        var authDb = services.GetRequiredService<AuthDbContext>();
        var usersDb = services.GetRequiredService<UsersDbContext>();
        var passwordService = services.GetRequiredService<PasswordService>();

        await SeedRolesAsync(authDb);
        await SeedAdminUserAsync(authDb, usersDb, passwordService);
    }

    private static async Task SeedRolesAsync(AuthDbContext authDb)
    {
        if (await authDb.Roles.AnyAsync()) return;

        await authDb.Roles.AddRangeAsync(
            new Rol { NombreRol = "Administrador" },
            new Rol { NombreRol = "Empleado" },
            new Rol { NombreRol = "Cliente" }
        );

        await authDb.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(
        AuthDbContext authDb,
        UsersDbContext usersDb,
        PasswordService passwordService)
    {
        if (await usersDb.Usuarios.AnyAsync()) return;

        var passwordData = passwordService.HashPassword("Admin123*");

        var adminRol = await authDb.Roles
            .FirstAsync(r => r.NombreRol == "Administrador");

        await usersDb.Usuarios.AddAsync(new User
        {
            Nombre = "Administrador",
            NombreUsuario = "admin",
            Correo = "admin@demo.com",
            Telefono = "0000000000",
            ClaveHash = passwordData.Hash,
            ClaveSalt = passwordData.Salt,
            IdRol = adminRol.IdRol,
            Activo = true
        });

        await usersDb.SaveChangesAsync();
    }
}