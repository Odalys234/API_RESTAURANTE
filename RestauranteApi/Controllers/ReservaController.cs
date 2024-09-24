using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            var reservas = await _reservaService.GetReservas();
            return Ok(reservas);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _reservaService.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservasByCliente(int clienteId)
        {
            var reservas = await _reservaService.GetReservasByClienteId(clienteId);
            return Ok(reservas);
        }

       
        [HttpPost]
        public async Task<ActionResult<Reserva>> CreateReserva(Reserva reserva)
        {
            var reservaCreada = await _reservaService.CreateReserva(reserva);
            return CreatedAtAction(nameof(GetReserva), new { id = reservaCreada.Id }, reservaCreada);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(int id, Reserva reserva)
        {
            await _reservaService.UpdateReserva(reserva, id);
            return NoContent();
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            await _reservaService.DeleteReserva(id);
            return NoContent();
        }
    }
}
