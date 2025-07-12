using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PlataformaEscolar.Data;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("1-Auth", new OpenApiInfo { Title = "Auth", Version = "v1" });
    c.SwaggerDoc("2-Grado-Grupo", new OpenApiInfo { Title = "Grado-Grupo", Version = "v1" });
    c.SwaggerDoc("3-Clases-Horarios", new OpenApiInfo { Title = "Clases-Horarios", Version = "v1" });
    c.SwaggerDoc("4-Calificaciones", new OpenApiInfo { Title = "Calificaciones", Version = "v1" });
    c.SwaggerDoc("5-Comentarios", new OpenApiInfo { Title = "Comentarios", Version = "v1" });
    c.SwaggerDoc("6-Alumnos", new OpenApiInfo { Title = "Alumnos", Version = "v1" });
    c.SwaggerDoc("7-Profesores", new OpenApiInfo { Title = "Profesores", Version = "v1" });
    c.SwaggerDoc("8-Administradores", new OpenApiInfo { Title = "Administradores", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        var groupName = apiDesc.GroupName ?? "";
        return docName == groupName;
    });
});
builder.Services.AddControllers();

// Configuración de DbContext y servicios
builder.Services.AddDbContext<PlataformaEscolarContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
// builder.Services.AddScoped<IAlumnoService, AlumnoService>();
// builder.Services.AddScoped<IProfesorService, ProfesorService>();
// builder.Services.AddScoped<IAdministradorService, AdministradorService>();
// builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
// builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
// builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<PlataformaEscolar.Services.ICalificacionService, PlataformaEscolar.Services.CalificacionService>();
builder.Services.AddScoped<PlataformaEscolar.Services.IGradoGrupoService, PlataformaEscolar.Services.GradoGrupoService>();
builder.Services.AddScoped<PlataformaEscolar.Services.IClaseHorarioService, PlataformaEscolar.Services.ClaseHorarioService>();
builder.Services.AddScoped<PlataformaEscolar.Services.IComentarioService, PlataformaEscolar.Services.ComentarioService>();

// Configuración de Identity y JWT
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PlataformaEscolarContext>();
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AlumnoPolicy", policy => policy.RequireRole("Alumno"));
    options.AddPolicy("ProfesorPolicy", policy => policy.RequireRole("Profesor"));
    options.AddPolicy("AdministradorPolicy", policy => policy.RequireRole("Administrador"));
});

var app = builder.Build();

// Crear roles si no existen
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = new[] { "Alumno", "Profesor", "Administrador" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
            roleManager.CreateAsync(new IdentityRole(role)).Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); // Eliminado para evitar conflicto
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/1-Auth/swagger.json", "Auth");
        c.SwaggerEndpoint("/swagger/2-Grado-Grupo/swagger.json", "Grado-Grupo");
        c.SwaggerEndpoint("/swagger/3-Clases-Horarios/swagger.json", "Clases-Horarios");
        c.SwaggerEndpoint("/swagger/4-Calificaciones/swagger.json", "Calificaciones");
        c.SwaggerEndpoint("/swagger/5-Comentarios/swagger.json", "Comentarios");
        c.SwaggerEndpoint("/swagger/6-Alumnos/swagger.json", "Alumnos");
        c.SwaggerEndpoint("/swagger/7-Profesores/swagger.json", "Profesores");
        c.SwaggerEndpoint("/swagger/8-Administradores/swagger.json", "Administradores");
        c.ConfigObject.AdditionalItems["urls.primaryName"] = "Auth";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Eliminar el endpoint /weatherforecast y su modelo asociado

app.Run();
