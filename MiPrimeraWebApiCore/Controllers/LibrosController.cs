using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimeraWebApiCore.Contexts;
using MiPrimeraWebApiCore.Entities;

namespace MiPrimeraWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public LibrosController(ApplicationDBContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x => x.Autor).ToList();
        }

        /// <summary>
        /// Acción asíncrona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name ="ObtenerLibro")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }


        [HttpPost]
        public ActionResult Post([FromBody]Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Libro value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}