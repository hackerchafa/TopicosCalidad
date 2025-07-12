using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public class ClaseHorarioService : IClaseHorarioService
    {
        public Task<IEnumerable<ClaseHorario>> GetAllAsync() => Task.FromResult<IEnumerable<ClaseHorario>>(new List<ClaseHorario>());
        public Task<ClaseHorario?> GetByIdAsync(int id) => Task.FromResult<ClaseHorario?>(null);
        public Task<ClaseHorario> AddAsync(ClaseHorario claseHorario) => Task.FromResult(claseHorario);
        public Task<ClaseHorario> UpdateAsync(ClaseHorario claseHorario) => Task.FromResult(claseHorario);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(true);
    }
} 