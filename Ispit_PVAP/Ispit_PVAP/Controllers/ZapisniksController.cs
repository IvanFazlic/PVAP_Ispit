using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ispit_PVAP.Models;

namespace Ispit_PVAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZapisniksController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public ZapisniksController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Zapisniks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetZapisniks()
        {
            return await _context.Zapisniks.ToListAsync();
        }

        // GET: api/Zapisniks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zapisnik>> GetZapisnik(int id)
        {
            var zapisnik = await _context.Zapisniks.FindAsync(id);

            if (zapisnik == null)
            {
                return NotFound();
            }

            return zapisnik;
        }

        // PUT: api/Zapisniks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZapisnik(int id, Zapisnik zapisnik)
        {
            if (id != zapisnik.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(zapisnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZapisnikExists(id))
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

        // POST: api/Zapisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zapisnik>> PostZapisnik(ZapisnikDto zapisnik)
        {
            var ispitZaUnos = await _context.Prijava
                .FirstOrDefaultAsync(x => x.IdStudneta == zapisnik.IdStudenta
                && x.IdIspita == zapisnik.IdIspita);

            if (ispitZaUnos == null)
            {
                return BadRequest();
            }

            Zapisnik unos = new Zapisnik
            {
                IdStudenta = ispitZaUnos.IdStudneta,
                IdIspita = ispitZaUnos.IdIspita,
                Bodovi = zapisnik.Bodovi,
                Ocena = (float)Math.Ceiling(float.Parse(zapisnik.Bodovi) / 10)
            };
            if(unos.Ocena < 6 || unos.Ocena > 10)
            {
                return BadRequest();
            }
            _context.Zapisniks.Add(unos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZapisnikExists(zapisnik.IdStudenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetZapisnik", new { id = unos.IdStudenta }, unos);
        }

        // DELETE: api/Zapisniks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZapisnik(int id)
        {
            var zapisnik = await _context.Zapisniks.FindAsync(id);
            if (zapisnik == null)
            {
                return NotFound();
            }

            _context.Zapisniks.Remove(zapisnik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZapisnikExists(int id)
        {
            return _context.Zapisniks.Any(e => e.IdStudenta == id);
        }
    }
}
