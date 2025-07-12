using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Repositories
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor> GetByIdAsync(int id);
        Task<Profesor> AddAsync(Profesor profesor);
        Task<Profesor> UpdateAsync(Profesor profesor);
        Task<bool> DeleteAsync(int id);
    }
} 