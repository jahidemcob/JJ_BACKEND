using Auth.Application.Services;
using Auth.Application.UseCases;
using Auth.Domain.Repositories;
using Auth.Infrastructure.Context;
using Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;   
using Microsoft.IdentityModel.Tokens;                 
using System.Text;                                    

var builder = WebApplication.CreateBuilder(args);

// Servicios Básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config BD
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection"))
);

// Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

// Servicios de Dominio
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenService>();

// Casos de Uso
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();

// JWT ?? CORREGIDO ??
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

// Construcción de la aplicación
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();