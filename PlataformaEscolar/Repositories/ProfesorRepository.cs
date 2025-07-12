using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaEscolar.Data;

namespace PlataformaEscolar.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly PlataformaEscolarContext _context;
        public ProfesorRepository(PlataformaEscolarContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profesor>> GetAllAsync() => await _context.Profesores.ToListAsync();
        public async Task<Profesor> GetByIdAsync(int id) => await _context.Profesores.FindAsync(id);
        public async Task<Profesor> AddAsync(Profesor profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }
        public async Task<Profesor> UpdateAsync(Profesor profesor)
        {
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null) return false;
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 