using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoService.GetPedidos();
            return Ok(pedidos);
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

     
        [HttpPost]
        public async Task<ActionResult<Pedido>> CreatePedido(Pedido pedido)
        {
            var pedidoCreado = await _pedidoService.CreatePedido(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = pedidoCreado.Id }, pedidoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, Pedido pedido)
        {
            await _pedidoService.UpdatePedido(pedido, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            await _pedidoService.DeletePedido(id);
            return NoContent();
        }
    }
}
