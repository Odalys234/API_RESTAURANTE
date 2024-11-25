using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategorias();
            return Ok(categorias);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
        {
            var categoriaCreada = await _categoriaService.CreateCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCreada.Id }, categoriaCreada);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoria)
        {
            await _categoriaService.UpdateCategoria(categoria, id);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            await _categoriaService.DeleteCategoria(id);
            return NoContent();
        }
    }
}
