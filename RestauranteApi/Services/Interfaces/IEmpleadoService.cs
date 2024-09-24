using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteApi.Models;

namespace RestauranteApi.Services.Interfaces
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleadoById(int id);
        Task<IEnumerable<Empleado>> GetEmpleadosByPuestoId(int puestoId);
        Task<Empleado> CreateEmpleado(Empleado empleado);
        Task UpdateEmpleado(Empleado empleado, int id);
        Task DeleteEmpleado(int id);
    }
}
