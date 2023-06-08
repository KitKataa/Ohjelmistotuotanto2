using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SharedLib;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatkakohdesController : ControllerBase
    {
        private readonly MydbContext _context;

        public MatkakohdesController(MydbContext context)
        {
            _context = context;
        }

        // GET: api/Matkakohdes
        [HttpGet]
        public async Task<ActionResult<List<matkakohdeDTO>>> GetLocations()
        {
            var l = await _context.Matkakohdes.ToListAsync();
            List<matkakohdeDTO> locations = new List<matkakohdeDTO>();

            foreach (var item in l)
            {
                matkakohdeDTO m = item.toMatkakohdeDTO();
                locations.Add(m);
            }

            if (locations == null) return NotFound();
            return locations;
        }

        // GET: api/Matkakohdes/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<matkakohdeDTO>> GetLocation(long id)
        {
            var matkakohde = await _context.Matkakohdes.FindAsync(id);

            if (matkakohde == null)
            {
                return NotFound();
            }

            return matkakohde.toMatkakohdeDTO();
        }

        // PUT: api/Matkakohdes/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<ActionResult<matkakohdeDTO>> PutLocation(long id, matkakohdeDTO m)
        {
            //Varmistutaan siitä, että kyseinen matkakohde on olemassa ennen muokkausta
            Matkakohde? location = await _context.Matkakohdes.Where(a => a.Idmatkakohde == id).FirstOrDefaultAsync();
            if (location == null) return NotFound();

            //Sellaista matkakohdetta, johon liittyy joku matkakertomus, ei saa poistaa tai päivittää
            Tarina? t = await _context.Tarinas.Where(a => a.Idmatkakohde == id).FirstOrDefaultAsync();
            if(t != null) { return NoContent(); } 

            if (m == null) return BadRequest(); 
            else
            {
                location.Kuva = m.kuva;
                location.Kohdenimi = m.kohdenimi;
                location.Paikkakunta = m.paikkakunta;
                location.Maa = m.maa;
                location.Kuvausteksti = m.kuvausteksti;
            }
          

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatkakohdeExists(id))
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

        // POST: api/Matkakohdes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<matkakohdeDTO>> PostLocation(matkakohdeDTO matkakohde)
        {
            if (!ModelState.IsValid) return BadRequest();

            Matkakohde m = new Matkakohde();
            m.Kuva = matkakohde.kuva;
            m.Kohdenimi = matkakohde.kohdenimi;
            m.Paikkakunta = matkakohde.paikkakunta;
            m.Maa = matkakohde.maa;
            m.Kuvausteksti = matkakohde.kuvausteksti;

            _context.Matkakohdes.Add(m);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = m.Idmatkakohde }, m.toMatkakohdeDTO());
        }

        // DELETE: api/Matkakohdes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<matkaajaDTO>> DeleteLocation(long id)
        {
            Matkakohde? m = await _context.Matkakohdes.Where(a => a.Idmatkakohde == id).FirstOrDefaultAsync();
            if (m == null) return NotFound();

            //Sellaista matkakohdetta, johon liittyy joku matkakertomus, ei saa poistaa tai päivittää
            Tarina? t = await _context.Tarinas.Where(a => a.Idmatkakohde == id).FirstOrDefaultAsync();
            if (t != null)  return BadRequest(); 

            _context.Matkakohdes.Remove(m);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatkakohdeExists(long id)
        {
            return _context.Matkakohdes.Any(e => e.Idmatkakohde == id);
        }
    }


}
