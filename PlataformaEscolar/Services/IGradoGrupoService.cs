using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public interface IGradoGrupoService
    {
        Task<IEnumerable<GradoGrupo>> GetAllAsync();
        Task<GradoGrupo?> GetByIdAsync(int id);
        Task<GradoGrupo> AddAsync(GradoGrupo gradoGrupo);
        Task<GradoGrupo> UpdateAsync(GradoGrupo gradoGrupo);
        Task<bool> DeleteAsync(int id);
    }
} 