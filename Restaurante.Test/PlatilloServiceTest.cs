using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Context
{
    public class PlatilloServiceTest
    {
        private readonly PlatilloService _platilloService;
        private readonly RestauranteContext _restauranteContext;

        public PlatilloServiceTest()
        {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _platilloService = new PlatilloService(_restauranteContext);
        }

        [Fact]
        public async Task CreatePlatillo()
        {
            var platillo = new Platillo
            {
                NombrePlatillo = "Pollo",
                Descripcion = "Rico Pollo",
                Imagen = "https://www.elespectador.com/resizer/v2/2F4WF2CH7BC4DCCLMRBGS34KVQ.jpg?auth=744477b0610366720bbeeb9f3515ec384550475a1f106e81ab1d356c88294960&width=920&height=613&smart=true&quality=60",
                Precio = 5.0m, 
                CategoriaId = 1 
            };
            var result = await _platilloService.CreatePlatillo(platillo);
            var platilloFromDb = await _restauranteContext.Platillos.FirstOrDefaultAsync(p => p.NombrePlatillo == "Pollo");

            Assert.NotNull(platilloFromDb);
            Assert.Equal("Pollo", platilloFromDb.NombrePlatillo);
            Assert.Equal("Rico Pollo", platilloFromDb.Descripcion);
            Assert.Equal("https://www.elespectador.com/resizer/v2/2F4WF2CH7BC4DCCLMRBGS34KVQ.jpg?auth=744477b0610366720bbeeb9f3515ec384550475a1f106e81ab1d356c88294960&width=920&height=613&smart=true&quality=60", platilloFromDb.Imagen);
            Assert.Equal(5.0m, platilloFromDb.Precio); 
            Assert.Equal(1, platilloFromDb.CategoriaId); 
        }

        [Fact]
        public async Task UpdatePlatillo()
        {
            var platillo = new Platillo
            {
                NombrePlatillo = "Marisco",
                Descripcion = "Rico Marisco",
                Imagen = "https://img.freepik.com/foto-gratis/deliciosa-langosta-mariscos-gourmet_23-2151713002.jpg?t=st=1729361157~exp=1729364757~hmac=59758a49a39c2317d47c093ed7984fa0c0b23ac42efabbda26cc0bcf2958daa5&w=360",
                Precio = 10.0m, 
                CategoriaId = 2
            };
            _restauranteContext.Platillos.Add(platillo);
            await _restauranteContext.SaveChangesAsync();

            var updatePlatillo = new Platillo
            {
                NombrePlatillo = "Marisco",
                Descripcion = "Deliciosos mariscos",
                Imagen = "https://img.freepik.com/foto-gratis/deliciosa-langosta-mariscos-gourmet_23-2151713002.jpg?t=st=1729361157~exp=1729364757~hmac=59758a49a39c2317d47c093ed7984fa0c0b23ac42efabbda26cc0bcf2958daa5&w=360",
                Precio = 10.0m, 
                CategoriaId = 2 
            };

            await _platilloService.UpdatePlatillo(updatePlatillo, platillo.Id);
            var platilloFromDb = await _restauranteContext.Platillos.FindAsync(platillo.Id);
            Assert.NotNull(platilloFromDb);
            Assert.Equal("Marisco", platilloFromDb.NombrePlatillo);
            Assert.Equal("Deliciosos mariscos", platilloFromDb.Descripcion);
            Assert.Equal("https://img.freepik.com/foto-gratis/deliciosa-langosta-mariscos-gourmet_23-2151713002.jpg?t=st=1729361157~exp=1729364757~hmac=59758a49a39c2317d47c093ed7984fa0c0b23ac42efabbda26cc0bcf2958daa5&w=360", platilloFromDb.Imagen);
            Assert.Equal(10.0m, platilloFromDb.Precio); 
            Assert.Equal(2, platilloFromDb.CategoriaId); 
        }

        [Fact]
        public async Task DeletePlatillo()
        {
            var platillo = new Platillo
            {
                NombrePlatillo = "Pollo",
                Descripcion = "Rico Pollo",
                Imagen = "https://www.elespectador.com/resizer/v2/2F4WF2CH7BC4DCCLMRBGS34KVQ.jpg?auth=744477b0610366720bbeeb9f3515ec384550475a1f106e81ab1d356c88294960&width=920&height=613&smart=true&quality=60",
                Precio = 5.0m, 
                CategoriaId = 1 
            };
            _restauranteContext.Platillos.Add(platillo);
            await _restauranteContext.SaveChangesAsync();

            await _platilloService.DeletePlatillo(platillo.Id);
            var platilloFromDb = await _restauranteContext.Platillos.FindAsync(platillo.Id);
            Assert.Null(platilloFromDb);
        }

        [Fact]
        public async Task GetPlatilloById()
        {
            var platillo = new Platillo
            {
                NombrePlatillo = "Pollo",
                Descripcion = "Rico Pollo",
                Imagen = "https://www.elespectador.com/resizer/v2/2F4WF2CH7BC4DCCLMRBGS34KVQ.jpg?auth=744477b0610366720bbeeb9f3515ec384550475a1f106e81ab1d356c88294960&width=920&height=613&smart=true&quality=60",
                Precio = 5.0m, 
                CategoriaId = 1 
            };
            _restauranteContext.Platillos.Add(platillo);
            await _restauranteContext.SaveChangesAsync();

            var result = await _platilloService.GetPlatilloById(platillo.Id);
            Assert.NotNull(result);
            Assert.Equal("Pollo", result.NombrePlatillo);
            Assert.Equal("Rico Pollo", result.Descripcion);
            Assert.Equal("https://www.elespectador.com/resizer/v2/2F4WF2CH7BC4DCCLMRBGS34KVQ.jpg?auth=744477b0610366720bbeeb9f3515ec384550475a1f106e81ab1d356c88294960&width=920&height=613&smart=true&quality=60", result.Imagen);
            Assert.Equal(5.0m, result.Precio); 
            Assert.Equal(1, result.CategoriaId); 
        }

        [Fact]
        public async Task GetPlatillos()
        {
            var platillos = new List<Platillo>
            {
                new Platillo
                {
                    NombrePlatillo = "Papas",
                    Descripcion = "Papas fritas",
                    Imagen = "https://w6h5a5r4.rocketcdn.me/wp-content/uploads/2023/08/papas-fritas-cubiertas-queso-cheddar-vertido-o-tirado-1.jpg",
                    Precio = 2.0m, 
                    CategoriaId = 3 
                },
                new Platillo
                {
                    NombrePlatillo = "Yuca",
                    Descripcion = "Yuca frita",
                    Imagen = "https://www.tipicosmargoth.com/wp-content/uploads/2020/05/yuca-frita-1.jpg",
                    Precio = 3.0m, 
                    CategoriaId = 4 
                }
            };

            _restauranteContext.Platillos.AddRange(platillos);
            await _restauranteContext.SaveChangesAsync();

            var result = await _platilloService.GetPlatillos();
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.NombrePlatillo == "Papas");
            Assert.Contains(result, p => p.NombrePlatillo == "Yuca");
        }
        [Fact]
        public async Task GetPlatillosByCategoriaId()
        {
            var categoriaId = 1;

            var platillos = new List<Platillo>
            {
                new Platillo
                {
                    NombrePlatillo = "Pupusa",
                    Descripcion = "Rica Pupusas",
                    Imagen = "https://www.paulinacocina.net/wp-content/uploads/2023/04/20230410_111551_0002-800x450.jpg",
                    Precio = 5.0m,
                    CategoriaId = categoriaId
                },
                new Platillo
                {
                    NombrePlatillo = "Tamal",
                    Descripcion = "Rico tamal",
                    Imagen = "https://imagenes.eltiempo.com/files/image_1200_600/uploads/2022/12/20/63a1fe9ac7981.jpeg",
                    Precio = 1.0m,
                    CategoriaId = categoriaId
                }
            };
            _restauranteContext.Platillos.AddRange(platillos);
            await _restauranteContext.SaveChangesAsync();

            var result = await _platilloService.GetPlatillosByCategoriaId(categoriaId);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.NombrePlatillo == "Pupusa");
            Assert.Contains(result, p => p.NombrePlatillo == "Tamal");
        }
       

    }
}
