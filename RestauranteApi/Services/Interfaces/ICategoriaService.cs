using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetCategorias();
        Task<Categoria> GetCategoriaById(int id);
        Task<Categoria> CreateCategoria(Categoria categoria);
        Task UpdateCategoria(Categoria categoria, int id);
        Task DeleteCategoria(int id);
    }
}
