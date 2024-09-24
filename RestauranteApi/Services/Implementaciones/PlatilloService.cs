using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class PlatilloService : IPlatilloService
    {
        private readonly RestauranteContext _restauranteContext;

        public PlatilloService(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        
        public async Task<IEnumerable<Platillo>> GetPlatillos()
        {
            return await _restauranteContext.Platillos.ToListAsync();
        }

        
        public async Task<Platillo> GetPlatilloById(int id)
        {
            return await _restauranteContext.Platillos.FirstOrDefaultAsync(p => p.Id == id);
        }

        
        public async Task<IEnumerable<Platillo>> GetPlatillosByCategoriaId(int categoriaId)
        {
            return await _restauranteContext.Platillos.Where(p => p.CategoriaId == categoriaId).ToListAsync();
        }

       
        public async Task<Platillo> CreatePlatillo(Platillo platillo)
        {
            _restauranteContext.Platillos.Add(platillo);
            await _restauranteContext.SaveChangesAsync();
            return platillo;
        }

        
        public async Task UpdatePlatillo(Platillo platillo, int id)
        {
            var platilloExistente = await _restauranteContext.Platillos.FirstOrDefaultAsync(p => p.Id == id);
            if (platilloExistente == null)
            {
                throw new KeyNotFoundException("Platillo no encontrado");
            }

            platilloExistente.NombrePlatillo = platillo.NombrePlatillo;
            platilloExistente.Descripcion = platillo.Descripcion;
            platilloExistente.Precio = platillo.Precio;
            platilloExistente.Imagen = platillo.Imagen;
            platilloExistente.CategoriaId = platillo.CategoriaId;

            await _restauranteContext.SaveChangesAsync();
        }

        
        public async Task DeletePlatillo(int id)
        {
            var platillo = await _restauranteContext.Platillos.FirstOrDefaultAsync(p => p.Id == id);
            if (platillo == null)
            {
                throw new KeyNotFoundException("Platillo no encontrado");
            }

            _restauranteContext.Platillos.Remove(platillo);
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
