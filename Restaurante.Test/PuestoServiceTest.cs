using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace RestauranteApi.Context;

public class PuestoServiceTest {
    private readonly PuestoService _puestoService;
    private readonly RestauranteContext _restauranteContext;

   public PuestoServiceTest() {
        _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
    
        _puestoService = new PuestoService(_restauranteContext);
    }

    [Fact]
    public async Task CreatePuesto() {
        var puesto = new Puesto {
            Nombre = "Gerente",
            Descripcion = "bodega"
            
        };

        var result = await _puestoService.CreatePuesto(puesto);
        var puestoFromDb = await _restauranteContext.Puestos.FirstOrDefaultAsync(p => p.Nombre == "Gerente");
       
        Assert.NotNull(puestoFromDb);
        Assert.Equal("Gerente", puestoFromDb.Nombre);
        Assert.Equal("bodega", puestoFromDb.Descripcion);
    }
    [Fact]
    public async Task UpdatePuesto() {
        var puesto = new Puesto {
            Nombre = "Gerente",
            Descripcion = "bodega"
        };
       
        _restauranteContext.Puestos.Add(puesto);
        await _restauranteContext.SaveChangesAsync();
       
        var updatePuesto = new Puesto {
             Nombre = "Gerente",
            Descripcion = "bodega"
        };
       
        await _puestoService.UpdatePuesto(updatePuesto, puesto.Id);
       
        var puestoFromDb = await _restauranteContext.Puestos.FindAsync(puesto.Id);
        Assert.NotNull(puestoFromDb);
        Assert.Equal("Gerente", puestoFromDb.Nombre);
        Assert.Equal("bodega", puestoFromDb.Descripcion);
    }

    [Fact]
    public async Task DeletePuesto() {
        var puesto = new Puesto {
            Nombre = "Gerente",
            Descripcion = "bodega"
        };
       
        _restauranteContext.Puestos.Add(puesto);
        await _restauranteContext.SaveChangesAsync();

        await _puestoService.DeletePuesto(puesto.Id);
       
        var puestoFromDb = await _restauranteContext.Puestos.FindAsync(puesto.Id);
        Assert.Null(puestoFromDb);
    }

    [Fact]
    public async Task GetPuestoById() {
        var puesto = new Puesto {
            Nombre = "Gerente",
            Descripcion = "bodega"
        };
       
        _restauranteContext.Puestos.Add(puesto);
        await _restauranteContext.SaveChangesAsync();
       
        var result = await _puestoService.GetPuestoById(puesto.Id);
       
        Assert.NotNull(result);
        Assert.Equal("Gerente", result.Nombre);
        Assert.Equal("bodega", result.Descripcion);
       
    }

    [Fact]
    public async Task GetPuestos() {
        var puestos = new List<Puesto> {
            new Puesto { Nombre = "Gerente", Descripcion = "bodega" },
            new Puesto { Nombre = "Gerente1", Descripcion = "bodega1" }
        };
       
        _restauranteContext.Puestos.AddRange(puestos);
        await _restauranteContext.SaveChangesAsync();
       
        var result = await _puestoService.GetPuestos();
       
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Nombre == "Gerente");
        Assert.Contains(result, p => p.Nombre == "Gerente1");
    }


}

