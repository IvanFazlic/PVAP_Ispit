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
    public class Prijava_brojIndeksaController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public Prijava_brojIndeksaController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Prijava_brojIndeksa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prijava_brojIndeksa>>> GetPrijava()
        {
            return await _context.Prijava.Include(x=> x.IdIspitaNavigation).ToListAsync();
        }

        // GET: api/Prijava_brojIndeksa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prijava_brojIndeksa>> GetPrijava_brojIndeksa(int id)
        {
            var prijava_brojIndeksa = await _context.Prijava.FindAsync(id);

            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            return prijava_brojIndeksa;
        }

        // PUT: api/Prijava_brojIndeksa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrijava_brojIndeksa(int id, Prijava_brojIndeksa prijava_brojIndeksa)
        {
            if (id != prijava_brojIndeksa.IdPrijave)
            {
                return BadRequest();
            }

            _context.Entry(prijava_brojIndeksa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Prijava_brojIndeksaExists(id))
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

        // POST: api/Prijava_brojIndeksa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prijava_brojIndeksa>> PostPrijava_brojIndeksa(StudentPredmetDodavanje prijava_brojIndeksa)
        {
            var proveraPrijavljenogIspita = await _context.Prijava
                .FirstOrDefaultAsync(x => x.IdStudneta == prijava_brojIndeksa.IdStudenta
                && x.IdIspitaNavigation.IdPredmeta == prijava_brojIndeksa.IdPredmeta);
            var ispit = await _context.Ispits
                .FirstOrDefaultAsync(x => x.IdPredmeta == prijava_brojIndeksa.IdPredmeta);
            if (ispit == null || proveraPrijavljenogIspita!=null)
            {
                return BadRequest();
            }
            Prijava_brojIndeksa prijava = new Prijava_brojIndeksa
            {
                IdStudneta = prijava_brojIndeksa.IdStudenta,
                IdIspita = ispit.IdIspita,
            };
            _context.Prijava.Add(prijava);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPrijava_brojIndeksa", new { id = prijava.IdPrijave }, prijava);
        }

        // DELETE: api/Prijava_brojIndeksa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrijava_brojIndeksa(int id)
        {
            var prijava_brojIndeksa = await _context.Prijava.FindAsync(id);
            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            _context.Prijava.Remove(prijava_brojIndeksa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Prijava_brojIndeksaExists(int id)
        {
            return _context.Prijava.Any(e => e.IdPrijave == id);
        }
    }
}
