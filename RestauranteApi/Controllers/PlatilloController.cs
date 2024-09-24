using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatilloController : ControllerBase
    {
        private readonly IPlatilloService _platilloService;

        public PlatilloController(IPlatilloService platilloService)
        {
            _platilloService = platilloService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platillo>>> GetPlatillos()
        {
            var platillos = await _platilloService.GetPlatillos();
            return Ok(platillos);
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Platillo>> GetPlatillo(int id)
        {
            var platillo = await _platilloService.GetPlatilloById(id);
            if (platillo == null)
            {
                return NotFound();
            }
            return Ok(platillo);
        }

       
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Platillo>>> GetPlatillosByCategoria(int categoriaId)
        {
            var platillos = await _platilloService.GetPlatillosByCategoriaId(categoriaId);
            return Ok(platillos);
        }

       
        [HttpPost]
        public async Task<ActionResult<Platillo>> CreatePlatillo([FromForm] Platillo platillo, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                
                var rutaImagenes = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

                
                if (!Directory.Exists(rutaImagenes))
                {
                    Directory.CreateDirectory(rutaImagenes);
                }

             
                var nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var rutaCompleta = Path.Combine(rutaImagenes, nombreImagen);

                
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                
                platillo.Imagen = Path.Combine("imagenes", nombreImagen);
            }

            var platilloCreado = await _platilloService.CreatePlatillo(platillo);
            return CreatedAtAction(nameof(GetPlatillo), new { id = platilloCreado.Id }, platilloCreado);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlatillo(int id, [FromForm] Platillo platillo, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                
                var rutaImagenes = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

                
                if (!Directory.Exists(rutaImagenes))
                {
                    Directory.CreateDirectory(rutaImagenes);
                }

              
                var nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var rutaCompleta = Path.Combine(rutaImagenes, nombreImagen);

             
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

              
                platillo.Imagen = Path.Combine("imagenes", nombreImagen);
            }

            await _platilloService.UpdatePlatillo(platillo, id);
            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatillo(int id)
        {
            await _platilloService.DeletePlatillo(id);
            return NoContent();
        }
    }
}
