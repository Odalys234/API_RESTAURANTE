using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Tests
{
    public class EmpleadoServiceTest
    {
        private readonly EmpleadoService _empleadoService;
        private readonly RestauranteContext _restauranteContext;

        public EmpleadoServiceTest()
        {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _empleadoService = new EmpleadoService(_restauranteContext);
        }

        [Fact]
        public async Task CreateEmpleado()
        {
            var empleado = new Empleado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juanperez@example.com",
                Telefono = "123-456-0789",
                PuestoId = 1 
            };

            var result = await _empleadoService.CreateEmpleado(empleado);
            var empleadoFromDb = await _restauranteContext.Empleados.FirstOrDefaultAsync(e => e.Nombre == "Juan");

            Assert.NotNull(empleadoFromDb);
            Assert.Equal("Juan", empleadoFromDb.Nombre);
            Assert.Equal("Pérez", empleadoFromDb.Apellido);
            Assert.Equal("juanperez@example.com", empleadoFromDb.Email);
            Assert.Equal("123-456-0789", empleadoFromDb.Telefono);
            Assert.Equal(1, empleadoFromDb.PuestoId);
        }

        [Fact]
        public async Task GetEmpleadoById()
        {
            var empleado = new Empleado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juanperez@example.com",
                Telefono = "123-456-0789",
                PuestoId = 1
            };

            _restauranteContext.Empleados.Add(empleado);
            await _restauranteContext.SaveChangesAsync();

            var result = await _empleadoService.GetEmpleadoById(empleado.Id);

            Assert.NotNull(result);
            Assert.Equal("Juan", result.Nombre);
            Assert.Equal("Pérez", result.Apellido);
            Assert.Equal("juanperez@example.com", result.Email);
            Assert.Equal("123-456-0789", result.Telefono);
            Assert.Equal(1, result.PuestoId);
        }

        [Fact]
        public async Task GetEmpleados()
        {
            var empleados = new List<Empleado>
            {
                new Empleado 
                { 
                    Nombre = "Carlos", 
                    Apellido = "Martínez", 
                    Email = "carlos@example.com", 
                    Telefono = "234-567-8901", 
                    PuestoId = 1 
                },
                new Empleado 
                { 
                    Nombre = "Ana", 
                    Apellido = "García", 
                    Email = "ana@example.com", 
                    Telefono = "345-678-9012", 
                    PuestoId = 2 
                }
            };

            _restauranteContext.Empleados.AddRange(empleados);
            await _restauranteContext.SaveChangesAsync();

            var result = await _empleadoService.GetEmpleados();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Nombre == "Carlos");
            Assert.Contains(result, e => e.Nombre == "Ana");
        }

        [Fact]
        public async Task UpdateEmpleado()
        {
            var empleado = new Empleado
            {
                Nombre = "Luis",
                Apellido = "Ramírez",
                Email = "luis@example.com",
                Telefono = "456-789-0123",
                PuestoId = 1
            };

            _restauranteContext.Empleados.Add(empleado);
            await _restauranteContext.SaveChangesAsync();

            empleado.Nombre = "Luis Actualizado";
            await _empleadoService.UpdateEmpleado(empleado, empleado.Id);

            var empleadoFromDb = await _restauranteContext.Empleados.FindAsync(empleado.Id);
            Assert.NotNull(empleadoFromDb);
            Assert.Equal("Luis Actualizado", empleadoFromDb.Nombre);
            Assert.Equal("Ramírez", empleadoFromDb.Apellido);
            Assert.Equal("luis@example.com", empleadoFromDb.Email);
            Assert.Equal("456-789-0123", empleadoFromDb.Telefono);
            Assert.Equal(1, empleadoFromDb.PuestoId);
        }

        [Fact]
        public async Task DeleteEmpleado()
        {
            var empleado = new Empleado
            {
                Nombre = "Elena",
                Apellido = "Gonzalez",
                Email = "elena@example.com",
                Telefono = "987-654-3210",
                PuestoId = 1
            };

            _restauranteContext.Empleados.Add(empleado);
            await _restauranteContext.SaveChangesAsync();

            await _empleadoService.DeleteEmpleado(empleado.Id);

            var empleadoFromDb = await _restauranteContext.Empleados.FindAsync(empleado.Id);
            Assert.Null(empleadoFromDb);
        }

        [Fact]
        public async Task GetEmpleadosByPuestoId()
        {
            var puestoId = 1;

            var empleados = new List<Empleado>
            {
                new Empleado 
                { 
                    Nombre = "Pedro", 
                    Apellido = "López", 
                    Email = "pedro@example.com", 
                    Telefono = "567-890-1234", 
                    PuestoId = puestoId 
                },
                new Empleado 
                { 
                    Nombre = "Laura", 
                    Apellido = "Martínez", 
                    Email = "laura@example.com", 
                    Telefono = "678-901-2345", 
                    PuestoId = puestoId 
                }
            };

            _restauranteContext.Empleados.AddRange(empleados);
            await _restauranteContext.SaveChangesAsync();

            var result = await _empleadoService.GetEmpleadosByPuestoId(puestoId);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Nombre == "Pedro");
            Assert.Contains(result, e => e.Nombre == "Laura");
        }
    }
}
