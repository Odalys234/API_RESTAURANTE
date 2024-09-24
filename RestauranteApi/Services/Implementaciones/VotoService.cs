using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class VotoService : IVotoService
    {
        private readonly RestauranteContext _restauranteContext;

        public VotoService(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        
        public async Task<IEnumerable<Voto>> GetVotos()
        {
            return await _restauranteContext.Votos.ToListAsync();
        }

       
        public async Task<Voto> GetVotoById(int id)
        {
            return await _restauranteContext.Votos.FirstOrDefaultAsync(v => v.Id == id);
        }

        
        public async Task<Voto> CreateVoto(Voto voto)
        {
            _restauranteContext.Votos.Add(voto);
            await _restauranteContext.SaveChangesAsync();
            return voto;
        }

      
        public async Task UpdateVoto(Voto voto, int id)
        {
            var votoExistente = await _restauranteContext.Votos.FirstOrDefaultAsync(v => v.Id == id);
            if (votoExistente == null)
            {
                throw new KeyNotFoundException("Voto no encontrado");
            }

            votoExistente.PlatilloId = voto.PlatilloId;
            votoExistente.ClienteId = voto.ClienteId;
            votoExistente.FechaVotacion = voto.FechaVotacion;

            await _restauranteContext.SaveChangesAsync();
        }

       
        public async Task DeleteVoto(int id)
        {
            var voto = await _restauranteContext.Votos.FirstOrDefaultAsync(v => v.Id == id);
            if (voto == null)
            {
                throw new KeyNotFoundException("Voto no encontrado");
            }

            _restauranteContext.Votos.Remove(voto);
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
