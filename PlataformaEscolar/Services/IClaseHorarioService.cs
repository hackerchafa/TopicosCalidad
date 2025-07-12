using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public interface IClaseHorarioService
    {
        Task<IEnumerable<ClaseHorario>> GetAllAsync();
        Task<ClaseHorario?> GetByIdAsync(int id);
        Task<ClaseHorario> AddAsync(ClaseHorario claseHorario);
        Task<ClaseHorario> UpdateAsync(ClaseHorario claseHorario);
        Task<bool> DeleteAsync(int id);
    }
} 