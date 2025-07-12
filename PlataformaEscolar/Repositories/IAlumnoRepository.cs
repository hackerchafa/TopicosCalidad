using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Repositories
{
    public interface IAlumnoRepository
    {
        Task<IEnumerable<Alumno>> GetAllAsync();
        Task<Alumno> GetByIdAsync(int id);
        Task<Alumno> AddAsync(Alumno alumno);
        Task<Alumno> UpdateAsync(Alumno alumno);
        Task<bool> DeleteAsync(int id);
    }
} 