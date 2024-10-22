using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Implementaciones;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestauranteApi.Tests {
    public class ReservaServiceTest {
        private readonly ReservaService _reservaService;
        private readonly RestauranteContext _restauranteContext;

        public ReservaServiceTest() {
            _restauranteContext = RestauranteContextMemory<RestauranteContext>.CreateDbContext(Guid.NewGuid().ToString());
            _reservaService = new ReservaService(_restauranteContext);
        }

        [Fact]
        public async Task CreateReserva() {
            var reserva = new Reserva {
                ClienteId = 12,  
                FechaReserva = DateTime.Parse("29/12/2024"),  
                HoraReserva = DateTime.Now.TimeOfDay,  
                NumeroPersonas = 2,  
                MesaAsignada = 8  
            };

            var result = await _reservaService.CreateReserva(reserva);
            var reservaFromDb = await _restauranteContext.Reservas.FirstOrDefaultAsync(r => r.ClienteId == 12);

            Assert.NotNull(reservaFromDb);
            Assert.Equal(12, reservaFromDb.ClienteId);
            Assert.Equal(DateTime.Parse("29/12/2024"), reservaFromDb.FechaReserva);
            Assert.Equal(2, reservaFromDb.NumeroPersonas);
            Assert.Equal(8, reservaFromDb.MesaAsignada); 
        }

        [Fact]
        public async Task UpdateReserva() {
            var reserva = new Reserva {
                ClienteId = 12,
                FechaReserva = DateTime.Parse("29/12/2024"),
                HoraReserva = DateTime.Now.TimeOfDay,
                NumeroPersonas = 2,
                MesaAsignada = 8
            };

            _restauranteContext.Reservas.Add(reserva);
            await _restauranteContext.SaveChangesAsync();

            var updateReserva = new Reserva {
                ClienteId = 12,
                FechaReserva = DateTime.Parse("30/12/2024"),
                HoraReserva = DateTime.Now.TimeOfDay,
                NumeroPersonas = 2,
                MesaAsignada = 8
            };

            await _reservaService.UpdateReserva(updateReserva, reserva.Id);

            var reservaFromDb = await _restauranteContext.Reservas.FindAsync(reserva.Id);
            Assert.NotNull(reservaFromDb);
            Assert.Equal(DateTime.Parse("30/12/2024"), reservaFromDb.FechaReserva);
            Assert.Equal(2, reservaFromDb.NumeroPersonas);
            Assert.Equal(8, reservaFromDb.MesaAsignada);  

        [Fact]
        public async Task DeleteReserva() {
            var reserva = new Reserva {
                ClienteId = 12,
                FechaReserva = DateTime.Parse("29/12/2024"),
                HoraReserva = DateTime.Now.TimeOfDay,
                NumeroPersonas = 2,
                MesaAsignada = 8
            };

            _restauranteContext.Reservas.Add(reserva);
            await _restauranteContext.SaveChangesAsync();

            await _reservaService.DeleteReserva(reserva.Id);

            var reservaFromDb = await _restauranteContext.Reservas.FindAsync(reserva.Id);
            Assert.Null(reservaFromDb);
        }

        [Fact]
        public async Task GetReservaById() {
            var reserva = new Reserva {
                ClienteId = 12,
                FechaReserva = DateTime.Parse("29/12/2024"),
                HoraReserva = DateTime.Now.TimeOfDay,
                NumeroPersonas = 2,
                MesaAsignada = 8
            };

            _restauranteContext.Reservas.Add(reserva);
            await _restauranteContext.SaveChangesAsync();

            var result = await _reservaService.GetReservaById(reserva.Id);

            Assert.NotNull(result);
            Assert.Equal(12, result.ClienteId);
            Assert.Equal(DateTime.Parse("29/12/2024"), result.FechaReserva);
            Assert.Equal(2, result.NumeroPersonas);
            Assert.Equal(8, result.MesaAsignada); 
        }

        [Fact]
        public async Task GetReservas() {
            var reservas = new List<Reserva> {
                new Reserva { ClienteId = 12, FechaReserva = DateTime.Now, HoraReserva = DateTime.Now.TimeOfDay, NumeroPersonas = 2, MesaAsignada = 8 },
            };

            _restauranteContext.Reservas.AddRange(reservas);
            await _restauranteContext.SaveChangesAsync();

            var result = await _reservaService.GetReservas();

            Assert.Single(result);  
            Assert.Contains(result, r => r.ClienteId == 12);
        }
        }
    }
}