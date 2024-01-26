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
    public class StudentsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentsController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoviStudent>>> GetStudents()
        {
            var studentsIzBaze = await _context.Students.ToListAsync();
            var noviStudenti = studentsIzBaze.Select(student => new NoviStudent
            {
                IdStudenta = student.IdStudenta,
                Ime = student.Ime,
                Prezime = student.Prezime,
                Indeks = $"{student.Smer}-{student.Broj}/{student.GodinaUpisa}"
            }).ToList();

            return noviStudenti;
        }

        // GET: api/Students/5
        [HttpGet("{kriterijumPretrage}")]
        public async Task<ActionResult<IEnumerable<NoviStudent>>> GetStudent(string kriterijumPretrage)
        {
            var studenti = await _context.Students
                .Where(student => student.Ime.Contains(kriterijumPretrage)
                || student.Prezime.Contains(kriterijumPretrage)
                || student.GodinaUpisa.Contains(kriterijumPretrage)
                || student.Smer.Contains(kriterijumPretrage)
                || student.Broj.ToString().Contains(kriterijumPretrage)
                || student.GodinaUpisa.Contains(kriterijumPretrage))
                .ToListAsync();

            if (studenti.Count == 0)
            {
                return NotFound();
            }
            var noviStudenti = studenti.Select(student => new NoviStudent
            {
                IdStudenta = student.IdStudenta,
                Ime = student.Ime,
                Prezime = student.Prezime,
                Indeks = $"{student.Smer}-{student.Broj}/{student.GodinaUpisa}"
            }).ToList();

            return noviStudenti;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentIzmena student)
        {
            var studentiZaIzmenu = await _context.Students.FindAsync(id);
            if (studentiZaIzmenu == null) 
            { 
                return NotFound(); 
            }
            studentiZaIzmenu.Ime = student.Ime;
            studentiZaIzmenu.Prezime = student.Prezime;
            _context.Entry(studentiZaIzmenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.IdStudenta }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.IdStudenta == id);
        }
    }
}
