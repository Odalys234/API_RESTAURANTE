using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class PuestoService : IPuestoService
    {
        private readonly RestauranteContext _restauranteContext;

        public PuestoService(RestauranteContext context)
        {
            _restauranteContext = context;
        }

        
        public async Task<Puesto> CreatePuesto(Puesto puesto)
        {
            _restauranteContext.Puestos.Add(puesto);
            await _restauranteContext.SaveChangesAsync();
            return puesto;
        }

        
        public async Task DeletePuesto(int id)
        {
            var puesto = await _restauranteContext.Puestos.FirstOrDefaultAsync(p => p.Id == id);
            if (puesto == null) throw new KeyNotFoundException("Puesto no encontrado");
            _restauranteContext.Puestos.Remove(puesto);
            await _restauranteContext.SaveChangesAsync();
        }

       
        public async Task<Puesto> GetPuestoById(int id)
        {
            return await _restauranteContext.Puestos.FindAsync(id);
        }

        
        public async Task<IEnumerable<Puesto>> GetPuestos()
        {
            return await _restauranteContext.Puestos.ToListAsync();
        }

     
        public async Task UpdatePuesto(Puesto puesto, int id)
        {
            var puestoExistente = await _restauranteContext.Puestos.FirstOrDefaultAsync(p => p.Id == id);
            if (puestoExistente == null) throw new KeyNotFoundException("Puesto no encontrado");

            puestoExistente.Nombre = puesto.Nombre;
            puestoExistente.Descripcion = puesto.Descripcion;
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
