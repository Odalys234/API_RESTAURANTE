using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class CategoriaService : ICategoriaService
    {
        private readonly RestauranteContext _restauranteContext;

        public CategoriaService(RestauranteContext context)
        {
            _restauranteContext = context;
        }

        public async Task<Categoria> CreateCategoria(Categoria categoria)
        {
            _restauranteContext.Categorias.Add(categoria);
            await _restauranteContext.SaveChangesAsync();
            return categoria;
        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await _restauranteContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null) throw new KeyNotFoundException("Categoría no encontrada");
            _restauranteContext.Categorias.Remove(categoria);
            await _restauranteContext.SaveChangesAsync();
        }

        public async Task<Categoria> GetCategoriaById(int id)
        {
            return await _restauranteContext.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _restauranteContext.Categorias.ToListAsync();
        }

        public async Task UpdateCategoria(Categoria categoria, int id)
        {
            var categoriaExistente = await _restauranteContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoriaExistente == null) throw new KeyNotFoundException("Categoría no encontrada");
            
            categoriaExistente.Nombre = categoria.Nombre;
            categoriaExistente.Descripcion = categoria.Descripcion;
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
