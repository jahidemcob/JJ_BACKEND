
using Backend.src.app.auth.infrastructure.Repositories;
using Backend.src.app.auth.application.UseCases;
using Backend.src.app.auth.domain.repositories;
using Backend.src.app.auth.application.Services;
using Backend.src.app.auth.infrastructure.Context;
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

// Servicios básicos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de base de datos
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"))
);

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"))
);

// Repositorios
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

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

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  ORDEN IMPORTANTE

app.UseMiddleware<Backend.src.app.Shared.Middleware.ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseCors("AllowAngular"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();