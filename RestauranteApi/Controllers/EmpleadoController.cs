using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            var empleados = await _empleadoService.GetEmpleados();
            return Ok(empleados);
        }

        
        [HttpGet("puesto/{puestoId}")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosByPuesto(int puestoId)
        {
            var empleados = await _empleadoService.GetEmpleadosByPuestoId(puestoId);
            return Ok(empleados);
        }

       
        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado empleado)
        {
            var empleadoCreado = await _empleadoService.CreateEmpleado(empleado);
            return CreatedAtAction(nameof(GetEmpleadoById), new { id = empleadoCreado.Id }, empleadoCreado);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleadoById(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, Empleado empleado)
        {
            await _empleadoService.UpdateEmpleado(empleado, id);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            await _empleadoService.DeleteEmpleado(id);
            return NoContent();
        }
    }
}
