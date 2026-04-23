
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

// Servicios b�sicos
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

// Configuraci�n de base de datos
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

// Servicios de dominio
builder.Services.AddScoped<PasswordService>();
    builder.Services.AddScoped<TokenService>();

// Casos de uso AUTH
    builder.Services.AddScoped<LoginUserUseCase>();
    builder.Services.AddScoped<RegisterUserUseCase>();

// Casos de uso USERS
    builder.Services.AddScoped<UserListUsecase>();
    builder.Services.AddScoped<GetUserByIdUsecase>();
    builder.Services.AddScoped<CreateUserUsecase>();
    builder.Services.AddScoped<UpdateUserUsecase>();
    builder.Services.AddScoped<DisableUserUsecase>();

// Casos de Uso SERVICE
    builder.Services.AddScoped<CreateServiceUseCase>();
    builder.Services.AddScoped<UpdateServiceUseCase>();
    builder.Services.AddScoped<DisableServiceUseCase>();
    builder.Services.AddScoped<GetServiceByIdUseCase>();
    builder.Services.AddScoped<GetAllServicesUseCase>();


//  JWT
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

            authDb.Database.Migrate();
            usersDb.Database.Migrate();
            servicesDb.Database.Migrate();

            Console.WriteLine("Migraciones aplicadas correctamente ✅");
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

app.UseSwagger();
app.UseSwaggerUI();

//  ORDEN IMPORTANTE

app.UseHttpsRedirection();
app.UseMiddleware<Backend.src.app.Shared.Middleware.ErrorHandlerMiddleware>();

app.UseCors("AllowAngular"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();