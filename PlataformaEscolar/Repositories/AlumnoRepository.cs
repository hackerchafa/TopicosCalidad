using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaEscolar.Data;

namespace PlataformaEscolar.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly PlataformaEscolarContext _context;
        public AlumnoRepository(PlataformaEscolarContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Alumno>> GetAllAsync() => await _context.Alumnos.ToListAsync();
        public async Task<Alumno> GetByIdAsync(int id) => await _context.Alumnos.FindAsync(id);
        public async Task<Alumno> AddAsync(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }
        public async Task<Alumno> UpdateAsync(Alumno alumno)
        {
            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null) return false;
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 