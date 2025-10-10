using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Helpers;
using MiApiDB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuración DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// 🔹 Servicios
builder.Services.AddScoped<JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 🔹 Swagger con JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Mi API", 
        Version = "v1" 
    });

    // 🔹 Configuración para que aparezca el botón "Authorize"
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingresa 'Bearer {tu token}'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference 
                { 
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                }
            },
            new string[] {}
        }
    });
});


// 🔹 CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🔹 Autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// 🔹 Autorización
builder.Services.AddAuthorization();

var app = builder.Build();

// 🔹 Crear admin inicial si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordHasher = new PasswordHasher<Usuario>();

    if (!db.Usuarios.Any(u => u.Rol == "Admin"))
    {
        var admin = new Usuario
        {
            Nombre = "Administrador",
            Apellido = "Principal",
            Correo = "admin@empresa.com",
            Rol = "Admin"
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");
        db.Usuarios.Add(admin);
        db.SaveChanges();
    }
}

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
