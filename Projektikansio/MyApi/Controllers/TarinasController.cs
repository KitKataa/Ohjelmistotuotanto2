using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SharedLib;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarinasController : ControllerBase
    {
        private readonly MydbContext _context;

        public TarinasController(MydbContext context)
        {
            _context = context;
        }

        // GET: api/Tarinas
        [HttpGet]
        public async Task<ActionResult<List<tarinaDTO>>> GetTarinas()
        {
            List<tarinaDTO> list = new List<tarinaDTO>();

            if (_context.Tarinas == null)
            {
                return NotFound();
            }

            var tarinas = await _context.Tarinas.Include(e => e.IdmatkakohdeNavigation).ToListAsync();

            foreach(var tarina in tarinas)
            {
                list.Add(tarina.toTarinaDTO());
            }

            return Ok(list);
        }

        // GET: api/Tarinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarina>> GetTarina(long id)
        {
            //var tarina = await _context.Tarinas.Include(e => e.IdmatkaNavigation).FirstOrDefaultAsync(e => e.Idtarina == id);
            var tarina = await _context.Tarinas.FindAsync(id);

            if (tarina == null)
            {
                return NotFound();
            }

            return tarina;
        }

        // PUT: api/Tarinas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<tarinaDTO>> PutTarina(long id, tarinaDTO tarina)
        {
            Tarina? t = await _context.Tarinas.FindAsync(id);
            if (t == null) return NotFound();

            if (tarina == null) return BadRequest();
            if (id != tarina.idtarina) return BadRequest();
            else
            {
                t.Idtarina = tarina.idtarina;
                t.Idmatka = tarina.idmatka;
                t.Idmatkakohde = tarina.idmatkakohde;
                t.Pvm = tarina.pvm;
                t.Teksti = tarina.teksti;
            }

            _context.Entry(t).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarinaExists(id))
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

        // POST: api/Tarinas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tarinaDTO>> PostTarina(tarinaDTO tarina)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState.ToString());
                return BadRequest();
            }

            Tarina t = new Tarina();
            t.Idtarina = tarina.idtarina;
            t.Idmatka = tarina.idmatka;
            t.Idmatkakohde = tarina.idmatkakohde;
            t.Teksti = tarina.teksti;
            t.Pvm = tarina.pvm;

            _context.Tarinas.Add(t);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarina", new { id = t.Idtarina }, t.toTarinaDTO());
        }

        // DELETE: api/Tarinas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<tarinaDTO>> DeleteTarina(long id)
        {
            Tarina? tarina = await _context.Tarinas.FindAsync(id);
            if (tarina == null) return NotFound();
            

            _context.Tarinas.Remove(tarina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarinaExists(long id)
        {
            return _context.Tarinas.Any(e => e.Idtarina == id);
        }
    }
}
