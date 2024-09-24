using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IVotoService
    {
        Task<IEnumerable<Voto>> GetVotos();  
        Task<Voto> GetVotoById(int id);  
        Task<Voto> CreateVoto(Voto voto); 
        Task UpdateVoto(Voto voto, int id);  
        Task DeleteVoto(int id);  
    }
}
