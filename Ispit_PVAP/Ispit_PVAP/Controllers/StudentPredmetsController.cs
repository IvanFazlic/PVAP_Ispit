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
    public class StudentPredmetsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentPredmetsController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/StudentPredmets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPredmet>>> GetStudentPredmets()
        {
            return await _context.StudentPredmets.Include(x => x.IdPredmetaNavigation).ToListAsync();
        }

        // GET: api/StudentPredmets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StudentPredmetPredmetiStudenta>>> GetStudentPredmet(int id)
        {
            var studentPredmet = await _context.StudentPredmets
                .Where(x => x.IdStudenta == id)
                .Include(x=>x.IdPredmetaNavigation)
                .Select(x => new StudentPredmetPredmetiStudenta {
                    Naziv = x.IdPredmetaNavigation.Naziv 
                })
                .ToListAsync();
            if (studentPredmet == null)
            {
                return NotFound();
            }

            return studentPredmet;
        }

        // PUT: api/StudentPredmets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentPredmet(int id, StudentPredmet studentPredmet)
        {
            if (id != studentPredmet.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(studentPredmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentPredmetExists(id))
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

        // POST: api/StudentPredmets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentPredmet>> PostStudentPredmet(StudentPredmetDodavanje studentPredmet)
        {
            var studenti = await _context.Students.FirstOrDefaultAsync(x => x.IdStudenta == studentPredmet.IdStudenta);
            var predmeti = await _context.Predmets.FirstOrDefaultAsync(x => x.IdPredmeta == studentPredmet.IdPredmeta);
            if (studentPredmet == null || studenti == null || predmeti == null)
            {
                return BadRequest();
            }

            StudentPredmet studentZaDodavanje = new StudentPredmet
            {
                IdStudenta = studentPredmet.IdStudenta,
                IdPredmeta = studentPredmet.IdPredmeta,
                SkolskaGodina = DateTime.Now.Month <= 8 ? $"{DateTime.Now.Year-1}/{(DateTime.Now.Year) % 100:D2}" : $"{DateTime.Now.Year}/{(DateTime.Now.Year + 1) % 100:D2}"
            };

            _context.StudentPredmets.Add(studentZaDodavanje);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPredmetExists(studentPredmet.IdStudenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetStudentPredmet", new { id = studentZaDodavanje.IdStudenta }, studentZaDodavanje);
        }


        // DELETE: api/StudentPredmets/5
        [HttpDelete("{idStudenta}/{idPredmeta}")]
        public async Task<IActionResult> DeleteStudentPredmet(int idStudenta, int idPredmeta)
        {
            var studentPredmet = await _context.StudentPredmets.FirstOrDefaultAsync(x => x.IdStudenta == idStudenta && x.IdPredmeta == idPredmeta);
            if (studentPredmet == null)
            {
                return BadRequest("Ne postoji navedeni student/predmet");
            }
            var zapisnik = await _context.Zapisniks.Where(x => x.IdStudenta== idStudenta && x.Ocena > 5).ToListAsync();
            var ispit = await _context.Ispits.FirstOrDefaultAsync(x => x.IdPredmeta == idPredmeta);
            if(ispit != null)
            {
                foreach (var x in zapisnik)
                {
                    if (ispit.IdIspita == x.IdIspita)
                    {
                        return BadRequest("Vec postoji ocena iz predmeta");
                    }
                }
            }

            _context.StudentPredmets.Remove(studentPredmet);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool StudentPredmetExists(int id)
        {
            return _context.StudentPredmets.Any(e => e.IdStudenta == id);
        }
    }
}
