using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlataformaEscolar.Models;

namespace PlataformaEscolar.Data
{
    public class PlataformaEscolarContext : IdentityDbContext
    {
        public PlataformaEscolarContext(DbContextOptions<PlataformaEscolarContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
    }
} 