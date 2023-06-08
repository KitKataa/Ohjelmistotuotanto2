using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KuvasController : ControllerBase
    {
        private readonly MydbContext _context;
        private readonly IWebHostEnvironment env;


        public KuvasController(MydbContext context)
        {
            _context = context;
            this.env = env;
        }

        // GET: api/Kuvas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kuva>>> GetKuvas()
        {
            return await _context.Kuvas.ToListAsync();
        }

        // GET: api/Kuvas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kuva>> GetKuva(long id)
        {
            var kuva = await _context.Kuvas.FindAsync(id);

            if (kuva == null)
            {
                return NotFound();
            }

            return kuva;
        }

        // PUT: api/Kuvas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKuva(long id, Kuva kuva)
        {
            if (id != kuva.Idkuva)
            {
                return BadRequest();
            }

            _context.Entry(kuva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KuvaExists(id))
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

        // POST: api/Kuvas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Kuva>> PostKuva(Kuva kuva)
        //{
        //    _context.Kuvas.Add(kuva);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetKuva", new { id = kuva.Idkuva }, kuva);
        //}

        [HttpPost]
        public async Task Post([FromBody] Kuva[] files)
        {
            foreach (var file in files)
            {
                var buf = Convert.FromBase64String(file.Kuva1);
                await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString("N") + "-" + file.Kuva1, buf);
            }
        }

        // DELETE: api/Kuvas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKuva(long id)
        {
            var kuva = await _context.Kuvas.FindAsync(id);
            if (kuva == null)
            {
                return NotFound();
            }

            _context.Kuvas.Remove(kuva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KuvaExists(long id)
        {
            return _context.Kuvas.Any(e => e.Idkuva == id);
        }
    }
}
