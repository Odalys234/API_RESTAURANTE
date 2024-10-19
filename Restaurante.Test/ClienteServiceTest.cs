using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Context {
    public class ClienteServiceTest {
        private readonly ClienteService _clienteService;
        private readonly RestauranteContext _restauranteContext;

        public ClienteServiceTest() {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _clienteService = new ClienteService(_restauranteContext);
        }

        [Fact]
        public async Task CreateCliente() {
            var cliente = new Cliente {
                Nombre = "Carlos",
                Apellido = "Méndez",
                Telefono = "123456789",
                Email = "carlos.mendez@gmail.com"
            };

            var result = await _clienteService.CreateCliente(cliente);
            var clienteFromDb = await _restauranteContext.Clientes.FirstOrDefaultAsync(c => c.Email == "carlos.mendez@gmail.com");
            
            Assert.NotNull(clienteFromDb);
            Assert.Equal("Carlos", clienteFromDb.Nombre);
            Assert.Equal("Méndez", clienteFromDb.Apellido);
            Assert.Equal("123456789", clienteFromDb.Telefono);
            Assert.Equal("carlos.mendez@gmail.com", clienteFromDb.Email);
        }

        [Fact]
        public async Task UpdateCliente() {
            var cliente = new Cliente {
                Nombre = "Ana",
                Apellido = "Pérez",
                Telefono = "987654321",
                Email = "ana.perez@gmail.com"
            };

            _restauranteContext.Clientes.Add(cliente);
            await _restauranteContext.SaveChangesAsync();

            var updateCliente = new Cliente {
                Nombre = "Ana María",
                Apellido = "García",
                Telefono = "111222333",
                Email = "ana.garcia@gmail.com"
            };

            await _clienteService.UpdateCliente(updateCliente, cliente.Id);

            var clienteFromDb = await _restauranteContext.Clientes.FindAsync(cliente.Id);
            Assert.NotNull(clienteFromDb);
            Assert.Equal("Ana María", clienteFromDb.Nombre);
            Assert.Equal("García", clienteFromDb.Apellido);
            Assert.Equal("111222333", clienteFromDb.Telefono);
            Assert.Equal("ana.garcia@gmail.com", clienteFromDb.Email);
        }

        [Fact]
        public async Task DeleteCliente() {
            var cliente = new Cliente {
                Nombre = "Juan",
                Apellido = "Gómez",
                Telefono = "555555555",
                Email = "juan.gomez@gmail.com"
            };

            _restauranteContext.Clientes.Add(cliente);
            await _restauranteContext.SaveChangesAsync();

            await _clienteService.DeleteCliente(cliente.Id);

            var clienteFromDb = await _restauranteContext.Clientes.FindAsync(cliente.Id);
            Assert.Null(clienteFromDb);
        }

        [Fact]
        public async Task GetClienteById() {
            var cliente = new Cliente {
                Nombre = "Luis",
                Apellido = "Ramírez",
                Telefono = "444444444",
                Email = "luis.ramirez@gmail.com"
            };

            _restauranteContext.Clientes.Add(cliente);
            await _restauranteContext.SaveChangesAsync();

            var result = await _clienteService.GetClienteById(cliente.Id);

            Assert.NotNull(result);
            Assert.Equal("Luis", result.Nombre);
            Assert.Equal("Ramírez", result.Apellido);
            Assert.Equal("444444444", result.Telefono);
            Assert.Equal("luis.ramirez@gmail.com", result.Email);
        }

        [Fact]
        public async Task GetClientes() {
            var clientes = new List<Cliente> {
                new Cliente { Nombre = "Julia", Apellido = "Martínez", Telefono = "123123123", Email = "julia.martinez@gmail.com" },
                new Cliente { Nombre = "Pedro", Apellido = "Sánchez", Telefono = "321321321", Email = "pedro.sanchez@gmail.com" }
            };

            _restauranteContext.Clientes.AddRange(clientes);
            await _restauranteContext.SaveChangesAsync();

            var result = await _clienteService.GetClientes();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nombre == "Julia");
            Assert.Contains(result, c => c.Nombre == "Pedro");
        }
    }
}
