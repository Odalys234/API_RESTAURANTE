<<<<<<< HEAD:RESTAURANTE-API--main/RestauranteApi/Services/Interfaces/IPedidoService.cs
using System;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces;

public interface IPedidoService
{
     Task<IEnumerable<Pedido>> GetPedido();
        Task<Pedido> GetPedidoById(int id);
        Task<IEnumerable<Pedido>> GetPedidosByPuestoId(int puestoId);
        Task<Pedido> CreatePedido(Pedido empleado);
        Task UpdatePedido(Pedido empleado, int id);
        Task DeletePedido(int id);

=======
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetPedidos();  
        Task<Pedido> GetPedidoById(int id); 
        Task<Pedido> CreatePedido(Pedido pedido);  
        Task UpdatePedido(Pedido pedido, int id);  
        Task DeletePedido(int id);  
    }
>>>>>>> 0d264c3 (ProyectoRestauranteApi):RestauranteApi/Services/Interfaces/IPedidoService.cs
}
