using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> GetReservas();
        Task<Reserva> GetReservaById(int id);
        Task<IEnumerable<Reserva>> GetReservasByClienteId(int clienteId);
        Task<Reserva> CreateReserva(Reserva reserva);
        Task UpdateReserva(Reserva reserva, int id);
        Task DeleteReserva(int id);
    }
}
