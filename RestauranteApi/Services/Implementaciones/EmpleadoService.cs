using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;

namespace RestauranteApi.Services.Implementaciones
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly RestauranteContext _restauranteContext;

        public EmpleadoService(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        
        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            return await _restauranteContext.Empleados.ToListAsync();
        }

        
        public async Task<Empleado> GetEmpleadoById(int id)
        {
            return await _restauranteContext.Empleados.FirstOrDefaultAsync(e => e.Id == id);
        }

       
        public async Task<IEnumerable<Empleado>> GetEmpleadosByPuestoId(int puestoId)
        {
            return await _restauranteContext.Empleados.Where(e => e.PuestoId == puestoId).ToListAsync();
        }

        
        public async Task<Empleado> CreateEmpleado(Empleado empleado)
        {
            _restauranteContext.Empleados.Add(empleado);
            await _restauranteContext.SaveChangesAsync();
            return empleado;
        }

        
        public async Task UpdateEmpleado(Empleado empleado, int id)
        {
            var empleadoExistente = await _restauranteContext.Empleados.FirstOrDefaultAsync(e => e.Id == id);
            if (empleadoExistente == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }

            empleadoExistente.Nombre = empleado.Nombre;
            empleadoExistente.Apellido = empleado.Apellido;
            empleadoExistente.Telefono = empleado.Telefono;
            empleadoExistente.Email = empleado.Email;
            empleadoExistente.PuestoId = empleado.PuestoId;

            await _restauranteContext.SaveChangesAsync();
        }

        
        public async Task DeleteEmpleado(int id)
        {
            var empleado = await _restauranteContext.Empleados.FirstOrDefaultAsync(e => e.Id == id);
            if (empleado == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }

            _restauranteContext.Empleados.Remove(empleado);
            await _restauranteContext.SaveChangesAsync();
        }
    }
}
