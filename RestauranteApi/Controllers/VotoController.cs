using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly IVotoService _votoService;

        public VotoController(IVotoService votoService)
        {
            _votoService = votoService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voto>>> GetVotos()
        {
            var votos = await _votoService.GetVotos();
            return Ok(votos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Voto>> GetVoto(int id)
        {
            var voto = await _votoService.GetVotoById(id);
            if (voto == null)
            {
                return NotFound();
            }
            return Ok(voto);
        }

       
       

       
        [HttpPost]
        public async Task<ActionResult<Voto>> CreateVoto(Voto voto)
        {
            var votoCreado = await _votoService.CreateVoto(voto);
            return CreatedAtAction(nameof(GetVoto), new { id = votoCreado.Id }, votoCreado);
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoto(int id, Voto voto)
        {
            await _votoService.UpdateVoto(voto, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoto(int id)
        {
            await _votoService.DeleteVoto(id);
            return NoContent();
        }
    }
}
