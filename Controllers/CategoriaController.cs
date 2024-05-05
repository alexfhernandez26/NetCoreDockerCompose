using microservices.Entities;
using microservices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private IAppDbContext _context;
        public CategoriaController(IAppDbContext context)
        {
            _context = context;
        }
        // GET: api/<Categoria>
        [HttpGet]
        public async Task<IEnumerable<Categorias>> GetAll()
        {
            var res =await _context.Categorias.Where(c => c.Habilitado == true).ToListAsync();
            return res;
        }

        // GET api/<Categoria>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res =await _context.Categorias.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            return Ok(res);
        }

        // POST api/<Categoria>
        [HttpPost]
        public async Task<Categorias> Post([FromBody] Categorias categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // PUT api/<Categoria>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Categorias categorias)
        {
            if (id != categorias.ID) throw new Exception();
            var res = await _context.Categorias.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            res.Descripcion = categorias.Descripcion;
            res.Habilitado = categorias.Habilitado;
            await _context.SaveChangesAsync();
            return Ok(res);
        }

        // DELETE api/<Categoria>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _context.Categorias.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            _context.Categorias.Remove(res);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

