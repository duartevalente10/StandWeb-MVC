using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;

namespace StandWeb.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _caminho;

        public CarrosAPI(ApplicationDbContext context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
        }

        // GET: api/CarrosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrosAPIViewModel>>> GetCarros()
        {
            var listaCarros = await _context.Carros
                .Select(f => new CarrosAPIViewModel
                {
                    IdCarros = f.IdCarros,
                    Modelo = f.Modelo,
                    Ano = f.Ano,
                    Foto = f.Foto,
                    Cilindrada = f.Cilindrada,
                    Potencia = f.Potencia,
                    Combustivel = f.Combustivel,
                    Preco = f.Preco
                })
                .OrderBy(f => f.IdCarros)
                .ToListAsync();
            return listaCarros;
        }

        // GET: api/CarrosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carros>> GetCarros(int id)
        {
            var carros = await _context.Carros.FindAsync(id);

            if (carros == null)
            {
                return NotFound();
            }

            return carros;
        }

        // PUT: api/CarrosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarros(int id, Carros carros)
        {
            if (id != carros.IdCarros)
            {
                return BadRequest();
            }

            _context.Entry(carros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarrosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carros>> PostCarros([FromForm] Carros carros  , IFormFile UpFotografia)
        {

            carros.Foto = "";
            string localizacao = _caminho.WebRootPath;
            var nomeFoto = Path.Combine(localizacao, "fotos", UpFotografia.FileName);
            var fotoUp = new FileStream(nomeFoto, FileMode.Create);
            await UpFotografia.CopyToAsync(fotoUp);
            carros.Foto = UpFotografia.FileName;

            try
            {
                _context.Carros.Add(carros);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return CreatedAtAction("GetCarros", new { id = carros.IdCarros }, carros);
        }

        // DELETE: api/CarrosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarros(int id)
        {
            var carros = await _context.Carros.FindAsync(id);
            if (carros == null)
            {
                return NotFound();
            }

            _context.Carros.Remove(carros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrosExists(int id)
        {
            return _context.Carros.Any(e => e.IdCarros == id);
        }
    }
}
