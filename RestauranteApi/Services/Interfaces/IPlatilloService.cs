using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IPlatilloService
    {
        Task<IEnumerable<Platillo>> GetPlatillos();
        Task<Platillo> GetPlatilloById(int id);
        Task<IEnumerable<Platillo>> GetPlatillosByCategoriaId(int categoriaId);  
        Task<Platillo> CreatePlatillo(Platillo platillo);
        Task UpdatePlatillo(Platillo platillo, int id);
        Task DeletePlatillo(int id);
    }
}
