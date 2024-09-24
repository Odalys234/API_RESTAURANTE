using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IPuestoService
    {
        Task<IEnumerable<Puesto>> GetPuestos();
        Task<Puesto> GetPuestoById(int id);
        Task<Puesto> CreatePuesto(Puesto puesto);
        Task UpdatePuesto(Puesto puesto, int id);
        Task DeletePuesto(int id);
    }
}
