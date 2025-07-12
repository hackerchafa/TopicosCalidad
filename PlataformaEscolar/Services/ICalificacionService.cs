using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public interface ICalificacionService
    {
        Task<IEnumerable<Calificacion>> GetAllAsync();
        Task<Calificacion?> GetByIdAsync(int id);
        Task<Calificacion> AddAsync(Calificacion calificacion);
        Task<Calificacion> UpdateAsync(Calificacion calificacion);
        Task<bool> DeleteAsync(int id);
    }
} 