using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        private readonly IPuestoService _puestoService;

        public PuestoController(IPuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puesto>>> GetPuestos()
        {
            var puestos = await _puestoService.GetPuestos();
            return Ok(puestos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPuesto(int id)
        {
            var puesto = await _puestoService.GetPuestoById(id);
            if (puesto == null)
            {
                return NotFound();
            }
            return Ok(puesto);
        }

      
        [HttpPost]
        public async Task<ActionResult<Puesto>> CreatePuesto(Puesto puesto)
        {
            var puestoCreado = await _puestoService.CreatePuesto(puesto);
            return CreatedAtAction(nameof(GetPuesto), new { id = puestoCreado.Id }, puestoCreado);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePuesto(int id, Puesto puesto)
        {
            await _puestoService.UpdatePuesto(puesto, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuesto(int id)
        {
            await _puestoService.DeletePuesto(id);
            return NoContent();
        }
    }
}
