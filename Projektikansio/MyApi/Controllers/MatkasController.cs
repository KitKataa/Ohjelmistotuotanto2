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
    public class MatkasController : ControllerBase
    {
        private readonly MydbContext _context;

        public MatkasController(MydbContext context)
        {
            _context = context;
        }

        // GET: api/Matkas
        [HttpGet]
        public async Task<ActionResult<List<Matka>>> GetMatkas()
        {
            return await _context.Matkas.ToListAsync();
        }

        // GET: api/Matkas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matka>> GetMatka(long id)
        {
            //var tarina = await _context.Matkas.Include(e => e.Idmatka).FirstOrDefaultAsync(e => e.Idtarina == id);
            var matka = await _context.Matkas.FindAsync(id);

            if (matka == null)
            {
                return NotFound();
            }

            return matka;
        }

        // PUT: api/Matkas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<matkaDTO>> PutMatka(long id, matkaDTO matka)
        {
            Matka? m = await _context.Matkas.FindAsync(id);
            if (m == null) return NotFound();

            //Sellaista matkaa, johon liittyy joku matkakertomus, ei saa poistaa tai päivittää
            Tarina? t = await _context.Tarinas.Where(a => a.Idmatka == id).FirstOrDefaultAsync();
            if (t != null) { return NoContent(); }

            if (matka == null) return BadRequest();
            if (id != matka.idmatka) return BadRequest();
            else
            {
                m.Idmatkaaja = matka.idmatkaaja;
                m.Alkupvm = matka.alkupvm;
                m.Loppupvm = matka.loppupvm;
                m.Yksityinen = matka.yksityinen;
            }

            _context.Entry(m).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatkaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Matkas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<matkaDTO>> PostMatka(matkaDTO matka)
        {
            if (!ModelState.IsValid) return BadRequest();

            Matka m = new Matka();
            m.Idmatkaaja = matka.idmatkaaja;
            m.Alkupvm = matka.alkupvm;
            m.Loppupvm = matka.loppupvm;
            m.Yksityinen = matka.yksityinen;

            _context.Matkas.Add(m);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatka", new { id = m.Idmatka }, m.toMatkaDTO());
        }

        // DELETE: api/Matkas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<matkaDTO>> DeleteMatka(long id)
        {
            Matka? matka = await _context.Matkas.FindAsync(id);
            if (matka == null) return NotFound();

            //Sellaista matkaa, johon liittyy joku matkakertomus, ei saa poistaa tai päivittää
            Tarina? t = await _context.Tarinas.Where(a => a.Idmatka == id).FirstOrDefaultAsync();
            if (t != null) { return BadRequest(); }

            _context.Matkas.Remove(matka);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatkaExists(long id)
        {
            return _context.Matkas.Any(e => e.Idmatka == id);
        }
    }
}
