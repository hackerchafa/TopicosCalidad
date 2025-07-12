using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Services
{
    public class ComentarioService : IComentarioService
    {
        public Task<IEnumerable<Comentario>> GetAllAsync() => Task.FromResult<IEnumerable<Comentario>>(new List<Comentario>());
        public Task<Comentario?> GetByIdAsync(int id) => Task.FromResult<Comentario?>(null);
        public Task<Comentario> AddAsync(Comentario comentario) => Task.FromResult(comentario);
        public Task<Comentario> UpdateAsync(Comentario comentario) => Task.FromResult(comentario);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(true);
    }
} 