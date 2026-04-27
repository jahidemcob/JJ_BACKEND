using Backend.src.app.auth.application.Services;
using Backend.src.app.auth.application.UseCases;
using Backend.src.app.auth.domain.repositories;
using Backend.src.app.auth.infrastructure.Context;
using Backend.src.app.auth.infrastructure.Repositories;
using Backend.src.app.Features.Services.application.usecases;
using Backend.src.app.Features.Services.domain.repositories;
using Backend.src.app.Features.Services.infrastructure.Context;
using Backend.src.app.Features.Services.infrastructure.repositories;
using Backend.src.app.Features.Users.application.usecases;
using Backend.src.app.Features.Users.application.UseCases;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.infrastructure.Context;
using Backend.src.app.Features.Users.infrastructure.Repositories;
using Backend.src.app.Shared.Security;

using Backend.src.app.auth.domain.entities;
using Backend.src.app.Features.Users.domain.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// Servicios básicos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Contexts
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"))
);

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"))
);

builder.Services.AddDbContext<ServicesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"))
);

// Repositorios
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IServicesRepository, ServiceRepository>();

// Servicios
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenService>();

// AUTH
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();

// USERS
builder.Services.AddScoped<UserListUsecase>();
builder.Services.AddScoped<GetUserByIdUsecase>();
builder.Services.AddScoped<CreateUserUsecase>();
builder.Services.AddScoped<UpdateUserUsecase>();
builder.Services.AddScoped<DisableUserUsecase>();

// SERVICES
builder.Services.AddScoped<CreateServiceUseCase>();
builder.Services.AddScoped<UpdateServiceUseCase>();
builder.Services.AddScoped<DisableServiceUseCase>();
builder.Services.AddScoped<GetServiceByIdUseCase>();
builder.Services.AddScoped<GetAllServicesUseCase>();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"])
            )
        };
    });

var app = builder.Build();

//  SOLO EN DOCKER 
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var retries = 10;
        var delay = TimeSpan.FromSeconds(5);

        while (retries > 0)
        {
            try
            {
                var authDb = services.GetRequiredService<AuthDbContext>();
                var usersDb = services.GetRequiredService<UsersDbContext>();
                var servicesDb = services.GetRequiredService<ServicesDbContext>();
                var passwordService = services.GetRequiredService<PasswordService>();

                // Migraciones
                authDb.Database.Migrate();
                usersDb.Database.Migrate();
                servicesDb.Database.Migrate();

                var PasswordService = services.GetRequiredService<PasswordService>();

              
                if (!authDb.Roles.Any())
                {
                    authDb.Roles.AddRange(
                        new Rol { NombreRol = "Administrador" },
                        new Rol { NombreRol = "Empleado" },
                        new Rol { NombreRol = "Cliente" }
                    );

                    authDb.SaveChanges();
                }

                if (!usersDb.Usuarios.Any()) 
                {
                    var passwordData = passwordService.HashPassword("Admin123*");

                    var adminRol = authDb.Roles.First(r => r.NombreRol == "Administrador");

                    usersDb.Usuarios.Add(new User
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

                    usersDb.SaveChanges();
                }

                Console.WriteLine("Migraciones y seed aplicados correctamente ");
                break;
            }
            catch (Exception ex)
            {
                retries--;
                Console.WriteLine($"Error conectando a DB, reintentos restantes: {retries}");
                Console.WriteLine(ex.Message);

                if (retries == 0) throw;

                Thread.Sleep(delay);
            }
        }
    }
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Middleware
app.UseHttpsRedirection();
app.UseMiddleware<Backend.src.app.Shared.Middleware.ErrorHandlerMiddleware>();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();