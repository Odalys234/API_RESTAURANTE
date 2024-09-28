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
}
