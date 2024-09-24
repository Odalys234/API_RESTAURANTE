using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class ReservaService : IReservaService
    {
        private readonly RestauranteContext _restauranteContext;

        public ReservaService(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

       
        public async Task<IEnumerable<Reserva>> GetReservas()
        {
            return await _restauranteContext.Reservas.ToListAsync();
        }

        
        public async Task<Reserva> GetReservaById(int id)
        {
            return await _restauranteContext.Reservas.FirstOrDefaultAsync(r => r.Id == id);
        }

        
        public async Task<IEnumerable<Reserva>> GetReservasByClienteId(int clienteId)
        {
            return await _restauranteContext.Reservas.Where(r => r.ClienteId == clienteId).ToListAsync();
        }

        
        public async Task<Reserva> CreateReserva(Reserva reserva)
        {
            _restauranteContext.Reservas.Add(reserva);
            await _restauranteContext.SaveChangesAsync();
            return reserva;
        }

        
        public async Task UpdateReserva(Reserva reserva, int id)
        {
            var reservaExistente = await _restauranteContext.Reservas.FirstOrDefaultAsync(r => r.Id == id);
            if (reservaExistente == null)
            {
                throw new KeyNotFoundException("Reserva no encontrada");
            }

            reservaExistente.FechaReserva = reserva.FechaReserva;
            reservaExistente.HoraReserva = reserva.HoraReserva;
            reservaExistente.NumeroPersonas = reserva.NumeroPersonas;
            reservaExistente.MesaAsignada = reserva.MesaAsignada;
            reservaExistente.ClienteId = reserva.ClienteId;

            await _restauranteContext.SaveChangesAsync();
        }

        
        public async Task DeleteReserva(int id)
        {
            var reserva = await _restauranteContext.Reservas.FirstOrDefaultAsync(r => r.Id == id);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva no encontrada");
            }

            _restauranteContext.Reservas.Remove(reserva);
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
