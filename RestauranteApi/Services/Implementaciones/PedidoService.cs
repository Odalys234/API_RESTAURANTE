<<<<<<< HEAD:RESTAURANTE-API--main/RestauranteApi/Services/Implementaciones/PedidoService.cs
using System;
=======
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
>>>>>>> 0d264c3 (ProyectoRestauranteApi):RestauranteApi/Services/Implementaciones/PedidoService.cs
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

<<<<<<< HEAD:RESTAURANTE-API--main/RestauranteApi/Services/Implementaciones/PedidoService.cs
namespace RestauranteApi.Services.Implementaciones;

public class PedidoService : IPedidoService 
{
    private readonly  RestauranteContext _restauranteContext;

    public PedidoService(RestauranteContext restauranteContext)
    {
        _restauranteContext = restauranteContext;
    }

      public async Task<IEnumerable<Pedido>> GetPedido()
        {
            return await _restauranteContext.Pedidos.ToListAsync();
        }

        
        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _restauranteContext.Pedidos.FirstOrDefaultAsync(pe => pe.Id == id);
        }

        
        public async Task<IEnumerable<Pedido>> GetPedidoByCategoriaId(int categoriaId)
        {
            return await _restauranteContext.Pedidos.Where(pe => pe.CategoriaId == categoriaId).ToListAsync();
        }

       
=======
namespace RestauranteApi.Services.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly RestauranteContext _restauranteContext;

        public PedidoService(RestauranteContext context)
        {
            _restauranteContext = context;
        }

>>>>>>> 0d264c3 (ProyectoRestauranteApi):RestauranteApi/Services/Implementaciones/PedidoService.cs
        public async Task<Pedido> CreatePedido(Pedido pedido)
        {
            _restauranteContext.Pedidos.Add(pedido);
            await _restauranteContext.SaveChangesAsync();
            return pedido;
        }

<<<<<<< HEAD:RESTAURANTE-API--main/RestauranteApi/Services/Implementaciones/PedidoService.cs
        
        public async Task UpdatePedido(Pedido pedido, int id)
        {
            var pedidoExistente = await _restauranteContext.Pedidos.FirstOrDefaultAsync(pe => pe.Id == id);
            if (pedidoExistente == null)
            {
                throw new KeyNotFoundException("Pedido no encontrade");
            }

            pedidoExistente.Pedido = pedido.FechaPedido;
            pedidoExistente.Pedido = pedido.NombreCliente;
            pedidoExistente.Pedido =pedido.Telefono;

            await _restauranteContext.SaveChangesAsync();
        }

        
        public async Task DeletePedido(int id)
        {
            var pedido = await _restauranteContext.Pedidos.FirstOrDefaultAsync(pe => pe.Id == id);
            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido encontrado");
            }

            _restauranteContext.Pedidos.Remove(pedido);
            await _restauranteContext.SaveChangesAsync();
        }
    }
=======
    
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
            pedidoExistente.FechaPedido = pedido.FechaPedido;
         

            await _restauranteContext.SaveChangesAsync();
        }
    }
}
>>>>>>> 0d264c3 (ProyectoRestauranteApi):RestauranteApi/Services/Implementaciones/PedidoService.cs
