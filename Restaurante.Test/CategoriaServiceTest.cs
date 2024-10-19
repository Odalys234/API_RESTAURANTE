//pueden usar las mismas referencias
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace RestauranteApi.Context;
//hasta aqui
// entidad  asignada
public class CategoriaServiceTest {
    private readonly CategoriaService _categoriaService;
    private readonly RestauranteContext _restauranteContext;

   public CategoriaServiceTest() {
        _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
    //variable segun entidad asignada
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


    }

