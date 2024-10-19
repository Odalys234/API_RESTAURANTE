using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Context {
    public class CategoriaServiceTest {
        private readonly CategoriaService _categoriaService;
        private readonly RestauranteContext _restauranteContext;

        public CategoriaServiceTest() {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _categoriaService = new CategoriaService(_restauranteContext);
        }

        [Fact]
        public async Task CreateCategoria() {
            var categoria = new Categoria {
                Nombre = "Sopas",
                Descripcion = "tipos sopas"
            };

            var result = await _categoriaService.CreateCategoria(categoria);
            var categoriaFromDb = await _restauranteContext.Categorias.FirstOrDefaultAsync(c => c.Nombre == "Sopas");
            
            Assert.NotNull(categoriaFromDb);
            Assert.Equal("Sopas", categoriaFromDb.Nombre);
            Assert.Equal("tipos sopas", categoriaFromDb.Descripcion);
        }

        [Fact]
        public async Task UpdateCategoria() {
            var categoria = new Categoria {
                Nombre = "Bebidas",
                Descripcion = "tipos bebidas"
            };

            _restauranteContext.Categorias.Add(categoria);
            await _restauranteContext.SaveChangesAsync();

            var updateCategoria = new Categoria {
                Nombre = "Postres",
                Descripcion = "tipos postres"
            };

            await _categoriaService.UpdateCategoria(updateCategoria, categoria.Id);

            var categoriaFromDb = await _restauranteContext.Categorias.FindAsync(categoria.Id);
            Assert.NotNull(categoriaFromDb);
            Assert.Equal("Postres", categoriaFromDb.Nombre);
            Assert.Equal("tipos postres", categoriaFromDb.Descripcion);
        }

        [Fact]
        public async Task DeleteCategoria() {
            var categoria = new Categoria {
                Nombre = "Ensaladas",
                Descripcion = "tipos ensaladas"
            };

            _restauranteContext.Categorias.Add(categoria);
            await _restauranteContext.SaveChangesAsync();

            await _categoriaService.DeleteCategoria(categoria.Id);

            var categoriaFromDb = await _restauranteContext.Categorias.FindAsync(categoria.Id);
            Assert.Null(categoriaFromDb);
        }

        [Fact]
        public async Task GetCategoriaById() {
            var categoria = new Categoria {
                Nombre = "Carnes",
                Descripcion = "tipos carnes"
            };

            _restauranteContext.Categorias.Add(categoria);
            await _restauranteContext.SaveChangesAsync();

            var result = await _categoriaService.GetCategoriaById(categoria.Id);

            Assert.NotNull(result);
            Assert.Equal("Carnes", result.Nombre);
            Assert.Equal("tipos carnes", result.Descripcion);
        }

        [Fact]
        public async Task GetCategorias() {
            var categorias = new List<Categoria> {
                new Categoria { Nombre = "Mariscos", Descripcion = "tipos mariscos" },
                new Categoria { Nombre = "Pastas", Descripcion = "tipos pastas" }
            };

            _restauranteContext.Categorias.AddRange(categorias);
            await _restauranteContext.SaveChangesAsync();

            var result = await _categoriaService.GetCategorias();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nombre == "Mariscos");
            Assert.Contains(result, c => c.Nombre == "Pastas");
        }
    }
}
