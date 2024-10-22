using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Tests
{
    public class PedidoServiceTest
    {
        private readonly PedidoService _pedidoService;
        private readonly RestauranteContext _restauranteContext;

        public PedidoServiceTest()
        {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _pedidoService = new PedidoService(_restauranteContext);
        }

        [Fact]
        public async Task CreatePedido()
        {
            var pedido = new Pedido
            {
                Nombre = "Hola",
                Apellido = "Evergarden",
                Gmail = "hola@gmail.com",
                DetallesPedido = "Ninguno", 
                Telefono = "123456789",
                FechaPedido = DateTime.Parse("2024-10-30")
            };

            var result = await _pedidoService.CreatePedido(pedido);
            var pedidoFromDb = await _restauranteContext.Pedidos.FirstOrDefaultAsync(p => p.Nombre == "Hola");

            Assert.NotNull(pedidoFromDb);
            Assert.Equal("Hola", pedidoFromDb.Nombre);
            Assert.Equal("Evergarden", pedidoFromDb.Apellido);
            Assert.Equal("hola@gmail.com", pedidoFromDb.Gmail);
            Assert.Equal("Ninguno", pedidoFromDb.DetallesPedido);
            Assert.Equal("123456789", pedidoFromDb.Telefono);
            Assert.Equal(DateTime.Parse("2024-10-30"), pedidoFromDb.FechaPedido);
        }

        [Fact]
        public async Task UpdatePedido()
        {
            var pedido = new Pedido
            {
                Nombre = "Violet",
                Apellido = "Test",
                Gmail = "violet@gmail.com",
                DetallesPedido = "Ninguno",
                Telefono = "12345678",
                FechaPedido = DateTime.Now
            };

            _restauranteContext.Pedidos.Add(pedido);
            await _restauranteContext.SaveChangesAsync();

            var updatePedido = new Pedido
            {
                Nombre = "Maria",
                Apellido = "Test",
                Gmail = "maria@gmail.com",
                DetallesPedido = "Modificado",
                Telefono = "1234",
                FechaPedido = DateTime.Now
            };

            await _pedidoService.UpdatePedido(updatePedido, pedido.Id);

            var pedidoFromDb = await _restauranteContext.Pedidos.FindAsync(pedido.Id);
            Assert.NotNull(pedidoFromDb);
            Assert.Equal("Maria", pedidoFromDb.Nombre);
            Assert.Equal("Test", pedidoFromDb.Apellido);
            Assert.Equal("maria@gmail.com", pedidoFromDb.Gmail);
            Assert.Equal("Modificado", pedidoFromDb.DetallesPedido);
            Assert.Equal("1234", pedidoFromDb.Telefono);
        }

        [Fact]
        public async Task DeletePedido()
        {
            var pedido = new Pedido
            {
                Nombre = "Hola",
                Apellido = "Evergarden",
                Gmail = "hola@gmail.com",
                DetallesPedido = "Ninguno",
                Telefono = "123456789",
                FechaPedido = DateTime.Parse("2024-10-30")
            };

            _restauranteContext.Pedidos.Add(pedido);
            await _restauranteContext.SaveChangesAsync();

            await _pedidoService.DeletePedido(pedido.Id);

            var pedidoFromDb = await _restauranteContext.Pedidos.FindAsync(pedido.Id);
            Assert.Null(pedidoFromDb);
        }

        [Fact]
        public async Task GetPedidoById()
        {
            var pedido = new Pedido
            {
                Nombre = "Hola",
                Apellido = "Evergarden",
                Gmail = "hola@gmail.com",
                DetallesPedido = "Ninguno",
                Telefono = "123456789",
                FechaPedido = DateTime.Parse("2024-10-30")
            };

            _restauranteContext.Pedidos.Add(pedido);
            await _restauranteContext.SaveChangesAsync();

            var result = await _pedidoService.GetPedidoById(pedido.Id);

            Assert.NotNull(result);
            Assert.Equal("Hola", result.Nombre);
            Assert.Equal("Evergarden", result.Apellido);
            Assert.Equal("hola@gmail.com", result.Gmail);
            Assert.Equal("Ninguno", result.DetallesPedido);
            Assert.Equal("123456789", result.Telefono);
            Assert.Equal(DateTime.Parse("2024-10-30"), result.FechaPedido);
        }

        [Fact]
        public async Task GetPedidos()
        {
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    Nombre = "Hola",
                    Apellido = "Evergarden",
                    Gmail = "hola@gmail.com",
                    DetallesPedido = "Ninguno",
                    Telefono = "123456789",
                    FechaPedido = DateTime.Parse("2024-10-30")
                },
            };

            _restauranteContext.Pedidos.AddRange(pedidos);
            await _restauranteContext.SaveChangesAsync();

            var result = await _pedidoService.GetPedidos();

            Assert.Equal(1, result.Count());
            Assert.Contains(result, p => p.Nombre == "Hola" && p.DetallesPedido == "Ninguno");
        }
    }
}
