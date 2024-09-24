<<<<<<< HEAD:RESTAURANTE-API--main/RestauranteApi/Controllers/PedidoController.cs
using System;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Controllers;

   [ApiController]
   [Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private static List<Pedido> _pedidos = new List<Pedido>();
    private static List<Producto> _productosDisponibles = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Pupusa de Queso", Precio = 1.50m },
    };

    // Obtener todos los pedidos
    [HttpGet]
    public IActionResult GetPedidos()
    {
        return Ok(_pedidos);
    }

    // Obtener un pedido por ID
    [HttpGet("{id}")]
    public IActionResult GetPedidoById(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido == null)
            return NotFound();
        
        return Ok(pedido);
    }

    // Crear un pedido
    [HttpPost]
    public IActionResult CreatePedido([FromBody] List<int> idsDeProductos)
    {
        var nuevoPedido = new Pedido();
        
        // Agregar productos al pedido basado en los IDs
        foreach (var idProducto in idsDeProductos)
        {
            var producto = _productosDisponibles.FirstOrDefault(p => p.Id == idProducto);
            if (producto != null)
            {
                nuevoPedido.Productos.Add(producto);
            }
        }
        
        // Calcular el total
        nuevoPedido.CalcularTotal();
        nuevoPedido.Id = _pedidos.Count + 1;
        
        _pedidos.Add(nuevoPedido);
        return CreatedAtAction(nameof(GetPedidoById), new { id = nuevoPedido.Id }, nuevoPedido);
    }
}

  
=======
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
>>>>>>> 0d264c3 (ProyectoRestauranteApi):RestauranteApi/Controllers/PedidoController.cs
