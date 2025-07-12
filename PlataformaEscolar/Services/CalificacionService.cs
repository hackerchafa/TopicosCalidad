using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public class CalificacionService : ICalificacionService
    {
        public Task<IEnumerable<Calificacion>> GetAllAsync() => Task.FromResult<IEnumerable<Calificacion>>(new List<Calificacion>());
        public Task<Calificacion?> GetByIdAsync(int id) => Task.FromResult<Calificacion?>(null);
        public Task<Calificacion> AddAsync(Calificacion calificacion) => Task.FromResult(calificacion);
        public Task<Calificacion> UpdateAsync(Calificacion calificacion) => Task.FromResult(calificacion);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(true);
    }
} 