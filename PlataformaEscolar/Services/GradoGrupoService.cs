using PlataformaEscolar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PlataformaEscolar.Services
{
    public class GradoGrupoService : IGradoGrupoService
    {
        private static List<GradoGrupo> _datos = new List<GradoGrupo>
        {
            new GradoGrupo { Id = 1, Nombre = "PrimeroA", Descripcion = "Primer grado, grupo A" },
            new GradoGrupo { Id = 2, Nombre = "SegundoB", Descripcion = "Segundo grado, grupo B" },
            new GradoGrupo { Id = 3, Nombre = "TerceroC", Descripcion = "Tercer grado, grupo C" },
            new GradoGrupo { Id = 4, Nombre = "CuartoD", Descripcion = "Cuarto grado, grupo D" },
            new GradoGrupo { Id = 5, Nombre = "QuintoE", Descripcion = "Quinto grado, grupo E" }
        };
        public Task<IEnumerable<GradoGrupo>> GetAllAsync() => Task.FromResult<IEnumerable<GradoGrupo>>(_datos);
        public Task<GradoGrupo?> GetByIdAsync(int id) => Task.FromResult(_datos.FirstOrDefault(x => x.Id == id));
        public Task<GradoGrupo> AddAsync(GradoGrupo gradoGrupo)
        {
            gradoGrupo.Id = _datos.Max(x => x.Id) + 1;
            _datos.Add(gradoGrupo);
            return Task.FromResult(gradoGrupo);
        }
        public Task<GradoGrupo> UpdateAsync(GradoGrupo gradoGrupo)
        {
            var existente = _datos.FirstOrDefault(x => x.Id == gradoGrupo.Id);
            if (existente != null)
            {
                existente.Nombre = gradoGrupo.Nombre;
                existente.Descripcion = gradoGrupo.Descripcion;
            }
            return Task.FromResult(gradoGrupo);
        }
        public Task<bool> DeleteAsync(int id)
        {
            var existente = _datos.FirstOrDefault(x => x.Id == id);
            if (existente != null)
                _datos.Remove(existente);
            return Task.FromResult(true);
        }
    }
} 