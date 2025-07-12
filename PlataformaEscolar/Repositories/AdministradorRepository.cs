using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaEscolar.Data;

namespace PlataformaEscolar.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly PlataformaEscolarContext _context;
        public AdministradorRepository(PlataformaEscolarContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Administrador>> GetAllAsync() => await _context.Administradores.ToListAsync();
        public async Task<Administrador> GetByIdAsync(int id) => await _context.Administradores.FindAsync(id);
        public async Task<Administrador> AddAsync(Administrador administrador)
        {
            _context.Administradores.Add(administrador);
            await _context.SaveChangesAsync();
            return administrador;
        }
        public async Task<Administrador> UpdateAsync(Administrador administrador)
        {
            _context.Administradores.Update(administrador);
            await _context.SaveChangesAsync();
            return administrador;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var administrador = await _context.Administradores.FindAsync(id);
            if (administrador == null) return false;
            _context.Administradores.Remove(administrador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 