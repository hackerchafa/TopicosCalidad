using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEscolar.Repositories
{
    public interface IAdministradorRepository
    {
        Task<IEnumerable<Administrador>> GetAllAsync();
        Task<Administrador> GetByIdAsync(int id);
        Task<Administrador> AddAsync(Administrador administrador);
        Task<Administrador> UpdateAsync(Administrador administrador);
        Task<bool> DeleteAsync(int id);
    }
} 