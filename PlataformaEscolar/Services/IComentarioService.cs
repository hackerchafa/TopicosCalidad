using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public interface IComentarioService
    {
        Task<IEnumerable<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task<Comentario> AddAsync(Comentario comentario);
        Task<Comentario> UpdateAsync(Comentario comentario);
        Task<bool> DeleteAsync(int id);
    }
} 