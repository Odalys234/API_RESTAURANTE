using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class ClienteService : IClienteService
    {
        private readonly RestauranteContext _restauranteContext;

        public ClienteService(RestauranteContext context)
        {
            _restauranteContext = context;
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            _restauranteContext.Clientes.Add(cliente);
            await _restauranteContext.SaveChangesAsync();
            return cliente;
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _restauranteContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");
            _restauranteContext.Clientes.Remove(cliente);
            await _restauranteContext.SaveChangesAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _restauranteContext.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _restauranteContext.Clientes.ToListAsync();
        }

        public async Task UpdateCliente(Cliente cliente, int id)
        {
            var clienteExistente = await _restauranteContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (clienteExistente == null) throw new KeyNotFoundException("Cliente no encontrado");

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Email = cliente.Email;
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
