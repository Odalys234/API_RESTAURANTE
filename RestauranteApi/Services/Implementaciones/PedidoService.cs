using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly RestauranteContext _restauranteContext;

        public PedidoService(RestauranteContext context)
        {
            _restauranteContext = context;
        }

    
        public async Task<Pedido> CreatePedido(Pedido pedido)
        {
            _restauranteContext.Pedidos.Add(pedido);
            await _restauranteContext.SaveChangesAsync();
            return pedido;
        }

   
        public async Task DeletePedido(int id)
        {
            var pedido = await _restauranteContext.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado");
            _restauranteContext.Pedidos.Remove(pedido);
            await _restauranteContext.SaveChangesAsync();
        }

       
        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _restauranteContext.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
        {
            return await _restauranteContext.Pedidos.ToListAsync();
        }

      
        public async Task UpdatePedido(Pedido pedido, int id)
        {
            var pedidoExistente = await _restauranteContext.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
            if (pedidoExistente == null) throw new KeyNotFoundException("Pedido no encontrado");

            pedidoExistente.Nombre = pedido.Nombre;
            pedidoExistente.Apellido = pedido.Apellido;
            pedidoExistente.Gmail = pedido.Gmail;
            pedidoExistente.Telefono = pedido.Telefono;
              pedidoExistente.DetallesPedido = pedido.DetallesPedido;
            pedidoExistente.FechaPedido = pedido.FechaPedido;
            

            await _restauranteContext.SaveChangesAsync();
        }
    }
}
