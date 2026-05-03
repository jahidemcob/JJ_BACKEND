using System.Text;

// FRAMEWORK / ASP.NET CORE
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; 

// AUTH MODULE
using Backend.src.app.auth.application.Services;
using Backend.src.app.auth.application.UseCases;
using Backend.src.app.auth.domain.entities;
using Backend.src.app.auth.domain.repositories;
using Backend.src.app.auth.infrastructure.Context;
using Backend.src.app.auth.infrastructure.Repositories;

// USERS MODULE
using Backend.src.app.Features.Users.application.usecases;
using Backend.src.app.Features.Users.application.UseCases;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.infrastructure.Context;
using Backend.src.app.Features.Users.infrastructure.Repositories;

// SERVICES MODULE
using Backend.src.app.Features.Services.application.usecases;
using Backend.src.app.Features.Services.domain.repositories;
using Backend.src.app.Features.Services.infrastructure.Context;
using Backend.src.app.Features.Services.infrastructure.repositories;

// MOTOBIKES MODULE
using Backend.src.app.Features.Motobikes.application.usecases;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.infrastructure.Context;
using Backend.src.app.Features.Motobikes.infrastructure.repositories;

// SHARED
using Backend.src.app.Shared.Security;
using Backend.src.app.Shared.Infrastructure;
using Backend.src.app.Shared.Constants;
using Backend.src.app.Shared.exceptions;


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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();


// SWAGGER CON JWT 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token así: Bearer {tu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// DB Contexts
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.Default)));
builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.Default)));
builder.Services.AddDbContext<ServicesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.Default)));
builder.Services.AddDbContext<MotobikesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.Default)));

// Repositorios
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IServicesRepository, ServiceRepository>();
builder.Services.AddScoped<IMotorbikesRepository, MotorbikesRepository>();

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
builder.Services.AddScoped<UpdateServiceStatusUsecase>();
builder.Services.AddScoped<GetServiceByIdUseCase>();
builder.Services.AddScoped<GetAllServicesUseCase>();

// MOTOBIKES
builder.Services.AddScoped<CreateMotorbikeUsecase>();
builder.Services.AddScoped<GetAllMotorbikesUsecase>();
builder.Services.AddScoped<GetByIdMotorbikeUsecase>();
builder.Services.AddScoped<UpdateMotorbikeUsecase>();
builder.Services.AddScoped<UpdateStatusMotorbikeUsecase>();


// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;

        var jwtKey = config["Jwt:Key"];

        if (string.IsNullOrEmpty(jwtKey))
            throw new ConfigurationException("JWT Key no está configurada");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            )
        };
    });

var app = builder.Build();

// Docker 
if (!app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await DbInitializer.InitializeAsync(scope.ServiceProvider);
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

await app.RunAsync();