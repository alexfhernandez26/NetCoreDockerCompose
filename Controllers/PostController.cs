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
    public class PostController : ControllerBase
    {
        private IAppDbContext _context;
        public PostController(IAppDbContext context)
        {
            _context = context;
        }
        // GET: api/<Categoria>
        [HttpGet]
        public async Task<IEnumerable<Post>> GetAll()
        {
            var res = await _context.Posts.Where(p => p.Habilitado == true).ToListAsync();
            return res;
        }

        // GET api/<Categoria>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _context.Posts.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            return Ok(res);
        }

        // POST api/<Categoria>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            var cat = await _context.Categorias.Where(c => c.ID == post.CategoriaId).FirstOrDefaultAsync();
            if (cat == null) return NotFound($"No existe categoria: {post.CategoriaId}");
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return Ok(post);
        }

        // PUT api/<Categoria>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Post post)
        {
            var cat = await _context.Categorias.Where(c => c.ID == post.CategoriaId).FirstOrDefaultAsync();
            if (cat == null) return NotFound($"No existe categoria: {post.CategoriaId}");
            if (id != post.ID) throw new Exception();
            var res = await _context.Posts.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            res.Titulo = post.Titulo;
            res.Contenido = post.Contenido;
            res.Habilitado = post.Habilitado;
            res.CategoriaId = post.CategoriaId;
            
            await _context.SaveChangesAsync();
            return Ok(res);
        }

        // DELETE api/<Categoria>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _context.Posts.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (res == null) return NotFound();
            _context.Posts.Remove(res);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
